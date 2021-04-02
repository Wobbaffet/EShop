using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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

        public BookController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public ActionResult Index()
        {
            List<Genre> model = uow.RepositoryGenre.GetAll();
            return View("PartialBooks", model);
        }

        public bool AddBookToCart(int bookId)
        {
            bool addToChart = false;
            AddBookToCart(uow.RepositoryBook.FindWithoutInclude(b => b.BookId == bookId), ref addToChart);

            int? cartItems = HttpContext.Session.GetInt32("cartItems");
            if (cartItems is null)
            {
                cartItems = 0;
            }
            else
            {
                if(addToChart)
                cartItems++;
            }

            HttpContext.Session.SetInt32("cartItems", (int)cartItems);


            return addToChart ;
        }

        private void AddBookToCart(Book book,ref bool addToCart)
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
                if(oi.Book.Supplies>oi.Quantity)
                oi.Quantity++;
                addToCart = false;
            }
            else
            {
                addToCart = true;
                order.OrderItems.Add(new OrderItem { Book = book, Quantity = 1 });
            }

            order.Total = order.OrderItems.Sum(ot => ot.Quantity * ot.Book.Price);

            HttpContext.Session.Set("order", JsonSerializer.SerializeToUtf8Bytes(order));

        }

        [HttpGet]
        public int NubmerOfBooks(List<string> genres)
        {
            List<Book> appropriate = new List<Book>();
            if (genres.Count > 0)
            {
                List<Book> books = uow.RepositoryBook.GetAll();
                foreach (var book in books)
                {
                    int i = 0;
                    foreach (var genre in book.Genres)
                    {
                        if (genres.Contains(genre.Name))
                            i++;
                    }
                    if (!(i == 0))
                        appropriate.Add(book);
                }
                return appropriate.Count;
            }
            else
                return uow.RepositoryBook.GetAll().Count;
        }

        [HttpGet]
        public int NubmerOfBooksByPriceGenre(string price, List<string> genres)
        {
            int firstPrice = 0;
            int secondPrice = 0;
            if (price== null || price == "No filters") { }
            else if (price.Contains("Less"))
            {
                secondPrice = 500;
            }
            else if (price.Contains("More"))
            {
                firstPrice = 5000;
                secondPrice = int.MaxValue;
            }
            else
            {
                string[] prices = price.Split(" - ");
                firstPrice = int.Parse(prices[0]);
                secondPrice = int.Parse(prices[1]);
            }
            List<Book> appropriate = new List<Book>();
            if (price == "No filters")
            {
                if (genres.Count > 0)
                {
                    List<Book> books = uow.RepositoryBook.GetAll();
                    foreach (var book in books)
                    {
                        int i = 0;
                        foreach (var genre in book.Genres)
                        {
                            if (genres.Contains(genre.Name))
                                i++;
                        }
                        if (!(i == 0))
                            appropriate.Add(book);
                    }
                    return appropriate.Count;
                }
                else
                    return uow.RepositoryBook.GetAll().Count;
            }
            else
            {
                if (genres.Count > 0)
                {
                    List<Book> books = uow.RepositoryBook.GetAll().FindAll(b => b.Price >= firstPrice && b.Price <= secondPrice);
                    foreach (var book in books)
                    {
                        int i = 0;
                        foreach (var genre in book.Genres)
                        {
                            if (genres.Contains(genre.Name))
                                i++;
                        }
                        if (!(i == 0))
                            appropriate.Add(book);
                    }
                    return appropriate.Count;
                }
                else
                    return uow.RepositoryBook.GetAll().FindAll(b => b.Price >= firstPrice && b.Price <= secondPrice).Count;
            }
        }

        private List<Book> AllBooksByPriceGenre(string price, List<string> genres)
        {
            int firstPrice = 0;
            int secondPrice = 0;
            if (price == null || price == "No filters") { }
            else if (price.Contains("Less"))
            {
                secondPrice = 500;
            }
            else if (price.Contains("More"))
            {
                firstPrice = 5000;
                secondPrice = int.MaxValue;
            }
            else
            {
                string[] prices = price.Split(" - ");
                firstPrice = int.Parse(prices[0]);
                secondPrice = int.Parse(prices[1]);
            }
            List<Book> appropriate = new List<Book>();
            if (price == "No filters")
            {
                if (genres.Count > 0)
                {
                    List<Book> books = uow.RepositoryBook.GetAll();
                    foreach (var book in books)
                    {
                        int i = 0;
                        foreach (var genre in book.Genres)
                        {
                            if (genres.Contains(genre.Name))
                                i++;
                        }
                        if (!(i == 0))
                            appropriate.Add(book);
                    }
                    return appropriate;
                }
                else
                    return uow.RepositoryBook.GetAll();
            }
            else
            {
                if (genres.Count > 0)
                {
                    List<Book> books = uow.RepositoryBook.GetAll().FindAll(b => b.Price >= firstPrice && b.Price <= secondPrice);
                    foreach (var book in books)
                    {
                        int i = 0;
                        foreach (var genre in book.Genres)
                        {
                            if (genres.Contains(genre.Name))
                                i++;
                        }
                        if (!(i == 0))
                            appropriate.Add(book);
                    }
                    return appropriate;
                }
                else
                    return uow.RepositoryBook.GetAll().FindAll(b => b.Price >= firstPrice && b.Price <= secondPrice);
            }
        }

        [HttpGet]
        public List<Book> ReturnTwelveBooks(int pagiNumber, string price, List<string> genres)
        {
            int max;
            if (price == "No filters")
                max = NubmerOfBooks(genres);
            else
                max = NubmerOfBooksByPriceGenre(price, genres);

            List<Book> books = new List<Book>();
            if (pagiNumber * 12 > max)
            {
                books = AllBooksByPriceGenre(price, genres).GetRange(pagiNumber * 12 - 12, 12 - pagiNumber * 12 + max);
            }
            else
            {
                books = AllBooksByPriceGenre(price, genres).GetRange(pagiNumber * 12 - 12, 12);
            }
            foreach (var book in books)
            {
                book.Autors.Clear();
                book.Genres.Clear();
            }

            return books;
        }

        public List<Book> SearchBooks(string autor)
        {
            List<Book> books = uow.RepositoryBook.Search(autor);
            foreach (var item in books)
            {
                item.Autors.Clear();
            }
            return books;
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
                Book b = uow.RepositoryBook.FindWithInclude(b => b.Title == s[0]);
                return RedirectToAction("ShowItem", "Book", new { bookId = b.BookId });
            }
            catch (NullReferenceException )
            {

                return Index();
            }
        }
        public ActionResult ShowItem(int bookId)
        {
            Book model = uow.RepositoryBook.FindWithInclude(b => b.BookId == bookId);
            return View("ShowItem", model);
        }

        public List<Book> FindBooksByTitle(string title)
        {
            List<Book> books = new List<Book>();
            if (title == null)
                return books;
            foreach (var item in uow.RepositoryBook.GetAll())
            {
                if (item.Title.ToLower().StartsWith(title.ToLower()))
                {
                    item.Autors = null;
                    item.Genres = null;
                    books.Add(item);
                }
            }
            return books;
        }
    }
}
