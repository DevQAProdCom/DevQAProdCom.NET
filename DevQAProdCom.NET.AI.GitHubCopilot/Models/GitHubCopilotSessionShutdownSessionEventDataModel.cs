using System.Text.Json.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotSessionShutdownSessionEventDataModel
    {
        [JsonPropertyName("totalPremiumRequests")]
        public double? TotalPremiumRequests { get; set; }

        [JsonPropertyName("totalNanoAiu")]
        public double? TotalNanoAiu { get; set; }

        [JsonPropertyName("sessionStartTime")]
        public long SessionStartTime { get; set; }

        [JsonPropertyName("modelMetrics")]
        public Dictionary<string, GitHubCopilotSessionShutdownModelMetricModel> ModelMetrics { get; set; } = new();

        [JsonPropertyName("agentMetrics")]
        public Dictionary<string, GitHubCopilotSessionShutdownAgentMetricModel>? AgentMetrics { get; set; }
    }
}
