using System.Text.RegularExpressions;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Constants
{
    internal class Const
    {
        public static string DirectoryWithLogs = Path.Combine("..", "..", "..", "..", $"{SharedTestConstants.ProjectNameWithPreconditionTests}", "bin", "Debug", "net8.0", $"{SharedTestConstants.LogsFolder}");
        public static string ExpectedFileNameRegex = @"log\d{4}\d{2}\d{2}";
        public static Regex ExpectedJsonFileRegex = new Regex(@$"^{ExpectedFileNameRegex}.json$");
        public static Regex ExpectedTxtFileRegex = new Regex(@$"^{ExpectedFileNameRegex}.txt$");
    }
}
