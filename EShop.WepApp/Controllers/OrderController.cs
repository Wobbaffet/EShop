using EShop.Data.UnitOfWork;
using EShop.Model.Domain;
using EShop.WepApp.Fillters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Controllers
{
    [LoggedUserFillter]
    [ForbiddenForAdminFillter]
    [AddToCartFillter]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork uow;

        public OrderController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        // GET: OrderController
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

      
    }
}
