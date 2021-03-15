using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public int CustomerId { get; set; }
    }
}
