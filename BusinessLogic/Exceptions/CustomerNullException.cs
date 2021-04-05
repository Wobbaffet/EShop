using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions
{
    public class CustomerNullException : Exception
    {
        public CustomerNullException(string message) : base(message)
        {

        }
    }
}
