using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    public class Address
    {
        //public int AddressId { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string CityName { get; set; }
        public int PTT { get; set; }
        /*public Customer Customer { get; set; }*/
    }
}
