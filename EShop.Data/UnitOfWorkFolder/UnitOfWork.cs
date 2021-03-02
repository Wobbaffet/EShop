using EShop.Data.Implementation;
using EShop.Data.UnitOfWork;
using EShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.UnitOfWorkFolder
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShopContext context;

        public UnitOfWork(ShopContext context)
        {
            this.context = context;
        }
        public IRepositoryBook RepositoryBook { get ; set ; }
        public IRepostiryCustomer RepostiryCustomer { get; set; }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
