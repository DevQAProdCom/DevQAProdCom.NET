using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.AI.Shared.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using CopilotIoUtils = DevQAProdCom.NET.AI.GitHubCopilot.Utils.IoUtils;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Instructions
{
    public class GitHubCopilotInstructionsSearcher : ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<GitHubCopilotAiInstructionConfigurationModel>>
    {
        private readonly ILogger _logger;

        public GitHubCopilotInstructionsSearcher(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual List<IAiEntityWithTConfigurationType<GitHubCopilotAiInstructionConfigurationModel>> Search(string path, bool useExtendedSearch = true)
        {
            var instructions = new List<IAiEntityWithTConfigurationType<GitHubCopilotAiInstructionConfigurationModel>>();

            if (GlobalIoUtils.FileExists(path))
            {
                var instructionFromFile = SearchInFile(path);
                if (instructionFromFile != null)
                {
                    instructions.Add(instructionFromFile);
                }
                return instructions;
            }

            if (GlobalIoUtils.DirectoryExists(path))
            {
                var files = CopilotIoUtils.GetFilesWithCopilotInstructions(path, useExtendedSearch);

                foreach (var file in files)
                {
                    var instructionFromFile = SearchInFile(file);
                    if (instructionFromFile != null)
                    {
                        instructions.Add(instructionFromFile);
                    }
                }

                return instructions;
            }

            return instructions;
        }

        private IAiEntityWithTConfigurationType<GitHubCopilotAiInstructionConfigurationModel>? SearchInFile(string filePath)
        {
            try
            {
                var instruction = YamlUtils.SplitEntityDataAndYamlMetaData<AiEntityWithConfigurationTypeModel<GitHubCopilotAiInstructionConfigurationModel>, GitHubCopilotAiInstructionConfigurationModel>(filePath);
                instruction.FilePath = filePath;
                return instruction;
            }
            catch (Exception exception)
            {
                _logger.Error("[{TypeName}] Error processing instruction file '{FilePath}': {ErrorMessage}", nameof(GitHubCopilotInstructionsSearcher), filePath, exception.Message);
                return null;
            }
        }
    }
}
