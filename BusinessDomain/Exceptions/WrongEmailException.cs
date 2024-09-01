using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException() : base("Password Is Wrong")
        {
        }
        public WrongPasswordException(string message) : base(message)
        {
        }
    }
}
