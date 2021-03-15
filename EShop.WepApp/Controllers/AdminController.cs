using EShop.Data.UnitOfWork;
using EShop.WepApp.APIHelpers;
using EShop.WepApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            
            return View("Index", model);
        }
    }
}
