using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.AI.Shared.Models;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Mappers
{
    public class GitHubCopilotAiAgentsConfigurationsMappers : IAiAgentsConfigurationsMappers<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>
    {
        public IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata> ToAiAgentWithConfiguration(
            IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata> aiAgent,
            IAiAgentConfigurationWithoutAiEntityIdentifiers? configuration = null)
        {
            var customMetadata = new GitHubCopilotAiAgentConfigurationCustomMetadataModel
            {
                Configurations = new List<IAiAgentConfigurationWithoutAiEntityIdentifiers>
                {
                    new AiAgentConfigurationWithoutAiEntityIdentifiersModel
                    {
                        ConfigurationIdentifier = configuration?.ConfigurationIdentifier,

                        Permissions = configuration?.Permissions,
                        Instructions = configuration?.Instructions,
                        Skills = configuration?.Skills,
                        Subagents = configuration?.Subagents,
                        Hooks = configuration?.Hooks,
                        McpServers = configuration?.McpServers,
                    }
                }
            };

            return new AiEntityWithTConfigurationTypeModel<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>
            {
                FilePath = aiAgent.FilePath,
                Prompt = aiAgent.Prompt,
                ConfigurationData = new GitHubCopilotAiAgentConfigurationModelWithCustomMedata
                {
                    Name = aiAgent.ConfigurationData.Name,
                    Description = aiAgent.ConfigurationData.Description,

                    //agent properties are treated as shared across all configurations, hovewer if the same property is specified in a configuration, it always overrides the agent set property
                    Tools = configuration?.Tools ?? aiAgent.ConfigurationData.Tools,
                    Model = configuration?.Model ?? aiAgent.ConfigurationData.Model,
                    CustomMetadata = customMetadata
                }
            };
        }

        public IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata> ToAiAgentWithConfiguration(IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata> aiAgent, IAiAgentConfiguration configuration)
        {
            var customMetadata = new GitHubCopilotAiAgentConfigurationCustomMetadataModel
            {
                Configurations = new List<IAiAgentConfigurationWithoutAiEntityIdentifiers>
                {
                    new AiAgentConfigurationWithoutAiEntityIdentifiersModel
                    {
                        ConfigurationIdentifier = configuration.ConfigurationIdentifier,

                        Permissions = configuration.Permissions,
                        Instructions = configuration.Instructions,
                        Skills = configuration.Skills,
                        Subagents = configuration.Subagents,
                        Hooks = configuration.Hooks,
                        McpServers = configuration.McpServers,
                    }
                }
            };

            return new AiEntityWithTConfigurationTypeModel<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>
            {
                FilePath = aiAgent.FilePath,
                Prompt = aiAgent.Prompt,
                ConfigurationData = new GitHubCopilotAiAgentConfigurationModelWithCustomMedata
                {
                    Name = configuration.Name,
                    Description = configuration.Description,

                    Tools = configuration.Tools,
                    Model = configuration.Model,
                    CustomMetadata = customMetadata
                }
            };
        }

        public IAiAgentConfiguration ToIAiAgentConfigurationFromAgentWithTConfigurationTypeConfigurations(IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata> aiAgent, IAiAgentConfigurationWithoutAiEntityIdentifiers configuration)
        {
            return new AiAgentConfigurationModel
            {
                ConfigurationIdentifier = configuration.ConfigurationIdentifier,

                Name = aiAgent.ConfigurationData.Name,
                Description = aiAgent.ConfigurationData.Description,

                //agent properties are treated as shared across all configurations, hovewer if the same property is specified in a configuration, it always overrides the agent set property
                Tools = configuration.Tools ?? aiAgent.ConfigurationData.Tools,
                Model = configuration.Model ?? aiAgent.ConfigurationData.Model,

                Permissions = configuration.Permissions,
                Instructions = configuration.Instructions,
                Skills = configuration.Skills,
                Subagents = configuration.Subagents,
                Hooks = configuration.Hooks,
                McpServers = configuration.McpServers,
            };
        }
    }
}
