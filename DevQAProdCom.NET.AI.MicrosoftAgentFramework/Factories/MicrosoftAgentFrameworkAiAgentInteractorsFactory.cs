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
        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor()
        {
            var gitHubCopilotClientService = serviceProvider.GetRequiredService<IGitHubCopilotClientService>();
            var microsoftAiAgentInteractor = serviceProvider.GetRequiredService<IMicrosoftAiAgentInteractor>();
            var logger = serviceProvider.GetRequiredService<ILogger>();
            var hookSearcher = new GitHubCopilotFileBasedHooksSearcher(logger);
            var hooksDefaultLocationsProvider = new GitHubCopilotFileBasedHooksDefaultLocationsProvider(useExtendedSearch: true);

            return new GitHubCopilotAiAgentInteractor(
                gitHubCopilotClientService,
                microsoftAiAgentInteractor,
                logger,
                hookSearcher,
                hooksDefaultLocationsProvider: hooksDefaultLocationsProvider);
        }

        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(
            IFileBasedHooksSearcher hookSearcher,
            ILocationsProvider? hooksDefaultLocationsProvider = null)
        {
            var gitHubCopilotClientService = serviceProvider.GetRequiredService<IGitHubCopilotClientService>();
            var microsoftAiAgentInteractor = serviceProvider.GetRequiredService<IMicrosoftAiAgentInteractor>();
            var logger = serviceProvider.GetRequiredService<ILogger>();

            return new GitHubCopilotAiAgentInteractor(
                gitHubCopilotClientService,
                microsoftAiAgentInteractor,
                logger,
                hookSearcher,
                hooksDefaultLocationsProvider);
        }

        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(
            IFileBasedHooksCollection allHooksCollection,
            IFileBasedHooksCollection sessionHooksCollection)
        {
            var gitHubCopilotClientService = serviceProvider.GetRequiredService<IGitHubCopilotClientService>();
            var microsoftAiAgentInteractor = serviceProvider.GetRequiredService<IMicrosoftAiAgentInteractor>();
            var logger = serviceProvider.GetRequiredService<ILogger>();

            return new GitHubCopilotAiAgentInteractor(
                gitHubCopilotClientService,
                microsoftAiAgentInteractor,
                logger,
                allHooksCollection,
                sessionHooksCollection);
        }
    }
}
