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

        // GET: BooksController
        public ActionResult Index()
        {
            List<Genre> model = uow.RepositoryGenre.GetAll();
            return View("PartialBooks", model);
        }

        public List<Autor> GetAllAutors()
        {
            List<Autor> autors = new List<Autor>();

            foreach (var book in uow.RepositoryBook.GetAll())
            {
                foreach (var a in book.Autors)
                {
                    if (!autors.Contains(a))
                    {
                        a.Books.Clear();
                        autors.Add(a);
                    }
                }
            }
            return autors;
        }


        public ActionResult AddBookToCart(int bookId)
        {
            AddBookToCart(uow.RepositoryBook.FindWithoutInclude(b => b.BookId == bookId));

            int? cartItems = HttpContext.Session.GetInt32("cartItems");
            if (cartItems is null)
            {
                cartItems = 0;
            }
            else
                cartItems++;

            HttpContext.Session.SetInt32("cartItems", (int)cartItems);


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

            OrderItem oi = order.OrderItems.Find(oi => oi.Book.BookId == book.BookId);
            if (oi != null)
            {
                oi.Quantity++;
            }
            else
            {

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
        public int NubmerOfBooksByAutorGenre(string autor, List<string> genres)
        {
            List<Book> appropriate = new List<Book>();
            if (autor == "All")
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
                    List<Book> books = uow.RepositoryBook.Search(autor);
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
                    return uow.RepositoryBook.Search(autor).Count;
            }
        }

        private List<Book> AllBooksByAutorGenre(string autor, List<string> genres)
        {
            List<Book> appropriate = new List<Book>();
            if (autor == "All")
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
                    List<Book> books = uow.RepositoryBook.Search(autor);
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
                    return uow.RepositoryBook.Search(autor);
            }
        }

        [HttpGet]
        public List<Book> ReturnSixBooks(int pagiNumber, string autor, List<string> genres)
        {
            int max;
            if (autor == "all" || autor == "All")
                max = NubmerOfBooks(genres);
            else
                max = NubmerOfBooksByAutorGenre(autor, genres);

            List<Book> books = new List<Book>();
            if (pagiNumber * 6 > max)
            {
                books = AllBooksByAutorGenre(autor, genres).GetRange(pagiNumber * 6 - 6, 6 - pagiNumber * 6 + max);
            }
            else
            {
                books = AllBooksByAutorGenre(autor, genres).GetRange(pagiNumber * 6 - 6, 6);
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
            string[] s = book.Split(" (");
            Book b = uow.RepositoryBook.FindWithInclude(b => b.Title == s[0]);
            return RedirectToAction("ShowItem", "Book", new { bookId = b.BookId });
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
                if (item.Title.ToLower().StartsWith(title))
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
