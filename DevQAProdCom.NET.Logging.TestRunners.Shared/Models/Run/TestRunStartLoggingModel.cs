namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Run
{
    public class TestRunStartLoggingModel : TestRunBaseLoggingModel
    {
        public string? Description { get; set; }
        public DateTime? StartTime { get; set; }
        public int? TotalAmountOfTests { get; set; }
        public int? TestsWithExecuteState { get; set; }
        public int? TestsWithSkipState { get; set; }
        public Dictionary<string, object>? AdditionalInfo { get; set; }
    }
}
