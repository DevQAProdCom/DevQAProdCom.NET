namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Suite
{
    public class TestSuiteEndLoggingModel : TestSuiteLoggingModel
    {
        public DateTime? EndTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Total { get; set; }
        public int? Passed { get; set; }
        public int? Failed { get; set; }
        public int? Skipped { get; set; }
        public Dictionary<string, string>? OtherResults { get; set; }
        public List<string>? FailureMessages { get; set; }
        public List<string>? StackTrace { get; set; }
        public List<string> Categories { get; set; }
    }
}
