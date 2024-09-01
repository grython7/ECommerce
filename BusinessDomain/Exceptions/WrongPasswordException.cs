using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Exceptions
{
    public class WrongEmailException : Exception
    {
        public WrongEmailException() : base("Email Doesn't Exist")
        {
        }
        public WrongEmailException(string message) : base(message)
        {
        }
    }
}
