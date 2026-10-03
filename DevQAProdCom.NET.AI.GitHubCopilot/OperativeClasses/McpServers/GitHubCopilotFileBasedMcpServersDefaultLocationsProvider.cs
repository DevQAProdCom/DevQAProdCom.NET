using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using CopilotIoUtils = DevQAProdCom.NET.AI.GitHubCopilot.Utils.IoUtils;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers
{
    public class GitHubCopilotFileBasedMcpServersDefaultLocationsProvider : BaseLocationsProvider
    {
        public GitHubCopilotFileBasedMcpServersDefaultLocationsProvider(bool useExtendedSearch = true)
        {
            var defaultLocations = GetDefaultLocations(useExtendedSearch);
            Locations.AddRange(defaultLocations);
        }

        public List<string> GetDefaultLocations(bool useExtendedSearch = true)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var defaultLocations = CopilotIoUtils.GetCopilotHooks(currentDirectory, useExtendedSearch);

            if (GlobalIoUtils.TryGetNearestSolutionDirectoryAsCurrentOrParent(out var solutionDirectory, currentDirectory)
                && !string.IsNullOrEmpty(solutionDirectory)
                && solutionDirectory != currentDirectory)
            {
                defaultLocations.AddRange(CopilotIoUtils.GetCopilotHooks(solutionDirectory, useExtendedSearch));
            }

            return defaultLocations.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }




    }
}
