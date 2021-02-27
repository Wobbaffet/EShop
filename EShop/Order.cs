using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
    }
}
