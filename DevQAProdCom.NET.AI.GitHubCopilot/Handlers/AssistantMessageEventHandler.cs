using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Handlers
{
    public class AssistantMessageEventHandler : SessionEventHandler
    {
        private readonly List<string> _reasoningTextHistory = new();
        public AssistantMessageEventHandler(ILogger logger) : base(logger) { }

        public override void HandleEvent(string @event, IAiInteractionDataBank interactionDataBank)
        {
            var assistantMessageEvent = GetEvent<AssistantMessageEvent>(@event);

            if (assistantMessageEvent != null)
            {
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
                        Logger.Info($"🤖🧠 AI REASONINGS:\n {reasoningText}");
                    }
                }

                if (!string.IsNullOrEmpty(assistantMessageEvent.Data.Content))
                {
                    interactionDataBank.Append($"{id}:{nameof(assistantMessageEvent.Data.Content)}", assistantMessageEvent.Data.Content);
                    Logger.Info($"🤖📝 AI TEXT CONTENT:\n {assistantMessageEvent.Data.Content}");
                }
            }
        }
    }
}
