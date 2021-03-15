using EShop.Data.UnitOfWork;
using EShop.Data.UnitOfWorkFolder;
using EShop.Model;
using EShop.Model.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace EShop.ConsoleApp.Domain
{
    class Program
    {
        static void Main(string[] args)
        {
            ShopContext context = new ShopContext();
            IUnitOfWork uow = new EShopUnitOfWork(context);

            Customer customer = uow.RepostiryCustomer.Find(c => c.Email == "acavicic@gmail.com");
        }
    }
}
