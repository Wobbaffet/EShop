using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Fillters
{
    public class LoggedInFillter :ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            
            base.OnActionExecuted(context);
        }
    }
}
