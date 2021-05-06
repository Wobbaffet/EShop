using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EShop.Model.Validation
{
    
    ///<inheritdoc/>
    /// <summary>
    /// Represent Unique class
    /// </summary>
    /// <remarks>Represent validation atribute for Email</remarks>
    public class Unique : ValidationAttribute
    {
       
        ShopContext context = new ShopContext();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"> Represent email that needs to be checked</param>
        /// <returns> 
        /// <list type="bullet">
        /// <item>
        /// <term>True if email is the same as passed email</term>
        /// </item>
        /// <item>
        /// <term>False if email is not the same as passed email</term>
        /// </item>
        /// </list>
        /// </returns>
        public override bool IsValid(object value)
        {


            Customer c = context.Customer.ToList().Find(c => c.Email == value.ToString() && c.Status);
            return c is null;

        }

    }
}
