using System.Security.Cryptography;
using System.Text;

namespace backend.Helpers
{
    public class HashHelper
    {
        public static string GenerateSalt(int size = 32)
        {
            byte[] saltBytes = new byte[size];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string GenerateMD5Hash(string input, string salt)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input + salt);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyMD5Hash(string input, string salt, string storedHash)
        {
            string inputHash = GenerateMD5Hash(input, salt);
            return inputHash == storedHash;
        }
    }
}
