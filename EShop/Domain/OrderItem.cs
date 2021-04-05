using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Model.Domain
{
    /// <summary>
    /// Represent OrderItem class
    /// </summary>
    public class OrderItem
    {
        /// <value>Represent order item id as int</value>
        public int OrderItemId { get; set; }
        /// <value>Represent quantity of book</value>
        public int Quantity { get; set; }
        /// <value>Reference to <c>Book</c> class</value>
        /// <see cref="Book.class"/>
        public Book Book { get; set; }
      

    }
}
