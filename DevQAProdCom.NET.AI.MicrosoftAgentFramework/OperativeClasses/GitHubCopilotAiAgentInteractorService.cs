using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers.GitHubCopilot;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses
{
    public class GitHubCopilotAiAgentInteractorService : MicrosoftAiAgentInteractorService<GitHubCopilotAiAgentInteractorService>
    {
        public GitHubCopilotAiAgentInteractorService(ILogger logger, bool addDefaultContentHanlders = true) : base(logger)
        {
            if (addDefaultContentHanlders)
            {
                AddDefaultContentHandlers();
            }
        }

        public GitHubCopilotAiAgentInteractorService(AIAgent agent, ILogger logger, AgentRunOptions? agentRunOptions = null, List<IAiContentHandler>? aiContentHandlers = null, bool addDefaultContentHanlders = true) : base(agent, logger, agentRunOptions, aiContentHandlers)
        {
            if (addDefaultContentHanlders)
            {
                AddDefaultContentHandlers();
            }
        }

        private void AddDefaultContentHandlers()
        {
            var handler = new GitHubCopilotAssistantMessageEventAiContentHandler(Logger);
            WithAiContentHandlers(handler);
        }
    }
}
