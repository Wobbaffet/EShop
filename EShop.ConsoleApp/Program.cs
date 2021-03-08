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

            Order o = new Order { Date = DateTime.Now, Total = 1000, OrderItems = new List<OrderItem>() };

            Book book = uow.RepositoryBook.Find(b => b.BookId == 2);
            OrderItem oi = new OrderItem
            {
                Book = book,
                Quantity = 1
            };
            book = uow.RepositoryBook.Find(b => b.BookId == 3);
            o.OrderItems.Add(oi);
            oi = new OrderItem
            {
                Book = book,
                Quantity = 1
            };
            o.OrderItems.Add(oi);
            uow.RepositoryOrder.Add(o);
            uow.Commit();
        }
    }
}
