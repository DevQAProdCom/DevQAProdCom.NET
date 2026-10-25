using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Factories
{
    public class MicrosoftAgentFrameworkAiAgentInteractorsFactory(IServiceProvider serviceProvider) : IMicrosoftAgentFrameworkAiAgentInteractorsFactory
    {
        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(

            // Agents 
            ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>>? agentsSearcher = null,
            ILocationsProvider? agentsDefaultLocationsProvider = null,
            IAiAgentsConfigurationsMappers<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>? agentsConfigurationsMappers = null,
            ISearchEntitiesInLocations<IAiAgentConfiguration>? agentsConfigurationsSearcher = null,
            ILocationsProvider? agentsDefaultConfigurationsLocationsProvider = null,
            IAiAgentsCollectionWithConfigurations<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>? allAgentsCollection = null,
            IAiAgentsCollectionWithConfigurations<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>? sessionAgentsCollection = null,

            // Instructions
            ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiInstructionConfiguration>>? instructionsSearcher = null,
            ILocationsProvider? instructionsDefaultLocationsProvider = null,
            IAiEntitiesCollection<IGitHubCopilotAiInstructionConfiguration>? allInstructionsCollection = null,
            IAiEntitiesCollection<IGitHubCopilotAiInstructionConfiguration>? sessionInstructionsCollection = null,

            // Skills
            ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiSkillConfiguration>>? skillsSearcher = null,
            ILocationsProvider? skillsDefaultLocationsProvider = null,
            IAiEntitiesCollection<IGitHubCopilotAiSkillConfiguration>? allSkillsCollection = null,
            IAiEntitiesCollection<IGitHubCopilotAiSkillConfiguration>? sessionSkillsCollection = null,

            // Hooks
            IFileBasedHooksSearcher? fileBasedHooksSearcher = null,
            ILocationsProvider? fileBasedHooksDefaultLocationsProvider = null,
            IFileBasedHooksCollection? allFileBasedHooksCollection = null,
            IFileBasedHooksCollection? sessionFileBasedHooksCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>? allSdkBasedSessionHooksCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>? sessionSdkBasedSessionHooksCollection = null,

            // MCP Servers
            IFileBasedMcpServersSearcher? fileBasedMcpServersSearcher = null,
            ILocationsProvider? fileBasedMcpServersDefaultLocationsProvider = null,
            IFileBasedMcpServersCollection? allFileBasedMcpServersCollection = null,
            IFileBasedMcpServersCollection? sessionFileBasedMcpServersCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>? allSdkBasedMcpServersCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>? sessionSdkBasedMcpServersCollection = null,
            
            // Other
            IGitHubCopilotMappers? gitHubCopilotMappers = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotPermissionDecision>? allPermissionDecisionsCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotPermissionDecision>? sessionPermissionDecisionsCollection = null,
            CopilotClientMode copilotClientMode = CopilotClientMode.Empty)
        {
            var logger = serviceProvider.GetRequiredService<ILogger>();

            var gitHubCopilotClientService = serviceProvider.GetRequiredService<IGitHubCopilotClientService>();
            var microsoftAiAgentInteractor = serviceProvider.GetRequiredService<IMicrosoftAiAgentInteractor>();

            fileBasedHooksSearcher ??= new GitHubCopilotFileBasedHooksSearcher(logger);
            fileBasedMcpServersSearcher ??= new GitHubCopilotFileBasedMcpServersSearcher(logger);

            return new GitHubCopilotAiAgentInteractor(
                gitHubCopilotClientService,
                microsoftAiAgentInteractor,
                logger,

                agentsSearcher: agentsSearcher,
                agentsDefaultLocationsProvider: agentsDefaultLocationsProvider,
                agentsConfigurationsMappers: agentsConfigurationsMappers,
                agentsConfigurationsSearcher: agentsConfigurationsSearcher,
                agentsDefaultConfigurationsLocationsProvider: agentsDefaultConfigurationsLocationsProvider,
                allAgentsCollection: allAgentsCollection,
                sessionAgentsCollection: sessionAgentsCollection,

                instructionsSearcher: instructionsSearcher,
                instructionsDefaultLocationsProvider: instructionsDefaultLocationsProvider,
                allInstructionsCollection: allInstructionsCollection,
                sessionInstructionsCollection: sessionInstructionsCollection,

                skillsSearcher: skillsSearcher,
                skillsDefaultLocationsProvider: skillsDefaultLocationsProvider,
                allSkillsCollection: allSkillsCollection,
                sessionSkillsCollection: sessionSkillsCollection,

                fileBasedHooksSearcher: fileBasedHooksSearcher,
                fileBasedHooksDefaultLocationsProvider: fileBasedHooksDefaultLocationsProvider,
                allFileBasedHooksCollection: allFileBasedHooksCollection,
                sessionFileBasedHooksCollection: sessionFileBasedHooksCollection,
                allSdkBasedSessionHooksCollection: allSdkBasedSessionHooksCollection,
                sessionSdkBasedSessionHooksCollection: sessionSdkBasedSessionHooksCollection,

                fileBasedMcpServersSearcher: fileBasedMcpServersSearcher,
                fileBasedMcpServersDefaultLocationsProvider: fileBasedMcpServersDefaultLocationsProvider,
                allFileBasedMcpServersCollection: allFileBasedMcpServersCollection,
                sessionFileBasedMcpServersCollection: sessionFileBasedMcpServersCollection,
                allSdkBasedMcpServersCollection: allSdkBasedMcpServersCollection,
                sessionSdkBasedMcpServersCollection: sessionSdkBasedMcpServersCollection,

                gitHubCopilotMappers: gitHubCopilotMappers,
                allPermissionDecisionsCollection: allPermissionDecisionsCollection,
                sessionPermissionDecisionsCollection: sessionPermissionDecisionsCollection,
                copilotClientMode: copilotClientMode);
        }
    }
}
