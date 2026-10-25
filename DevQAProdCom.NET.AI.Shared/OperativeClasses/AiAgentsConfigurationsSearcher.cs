using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class AiAgentsConfigurationsSearcher : ISearchEntitiesInLocations<IAiAgentConfiguration>
    {
        private readonly ILogger _logger;

        public AiAgentsConfigurationsSearcher(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<IAiAgentConfiguration> Search(string path, bool useExtendedSearch = true)
        {
            var configurations = new List<IAiAgentConfiguration>();

            if (IoUtils.FileExists(path))
            {
                var configurationFromFile = SearchInFile(path);
                if (configurationFromFile != null)
                {
                    configurations.Add(configurationFromFile);
                }

                return configurations;
            }

            if (IoUtils.DirectoryExists(path))
            {
                var searchOption = useExtendedSearch ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                var files = IoUtils.GetJsonFilesInDirectory(path, searchOption);

                foreach (var file in files)
                {
                    var configurationFromFile = SearchInFile(file);
                    if (configurationFromFile != null)
                    {
                        configurations.Add(configurationFromFile);
                    }
                }
            }

            return configurations;
        }

        private IAiAgentConfiguration? SearchInFile(string filePath)
        {
            try
            {
                var json = File.ReadAllText(filePath);
                var configuration = json.FromJson<AiAgentConfigurationModel>();

                if (configuration != null)
                {
                    return configuration;
                }
            }
            catch (Exception exception)
            {
                _logger.Error("[{TypeName}] Error processing agent configuration file '{FilePath}': {ErrorMessage}", nameof(AiAgentsConfigurationsSearcher), filePath, exception.Message);
            }

            return null;
        }
    }
}
