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
        {//kad se radi filtriranje za all ovde vraca nekog drugog autora
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

    }
}
