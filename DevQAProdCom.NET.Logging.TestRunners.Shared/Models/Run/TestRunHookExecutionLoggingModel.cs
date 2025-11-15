namespace DevQAProdCom.NET.Logging.TestRunners.Shared.Models.Run
{
    public class TestRunHookExecutionLoggingModel : TestRunBaseLoggingModel
    {
        public string? HookClassId { get; set; }
        public string? HookClassName { get; set; }

        public string? HookId { get; set; }
        public string? HookMethodName { get; set; }
    }
}
