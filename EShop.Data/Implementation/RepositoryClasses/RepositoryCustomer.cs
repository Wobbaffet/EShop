using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Implementation.RepositoryClasses
{
    public class RepositoryCustomer : IRepostiryCustomer
    {
        private ShopContext shopContext;

        public RepositoryCustomer(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public void Add(Customer entity)
        {
            shopContext.Add(entity);
        }

        public void Update(Customer customer)
        {
            shopContext.Update(customer);
        }

    }
}
