using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base($"InValid Customer Id")
        { 
        }
        public UserNotFoundException(String message) : base(message)
        {
        }
    }
}
