using EShop.Data.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IRepositoryBook RepositoryBook { get; set; }
        public IRepostiryCustomer RepostiryCustomer { get; set; }
        void Commit();
    }
}
