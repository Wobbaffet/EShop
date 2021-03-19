using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Fillters
{
    public class AdminAddedBookFillter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;
            int? newBooks = context.HttpContext.Session.GetInt32("numberOfSelectedBooks");
            if (newBooks is null)
                controller.ViewBag.ChosenBooks = 0;
            else
                controller.ViewBag.ChosenBooks = newBooks;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Controller controller = context.Controller as Controller;
            int? newBooks = context.HttpContext.Session.GetInt32("numberOfSelectedBooks");
            if (newBooks is null)
                controller.ViewBag.ChosenBooks = 0;
            else
                controller.ViewBag.ChosenBooks = newBooks;
        }
    }
}
