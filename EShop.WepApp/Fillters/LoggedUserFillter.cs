using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Fillters
{
    public class LoggedUserFillter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;
            if (context.HttpContext.Session.GetInt32("customerId") != null)
                controller.ViewBag.IsLogged = true;
            else
                controller.ViewBag.IsLogged = false;
        }
    }
}
