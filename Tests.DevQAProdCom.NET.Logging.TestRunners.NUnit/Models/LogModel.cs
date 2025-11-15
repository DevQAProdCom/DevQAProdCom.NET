using System.Text.Json.Serialization;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Models
{
    public class LogModel
    {
        [JsonPropertyName("LogRecordId")]
        public string LogRecordId { get; set; }

        [JsonPropertyName("@l")]
        public string Level { get; set; }

        [JsonPropertyName("@t")]
        public string TimeStamp { get; set; }

        [JsonPropertyName("@mt")]
        public string MessageTemplate { get; set; }

        [JsonPropertyName("TestRunId")]
        public string TestRunId { get; set; }

        [JsonPropertyName("TestRunName")]
        public string TestRunName { get; set; }

        [JsonPropertyName("TestRunDescription")]
        public string TestRunDescription { get; set; }

        [JsonPropertyName("VersionUnderTest")]
        public string VersionUnderTest { get; set; }

        [JsonPropertyName("TestSuiteName")]
        public string TestSuiteName { get; set; }

        [JsonPropertyName("TestSuiteId")]
        public string TestSuiteId { get; set; }

        [JsonPropertyName("TestId")]
        public string TestId { get; set; }

        [JsonPropertyName("TestMethodName")]
        public string TestMethodName { get; set; }

        [JsonPropertyName("TestArguments")]
        public Dictionary<string, object> TestArguments { get; set; } = new();

        [JsonPropertyName("TestRunExecutionFlowStage")]
        public string TestRunExecutionFlowStage { get; set; }

        [JsonPropertyName("LogEntryOrderNumber")]
        public string LogEntryOrderNumber { get; set; }

        [JsonPropertyName("ThreadId")]
        public int? ThreadId { get; set; }

        [JsonPropertyName("StartTime")]
        public string StartTime { get; set; }

        [JsonPropertyName("Result")]
        public string Result { get; set; }

        [JsonPropertyName("EndTime")]
        public string EndTime { get; set; }

        [JsonPropertyName("Duration")]
        public string Duration { get; set; }

        [JsonPropertyName("Total")]
        public string Total { get; set; }

        [JsonPropertyName("Passed")]
        public string Passed { get; set; }

        [JsonPropertyName("Failed")]
        public string Failed { get; set; }

        [JsonPropertyName("Skipped")]
        public string Skipped { get; set; }

        [JsonPropertyName("OtherResults")]
        public Dictionary<string, string>? OtherResults { get; set; } = new();

        [JsonPropertyName("FailureMessages")]
        public List<string>? FailureMessages { get; set; }

        [JsonPropertyName("StackTrace")]
        public string StackTrace { get; set; }

        [JsonPropertyName("AdditionalInfo")]
        public Dictionary<string, object>? AdditionalInfo { get; set; }

        [JsonPropertyName("HookClassId")]
        public string? HookClassId { get; set; }

        [JsonPropertyName("HookClassName")]
        public string? HookClassName { get; set; }
    }
}
