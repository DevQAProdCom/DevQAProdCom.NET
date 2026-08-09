using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.GitHubCopilot.Utils;
using DevQAProdCom.NET.AI.Shared.Collections;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Collections
{
    public class GitHubCopilotAiAgentsCollection : AiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>
    {
        public GitHubCopilotAiAgentsCollection(ILogger logger) : base(logger) { }

        public GitHubCopilotAiAgentsCollection(string baseFolder, ILogger logger) : base(baseFolder, logger) { }

        protected override List<string> FindEntitiesInDirectory(string directory)
        {
            return IoUtils.GetCopilotAgents(directory);
        }
    }
}
