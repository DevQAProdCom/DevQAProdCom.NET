using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses;
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
            var hookSearcher = new GitHubCopilotHooksSearcher(logger);
            var hooksDefaultLocationsProvider = new GitHubCopilotHooksDefaultLocationsProvider(useExtendedSearch: true);

            return new GitHubCopilotAiAgentInteractor(
                gitHubCopilotClientService,
                microsoftAiAgentInteractor,
                logger,
                hookSearcher,
                hooksDefaultLocationsProvider: hooksDefaultLocationsProvider);
        }

        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(
            IHooksSearcher hookSearcher,
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
            IHooksCollection allHooksCollection,
            IHooksCollection sessionHooksCollection)
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
