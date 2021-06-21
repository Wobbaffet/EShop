using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Exceptions
{
    public class PasswordException:Exception
    {
        public PasswordException(string message) : base(message) { }
    }
}
