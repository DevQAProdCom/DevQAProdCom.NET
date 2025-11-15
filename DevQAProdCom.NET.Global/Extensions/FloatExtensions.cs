namespace DevQAProdCom.NET.Global.Extensions
{
    public static class FloatExtensions
    {
        public static int ToInt32(this float number)
        {
            return Convert.ToInt32(number);
        }

        public static DateTime ToDateTimeFromUnixTimeSeconds(this float unixTimeSeconds)
        {
            return DateTimeOffset.FromUnixTimeSeconds((long)unixTimeSeconds).UtcDateTime;
        }
    }
}
