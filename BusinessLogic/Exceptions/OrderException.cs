using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Exceptions
{
public class OrderException:Exception
    {
        public OrderException() : base() { }
        public OrderException(string message) : base(message) { }

    }
}
