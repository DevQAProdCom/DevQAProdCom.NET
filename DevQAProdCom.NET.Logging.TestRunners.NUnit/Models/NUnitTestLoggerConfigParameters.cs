using System.Text.Json.Serialization;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Enumerations;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces;

namespace DevQAProdCom.NET.Logging.TestRunners.NUnit.Models
{
    public class NUnitTestLoggerConfigParameters : ITestLoggerConfigParameters
    {
        [JsonPropertyName("testRunId")]
        public string TestRunId { get; set; }

        [JsonPropertyName("testRunName")]
        public string? TestRunName { get; set; }

        [JsonPropertyName("testRunDescription")]
        public string? TestRunDescription { get; set; }

        [JsonPropertyName("versionUnderTest")]
        public string? VersionUnderTest { get; set; }

        [JsonPropertyName("writeMode")]
        public List<string> WriteMode { get; set; } = new List<string>() { TestRunnerLoggingWriteMode.SingleLogFilePerRun.ToString() };
    }
}
