using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Model.Domain
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        [NotMapped]
        public Book Book { get; set; }
        public int BookId { get; set; }

    }
}
