using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EShop.Data.UnitOfWork;
using EShop.Model;
using EShop.Model.Domain;
using EShop.WepApp.Fillters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShop.WepApp.Controllers
{
    [LoggedUserFillter]
    [ForbiddenForAdminFillter]
    [AddToCartFillter]
    public class CartController : Controller
    {
        private readonly IUnitOfWork uow;

        public CartController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
      

        public void ChangeSupplisForThisItem(int id, int value)
        {
            byte[] orderByte = HttpContext.Session.Get("order");

            Order order = JsonSerializer.Deserialize<Order>(orderByte);
            order.OrderItems.Find(oi => oi.Book.BookId == id).Quantity = value;
            order.Total = order.OrderItems.Sum(ot => ot.Quantity * ot.Book.Price);
            HttpContext.Session.Set("order", JsonSerializer.SerializeToUtf8Bytes(order));
        }

        [HttpDelete]
        public void RemoveItemFromCart(int bookid)
        {
            byte[] orderByte = HttpContext.Session.Get("order");

            Order order = JsonSerializer.Deserialize<Order>(orderByte);
            var orderItem = order.OrderItems.Find(oi => oi.Book.BookId == bookid);
            order.Total -= orderItem.Quantity*orderItem.Book.Price;
            order.OrderItems.RemoveAll(o => o.Book.BookId == bookid);
            HttpContext.Session.Set("order", JsonSerializer.SerializeToUtf8Bytes(order));
            int? items = HttpContext.Session.GetInt32("cartItems");
            HttpContext.Session.SetInt32("cartItems", (int)--items);
        }


    }
}
