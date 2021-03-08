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
            byte[] booksByte = HttpContext.Session.Get("books");

            List<Book> books;
            if (booksByte is null)
            {
                books = new List<Book>();
            }
            else
            {
                books = JsonSerializer.Deserialize<List<Book>>(booksByte);
            }

            books.Add(book);
            HttpContext.Session.Set("books", JsonSerializer.SerializeToUtf8Bytes(books));
        }
    }
}
