using System;
using System.Security.Cryptography;
using System.Text;

namespace Passport.Business.Internal
{
    public class Sha512CryptoRepository : ICryptoRepository
    {
        public string Hash(string input)
        {
            using (var sha512 = new SHA512Managed())
            {
                var buffer = Encoding.UTF8.GetBytes(input);
                var hashedBuffer = sha512.ComputeHash(buffer);
                return Convert.ToBase64String(hashedBuffer);
            }
        }
    }
}