using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces
{
    public interface IMicrosoftAgentFrameworkAiAgentInteractorsFactory
    {
        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(
            IFileBasedHooksSearcher? hookSearcher = null,
            ILocationsProvider? hooksDefaultLocationsProvider = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>? allSdkBasedSessionHooksCollection = null);

        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(
            IFileBasedHooksCollection allHooksCollection,
            IFileBasedHooksCollection sessionHooksCollection,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook> allSdkBasedSessionHooksCollection,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook> sessionSdkBasedSessionHooksCollection);
    }
}
