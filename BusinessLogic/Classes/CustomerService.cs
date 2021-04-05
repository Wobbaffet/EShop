using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using EShop.Data.UnitOfWork;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model;
using EShop.Model.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Classes
{


    public class CustomerService : ICustomer
    {

       
        public CustomerService()
        {

            uow = new EShopUnitOfWork(new ShopContext());
        }

        public IUnitOfWork uow { get ; set ; }

        public Customer Find(SignInViewModel model)
        {
            Customer customer = uow.RepostiryCustomer.FindWithoutInclude(c => c.Email == model.Email && c.Password == model.Password);

            if (customer is null)
                throw new SignInException("Wrong credentials");

            return customer;
        }
    }
}

