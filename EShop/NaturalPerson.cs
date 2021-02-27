using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model
{
    public class NaturalPerson : Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
