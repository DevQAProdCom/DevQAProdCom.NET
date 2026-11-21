namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class AiAgentTokenConsumptionMetricsModel
    {
        public string AgentName { get; set; } = string.Empty;
        public string AgentDisplayName { get; set; } = string.Empty;
        public string AgentIdentifier { get; set; } = string.Empty;
        public List<AiModelTokenConsumptionMetricsModel> ModelMetrics { get; set; } = new();
    }
}
