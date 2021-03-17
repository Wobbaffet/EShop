using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Fillters
{
    public class AddToCartFillter: ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;
            int? cartItems = context.HttpContext.Session.GetInt32("cartItems");
            if(cartItems is null)
            {
                controller.ViewBag.CartItems =0;
            }
            else
            controller.ViewBag.CartItems = cartItems+1;

        }
    }
}
