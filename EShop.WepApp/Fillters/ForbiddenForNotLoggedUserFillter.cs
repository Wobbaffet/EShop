using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Fillters
{
    public class ForbiddenForNotLoggedUserFillter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32("customerId") == null && context.HttpContext.Session.GetInt32("adminId") == null)
            {
                context.HttpContext.Response.Redirect("/Home/Index");
                context.Result = new EmptyResult();
            }
        }
    }
}
