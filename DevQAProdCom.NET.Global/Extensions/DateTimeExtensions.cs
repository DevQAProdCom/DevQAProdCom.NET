namespace DevQAProdCom.NET.Global.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToMySqlDbDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static DateTime ToMySqlDbDateTime(this DateTime dateTime)
        {
            return DateTime.Parse(dateTime.ToMySqlDbDateTimeString());
        }

        public static string ToMySqlDbDateTimeWith3FractionalPointsString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fff");
        }

        public static DateTime ToMySqlDbDateTimeWith3FractionalPoints(this DateTime dateTime)
        {
            return DateTime.Parse(dateTime.ToMySqlDbDateTimeWith3FractionalPointsString());
        }

        public static string ToMySqlDbDateTimeWith6FractionalPointsString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.ffffff");
        }

        public static DateTime ToMySqlDbDateTimeWith6FractionalPoints(this DateTime dateTime)
        {
            return DateTime.Parse(dateTime.ToMySqlDbDateTimeWith6FractionalPointsString());
        }

        public static string ToFileNameSupportedFormatWithMicroseconds(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddThh-mm-ss-ffffff");
        }

        public static string ToIso8601FormatWithMicroseconds(this DateTime dateTime)
        {
            return dateTime.ToString("O");
        }

        /// <summary>
        /// Converts a DateTime to a Unix timestamp in seconds (including fractional part for milliseconds).
        /// </summary>
        /// <param name="dateTime">The DateTime to convert.</param>
        /// <returns>The Unix timestamp as a float, or null if the input is null.</returns>
        public static float? ToUnixTimeSeconds(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;

            return (float)new DateTimeOffset(dateTime.Value).ToUnixTimeSeconds();
        }

        public static DateTime ConvertToUtc(this DateTime? dateTime)
        {
            if (dateTime == null)
                return default;

            return dateTime.Value.ConvertToUtc();
        }

        public static DateTime ConvertToUtc(this DateTime dateTime)
        {
            // If the DateTime is already UTC, return it as is
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime;
            }

            // If the DateTime is Local, convert it to UTC
            if (dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            // If the DateTime is Unspecified, assume it's local and convert to UTC
            // Alternatively, you could assume a specific time zone (e.g., TimeZoneInfo)
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local).ToUniversalTime();
        }
    }
}
