using System.Text.Json.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotSessionShutdownAgentMetricModel
    {
        [JsonPropertyName("agentName")]
        public string? AgentName { get; set; }

        [JsonPropertyName("agentDisplayName")]
        public string? AgentDisplayName { get; set; }

        [JsonPropertyName("modelMetrics")]
        public Dictionary<string, GitHubCopilotSessionShutdownModelMetricModel> ModelMetrics { get; set; } = new();
    }
}
