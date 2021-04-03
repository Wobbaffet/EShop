using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EShop.Model.Validation
{
    public class Unique : ValidationAttribute
    {
        ShopContext context = new ShopContext();
        public override bool IsValid(object value)
        {


            Customer c = context.Customer.ToList().Find(c => c.Email == value.ToString() && c.Status);
            return c is null;

        }

    }
}
