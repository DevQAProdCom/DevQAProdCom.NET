using Attribute = DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Auxiliary.Attribute;

namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Test
{
    public class TestExecutionEndLoggingModel : TestExecutionLoggingModel
    {
        public string? Result { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public List<string>? FailureMessages { get; set; }
        public List<string>? StackTrace { get; set; }
        //public List<Attribute> Attributes { get; set; }
        public List<string> Categories { get; set; }
    }
}
