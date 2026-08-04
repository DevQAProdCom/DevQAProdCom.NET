using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.GitHubCopilot.Utils;
using DevQAProdCom.NET.AI.Shared.Collections;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Collections
{
    public class GitHubCopilotAiAgentsCollection : AiAgentsCollection<GitHubCopilotAiAgentYamlConfigurationModel>
    {
        public GitHubCopilotAiAgentsCollection() : base() { }

        public GitHubCopilotAiAgentsCollection(string baseFolder) : base(baseFolder) { }

        protected override List<string> GetBaseAgentsLocations()
        {
            var agentsLocations = new List<string>();

            if (!string.IsNullOrEmpty(BaseFolder))
            {
                var agents = IoUtils.GetCopilotAgents(BaseFolder);
                agentsLocations.AddRange(agents);
            }
            else
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var agents = IoUtils.GetCopilotAgents(currentDirectory);
                agentsLocations.AddRange(agents);

                var solutionFolder = GlobalIoUtils.GetNearestSolutionDirectoryAsCurrentOrParent(currentDirectory);
                agents = IoUtils.GetCopilotAgents(solutionFolder);
                agentsLocations.AddRange(agents);
            }

            return agentsLocations;
        }
    }
}
