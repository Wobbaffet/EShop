using EShop.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Controllers
{
    public class CustomerController : Controller
    {
        private IUnitOfWork uow;
        public CustomerController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public ActionResult SignUp()
        {
            return View("SignUp");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
