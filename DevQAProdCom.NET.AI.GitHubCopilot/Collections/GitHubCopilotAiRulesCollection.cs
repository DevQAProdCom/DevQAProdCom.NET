using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.GitHubCopilot.Utils;
using DevQAProdCom.NET.AI.Shared.Collections;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Collections
{
    public class GitHubCopilotAiRulesCollection : AiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>
    {
        public GitHubCopilotAiRulesCollection() : base() { }

        public GitHubCopilotAiRulesCollection(string baseFolder) : base(baseFolder) { }
        protected override List<string> FindEntitiesInDirectory(string directory)
        {
            return IoUtils.GetCopilotInstructions(directory);
        }
    }
}
