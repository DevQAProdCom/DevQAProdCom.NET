using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces
{
    public interface IMicrosoftAgentFrameworkAiAgentInteractorsFactory
    {
        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor(
            ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>>? agentsSearcher = null,
            ILocationsProvider? agentsDefaultLocationsProvider = null,
            IAiAgentsConfigurationsMappers<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>? mapper = null,
            ISearchEntitiesInLocations<IAiAgentConfiguration>? agentsConfigurationsSearcher = null,
            ILocationsProvider? agentsDefaultConfigurationsLocationsProvider = null,
            IAiAgentsCollectionWithConfigurations<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>? allAgentsCollection = null,
            IAiAgentsCollectionWithConfigurations<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>? sessionAgentsCollection = null,

            ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiInstructionConfiguration>>? instructionsSearcher = null,
            ILocationsProvider? instructionsDefaultLocationsProvider = null,
            IAiEntitiesCollection<IGitHubCopilotAiInstructionConfiguration>? allInstructionsCollection = null,
            IAiEntitiesCollection<IGitHubCopilotAiInstructionConfiguration>? sessionInstructionsCollection = null,

            ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiSkillConfiguration>>? skillsSearcher = null,
            ILocationsProvider? skillsDefaultLocationsProvider = null,
            IAiEntitiesCollection<IGitHubCopilotAiSkillConfiguration>? allSkillsCollection = null,
            IAiEntitiesCollection<IGitHubCopilotAiSkillConfiguration>? sessionSkillsCollection = null,

            IFileBasedHooksSearcher? hooksSearcher = null,
            ILocationsProvider? hooksDefaultLocationsProvider = null,
            IFileBasedHooksCollection? allFileBasedHooksCollection = null,
            IFileBasedHooksCollection? sessionFileBasedHooksCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>? allSdkBasedSessionHooksCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>? sessionSdkBasedSessionHooksCollection = null,

            IFileBasedMcpServersSearcher? mcpServersSearcher = null,
            ILocationsProvider? mcpServersDefaultLocationsProvider = null,
            IFileBasedMcpServersCollection? allFileBasedMcpServersCollection = null,
            IFileBasedMcpServersCollection? sessionFileBasedMcpServersCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>? allSdkBasedMcpServersCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>? sessionSdkBasedMcpServersCollection = null,

            IGitHubCopilotMappers? gitHubCopilotMappers = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotPermissionDecision>? allPermissionDecisionsCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotPermissionDecision>? sessionPermissionDecisionsCollection = null,
            CopilotClientMode copilotClientMode = CopilotClientMode.Empty);
    }
}
