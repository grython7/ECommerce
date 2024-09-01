using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Exceptions
{
    public class WeakPasswordException : Exception
    {
        public WeakPasswordException() : base("The password entered is weak") 
        {
        }
        public WeakPasswordException(string message) : base(message)
        {
        }
    }
}
