using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.AI.Shared.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using CopilotIoUtils = DevQAProdCom.NET.AI.GitHubCopilot.Utils.IoUtils;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Agents
{
    public class GitHubCopilotAgentsSearcher : ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>>
    {
        private readonly ILogger _logger;

        public GitHubCopilotAgentsSearcher(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual List<IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>> Search(string path, bool useExtendedSearch = true)
        {
            var agents = new List<IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>>();

            if (GlobalIoUtils.FileExists(path))
            {
                var agentFromFile = SearchInFile(path);
                if (agentFromFile != null)
                {
                    agents.Add(agentFromFile);
                }
                return agents;
            }

            if (GlobalIoUtils.DirectoryExists(path))
            {
                var files = CopilotIoUtils.GetFilesWithCopilotAgents(path, useExtendedSearch);

                foreach (var file in files)
                {
                    var agentFromFile = SearchInFile(file);
                    if (agentFromFile != null)
                    {
                        agents.Add(agentFromFile);
                    }
                }

                return agents;
            }

            return agents;
        }

        private IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>? SearchInFile(string filePath)
        {
            try
            {
                var agent = YamlUtils.SplitEntityDataAndYamlMetaData<AiEntityWithTConfigurationTypeModel<GitHubCopilotAiAgentConfigurationModelWithCustomMedata>, GitHubCopilotAiAgentConfigurationModelWithCustomMedata>(filePath);
                agent.FilePath = filePath;

                return new AiEntityWithTConfigurationTypeModel<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>
                {
                    FilePath = agent.FilePath,
                    Prompt = agent.Prompt,
                    ConfigurationData = agent.ConfigurationData
                };
            }
            catch (Exception exception)
            {
                _logger.Error("[{TypeName}] Error processing agent file '{FilePath}': {ErrorMessage}", nameof(GitHubCopilotAgentsSearcher), filePath, exception.Message);
                return null;
            }
        }
    }
}
