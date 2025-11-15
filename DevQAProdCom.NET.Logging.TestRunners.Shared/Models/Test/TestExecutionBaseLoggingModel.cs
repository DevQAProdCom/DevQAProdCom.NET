using DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Suite;

namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Test
{
    public class TestExecutionBaseLoggingModel : TestSuiteLoggingModel
    {
        public string? TestId { get; set; }
        public string TestMethodName { get; set; }
    }
}
