namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotAgentTokenConsumptionModel
    {
        public required string AgentName { get; set; }
        public bool HasAssistantUsage { get; set; }
        public long InputTokens { get; set; }
        public long OutputTokens { get; set; }
        public long CacheReadTokens { get; set; }
        public long CacheWriteTokens { get; set; }
        public long ReasoningTokens { get; set; }
        public long? TotalTokens { get; set; }
        public double? TotalNanoAiu { get; set; }
    }
}
