using System.Security.Cryptography;
using System.Text;

namespace DevQAProdCom.NET.Global.Extensions.StringExtensions
{
    public static class SecurityStringExtensions
    {
        public static string ToSha512HashedBase64EncodedString(this string @string)
        {
            if (string.IsNullOrEmpty(@string))
                return @string;

            byte[] data = Encoding.ASCII.GetBytes(@string);
            byte[] hash = SHA512.Create().ComputeHash(data);

            return Convert.ToBase64String(hash);
        }
    }
}
