﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Domain
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

    }
}
