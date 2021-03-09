using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.WepApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork uow;

        public BookController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        // GET: BooksController
        public ActionResult Index()
        {
            List<Book> books = uow.RepositoryBook.GetAll();
            return View("Index", books);
        }



        public ActionResult AddBookToCart(int bookId)
        {
            AddBookToCart(uow.RepositoryBook.Find(b => b.BookId == bookId));
            return Index();
        }

        private void AddBookToCart(Book book)
        {
            byte[] orderByte = HttpContext.Session.Get("order");

            Order order;
            if (orderByte is null)
            {
                order = new Order();
                order.OrderItems = new List<OrderItem>();
            }
            else
            {
                order = JsonSerializer.Deserialize<Order>(orderByte);
            }

            OrderItem oi = order.OrderItems.Find(oi => oi.BookId == book.BookId);
            if (oi != null)
            {
                oi.Quantity++;
            }
            else
            {
            order.OrderItems.Add(new OrderItem { BookId = book.BookId, Book = book, Quantity = 1 });
            }
            
            HttpContext.Session.Set("order", JsonSerializer.SerializeToUtf8Bytes(order));
            //if (order.OrderItems.Count == 2)
            //{
            //    order.Date = DateTime.Now;
            //    order.Total = 999;
            //    uow.RepositoryOrder.Add(order);
            //    uow.Commit();
            //}
        }
    }
}
