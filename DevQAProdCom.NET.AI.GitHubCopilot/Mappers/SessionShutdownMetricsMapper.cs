using DevQAProdCom.NET.AI.GitHubCopilot.Models;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Mappers
{
    public class SessionShutdownMetricsMapper
    {
        private const string MainAgentId = "main";
        private const double NanoAiuPerAiCredit = 1_000_000_000d;
        private readonly decimal _priceInUsdPerPremiumRequest;
        private readonly decimal _priceInUsdPerAICredit;

        public SessionShutdownMetricsMapper(decimal priceInUsdPerPremiumRequest, decimal priceInUsdPerAICredit)
        {
            _priceInUsdPerPremiumRequest = priceInUsdPerPremiumRequest;
            _priceInUsdPerAICredit = priceInUsdPerAICredit;
        }

        public GitHubCopilotSessionShutdownMetricsModel Map(GitHubCopilotSessionShutdownSessionEventModel sessionShutdownEvent, string? mainAgentName = null, string? mainAgentDisplayName = null)
        {
            ArgumentNullException.ThrowIfNull(sessionShutdownEvent);

            var shutdown = sessionShutdownEvent.Data;
            var totalPremiumRequests = shutdown.TotalPremiumRequests ?? 0d;
            var totalNanoAiu = shutdown.TotalNanoAiu ?? 0d;
            var sessionStartTime = DateTimeOffset.FromUnixTimeMilliseconds(shutdown.SessionStartTime).UtcDateTime;
            var sessionEndTime = sessionShutdownEvent.Timestamp.UtcDateTime;

            return new GitHubCopilotSessionShutdownMetricsModel
            {
                TotalPremiumRequests = totalPremiumRequests,
                TotalPremiumRequestsCostInUsd = (decimal)totalPremiumRequests * _priceInUsdPerPremiumRequest,
                TotalNanoAiu = totalNanoAiu,
                TotalNanoAiuInUsd = ConvertNanoAiuToUsd(totalNanoAiu),
                SessionStartTime = sessionStartTime,
                SessionEndTime = sessionEndTime,
                SessionDuration = sessionEndTime - sessionStartTime,
                ModelMetrics = shutdown.ModelMetrics.Select(model => MapModel(model.Key, model.Value)).ToList(),
                AgentMetrics = shutdown.AgentMetrics?.Select(agent =>
                {
                    var isMainAgent = agent.Key == MainAgentId;
                    var agentName = isMainAgent
                        ? FirstNonEmpty(mainAgentName, mainAgentDisplayName, agent.Value.AgentName, agent.Value.AgentDisplayName, "primary")
                        : FirstNonEmpty(agent.Value.AgentName, agent.Value.AgentDisplayName, "unknown");
                    var agentDisplayName = isMainAgent
                        ? FirstNonEmpty(mainAgentDisplayName, mainAgentName, agent.Value.AgentDisplayName, agent.Value.AgentName, agentName)
                        : FirstNonEmpty(agent.Value.AgentDisplayName, agent.Value.AgentName, agentName);

                    return new AiAgentTokenConsumptionMetricsModel
                    {
                        AgentName = agentName,
                        AgentDisplayName = agentDisplayName,
                        AgentIdentifier = agent.Key,
                        ModelMetrics = agent.Value.ModelMetrics.Select(model => MapModel(model.Key, model.Value)).ToList()
                    };
                }).ToList() ?? new List<AiAgentTokenConsumptionMetricsModel>()
            };
        }

        private AiModelTokenConsumptionMetricsModel MapModel(string modelName, GitHubCopilotSessionShutdownModelMetricModel modelMetric)
        {
            var totalPremiumRequestsCost = modelMetric.Requests.Cost ?? 0d;
            var totalNanoAiu = modelMetric.TotalNanoAiu ?? 0d;
            return new AiModelTokenConsumptionMetricsModel
            {
                ModelName = modelName,
                TotalPremiumRequests = modelMetric.Requests.Count ?? 0,
                TotalPremiumRequestsCost = totalPremiumRequestsCost,
                TotalPremiumRequestsCostInUsd = (decimal)totalPremiumRequestsCost * _priceInUsdPerPremiumRequest,
                TotalNanoAiu = totalNanoAiu,
                TotalNanoAiuInUsd = ConvertNanoAiuToUsd(totalNanoAiu),
                InputTokens = modelMetric.Usage.InputTokens ?? 0,
                OutputTokens = modelMetric.Usage.OutputTokens ?? 0,
                CacheReadTokens = modelMetric.Usage.CacheReadTokens ?? 0,
                CacheWriteTokens = modelMetric.Usage.CacheWriteTokens ?? 0,
                ReasoningTokens = modelMetric.Usage.ReasoningTokens ?? 0
            };
        }

        private decimal ConvertNanoAiuToUsd(double totalNanoAiu)
        {
            return (decimal)(totalNanoAiu / NanoAiuPerAiCredit) * _priceInUsdPerAICredit;
        }

        private static string FirstNonEmpty(params string?[] values)
        {
            return values.First(value => !string.IsNullOrWhiteSpace(value))!;
        }
    }
}
