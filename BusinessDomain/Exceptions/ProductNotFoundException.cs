using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base($"Product not found")
        { 
        }
        public ProductNotFoundException(Guid id) : base($"Product with Id {id} not found")
        {
        }
    }
}
