using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Factories
{
    public class MicrosoftAgentFrameworkAiAgentInteractorsFactory(IServiceProvider serviceProvider) : IMicrosoftAgentFrameworkAiAgentInteractorsFactory
    {
        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(
            IFileBasedMcpServersSearcher? mcpServersSearcher = null,
            ILocationsProvider? mcpServersDefaultLocationsProvider = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>? allSdkBasedMcpServersCollection = null,

            IFileBasedHooksSearcher? hookSearcher = null,
            ILocationsProvider? hooksDefaultLocationsProvider = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>? allSdkBasedSessionHooksCollection = null)
        {
            var logger = serviceProvider.GetRequiredService<ILogger>();

            var gitHubCopilotClientService = serviceProvider.GetRequiredService<IGitHubCopilotClientService>();
            var microsoftAiAgentInteractor = serviceProvider.GetRequiredService<IMicrosoftAiAgentInteractor>();

            hookSearcher ??= new GitHubCopilotFileBasedHooksSearcher(logger);
            mcpServersSearcher ??= new GitHubCopilotFileBasedMcpServersSearcher(logger);

            return new GitHubCopilotAiAgentInteractor(
                gitHubCopilotClientService,
                microsoftAiAgentInteractor,
                logger,

                mcpServersSearcher: mcpServersSearcher,
                mcpServersDefaultLocationsProvider: mcpServersDefaultLocationsProvider,
                allSdkBasedMcpServersCollection: allSdkBasedMcpServersCollection,

                hooksSearcher: hookSearcher,
                hooksDefaultLocationsProvider: hooksDefaultLocationsProvider,
                allSdkBasedSessionHooksCollection: allSdkBasedSessionHooksCollection);
        }
    }
}
