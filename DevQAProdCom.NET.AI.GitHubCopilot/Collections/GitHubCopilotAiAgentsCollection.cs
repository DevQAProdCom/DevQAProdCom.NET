using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.GitHubCopilot.Utils;
using DevQAProdCom.NET.AI.Shared.Collections;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Collections
{
    public class GitHubCopilotAiAgentsCollection : AiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>
    {
        public GitHubCopilotAiAgentsCollection() : base() { }

        public GitHubCopilotAiAgentsCollection(string baseFolder) : base(baseFolder) { }

        protected override List<string> GetBaseEntitiesLocations()
        {
            var entitiesLocations = new List<string>();

            if (!string.IsNullOrEmpty(BaseFolder))
            {
                var entities = IoUtils.GetCopilotAgents(BaseFolder);
                entitiesLocations.AddRange(entities);
            }
            else
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var entities = IoUtils.GetCopilotAgents(currentDirectory);
                entitiesLocations.AddRange(entities);

                var solutionFolder = GlobalIoUtils.GetNearestSolutionDirectoryAsCurrentOrParent(currentDirectory);
                entities = IoUtils.GetCopilotAgents(solutionFolder);
                entitiesLocations.AddRange(entities);
            }

            return entitiesLocations;
        }
    }
}
