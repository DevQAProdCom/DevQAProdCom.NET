namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Test
{
    public class TestExecutionLoggingModel : TestExecutionBaseLoggingModel
    {
        public int? TestRetryAttempt { get; set; }
        public int? TestRepeatAttempt { get; set; }
    }
}
