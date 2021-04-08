using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions
{
    /// <summary>
    /// Represent CustomerNullException class
    /// </summary>
    /// <remarks>
    /// Throws when object Customer is null
    /// </remarks>
    public class CustomerNullException : Exception
    {
        public CustomerNullException(string message) : base(message)
        {

        }
    }
}
