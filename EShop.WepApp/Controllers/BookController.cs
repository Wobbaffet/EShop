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
    [AddToCartFillter]
    public class BookController : Controller
    {
        private readonly IUnitOfWork uow;

        public BookController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        // GET: BooksController
        public ActionResult Index(/*List<Book> bs*/)
        {
            //BookViewModel bvm = new BookViewModel();

            ////List<Book> books = new List<Book>();
            ////if (bs is null || bs.Count == 0)
            ////    books = uow.RepositoryBook.GetAll();
            ////else
            ////    books = bs;

            //List<Book> books = uow.RepositoryBook.GetAll();
            //List<Autor> autors = new List<Autor>();

            //foreach (var book in uow.RepositoryBook.GetAll())
            //{
            //    foreach (var autor in book.Autors)
            //    {
            //        if (!autors.Contains(autor))
            //        {
            //            autors.Add(autor);
            //        }
            //    }
            //}
            //bvm.Books = books;
            //bvm.Autors = autors;
            return View("PartialBooks");
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

           int ?cartItems = HttpContext.Session.GetInt32("cartItems");
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
               
                order.OrderItems.Add(new OrderItem { Book=book,Quantity = 1 });
            }

            order.Total = order.OrderItems.Sum(ot => ot.Quantity * ot.Book.Price);

            HttpContext.Session.Set("order", JsonSerializer.SerializeToUtf8Bytes(order));

        }

        [HttpGet]
        public int NubmerOfBooks()
        {
            return uow.RepositoryBook.GetAll().Count;
        }

        [HttpGet]
        public int NubmerOfBooksByAutor(string autor)
        {
            if (autor == "All")
                return uow.RepositoryBook.GetAll().Count;
            else
                return uow.RepositoryBook.Search(autor).Count;
        }

        [HttpGet]
        public List<Book> ReturnSixBooks(int pagiNumber, string autor)
        {//kad se radi filtriranje za all ovde vraca nekog drugog autora
            int max;
            if (autor == "all" || autor == "All")
                max = NubmerOfBooks();
            else
                max = NubmerOfBooksByAutor(autor);

            List<Book> books = new List<Book>();
            if (pagiNumber * 6 > max)
            {
                books = uow.RepositoryBook.GetAll().GetRange(pagiNumber * 6 - 6, 6 - pagiNumber * 6 + max);
            }
            else
            {
                books = uow.RepositoryBook.GetAll().GetRange(pagiNumber * 6 - 6, 6);
            }
            foreach (var book in books)
            {
                book.Autors.Clear();
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
