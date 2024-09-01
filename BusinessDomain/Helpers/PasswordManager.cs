using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Helpers
{
    public class PasswordManager
    {
        public static string GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }

        public static string HashPassword(string password, string salt)
        {
            using (var hasher = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] saltedPassword = new byte[passwordBytes.Length + saltBytes.Length];

                // Concatenate the password and the salt
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, saltedPassword, passwordBytes.Length, saltBytes.Length);

                // Hash the concatenated password and salt
                byte[] hashedBytes = hasher.ComputeHash(saltedPassword);

                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
