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


        private double total;

        /// <value>Represent sum of all book price in order </value>
        public double Total
        {
            get { return total; }
            set {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Total cannot be less or eqaul zero");
                total = value; 
            }
        }

        /// <value>Represent order status as enum OrderStatus </value>
        public OrderStatus OrderStatus { get; set; }


        private List<OrderItem> orderItems;

        /// <value>Represent list of <c>OrderItem</c> </value>
        public List<OrderItem> OrderItems
        {
            get { return orderItems; }
            set
            {
                if (value is null)
                    throw new NullReferenceException("Order item cannot be null");
                orderItems= value;
            }
        }


        private Customer customer;

        /// <value>Represent reference to <c>Customer</c></value>
        public Customer Customer
        {
            get { return customer; }
            set {
                if (value is null)
                    throw new NullReferenceException("Customer cannot be null");
                customer = value;
            }
        }


    }
}
