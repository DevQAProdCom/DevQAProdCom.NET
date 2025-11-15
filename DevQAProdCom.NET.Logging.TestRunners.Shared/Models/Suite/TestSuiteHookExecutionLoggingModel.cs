namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Suite
{
    public class TestSuiteHookExecutionLoggingModel : TestSuiteLoggingModel
    {
        public string? HookClassId { get; set; }
        public string? HookClassName { get; set; }
        public string? HookId { get; set; }
        public string? HookMethodName { get; set; }
    }
}
