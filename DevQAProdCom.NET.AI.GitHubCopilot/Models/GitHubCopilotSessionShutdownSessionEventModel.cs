using System.Text.Json.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotSessionShutdownSessionEventModel
    {
        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("data")]
        public GitHubCopilotSessionShutdownSessionEventDataModel Data { get; set; } = new();
    }
}
