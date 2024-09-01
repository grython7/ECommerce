using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException() : base("The email address is already registered")
        {
        }
        public EmailAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
