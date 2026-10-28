using DevQAProdCom.NET.AI.GitHubCopilot.Handlers;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using SessionEvent = DevQAProdCom.NET.AI.GitHubCopilot.Enumerations.SessionEvent;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Handlers
{
    public class SubagentAssistantMessageEventsCollectorHandler : SessionEventHandler
    {
        private readonly List<SubagentStartedEvent> _subagentStartedEvents = new();
        private readonly List<AssistantMessageEvent> _assistantMessageEvents = new();

        public IReadOnlyList<SubagentStartedEvent> SubagentStartedEvents => _subagentStartedEvents;
        public IReadOnlyList<AssistantMessageEvent> AssistantMessageEvents => _assistantMessageEvents;

        public SubagentAssistantMessageEventsCollectorHandler(ILogger logger) : base(logger) { }

        public override void HandleEvent(string @event, IAiInteractionDataBank interactionDataBank)
        {
            var subagentStartedEvent = GetEvent<SubagentStartedEvent>(@event, SessionEvent.SubagentStarted.GetDescriptionAttributeValue());

            if (subagentStartedEvent != null)
            {
                _subagentStartedEvents.Add(subagentStartedEvent);
            }

            var assistantMessageEvent = GetEvent<AssistantMessageEvent>(@event, SessionEvent.AssistantMessage.GetDescriptionAttributeValue());

            if (assistantMessageEvent != null)
            {
                _assistantMessageEvents.Add(assistantMessageEvent);
            }
        }
    }
}
