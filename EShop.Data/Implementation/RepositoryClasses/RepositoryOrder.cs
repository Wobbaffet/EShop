﻿using EShop.Data.Implementation.Interfaces;
using EShop.Model;
using EShop.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Implementation.RepositoryClasses
{
    public class RepositoryOrder : IRepositoryOrder
    {
        private readonly ShopContext context;

        public RepositoryOrder(ShopContext context)
        {
            this.context = context;
        }
        public void Add(Order entity)
        {
            context.Add(entity);
        }

        public Order Find(Predicate<Order> condition)
        {
            return context.Order.ToList().Find(condition);
        }

        public List<Order> GetAll()
        {
            return context.Order.Include(o => o.Customer).ToList();
        }
        public void Dispose()
        {
            context.Dispose();
        }

        public List<Order> GetAllOrders(Predicate<Order> condition)
        {
          
            return context.Order.Include(o=>o.OrderItems).ThenInclude(oi=>oi.Book).ToList().FindAll(condition);
        }
    }
}
