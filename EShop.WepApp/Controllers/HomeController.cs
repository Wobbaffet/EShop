using EShop.Data.Implementation;
using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.Fillters;
using EShop.WepApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EShop.WepApp.Controllers
{
    [LoggedUserFillter]
    [ForbiddenForAdminFillter]
    [AddToCartFillter]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork uow;

        public HomeController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        private int TotalNumberOfThisBook(List<Order> list, int id)
        {
            int number = 0;
            foreach (var order in list)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    if (orderItem.Book.BookId == id)
                        number += orderItem.Quantity;
                }
            }
            return number;
        }
        public ActionResult Index()
        {
            List<Book> model = new List<Book>();
            List<Book> latest = uow.RepositoryBook.GetAll();

            for (int i = latest.Count - 1; i > latest.Count - 11; i--)
            {
                if (i >= 0)
                    model.Add(latest[i]);
            }

            //<3 Drago volimo te 
            List<NewestBooks> newest = new List<NewestBooks>();

            List<Order> orders = uow.RepositoryOrder.GetAll();

            foreach (var order in orders)
            {
                foreach (var orderItems in order.OrderItems)
                {
                    int totalNumberOfThisBook = TotalNumberOfThisBook(orders, orderItems.Book.BookId);
                    if (newest.Count < 5 && !(newest.Any(b => b.bookId == orderItems.Book.BookId)))
                    {
                        newest.Add(new NewestBooks(orderItems.Book.BookId, totalNumberOfThisBook, orderItems.Book));
                    }
                    else if (!(newest.Any(b => b.bookId == orderItems.Book.BookId)))
                    {
                        if (totalNumberOfThisBook > newest.Min(b => b.numberOfPurchase))
                        {
                            newest.Remove(newest.Find(b => b.numberOfPurchase == newest.Min(b => b.numberOfPurchase)));
                            newest.Add(new NewestBooks(orderItems.Book.BookId, totalNumberOfThisBook, orderItems.Book));
                        }
                    }
                }
            }

            foreach (var item in newest)
            {
                model.Add(item.Book);
            }

            if (model is null || model.Count < 15)
                model = uow.RepositoryBook.GetAll();

            return View(model);
        }

        public struct NewestBooks
        {
            public int bookId;
            public int numberOfPurchase;
            public Book Book;

            public NewestBooks(int bookId, int numberOfBuying, Book book)
            {
                this.bookId = bookId;
                this.numberOfPurchase = numberOfBuying;
                this.Book = book;
            }
        }
        public ActionResult AboutUs()
        {
            return View("AboutUs");
        }
        public ActionResult ContactUs()
        {
            return View("ContactUs");
        }

        public ActionResult SendComment(string name, string email, string phone, string title, string text)
        {
            SendEmail(name, email, phone, title, text);
            return View("ContactUs");
        }

        private void SendEmail(string name, string email, string phone, string title, string text)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("dragojlo406@gmail.com", "pitajbabu406.");

            MailMessage message = new MailMessage();

            message.Subject = title;
            message.Body = $"{name} {email} {phone}\n\n{text}";

            message.To.Add("dragojlo406@gmail.com");
            message.From = new MailAddress("dragojlo406@gmail.com");

            try
            {
                smtp.Send(message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
