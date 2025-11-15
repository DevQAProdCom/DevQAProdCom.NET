using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Run;

namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Suite
{
    public class TestSuiteLoggingModel : TestRunBaseLoggingModel
    {
        public string? TestSuiteId { get; set; }
        public string TestSuiteName { get; set; }
    }
}
