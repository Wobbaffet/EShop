using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EShop.Model.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.WepApp.Controllers
{
    public class CartController : Controller
    {
        // GET: CartController
        public ActionResult Index()
        {
            byte[] orderByte = HttpContext.Session.Get("order");
            Order model = null;
            if (!(orderByte is null))
                model = JsonSerializer.Deserialize<Order>(orderByte);
            return View(model);
        }

        public void ChangeSupplisForThisItem(int id, int value)
        {
            byte[] orderByte = HttpContext.Session.Get("order");

            Order order = JsonSerializer.Deserialize<Order>(orderByte);
            order.OrderItems.Find(oi => oi.BookId == id).Quantity = value;
            HttpContext.Session.Set("order", JsonSerializer.SerializeToUtf8Bytes(order));
        }

        [HttpDelete]
        public void RemoveItemFromCart(int bookid)
        {
            RemoveFromCart(bookid);
        }

        private void RemoveFromCart(int id)
        {
            byte[] orderByte = HttpContext.Session.Get("order");

            Order order = JsonSerializer.Deserialize<Order>(orderByte);
            order.OrderItems.RemoveAll(o => o.BookId == id);
            HttpContext.Session.Set("order", JsonSerializer.SerializeToUtf8Bytes(order));//template method pattern
        }
    }
}
