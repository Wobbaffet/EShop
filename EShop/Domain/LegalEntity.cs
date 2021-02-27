using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    public class LegalEntity : Customer
    {
        public int TIN { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNumber { get; set; }
        public override string ToString()
        {
            return $"{CompanyName}";
        }
    }
}
