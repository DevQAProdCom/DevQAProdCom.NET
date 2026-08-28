using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.GitHubCopilot.Utils;
using DevQAProdCom.NET.AI.Shared.Collections;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Collections
{
    public class GitHubCopilotAiSkillsCollection : AiEntitiesCollection<GitHubCopilotAiSkillYamlConfigurationModel>
    {
        public GitHubCopilotAiSkillsCollection(ILogger logger, bool initializeWithDefaultLocations = true, string? collectionIdentifier = null)
            : base(logger, initializeWithDefaultLocations: initializeWithDefaultLocations, collectionIdentifier: collectionIdentifier) { }

        public GitHubCopilotAiSkillsCollection(string baseFolder, ILogger logger, bool initializeWithDefaultLocations = true, string? collectionIdentifier = null)
            : base(baseFolder, logger, initializeWithDefaultLocations: initializeWithDefaultLocations, collectionIdentifier: collectionIdentifier) { }
        protected override List<string> FindEntitiesInDirectory(string directory)
        {
            return IoUtils.GetCopilotSkills(directory);
        }
    }
}
