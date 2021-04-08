using EShop.Data.UnitOfWork;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model;
using System;

namespace BusinessLogic
{
    /// <summary>
    /// Represent interface for business logic 
    /// </summary>
    public interface IService
    {

        /// <value>Represent reference to IUnitOfWork</value>
        public IUnitOfWork uow { get;  set; }

    }
}
