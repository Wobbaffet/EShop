using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogic.Classes;
using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.Fillters;
using EShop.WepApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.WepApp.Controllers
{
    [LoggedUserFillter]
    [ForbiddenForAdminFillter]
    [AddToCartFillter]
    public class BookController : Controller
    {
        private readonly IUnitOfWork uow;
        private BookService service;
        public BookController(IUnitOfWork uow)
        {
            this.uow = uow;
            service = new BookService(uow);
           // service = new BookService(new EShopUnitOfWork(new ShopContext()));
        }


        public ActionResult ShowItem(int bookId)
        {
            return View("ShowItem", service.Find(bookId));
        }
        public ActionResult Index()
        {
            List<Genre> model = uow.RepositoryGenre.GetAll();
            return View("PartialBooks", model);
        }

        public bool AddBookToCart(int bookId)
        {
            bool addToChart = false;
            AddBookToCart(uow.RepositoryBook.Find(b => b.BookId == bookId), ref addToChart);

            int? cartItems = HttpContext.Session.GetInt32("cartItems");
            if (cartItems is null)
            {
                cartItems = 0;
            }
            else
            {
                if (addToChart)
                    cartItems++;
            }
            HttpContext.Session.SetInt32("cartItems", (int)cartItems);
            return addToChart;
        }

        private void AddBookToCart(Book book, ref bool addToCart)
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

            OrderItem oi = order.OrderItems.Find(oi => oi.Book.BookId == book.BookId);
            if (oi != null)
            {
                if (oi.Book.Supplies > oi.Quantity)
                    oi.Quantity++;
                addToCart = false;
            }
            else
            {
                addToCart = true;
                order.OrderItems.Add(new OrderItem { Book = book, Quantity = 1 });
            }

            order.Total = order.OrderItems.Sum(ot => ot.Quantity * ot.Book.Price);

            order.OrderItems.ForEach(oi => {
                oi.Book.Autors.Clear();
                oi.Book.Genres.Clear();
            } ) ;

            HttpContext.Session.Set("order", JsonSerializer.SerializeToUtf8Bytes(order));
        }

        [HttpGet]
        public int NubmerOfBooksByCondition(string price, List<string> genres) => service.GetBooksNumberByCondition(price, genres);
        [HttpGet]
        public List<Book> ReturnTwelveBooks(int pagiNumber, string price, List<string> genres)
        {
            try
            {
                var x= service.GetBooksByCondition(pagiNumber, price, genres);
                foreach (var item in x)
                {
                    item.Genres.Clear();
                    //item.Autors.Clear();
                }
                return x;

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                return new List<Book>();
            }
        }
        [HttpPost]
        public ActionResult GenerateBookUrl(int bookId)
        {
            return Json(new { redirectUrl = Url.Action("ShowItem", "Book", new { bookId = bookId }, Request.Scheme) });
        }
        public ActionResult PassBook(string book)
        {
            try
            {
                string[] s = book.Split(" (");
                Book b = uow.RepositoryBook.Find(b => b.Title == s[0]);
                return RedirectToAction("ShowItem", "Book", new { bookId = b.BookId });
            }
            catch (NullReferenceException)
            {
                return Index();
            }
        }
        public List<Book> FindBooksByTitle(string title)
        {
            try
            {
                return service.Search(title);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
        public void AddBooks()
        {
            byte[] booksByte = HttpContext.Session.Get("book");
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(booksByte);

            service.Add(books);
            books = new List<Book>();
            HttpContext.Session.Set("book", JsonSerializer.SerializeToUtf8Bytes(books));

            HttpContext.Session.Remove("numberOfSelectedBooks");
        }
    }
}
