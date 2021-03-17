using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.APIHelpers;
using EShop.WepApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EShop.WepApp.Controllers
{
    public class AdminController : Controller
    {
        private IUnitOfWork uow;

        public EShopServices Services { get; }

        public AdminController(IUnitOfWork uow, EShopServices services)
        {
            this.uow = uow;
            Services = services;
        }

        public async Task<IActionResult> Index(string name)
        {
            MainClass model = await Services.GetBooksFromAPI(name);
            model.genres = uow.RepositoryGenre.GetAll();

            return View("Index", model);
        }

        public ActionResult SelectedBooks()
        {
            byte[] booksByte = HttpContext.Session.Get("book");
            List<Book> model = null;
            if (!(booksByte is null))
                model = JsonSerializer.Deserialize<List<Book>>(booksByte);
            else
            {
                model = new List<Book>();
            }
            return View("SelectedBooks", model);
        }

        public void PickedBooks(string image, string title, double price, int supplies, string authors, string genres, string description)
        {
            Book book = new Book
            {
                Image = image,
                Title = title,
                Price = price,
                Supplies = supplies,
                Autors = GetAuthors(authors),
                Genres = GetGenres(genres),
                Description = description
            };
            byte[] booksByte = HttpContext.Session.Get("book");
            List<Book> books = null;
            if (!(booksByte is null))
                books = JsonSerializer.Deserialize<List<Book>>(booksByte);
            else
            {
                books = new List<Book>();
            }
            books.Add(book);
            HttpContext.Session.Set("book", JsonSerializer.SerializeToUtf8Bytes(books));
        }

        public void SaveBooks()
        {
            byte[] booksByte = HttpContext.Session.Get("book");
            List<Book> books = null;
            if (!(booksByte is null))
                books = JsonSerializer.Deserialize<List<Book>>(booksByte);
            foreach (var item in books)
            {
                uow.RepositoryBook.Add(item);
            }
            uow.Commit();
            books = null;
            HttpContext.Session.Set("book", JsonSerializer.SerializeToUtf8Bytes(books));
        }

        [HttpDelete]
        public void RemoveItemFromList(string title, string description)
        {
            byte[] booksByte = HttpContext.Session.Get("book");

            List<Book> books = JsonSerializer.Deserialize<List<Book>>(booksByte);
            books.RemoveAll(b => b.Title == title && b.Description == description);
            HttpContext.Session.Set("book", JsonSerializer.SerializeToUtf8Bytes(books));//template method pattern
        }

        private List<Genre> GetGenres(string genres)
        {
            string[] genresArr = genres.Split(", ");
            List<Genre> genresList = new List<Genre>();
            foreach (string item in genresArr)
            {
                if (item != "")
                    genresList.Add(new Genre { Name = item });
            }
            return genresList;
        }

        public List<Autor> GetAuthors(string authors)
        {
            string[] authorsArr = authors.Split("\n");
            List<Autor> authorsList = new List<Autor>();
            foreach (string item in authorsArr)
            {
                string author = item.Trim();
                if (author != "")
                {
                    string[] name = author.Split(" ");
                    if (name.Length == 2)
                        authorsList.Add(new Autor { FirstName = name[0], LastName = name[1] });
                    else if (name.Length > 2)
                    {
                        authorsList.Add(new Autor { FirstName = name[0] + name[1], LastName = name[2] });
                    }
                }
            }
            return authorsList;
        }
    }
}
