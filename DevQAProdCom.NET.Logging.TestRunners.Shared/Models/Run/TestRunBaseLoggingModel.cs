using DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces;

namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Run
{
    public class TestRunBaseLoggingModel : IHasTestRunExecutionFlowStageInfo
    {
        public string? LogEntryOrderNumber { get; set; }

        public string TestRunId { get; set; }
        public string? TestRunName { get; set; }
        public string? VersionUnderTest { get; set; }

        public string? TestRunExecutionFlowStage { get; set; }

        public string? Message { get; set; }
        public Dictionary<string, object>? MessageParametersValues { get; set; }
    }
}
