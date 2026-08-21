using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers.GitHubCopilot;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses
{
    public class GitHubCopilotAiAgentInteractorService : MicrosoftAiAgentInteractorService<GitHubCopilotAiAgentInteractorService>
    {
        public GitHubCopilotAiAgentInteractorService(ILogger logger) : base(logger) { }

        public GitHubCopilotAiAgentInteractorService(AIAgent agent, ILogger logger, AgentRunOptions? agentRunOptions = null, List<IAiContentHandler>? aiContentHandlers = null) : base(logger)
        {
            var handler = new GitHubCopilotAssistantMessageEventAiContentHandler(logger);
            WithAiContentHandlers(handler);
        }
    }
}
