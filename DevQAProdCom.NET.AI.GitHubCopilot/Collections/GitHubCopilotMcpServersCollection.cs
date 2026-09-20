using DevQAProdCom.NET.AI.GitHubCopilot.McpServers;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Collections
{
    public class GitHubCopilotMcpServersCollection : McpServersCollection
    {
        public GitHubCopilotMcpServersCollection(ILogger logger, bool initializeFromDefaultLocations = false, string? collectionIdentifier = null, bool useExtendedSearch = false) :
            base(logger, initializeFromDefaultLocations: initializeFromDefaultLocations, collectionIdentifier: collectionIdentifier, useExtendedSearch: useExtendedSearch)
        {

        }

        protected override void InitializeCollectionFromDefaultLocations()
        {
            McpServers.Add(new GitHubCopilotPlaywrightMcpServer(Logger));
        }
    }
}
