namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Test
{
    public class TestStartLoggingModel : TestExecutionLoggingModel
    {
        public DateTime? StartTime { get; set; }
        public Dictionary<string, object>? TestArguments { get; set; }
    }
}
