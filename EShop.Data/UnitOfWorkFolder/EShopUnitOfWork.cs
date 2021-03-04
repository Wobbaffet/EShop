using EShop.Data.Implementation;
using EShop.Data.Implementation.RepositoryClasses;
using EShop.Data.UnitOfWork;
using EShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.UnitOfWorkFolder
{
    public class EShopUnitOfWork : IUnitOfWork
    {
        private ShopContext context;

        public EShopUnitOfWork(ShopContext context)
        {
            this.context = context;
            RepostiryCustomer = new RepositoryCustomer(context);


        }
        public IRepositoryBook RepositoryBook { get ; set ; }
        public IRepostiryCustomer RepostiryCustomer { get; set; }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
