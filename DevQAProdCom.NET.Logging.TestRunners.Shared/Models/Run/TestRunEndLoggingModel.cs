namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Run
{
    public class TestRunEndLoggingModel : TestRunBaseLoggingModel
    {
        public DateTime? EndTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Total { get; set; }
        public int? Passed { get; set; }
        public int? Failed { get; set; }
        public int? Skipped { get; set; }
        public Dictionary<string, string>? OtherResults { get; set; }
    }
}
