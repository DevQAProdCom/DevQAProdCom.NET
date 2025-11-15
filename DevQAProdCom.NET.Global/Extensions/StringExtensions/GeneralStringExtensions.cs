namespace DevQAProdCom.NET.Global.Extensions.StringExtensions
{
    public static class GeneralStringExtensions
    {
        public static IEnumerable<string> ToEnumerable(this string @string, string separator)
        {
            return @string.Split(separator).Select(x => x.Trim());
        }

        public static string[] ToArray(this string @string, string separator)
        {
            return ToEnumerable(@string, separator).ToArray();
        }

        public static string ToStringEmptyIfNull(this string @string)
        {
            if (string.IsNullOrEmpty(@string)) return string.Empty;
            return @string;
        }

        public static bool IsEquivalentTo(this string? @string1, string? @string2)
        {
            return string.Compare(@string1, @string2, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        /// <summary>
        /// Converts a string to an enum value of the specified type.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The string value to convert.</param>
        /// <param name="ignoreCase">Indicates whether the comparison should ignore case.</param>
        /// <returns>The enum value corresponding to the string.</returns>
        /// <exception cref="ArgumentException">Thrown if the string cannot be converted to the specified enum.</exception>
        public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = true) where TEnum : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
            }

            if (Enum.TryParse(value, ignoreCase, out TEnum result))
            {
                return result;
            }

            throw new ArgumentException($"'{value}' is not a valid value for the enum '{typeof(TEnum).Name}'.");
        }

        public static string Repeat(this string @string, int amount)
        {
            string result = string.Empty;
            for (int i = 0; i < amount; i++)
                result += @string;

            return result;
        }

        public static string ToCamelCase(this string @string)
        {
            if (string.IsNullOrEmpty(@string) || !char.IsLetter(@string[0]))
                return @string;

            if (@string.Length == 1)
                return char.ToLowerInvariant(@string[0]).ToString();

            return char.ToLowerInvariant(@string[0]) + @string.Substring(1);
        }
    }
}
