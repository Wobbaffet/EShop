using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.Models
{
    public class UpdateCustomerViewModel
    {

        public int CustomerId { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string CityName { get; set; }
        public int PTT { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public int TIN { get; set; }

        public CustomerType Type { get; set; }
    }
}
