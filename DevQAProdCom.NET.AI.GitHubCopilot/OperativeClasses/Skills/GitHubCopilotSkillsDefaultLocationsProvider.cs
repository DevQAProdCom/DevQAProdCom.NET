using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using CopilotIoUtils = DevQAProdCom.NET.AI.GitHubCopilot.Utils.IoUtils;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Skills
{
    public class GitHubCopilotSkillsDefaultLocationsProvider : BaseLocationsProvider
    {
        public GitHubCopilotSkillsDefaultLocationsProvider(string? rootDirectory = null, bool useExtendedSearch = true)
        {
            var defaultLocations = GetDefaultLocations(rootDirectory, useExtendedSearch);
            Locations.AddRange(defaultLocations);
        }

        public List<string> GetDefaultLocations(string? rootDirectory = null, bool useExtendedSearch = true)
        {
            var currentDirectory = rootDirectory ?? Directory.GetCurrentDirectory();
            var defaultLocations = CopilotIoUtils.GetFilesWithCopilotSkills(currentDirectory, useExtendedSearch);

            if (GlobalIoUtils.TryGetNearestSolutionDirectoryAsCurrentOrParent(out var solutionDirectory, currentDirectory) && !string.IsNullOrEmpty(solutionDirectory) && solutionDirectory != currentDirectory)
            {
                defaultLocations.AddRange(CopilotIoUtils.GetFilesWithCopilotSkills(solutionDirectory, useExtendedSearch));
            }

            return defaultLocations.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }
    }
}
