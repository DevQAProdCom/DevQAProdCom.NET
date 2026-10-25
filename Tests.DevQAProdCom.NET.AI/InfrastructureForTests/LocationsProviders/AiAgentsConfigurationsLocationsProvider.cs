using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Global.Utils;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.LocationsProviders
{
    internal class AiAgentsConfigurationsLocationsProvider : BaseLocationsProvider
    {
        public AiAgentsConfigurationsLocationsProvider(string? rootDirectory = null, bool useExtendedSearch = true)
        {
            var defaultLocations = GetDefaultLocations(rootDirectory, useExtendedSearch);
            Locations.AddRange(defaultLocations);
        }

        public List<string> GetDefaultLocations(string? rootDirectory = null, bool useExtendedSearch = true)
        {
            var currentDirectory = rootDirectory ?? Directory.GetCurrentDirectory();

            if (IoUtils.TryGetNearestSolutionDirectoryAsCurrentOrParent(out var solutionDirectory, currentDirectory) && !string.IsNullOrEmpty(solutionDirectory) && solutionDirectory != currentDirectory)
            {
                var agentsConfigurationsDirectory = Path.Combine(solutionDirectory, Const.Directories.TESTS, Const.Directories.INFRASTRUCTURE_FOR_TESTS, Const.Directories.AI_AGENTS_CONFIGURATIONS);
                IoUtils.CheckDirectoryMustExist(agentsConfigurationsDirectory);
                return new List<string> { agentsConfigurationsDirectory };
            }

            throw new InvalidOperationException($"Cannot find the nearest solution directory from the current directory: {currentDirectory}. Please ensure that the solution file (.sln) exists in the parent directories.");
        }
    }
}
