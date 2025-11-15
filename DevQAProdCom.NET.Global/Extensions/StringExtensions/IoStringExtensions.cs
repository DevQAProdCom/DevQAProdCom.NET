using System.Text;

namespace DevQAProdCom.NET.Global.Extensions.StringExtensions
{
    public static class IoStringExtensions
    {
        public static Stream ToStream(this string @string)
        {
            if (string.IsNullOrEmpty(@string))
                throw new ArgumentException(nameof(@string));

            byte[] byteArray = Encoding.UTF8.GetBytes(@string);
            return new MemoryStream(byteArray);
        }
    }
}
