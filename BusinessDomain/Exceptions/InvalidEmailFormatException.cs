using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Exceptions
{
    public class InvalidEmailFormatException : Exception
    {
        public InvalidEmailFormatException() : base("The email entered is invalid") 
        {
        }
        public InvalidEmailFormatException(string message) : base(message)
        {
        }
    }
}
