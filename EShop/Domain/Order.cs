using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    /// <summary>
    /// Represent Order class
    /// </summary>
    public class Order
    {
        /// <value>Represent order id as int</value>
        public int OrderId { get; set; }
        /// <value>Represent purchased date as DateTime class</value>

        public DateTime Date { get; set; }
        /// <value>Represent sum of all book price in order </value>

        public double Total { get; set; }
        /// <value>Represent order status as enum OrderStatus </value>
        public OrderStatus OrderStatus { get; set; }

        /// <value>Represent list of <c>OrderItem</c> </value>
        public List<OrderItem> OrderItems { get; set; }
        /// <value>Represent reference to <c>Customer</c></value>
        public Customer Customer { get; set; }
       
    }
}
