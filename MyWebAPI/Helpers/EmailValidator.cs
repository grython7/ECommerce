using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public static class EmailValidator
    {
        public static bool IsEmail(this String email){
            // remove leading/trailing white-space characters
            var trimmedEmail = email.Trim();
            if (trimmedEmail.IsNullOrEmpty())
                return false;
            // EmailAttribute is not good enough to validate an email
            string reg_pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(reg_pattern);
            return regex.IsMatch(trimmedEmail);
        }
    }
}
