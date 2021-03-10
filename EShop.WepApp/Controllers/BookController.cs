using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.Models;
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
        public ActionResult Index(List<Book> bs)
        {
            BookViewModel bvm = new BookViewModel();

            List<Book> books = new List<Book>();
            if (bs is null || bs.Count == 0)
                books = uow.RepositoryBook.GetAll();
            else
                books = bs;

            List<Autor> autors = new List<Autor>();

            foreach (var book in uow.RepositoryBook.GetAll())
            {
                foreach (var autor in book.Autors)
                {
                    if (!autors.Contains(autor))
                    {
                        autors.Add(autor);
                    }
                }
            }
            bvm.Books = books;
            bvm.Autors = autors;
            return View("Index", bvm);
        }

        public ActionResult SearchBooks(string autor)
        {
            List<Book> books = uow.RepositoryBook.Search(autor);
            return Index(books);
        }


        public ActionResult AddBookToCart(int bookId)
        {
            AddBookToCart(uow.RepositoryBook.Find(b => b.BookId == bookId));
            return Index(null);
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

            order.Total = order.OrderItems.Sum(ot => ot.Quantity * ot.Book.Price);

            HttpContext.Session.Set("order", JsonSerializer.SerializeToUtf8Bytes(order));

        }
    }
}
