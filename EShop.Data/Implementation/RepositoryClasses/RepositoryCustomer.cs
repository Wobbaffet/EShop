using EShop.Model;
using EShop.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Implementation.RepositoryClasses
{
    public class RepositoryCustomer : IRepositoryCustomer
    {
        private ShopContext shopContext;

        public RepositoryCustomer(ShopContext shopContext) => this.shopContext = shopContext;
        public void Add(Customer entity) => shopContext.Add(entity);
        public void Delete(Customer customer) => shopContext.Customer.Remove(customer);
        public Customer Find(Predicate<Customer> p) => shopContext.Customer.ToList().Find(p);
        public List<Customer> GetAll() => throw new NotImplementedException();
        public void Update(Customer customer) => shopContext.Update(customer);
    }
}
