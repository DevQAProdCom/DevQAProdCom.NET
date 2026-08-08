using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Mappers
{
    public class GitHubCopilotMappers
    {
        public CustomAgentConfig ToCustomAgentConfig(IAiAgent<GitHubCopilotAiAgentYamlConfigurationModel> aiAgent)
        {
            var config = new CustomAgentConfig();

            config.Name = aiAgent.ConfigurationData.Name;
            config.DisplayName = aiAgent.ConfigurationData.Name; //TODO Add Custom YAML Attribute for DisplayName
            config.Description = aiAgent.ConfigurationData.Description;
            config.Prompt = aiAgent.Prompt;
            config.Tools = aiAgent.ConfigurationData.Tools;
            config.Skills = aiAgent.ConfigurationData.Skills;
            config.Model = aiAgent.ConfigurationData.Model;

            return config;
        }
    }
}
