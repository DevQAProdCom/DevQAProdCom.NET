using System.Text.Json.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotSessionShutdownModelMetricModel
    {
        [JsonPropertyName("requests")]
        public GitHubCopilotSessionShutdownRequestsMetricModel Requests { get; set; } = new();

        [JsonPropertyName("usage")]
        public GitHubCopilotSessionShutdownUsageMetricModel Usage { get; set; } = new();

        [JsonPropertyName("totalNanoAiu")]
        public double? TotalNanoAiu { get; set; }
    }
}
