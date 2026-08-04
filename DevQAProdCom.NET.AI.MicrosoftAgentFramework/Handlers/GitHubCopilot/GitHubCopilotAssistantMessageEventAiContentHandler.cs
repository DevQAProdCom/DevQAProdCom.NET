using DevQAProdCom.NET.AI.GitHubCopilot.Handlers;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers.GitHubCopilot
{
    public class GitHubCopilotAssistantMessageEventAiContentHandler(ILogger logger) : GitHubCopilotSessionEventAiContentHandler(new AssistantMessageEventHandler(logger), logger)
    {
    }
}
