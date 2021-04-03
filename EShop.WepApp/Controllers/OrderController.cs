using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.Fillters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EShop.WepApp.Controllers
{
    [LoggedUserFillter]
    //[ForbiddenForAdminFillter]
    [AddToCartFillter]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork uow;

        public OrderController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        [ForbiddenForAdminFillter]
        [PurchaseFillter]
        public ActionResult Index()
        {
            List<Order> orders = uow.RepositoryOrder.GetAllOrders(o => o.CustomerId == (int)HttpContext.Session.GetInt32("customerId"));
            return View(orders);
        }

        public ActionResult ShowOrderItems(int orderId)
        {
            Order order = uow.RepositoryOrder.FindWithInclude(o => o.OrderId == orderId);
            return View("OrderItems",order);
        }

        public ActionResult Sort(string condition)
        {
            if (condition == "Status")
                return Json(new { redirectUrl = Url.Action("ViewOrders", "Order", new { sortStatus = true }) });
            else
                return Json(new { redirectUrl = Url.Action("ViewOrders", "Order", new { sortStatus = false }) });
        }


        public ActionResult ViewOrders(bool sortStatus)
        {
            List<Order> orders;
            if (sortStatus)
                orders = uow.RepositoryOrder.Sort();
            else
                orders = uow.RepositoryOrder.GetAll();
            return View("Orders", orders);
        }

        public ActionResult UpdateOrder()
        {
            byte[] orderByte = HttpContext.Session.Get("orderStatusChanged");
            if (orderByte is null)
                return ViewOrders(false);
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(orderByte);
            orders.ForEach(o =>
            {
                Order order = uow.RepositoryOrder.FindWithoutInclude(or => or.OrderId == o.OrderId);
                order.OrderStatus = o.OrderStatus;
                uow.Commit();
            });
            HttpContext.Session.Remove("orderStatusChanged");
            return ViewOrders(false);
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

        
    }
}
