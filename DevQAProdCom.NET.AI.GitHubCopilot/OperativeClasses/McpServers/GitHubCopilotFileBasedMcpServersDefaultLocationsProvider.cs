using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using CopilotIoUtils = DevQAProdCom.NET.AI.GitHubCopilot.Utils.IoUtils;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers
{
    public class GitHubCopilotFileBasedMcpServersDefaultLocationsProvider : BaseLocationsProvider
    {
        public GitHubCopilotFileBasedMcpServersDefaultLocationsProvider(string? rootDirectory = null, bool useExtendedSearch = true)
        {
            var defaultLocations = GetDefaultLocations(rootDirectory, useExtendedSearch);
            Locations.AddRange(defaultLocations);
        }

        public List<string> GetDefaultLocations(string? rootDirectory = null, bool useExtendedSearch = true)
        {
            var currentDirectory = rootDirectory ?? Directory.GetCurrentDirectory();
            var defaultLocations = CopilotIoUtils.GetFilesWithCopilotMcpServers(currentDirectory, useExtendedSearch);

            if (GlobalIoUtils.TryGetNearestSolutionDirectoryAsCurrentOrParent(out var solutionDirectory, currentDirectory) && !string.IsNullOrEmpty(solutionDirectory) && solutionDirectory != currentDirectory)
            {
                // Search in the solution directory for MCP server files, but do not use extended search to avoid duplicates related to some preinstalled CLI MCP servers that are already inside several projects in the solution.
                defaultLocations.AddRange(CopilotIoUtils.GetFilesWithCopilotMcpServers(solutionDirectory, useExtendedSearch: false));
            }

            return defaultLocations.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }
    }
}
