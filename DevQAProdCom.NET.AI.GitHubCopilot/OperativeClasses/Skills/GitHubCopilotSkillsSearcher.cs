using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.AI.Shared.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using CopilotIoUtils = DevQAProdCom.NET.AI.GitHubCopilot.Utils.IoUtils;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Skills
{
    public class GitHubCopilotSkillsSearcher : ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiSkillConfiguration>>
    {
        private readonly ILogger _logger;

        public GitHubCopilotSkillsSearcher(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual List<IAiEntityWithTConfigurationType<IGitHubCopilotAiSkillConfiguration>> Search(string path, bool useExtendedSearch = true)
        {
            var skills = new List<IAiEntityWithTConfigurationType<IGitHubCopilotAiSkillConfiguration>>();

            if (GlobalIoUtils.FileExists(path))
            {
                var skillFromFile = SearchInFile(path);
                if (skillFromFile != null)
                {
                    skills.Add(skillFromFile);
                }
                return skills;
            }

            if (GlobalIoUtils.DirectoryExists(path))
            {
                var files = CopilotIoUtils.GetFilesWithCopilotSkills(path, useExtendedSearch);

                foreach (var file in files)
                {
                    var skillFromFile = SearchInFile(file);
                    if (skillFromFile != null)
                    {
                        skills.Add(skillFromFile);
                    }
                }

                return skills;
            }

            return skills;
        }

        private IAiEntityWithTConfigurationType<IGitHubCopilotAiSkillConfiguration>? SearchInFile(string filePath)
        {
            try
            {
                var skill = YamlUtils.SplitEntityDataAndYamlMetaData<AiEntityWithTConfigurationTypeModel<GitHubCopilotAiSkillConfigurationModel>, GitHubCopilotAiSkillConfigurationModel>(filePath);

                return new AiEntityWithTConfigurationTypeModel<IGitHubCopilotAiSkillConfiguration>
                {
                    ConfigurationData = skill.ConfigurationData,
                    Prompt = skill.Prompt,
                    FilePath = filePath
                };
            }
            catch (Exception exception)
            {
                _logger.Error("[{TypeName}] Error processing skill file '{FilePath}': {ErrorMessage}", nameof(GitHubCopilotSkillsSearcher), filePath, exception.Message);
                return null;
            }
        }
    }
}
