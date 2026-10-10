using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.GitHubCopilot.Utils;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Collections
{
    public class GitHubCopilotAiAgentsCollection : AiEntitiesCollection<GitHubCopilotAiAgentConfigurationModel>
    {
        public GitHubCopilotAiAgentsCollection(ILogger logger, bool initializeFromDefaultLocations = false, string? collectionIdentifier = null, bool useExtendedSearch = false)
            : base(logger, initializeFromDefaultLocations: initializeFromDefaultLocations, collectionIdentifier: collectionIdentifier, useExtendedSearch: useExtendedSearch) { }

        public GitHubCopilotAiAgentsCollection(string baseFolder, ILogger logger, bool initializeFromDefaultLocations = false, string? collectionIdentifier = null, bool useExtendedSearch = false)
            : base(baseFolder, logger, initializeFromDefaultLocations: initializeFromDefaultLocations, collectionIdentifier: collectionIdentifier, useExtendedSearch: useExtendedSearch) { }

        protected override List<string> FindEntitiesInDirectory(string directory, bool useExtendedSearch = false)
        {
            return IoUtils.GetFilesWithCopilotAgents(directory, useExtendedSearch);
        }
    }
}
