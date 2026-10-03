using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Factories
{
    public class MicrosoftAgentFrameworkAiAgentInteractorsFactory(IServiceProvider serviceProvider) : IMicrosoftAgentFrameworkAiAgentInteractorsFactory
    {
        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(
            IFileBasedHooksSearcher? hookSearcher = null,
            ILocationsProvider? hooksDefaultLocationsProvider = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>? allSdkBasedSessionHooksCollection = null)
        {
            var logger = serviceProvider.GetRequiredService<ILogger>();

            var gitHubCopilotClientService = serviceProvider.GetRequiredService<IGitHubCopilotClientService>();
            var microsoftAiAgentInteractor = serviceProvider.GetRequiredService<IMicrosoftAiAgentInteractor>();
            hookSearcher ??= new GitHubCopilotFileBasedHooksSearcher(logger);

            return new GitHubCopilotAiAgentInteractor(
                gitHubCopilotClientService,
                microsoftAiAgentInteractor,
                logger,
                hookSearcher,
                hooksDefaultLocationsProvider,
                allSdkBasedSessionHooksCollection: allSdkBasedSessionHooksCollection);
        }

        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(
            IFileBasedHooksCollection allHooksCollection,
            IFileBasedHooksCollection sessionHooksCollection,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook> allSdkBasedSessionHooksCollection,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook> sessionSdkBasedSessionHooksCollection)
        {
            var gitHubCopilotClientService = serviceProvider.GetRequiredService<IGitHubCopilotClientService>();
            var microsoftAiAgentInteractor = serviceProvider.GetRequiredService<IMicrosoftAiAgentInteractor>();
            var logger = serviceProvider.GetRequiredService<ILogger>();

            return new GitHubCopilotAiAgentInteractor(
                gitHubCopilotClientService,
                microsoftAiAgentInteractor,
                logger,
                allHooksCollection,
                sessionHooksCollection,
                allSdkBasedSessionHooksCollection,
                sessionSdkBasedSessionHooksCollection);
        }
    }
}
