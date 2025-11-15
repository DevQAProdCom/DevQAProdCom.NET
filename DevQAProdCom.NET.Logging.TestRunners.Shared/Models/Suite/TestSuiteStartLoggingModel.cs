namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Suite
{
    public class TestSuiteStartLoggingModel : TestSuiteLoggingModel
    {
        public DateTime? StartTime { get; set; }
        public int? TestSuiteExecutionOrder { get; set; }
    }
}
