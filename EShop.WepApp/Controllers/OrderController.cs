using BusinessLogic.Classes;
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
        private OrderService service;

        public OrderController(IUnitOfWork uow)
        {
            this.uow = uow;
            service = new OrderService();
        }
        [ForbiddenForAdminFillter]
        [PurchaseFillter]

        #region AddedBusinessLogic
        public ActionResult GetAllOrders()
        {
            int? id = (int)HttpContext.Session.GetInt32("customerId");

            return View(service.GetAll(id));
        }
        public ActionResult ShowOrderItems(int orderId)
        {

            
            return View("OrderItems",service.GetOrderItems(orderId));
        }

        public ActionResult UpdateOrder()
        {
            byte[] orderByte = HttpContext.Session.Get("orderStatusChanged");
            if (orderByte is null)
                return ViewOrders(false);
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(orderByte);

            service.UpdateOrders(orders);

            HttpContext.Session.Remove("orderStatusChanged");
            return ViewOrders(false);


        }

        
        public ActionResult ViewOrders(bool sortStatus)
        {

            var orders = service.SortOrders(sortStatus);

            return View("Orders", orders);
        }

        #endregion

   
        public ActionResult Sort(string condition)
        {
            if (condition == "Status")
                return Json(new { redirectUrl = Url.Action("ViewOrders", "Order", new { sortStatus = true }) });
            else
                return Json(new { redirectUrl = Url.Action("ViewOrders", "Order", new { sortStatus = false }) });
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
