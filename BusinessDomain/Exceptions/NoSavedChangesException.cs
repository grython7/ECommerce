using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Exceptions
{
    public class NoSavedChangesException : Exception
    {
        public NoSavedChangesException() : base("No changes were saved to the database.")
        {
        }

        public NoSavedChangesException(string message) : base(message)
        {
        }
    }
}
