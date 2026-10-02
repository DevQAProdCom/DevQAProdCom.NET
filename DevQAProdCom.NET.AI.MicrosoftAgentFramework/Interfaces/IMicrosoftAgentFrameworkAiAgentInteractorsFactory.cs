using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces
{
    public interface IMicrosoftAgentFrameworkAiAgentInteractorsFactory
    {
        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor();

        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(
            IHooksSearcher hookSearcher,
            ILocationsProvider? hooksDefaultLocationsProvider = null);

        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(
            IHooksCollection allHooksCollection,
            IHooksCollection sessionHooksCollection);
    }
}
