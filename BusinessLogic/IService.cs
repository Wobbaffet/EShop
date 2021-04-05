using EShop.Data.UnitOfWork;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model;
using System;

namespace BusinessLogic
{
    public interface IService
    {


        public IUnitOfWork uow { get;  set; }

    }
}
