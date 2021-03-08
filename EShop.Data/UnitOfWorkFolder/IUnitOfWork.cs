using EShop.Data.Implementation;
using EShop.Data.Implementation.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IRepositoryBook RepositoryBook { get; set; }
        public IRepositoryCustomer RepostiryCustomer { get; set; }
        public IRepositoryOrder RepositoryOrder { get; set; }
        void Commit();
    }
}
