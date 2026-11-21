using DevQAProdCom.NET.AI.GitHubCopilot.Handlers;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers.GitHubCopilot
{
    public class GitHubCopilotSessionShutdownEventAiContentHandler(ILogger logger, decimal priceInUsdPerPremiumRequest, decimal priceInUsdPerAICredit)
        : GitHubCopilotSessionEventAiContentHandler(new SessionShutdownEventHandler(logger, priceInUsdPerPremiumRequest, priceInUsdPerAICredit), logger)
    {
    }
}
