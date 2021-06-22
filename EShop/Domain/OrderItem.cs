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

        private int quantity;

        /// <value>Represent quantity of book</value>
        /// <exception cref="ArgumentOutOfRangeException"
        public int Quantity
        {
            get { return quantity; }
            set {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Quantity must be positiv number");

                quantity = value; 
            }
        }


        private Book book;

        /// <value>Reference to <c>Book</c> class</value>
        /// <see cref="Book.class"/>
        /// <exception cref="NullReferenceException"
        public Book Book
        {
            get { return book; }
            set {
                if (value is null)
                    throw new NullReferenceException("Book cannot be null");
                book = value; 
            
            }
        }


    }
}
