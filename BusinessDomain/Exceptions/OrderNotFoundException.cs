using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() : base("InValid Order Id")
        {
        }
        public OrderNotFoundException(string message) : base(message)
        {
        }
    }
}
