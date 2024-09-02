using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException() : base("Insufficient stock") 
        {
        }
        public InsufficientStockException(string message) : base(message)
        {
        }
    }
}
