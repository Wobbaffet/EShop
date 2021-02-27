using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;

namespace EShop.ConsoleApp.Domain
{
    class Program
    {
        static void Main(string[] args)
        {
            ShopContext context = new ShopContext();
            List<Genre> gs = new List<Genre>
            {
                new Genre{ Name = "a" },
                new Genre{ Name = "s" },
                new Genre{ Name = "d" },
                new Genre{ Name = "f" },
            };
            context.Add(new Book { Genres = gs, Price = 1000, Title = "title", Supplies = 9 });
            context.SaveChanges();
            context.Dispose();
        }
    }
}
