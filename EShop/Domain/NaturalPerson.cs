using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    /// <inheritdoc/>
    /// <summary>
    /// Represent Natural Person class 
    /// <remarks>
    /// Contains properties <c>FirstName</c> and <c>LastName</c>
    /// </remarks>
    /// </summary>
    public class NaturalPerson : Customer
    {
        /// <value>Represent customer first name as string</value>
        
        public string FirstName { get; set; }
        /// <value>Represent customer last name as string</value>
        public string LastName { get; set; }
        /// <summary>
        /// Override string method
        /// </summary>
        /// <returns>string in format <c>FirstName</c>  <c>LastName</c></returns>
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
