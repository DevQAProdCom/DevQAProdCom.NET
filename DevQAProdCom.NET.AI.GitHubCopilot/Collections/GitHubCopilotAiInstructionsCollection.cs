using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.GitHubCopilot.Utils;
using DevQAProdCom.NET.AI.Shared.Collections;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Collections
{
    public class GitHubCopilotAiInstructionsCollection : AiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel>
    {
        public GitHubCopilotAiInstructionsCollection(ILogger logger, bool initializeWithDefaultLocations = true) : base(logger, initializeWithDefaultLocations: initializeWithDefaultLocations) { }

        public GitHubCopilotAiInstructionsCollection(string baseFolder, ILogger logger, bool initializeWithDefaultLocations = true) : base(baseFolder, logger, initializeWithDefaultLocations: initializeWithDefaultLocations) { }

        protected override List<string> FindEntitiesInDirectory(string directory)
        {
            return IoUtils.GetCopilotInstructions(directory);
        }
    }
}
