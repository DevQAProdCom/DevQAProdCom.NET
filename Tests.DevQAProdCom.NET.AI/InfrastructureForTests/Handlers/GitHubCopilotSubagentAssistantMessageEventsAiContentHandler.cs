using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers.GitHubCopilot;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Handlers
{
    public class GitHubCopilotSubagentAssistantMessageEventsAiContentHandler(SubagentAssistantMessageEventsCollectorHandler collectorHandler, ILogger logger) : GitHubCopilotSessionEventAiContentHandler(collectorHandler, logger)
    {
    }
}
