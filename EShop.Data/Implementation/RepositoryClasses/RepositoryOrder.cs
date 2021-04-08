using EShop.Data.Implementation.Interfaces;
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

        public RepositoryOrder(ShopContext context) => this.context = context;
        public void Add(Order entity) => context.Add(entity);
        public Order FindWithoutInclude(Predicate<Order> condition) => context.Order.ToList().Find(condition);
        public List<Order> GetAll() => context.Order.Include(o => o.Customer).ToList();
        public List<Order> GetAllOrders(Predicate<Order> condition) => context.Order.Include(o => o.Customer).Include(o => o.OrderItems).ThenInclude(oi => oi.Book).ToList().FindAll(condition);
        public Order FindWithInclude(Predicate<Order> condition) => context.Order.Include(o => o.OrderItems).ThenInclude(oi => oi.Book).ToList().Find(condition);
        public List<Order> Sort() => context.Order.Include(order => order.Customer).OrderBy(o => o.OrderStatus).ToList();
        public Order Find(Predicate<Order> p) => context.Order.Include(o => o.OrderItems).ThenInclude(oi => oi.Book).ToList().Find(p);
    }
}
