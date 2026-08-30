using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.GitHubCopilot.Utils;
using DevQAProdCom.NET.AI.Shared.Collections;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Collections
{
    public class GitHubCopilotAiAgentsCollection : AiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>
    {
        public GitHubCopilotAiAgentsCollection(ILogger logger, bool initializeWithDefaultLocations = true, string? collectionIdentifier = null, bool useExtendedSearch = false)
            : base(logger, initializeWithDefaultLocations: initializeWithDefaultLocations, collectionIdentifier: collectionIdentifier, useExtendedSearch: useExtendedSearch) { }

        public GitHubCopilotAiAgentsCollection(string baseFolder, ILogger logger, bool initializeWithDefaultLocations = true, string? collectionIdentifier = null, bool useExtendedSearch = false)
            : base(baseFolder, logger, initializeWithDefaultLocations: initializeWithDefaultLocations, collectionIdentifier: collectionIdentifier, useExtendedSearch: useExtendedSearch) { }

        protected override List<string> FindEntitiesInDirectory(string directory, bool useExtendedSearch = false)
        {
            return IoUtils.GetCopilotAgents(directory, useExtendedSearch);
        }
    }
}
