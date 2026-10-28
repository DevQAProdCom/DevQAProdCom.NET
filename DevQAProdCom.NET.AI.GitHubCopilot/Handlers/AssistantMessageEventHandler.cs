using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using SessionEvent = DevQAProdCom.NET.AI.GitHubCopilot.Enumerations.SessionEvent;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Handlers
{
    public class AssistantMessageEventHandler : SessionEventHandler
    {
        private readonly List<string> _reasoningTextHistory = new();
        private readonly Dictionary<string, SubagentStartedEvent> _subagentStartedEvents = new();

        public AssistantMessageEventHandler(ILogger logger) : base(logger) { }

        public override void HandleEvent(string @event, IAiInteractionDataBank interactionDataBank)
        {
            var subagentStartedEvent = GetEvent<SubagentStartedEvent>(@event, SessionEvent.SubagentStarted.GetDescriptionAttributeValue());

            if (subagentStartedEvent != null)
            {
                var key = subagentStartedEvent.AgentId ?? subagentStartedEvent.Data.ToolCallId;

                if (string.IsNullOrEmpty(key))
                {
                    throw new InvalidOperationException("SubagentStartedEvent has no AgentId or ToolCallId.");
                }

                if (_subagentStartedEvents.ContainsKey(key))
                {
                    throw new InvalidOperationException($"SubagentStartedEvent with id '{key}' was already added.");
                }

                _subagentStartedEvents.Add(key, subagentStartedEvent);
            }

            var assistantMessageEvent = GetEvent<AssistantMessageEvent>(@event, SessionEvent.AssistantMessage.GetDescriptionAttributeValue());

            if (assistantMessageEvent != null)
            {
                var relatedSubagent = GetRelatedSubagent(assistantMessageEvent);
                var id = $"{nameof(AssistantMessageEvent)}:{assistantMessageEvent.Id}";

                if (!string.IsNullOrEmpty(assistantMessageEvent.Data.ReasoningText))
                {
                    var reasoningText = assistantMessageEvent.Data.ReasoningText;

                    for (int i = _reasoningTextHistory.Count() - 1; i >= 0; i--)
                    {
                        var toReplaceText = _reasoningTextHistory[i];
                        reasoningText = reasoningText.Replace(toReplaceText, string.Empty).Trim();
                    }

                    if (!string.IsNullOrEmpty(reasoningText))
                    {
                        _reasoningTextHistory.Add(reasoningText);
                        interactionDataBank.Append($"{id}:{nameof(assistantMessageEvent.Data.ReasoningText)}", reasoningText);

                        if (relatedSubagent != null)
                        {
                            var agentName = relatedSubagent.Data.AgentName ?? relatedSubagent.Data.AgentDisplayName ?? "unknown";
                            var agentId = relatedSubagent.AgentId ?? relatedSubagent.Data.ToolCallId ?? "unknown";
                            Logger.Info($"🤖 [{agentName}/{agentId}] 🧠 AI REASONINGS:\n {reasoningText}");
                        }
                        else
                        {
                            Logger.Info($"🤖🧠 AI REASONINGS:\n {reasoningText}");
                        }
                    }
                }

                if (!string.IsNullOrEmpty(assistantMessageEvent.Data.Content))
                {
                    interactionDataBank.Append($"{id}:{nameof(assistantMessageEvent.Data.Content)}", assistantMessageEvent.Data.Content);

                    if (relatedSubagent != null)
                    {
                        var agentName = relatedSubagent.Data.AgentName ?? relatedSubagent.Data.AgentDisplayName ?? "unknown";
                        var agentId = relatedSubagent.AgentId ?? relatedSubagent.Data.ToolCallId ?? "unknown";
                        Logger.Info($"🤖 [{agentName}/{agentId}] 📝 AI TEXT CONTENT:\n {assistantMessageEvent.Data.Content}");
                    }
                    else
                    {
                        Logger.Info($"🤖📝 AI TEXT CONTENT:\n {assistantMessageEvent.Data.Content}");
                    }
                }
            }
        }

        private SubagentStartedEvent? GetRelatedSubagent(AssistantMessageEvent assistantMessageEvent)
        {
            var toolCallIds = assistantMessageEvent.Data.ToolRequests?
                .Where(t => !string.IsNullOrEmpty(t.ToolCallId))
                .Select(t => t.ToolCallId!)
                .ToHashSet(StringComparer.Ordinal) ?? new HashSet<string>();

            var related = _subagentStartedEvents.Values
                .Where(s => !string.IsNullOrEmpty(s.Data.ToolCallId) && toolCallIds.Contains(s.Data.ToolCallId))
                .ToList();

            if (related.Count > 1)
            {
                throw new InvalidOperationException($"Expected at most one related subagent for assistant message, but found {related.Count}.");
            }

            return related.FirstOrDefault();
        }
    }
}
