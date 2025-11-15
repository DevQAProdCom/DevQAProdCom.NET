using System.Globalization;
using System.Text.RegularExpressions;

namespace DevQAProdCom.NET.TestRunners.NUnit.Models.Types
{
    /// <summary>
    /// Is introduced due to NUnit specific DateTime format and errors that occur during XML deserialization of such kind of format. Requires manual deserialization method.
    /// </summary>
    public class NUnitXmlDateTime
    {
        public DateTime DateTime { get; set; }
        private const string Format = "yyyy-MM-dd HH:mm:ssZ";
        private Regex Regex = new Regex(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}Z$");

        public NUnitXmlDateTime(string dateTimeString)
        {
            if (!string.IsNullOrEmpty(dateTimeString))
            {
                if (Regex.IsMatch(dateTimeString)) // todo try catch
                    DateTime = DateTime.ParseExact(dateTimeString, Format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                else
                    DateTime = DateTime.Parse(dateTimeString);
            }
        }

        public static implicit operator DateTime(NUnitXmlDateTime f)
        {
            return f.DateTime;
        }
    }
}
