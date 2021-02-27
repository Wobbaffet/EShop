using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model
{
    public abstract class Customer
    {
        public int CustomerId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
