using EShop.Data.Implementation;
using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.Fillters;
using EShop.WepApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Controllers
{
    [LoggedUserFillter]
    [ForbiddenForAdminFillter]
    [AddToCartFillter]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork uow;

        public HomeController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        // private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private int TotalNumberOfThisBook(List<Order> list, int id)
        {
            int number = 0;
            foreach (var order in list)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    if (orderItem.Book.BookId == id)
                        number += orderItem.Quantity;
                }
            }
            return number;
        }
        public ActionResult Index()
        {
            List<Book> model = new List<Book>();
            List<Book> latest = uow.RepositoryBook.GetAll();

            for (int i = latest.Count - 1; i > latest.Count - 11; i--)
            {
                if (i >= 0)
                    model.Add(latest[i]);
            }


            List<NewestBooks> newest = new List<NewestBooks>();

            List<Order> orders = uow.RepositoryOrder.GetAll();

            foreach (var order in orders)
            {
                foreach (var orderItems in order.OrderItems)
                {
                    int totalNumberOfThisBook = TotalNumberOfThisBook(orders, orderItems.Book.BookId);
                    if (newest.Count < 5 && !(newest.Any(b => b.bookId == orderItems.Book.BookId)))
                    {
                        newest.Add(new NewestBooks(orderItems.Book.BookId, totalNumberOfThisBook, orderItems.Book));
                    }
                    else if (!(newest.Any(b => b.bookId == orderItems.Book.BookId)))
                    {
                        if (totalNumberOfThisBook > newest.Min(b => b.numberOfPurchase))
                        {
                            newest.Remove(newest.Find(b => b.numberOfPurchase == newest.Min(b => b.numberOfPurchase)));
                            newest.Add(new NewestBooks(orderItems.Book.BookId, totalNumberOfThisBook, orderItems.Book));
                        }
                    }
                }
            }

            foreach (var item in newest)
            {
                model.Add(item.Book);
            }

            if (model is null || model.Count < 15)
                model = uow.RepositoryBook.GetAll();

            return View(model);
        }

        public struct NewestBooks
        {
            public int bookId;
            public int numberOfPurchase;
            public Book Book;

            public NewestBooks(int bookId, int numberOfBuying, Book book)
            {
                this.bookId = bookId;
                this.numberOfPurchase = numberOfBuying;
                this.Book = book;
            }
        }
        public IActionResult Privacy()
        {

            return View();
        }
    }
}
