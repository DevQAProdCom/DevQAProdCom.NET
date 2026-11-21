using System.Globalization;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using SessionEvent = DevQAProdCom.NET.AI.GitHubCopilot.Enumerations.SessionEvent;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Handlers
{
    public class AssistantUsageEventHandler : SessionEventHandler
    {
        private const string MainAgentId = "main";
        private const string UnknownSessionId = "unknown";
        private const double NanoAiuPerAiCredit = 1_000_000_000d;
        private const string ApiCallTokenConsumptionTemplate = "💲📊 [SessionId={SessionId}] [{AgentName}/{AgentId}] TOKEN CONSUMPTION (Model={Model}): 💵 Total={TotalTokens}, 🪙 Input={InputTokens}, 🪙 Output={OutputTokens}, 🪙 CacheRead={CacheReadTokens}, 🪙 CacheWrite={CacheWriteTokens}, 🪙 Reasoning={ReasoningTokens}, 💰 AiCredits={AiCredits}";
        private const string AgentTotalTokenConsumptionTemplate = "💲📊 [SessionId={SessionId}] [{AgentName}/{AgentId}] TOTAL TOKEN CONSUMPTION: 💵 Total={TotalTokens}, 🪙 Input={InputTokens}, 🪙 Output={OutputTokens}, 🪙 CacheRead={CacheReadTokens}, 🪙 CacheWrite={CacheWriteTokens}, 🪙 Reasoning={ReasoningTokens}, 💰 AiCredits={AiCredits}";
        private const string AgentTotalTokensOnlyTemplate = "💲📊 [SessionId={SessionId}] [{AgentName}/{AgentId}] TOTAL TOKEN CONSUMPTION: 💵 Total={TotalTokens}, 💰 AiCredits={AiCredits}";
        private const string SessionTotalTokenConsumptionTemplate = "💲📊 [SessionId={SessionId}] [session] TOTAL TOKEN CONSUMPTION: 💵 Total={TotalTokens}, 🪙 Input={InputTokens}, 🪙 Output={OutputTokens}, 🪙 CacheRead={CacheReadTokens}, 🪙 CacheWrite={CacheWriteTokens}, 🪙 Reasoning={ReasoningTokens}, 💰 AiCredits={AiCredits}";
        private const string SessionTotalTokensOnlyTemplate = "💲📊 [SessionId={SessionId}] [session] TOTAL TOKEN CONSUMPTION: 💵 Total={TotalTokens}, 💰 AiCredits={AiCredits}";

        private readonly Dictionary<string, string> _subagentNames = new(StringComparer.Ordinal);
        private readonly Dictionary<string, GitHubCopilotAgentTokenConsumptionModel> _agentTokenConsumption = new(StringComparer.Ordinal);
        private string _sessionId = UnknownSessionId;
        private bool _hasChangesSinceFinally;

        public AssistantUsageEventHandler(ILogger logger) : base(logger) { }

        public override void HandleEvent(string @event, IAiInteractionDataBank interactionDataBank)
        {
            var sessionStartEvent = GetEvent<SessionStartEvent>(@event, SessionEvent.SessionStart.GetDescriptionAttributeValue());

            if (!string.IsNullOrEmpty(sessionStartEvent?.Data.SessionId))
                _sessionId = sessionStartEvent.Data.SessionId;

            var subagentStartedEvent = GetEvent<SubagentStartedEvent>(@event, SessionEvent.SubagentStarted.GetDescriptionAttributeValue());

            if (subagentStartedEvent != null)
            {
                RegisterSubagent(
                    subagentStartedEvent.AgentId,
                    subagentStartedEvent.Data.ToolCallId,
                    subagentStartedEvent.Data.AgentName,
                    subagentStartedEvent.Data.AgentDisplayName);
            }

            var assistantUsageEvent = GetEvent<AssistantUsageEvent>(@event, SessionEvent.AssistantUsage.GetDescriptionAttributeValue());

            if (assistantUsageEvent != null)
                HandleAssistantUsage(assistantUsageEvent);

            var subagentCompletedEvent = GetEvent<SubagentCompletedEvent>(@event, SessionEvent.SubagentCompleted.GetDescriptionAttributeValue());

            if (subagentCompletedEvent != null)
            {
                HandleSubagentTotal(
                    subagentCompletedEvent.AgentId,
                    subagentCompletedEvent.Data.ToolCallId,
                    subagentCompletedEvent.Data.AgentName,
                    subagentCompletedEvent.Data.AgentDisplayName,
                    subagentCompletedEvent.Data.TotalTokens);
            }

            var subagentFailedEvent = GetEvent<SubagentFailedEvent>(@event, SessionEvent.SubagentFailed.GetDescriptionAttributeValue());

            if (subagentFailedEvent != null)
            {
                HandleSubagentTotal(
                    subagentFailedEvent.AgentId,
                    subagentFailedEvent.Data.ToolCallId,
                    subagentFailedEvent.Data.AgentName,
                    subagentFailedEvent.Data.AgentDisplayName,
                    subagentFailedEvent.Data.TotalTokens);
            }
        }

        public override void Finally()
        {
            if (!_hasChangesSinceFinally || _agentTokenConsumption.Count == 0)
                return;

            foreach (var agent in _agentTokenConsumption)
            {
                var consumption = agent.Value;
                var totalTokens = GetTotalTokens(consumption);

                if (consumption.HasAssistantUsage)
                {
                    Logger.Info(AgentTotalTokenConsumptionTemplate, _sessionId, consumption.AgentName, agent.Key, totalTokens, consumption.InputTokens, consumption.OutputTokens, consumption.CacheReadTokens, consumption.CacheWriteTokens, consumption.ReasoningTokens, FormatAiCredits(consumption.TotalNanoAiu));
                }
                else
                {
                    Logger.Info(AgentTotalTokensOnlyTemplate, _sessionId, consumption.AgentName, agent.Key, totalTokens, FormatAiCredits(consumption.TotalNanoAiu));
                }
            }

            var allConsumption = _agentTokenConsumption.Values.ToList();
            var sessionTotalTokens = allConsumption.Sum(GetTotalTokens);
            var sessionTotalNanoAiu = allConsumption.All(c => c.TotalNanoAiu.HasValue) ? allConsumption.Sum(c => c.TotalNanoAiu!.Value) : (double?)null;

            if (allConsumption.All(c => c.HasAssistantUsage))
            {
                Logger.Info(SessionTotalTokenConsumptionTemplate, _sessionId, sessionTotalTokens, allConsumption.Sum(c => c.InputTokens), allConsumption.Sum(c => c.OutputTokens), allConsumption.Sum(c => c.CacheReadTokens), allConsumption.Sum(c => c.CacheWriteTokens), allConsumption.Sum(c => c.ReasoningTokens), FormatAiCredits(sessionTotalNanoAiu));
            }
            else
            {
                Logger.Info(SessionTotalTokensOnlyTemplate, _sessionId, sessionTotalTokens, FormatAiCredits(sessionTotalNanoAiu));
            }

            _hasChangesSinceFinally = false;
        }

        private void HandleAssistantUsage(AssistantUsageEvent assistantUsageEvent)
        {
            var usage = assistantUsageEvent.Data;
            var agentId = assistantUsageEvent.AgentId ?? usage.ParentToolCallId ?? MainAgentId;
            var consumption = GetGitHubCopilotAgentTokenConsumptionModel(agentId);

            var inputTokens = usage.InputTokens ?? 0;
            var outputTokens = usage.OutputTokens ?? 0;
            var cacheReadTokens = usage.CacheReadTokens ?? 0;
            var cacheWriteTokens = usage.CacheWriteTokens ?? 0;
            var reasoningTokens = usage.ReasoningTokens ?? 0;
            var totalNanoAiu = usage.CopilotUsage?.TotalNanoAiu;

            consumption.HasAssistantUsage = true;
            consumption.InputTokens += inputTokens;
            consumption.OutputTokens += outputTokens;
            consumption.CacheReadTokens += cacheReadTokens;
            consumption.CacheWriteTokens += cacheWriteTokens;
            consumption.ReasoningTokens += reasoningTokens;

            if (totalNanoAiu.HasValue)
                consumption.TotalNanoAiu = (consumption.TotalNanoAiu ?? 0) + totalNanoAiu.Value;

            _hasChangesSinceFinally = true;

            Logger.Info(ApiCallTokenConsumptionTemplate, _sessionId, consumption.AgentName, agentId, usage.Model ?? "unknown", inputTokens + outputTokens, inputTokens, outputTokens, cacheReadTokens, cacheWriteTokens, reasoningTokens, FormatAiCredits(totalNanoAiu));
        }

        private void HandleSubagentTotal(string? agentId, string? toolCallId, string? agentName, string? agentDisplayName, long? totalTokens)
        {
            RegisterSubagent(agentId, toolCallId, agentName, agentDisplayName);

            if (!totalTokens.HasValue)
                return;

            var id = agentId ?? toolCallId ?? "unknown";
            var consumption = GetGitHubCopilotAgentTokenConsumptionModel(id);
            consumption.TotalTokens = totalTokens.Value;
            _hasChangesSinceFinally = true;

            Logger.Info(AgentTotalTokensOnlyTemplate, _sessionId, consumption.AgentName, id, totalTokens.Value, FormatAiCredits(consumption.TotalNanoAiu));
        }

        private GitHubCopilotAgentTokenConsumptionModel GetGitHubCopilotAgentTokenConsumptionModel(string agentId)
        {
            if (!_agentTokenConsumption.TryGetValue(agentId, out var consumption))
            {
                consumption = new GitHubCopilotAgentTokenConsumptionModel
                {
                    AgentName = GetAgentName(agentId)
                };
                _agentTokenConsumption.Add(agentId, consumption);
            }
            else
            {
                consumption.AgentName = GetAgentName(agentId);
            }

            return consumption;
        }

        private string GetAgentName(string agentId)
        {
            if (_subagentNames.TryGetValue(agentId, out var agentName))
                return agentName;

            return agentId == MainAgentId ? "primary" : "unknown";
        }

        private void RegisterSubagent(string? agentId, string? toolCallId, string? agentName, string? agentDisplayName)
        {
            var name = agentName ?? agentDisplayName ?? "unknown";
            RegisterSubagentIdentifier(agentId, name);
            RegisterSubagentIdentifier(toolCallId, name);
        }

        private void RegisterSubagentIdentifier(string? identifier, string agentName)
        {
            if (string.IsNullOrEmpty(identifier))
                return;

            _subagentNames[identifier] = agentName;

            if (_agentTokenConsumption.TryGetValue(identifier, out var consumption))
                consumption.AgentName = agentName;
        }

        private static long GetTotalTokens(GitHubCopilotAgentTokenConsumptionModel consumption)
        {
            return consumption.TotalTokens ?? consumption.InputTokens + consumption.OutputTokens;
        }

        private static string FormatAiCredits(double? totalNanoAiu)
        {
            return totalNanoAiu.HasValue ? (totalNanoAiu.Value / NanoAiuPerAiCredit).ToString("0.#########", CultureInfo.InvariantCulture) : "unknown";
        }
    }
}
