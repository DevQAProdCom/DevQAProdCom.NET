using System.Text.Json.Serialization;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotSessionShutdownUsageMetricModel
    {
        [JsonPropertyName("inputTokens")]
        public long? InputTokens { get; set; }

        [JsonPropertyName("outputTokens")]
        public long? OutputTokens { get; set; }

        [JsonPropertyName("cacheReadTokens")]
        public long? CacheReadTokens { get; set; }

        [JsonPropertyName("cacheWriteTokens")]
        public long? CacheWriteTokens { get; set; }

        [JsonPropertyName("reasoningTokens")]
        public long? ReasoningTokens { get; set; }
    }
}
