using EShop.Model.Validation;
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
<<<<<<< HEAD
        public Address Address { get; set; }
=======
/*        public int AddressId { get; set; }
*/      public Address Address { get; set; }
>>>>>>> 0da3f5ee81b3c826ef4a9f5d21877eb2f69d1f07
        public List<Order> Orders { get; set; }
        public long VerificationCode{ get; set; }
        public bool Status { get; set; }
        public bool IsAdmin { get; set; }
    }
}
