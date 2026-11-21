using System.Text.Json.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotSessionShutdownRequestsMetricModel
    {
        [JsonPropertyName("count")]
        public long? Count { get; set; }

        [JsonPropertyName("cost")]
        public double? Cost { get; set; }
    }
}
