using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public static class PasswordValidator
    {
        public static bool IsStrong(this String password)
        {
            if (password.Length < 8)
            {
                return false;
            }

            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasDigit = false;
            bool hasSpecialChar = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUpperCase = true;
                if (char.IsLower(c))
                    hasLowerCase = true;
                if (char.IsDigit(c))
                    hasDigit = true;
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                    hasSpecialChar = true;
            }

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

    }
}
