using EShop.Data.Implementation;
using EShop.Data.Implementation.Interfaces;
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
            RepositoryBook = new RepositoryBook(context);
            RepositoryOrder = new RepositoryOrder(context);
            RepositoryGenre = new RepositoryGenre(context);
            RepositoryAutor = new RepositoryAutor(context);
        }
        public IRepositoryBook RepositoryBook { get ; set ; }
        public IRepositoryCustomer RepostiryCustomer { get; set; }
        public IRepositoryOrder RepositoryOrder { get; set; }
        public IRepositoryGenre RepositoryGenre { get; set; }
        public IRepositoryAutor RepositoryAutor { get; set; }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
