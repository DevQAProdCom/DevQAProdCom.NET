namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class AiModelTokenConsumptionMetricsModel
    {
        public string ModelName { get; set; } = string.Empty;
        public double TotalPremiumRequests { get; set; }
        public double TotalPremiumRequestsCost { get; set; }
        public decimal TotalPremiumRequestsCostInUsd { get; set; }
        public double TotalNanoAiu { get; set; }
        public decimal TotalNanoAiuInUsd { get; set; }
        public long InputTokens { get; set; }
        public long OutputTokens { get; set; }
        public long CacheReadTokens { get; set; }
        public long CacheWriteTokens { get; set; }
        public long ReasoningTokens { get; set; }
    }
}
