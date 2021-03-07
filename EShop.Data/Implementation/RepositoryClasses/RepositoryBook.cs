using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Implementation.RepositoryClasses
{
    public class RepositoryBook : IRepositoryBook
    {
        private readonly ShopContext context;

        public RepositoryBook(ShopContext context)
        {
            this.context = context;
        }
        public void Add(Book entity)
        {
            context.Add(entity);
        }
    }
}
