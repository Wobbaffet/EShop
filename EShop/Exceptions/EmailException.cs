using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Exceptions
{
    public class EmailException:Exception
    {

      public EmailException(string message): base(message) { }
    }
}
