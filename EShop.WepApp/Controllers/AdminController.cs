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

        public ActionResult ViewOrders()
        {

            var orders = uow.RepositoryOrder.GetAll();
            return View("Orders", orders);
        }


        public void OrderStatusChanged(int orderId, OrderStatus status)
        {

            byte[] orderByte = HttpContext.Session.Get("orderStatusChanged");
            List<Order> orders;
            if (orderByte is null)
            {
                orders = new List<Order>()
                {
                    new Order()
                    {

                   OrderId = orderId,
                   OrderStatus=status
                    }
                 };
            }
            else
            {

            orders = JsonSerializer.Deserialize<List<Order>>(orderByte);

            var exist = orders.Find(o => o.OrderId == orderId);
            if (exist is null)
            {
                orders.Add(new Order() { OrderId = orderId, OrderStatus = status });
            }
            else
                exist.OrderStatus = status;


            }

            HttpContext.Session.Set("orderStatusChanged", JsonSerializer.SerializeToUtf8Bytes(orders));


        }

        public ActionResult UpdateOrder()
        {
            byte[] orderByte = HttpContext.Session.Get("orderStatusChanged");
            if (orderByte is null)
                return ViewOrders();

            List<Order> orders= JsonSerializer.Deserialize<List<Order>>(orderByte);

            orders.ForEach(o =>
            {
                Order order = uow.RepositoryOrder.Find(or => or.OrderId == o.OrderId);
                order.OrderStatus = o.OrderStatus;
                uow.Commit();
            });

            HttpContext.Session.Remove("orderStatusChanged");

            return ViewOrders();
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
            //if (!(booksByte is null))
            books = JsonSerializer.Deserialize<List<Book>>(booksByte);
            foreach (var item in books)
            {
                for (int i = 0; i < item.Genres.Count; i++)
                {
                    item.Genres[i] = uow.RepositoryGenre.Find(g => g.Name == item.Genres[i].Name);
                }
                for (int i = 0; i < item.Autors.Count; i++)
                {
                    Autor a = uow.RepositoryAutor.Find(a => a.FirstName == item.Autors[i].FirstName && a.LastName == item.Autors[i].LastName);
                    if (a != null)
                    {
                        item.Autors[i] = a;
                    }
                }
                uow.RepositoryBook.Add(item);
            }
            uow.Commit();
            books = new List<Book>();
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
                {
                    var genre = new Genre() { Name = item };
                    genresList.Add(genre);
                }

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
                    if (name.Length == 1)
                    {
                        authorsList.Add(new Autor { FirstName = name[0], LastName = "" });
                    }
                    if (name.Length == 2)
                        authorsList.Add(new Autor { FirstName = name[0], LastName = name[1] });
                    else if (name.Length > 2)
                    {
                        string lastname = "";
                        for (int i = 2; i < name.Length; i++)
                        {
                            lastname = lastname + name[i];
                        }
                        authorsList.Add(new Autor { FirstName = name[0] + " " + name[1], LastName = lastname });
                    }
                }
            }
            return authorsList;
        }



    }
}
