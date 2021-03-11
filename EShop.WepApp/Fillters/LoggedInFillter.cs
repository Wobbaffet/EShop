using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Fillters
{
    public class LoggedInFillter :ActionFilterAttribute
    {
       /* public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32("customerId") != null)
            {
                context.HttpContext.Response.Redirect("/book/index");
             *//*   Controller controller = context.Controller as Controller;
                controller.ViewBag.Logged = true;*//*
            }

        }*/
      /*  public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Session.GetInt32("customerId") != null)
            {
              context.HttpContext.Response.Redirect("/book/index");
               
            }

            
        }*/


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32("customerId") != null)
            {
                context.HttpContext.Response.Redirect("/book/index");
                context.Result = new EmptyResult();
            }
        }
    }
}
