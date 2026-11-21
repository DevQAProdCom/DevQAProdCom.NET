using DevQAProdCom.NET.AI.GitHubCopilot.Mappers;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using SessionEvent = DevQAProdCom.NET.AI.GitHubCopilot.Enumerations.SessionEvent;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Handlers
{
    public class SessionShutdownEventHandler : SessionEventHandler
    {
        private readonly SessionShutdownMetricsMapper _mapper;
        private string? _mainAgentName;
        private string? _mainAgentDisplayName;

        public SessionShutdownEventHandler(ILogger logger, decimal priceInUsdPerPremiumRequest, decimal priceInUsdPerAICredit) : base(logger)
        {
            _mapper = new SessionShutdownMetricsMapper(priceInUsdPerPremiumRequest, priceInUsdPerAICredit);
        }

        public override void HandleEvent(string @event, IAiInteractionDataBank interactionDataBank)
        {
            var subagentSelectedEvent = GetEvent<SubagentSelectedEvent>(@event, SessionEvent.SubagentSelected.GetDescriptionAttributeValue());

            if (subagentSelectedEvent != null)
            {
                _mainAgentName = subagentSelectedEvent.Data.AgentName;
                _mainAgentDisplayName = subagentSelectedEvent.Data.AgentDisplayName;
            }

            var sessionShutdownEvent = GetEvent<GitHubCopilotSessionShutdownSessionEventModel>(@event, SessionEvent.SessionShutdown.GetDescriptionAttributeValue());

            if (sessionShutdownEvent == null)
                return;

            var metrics = _mapper.Map(sessionShutdownEvent, _mainAgentName, _mainAgentDisplayName);
            Logger.Info("💲📊 [SessionShutdownEvent]" + Environment.NewLine + "{SessionShutdownMetrics}", metrics.ToString());
        }
    }
}
