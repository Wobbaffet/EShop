using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    public abstract class Customer
    {
        public int CustomerId { get; set; }
        public string Password { get; set; }
      
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
/*        public int AddressId { get; set; }
*/        public Address Address { get; set; }
        public List<Order> Orders { get; set; }
        public long VerificationCode{ get; set; }
        public bool Status { get; set; }
        public bool IsAdmin { get; set; }
    }
}
