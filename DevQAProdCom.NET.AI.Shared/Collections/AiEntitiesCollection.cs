using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.AI.Shared.Utils;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Collections
{
    public class AiEntitiesCollection<TAiEntityYamlConfiguration> : IAiEntitiesCollection<TAiEntityYamlConfiguration>
        where TAiEntityYamlConfiguration : IAiEntityYamlConfiguration, new()
    {
        public string? BaseDirectory { get; set; }

        protected List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> Entities = new();

        protected ILogger Log;

        public AiEntitiesCollection(ILogger logger, bool skipInitialization = false)
        {
            ArgumentNullException.ThrowIfNull(logger);
            Log = logger;

            if (!skipInitialization)
            {
                InitializeCollection();
            }
        }

        public AiEntitiesCollection(string baseFolder, ILogger logger, bool skipInitialization = false) : this(logger, skipInitialization)
        {
            BaseDirectory = baseFolder;
        }

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> GetEntityData(string entityIdentifier)
        {
            if (TryGetEntityData(entityIdentifier, out var entityData))
            {
                return entityData!;
            }

            throw new InvalidOperationException($"Entity with identifier/name '{entityIdentifier}' is not found in the collection.");
        }

        public bool TryGetEntityData(string entityIdentifier, out IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>? entityData)
        {
            var matchingEntities = Entities.Where(x => x.ConfigurationData.Name == entityIdentifier).ToList();

            if (matchingEntities.Count > 1)
            {
                throw new InvalidOperationException($"Multiple entities with identifier/name '{entityIdentifier}' found in the collection. {matchingEntities.ToJson()}");
            }

            if (matchingEntities.Count == 1)
            {
                entityData = matchingEntities.First();
                return true;
            }

            entityData = default;
            return false;
        }

        private void InitializeCollection()
        {
            var baseEntitiesLocations = GetEntitiesLocations();
            var mdFiles = IoUtils.GetMarkdownFiles(baseEntitiesLocations);

            foreach (var mdFile in mdFiles)
            {
                AddEntityData(mdFile);
            }
        }

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> AddEntityData(string filePath)
        {
            if (File.Exists(filePath))
            {
                Log.Info($"Adding entity data from file: {filePath}");
                var entityData = YamlUtils.SplitEntityDataAndYamlMetaData<AiEntityWithTYamlConfigurationTypeModel<TAiEntityYamlConfiguration>, TAiEntityYamlConfiguration>(filePath);
                entityData.FilePath = filePath;
                Entities.Add(entityData);
                Log.Info($"Successfully added entity data from file: {filePath}");
                return entityData;
            }

            Log.Error($"File '{filePath}' with entity is not found.");
            throw new Exception($"File '{filePath}' with entity is not found.");
        }

        protected List<string> GetEntitiesLocations()
        {
            var entitiesLocations = new List<string>();

            Log.Info("Starting to gather entity locations.");

            if (!string.IsNullOrEmpty(BaseDirectory))
            {
                Log.Info($"Using specified BaseDirectory: {BaseDirectory}");
                var entities = FindEntitiesInDirectory(BaseDirectory);
                entitiesLocations.AddRange(entities);
                Log.Info($"Found {entities.Count} entities in BaseDirectory.");
            }
            else
            {
                Log.Info("BaseDirectory not specified, searching in current directory and solution folder.");
                var currentDirectory = Directory.GetCurrentDirectory();
                Log.Info($"Current directory: {currentDirectory}");
                var entities = FindEntitiesInDirectory(currentDirectory);
                entitiesLocations.AddRange(entities);
                Log.Info($"Found {entities.Count} entities in current directory.");

                var solutionDirectory = IoUtils.GetNearestSolutionDirectoryAsCurrentOrParent(currentDirectory);
                if (solutionDirectory != currentDirectory)
                {
                    Log.Info($"Solution folder: {solutionDirectory}");
                    entities = FindEntitiesInDirectory(solutionDirectory);
                    entitiesLocations.AddRange(entities);
                    Log.Info($"Found {entities.Count} entities in solution folder.");
                }
            }

            Log.Info($"Total entity locations found: {entitiesLocations.Count}");
            if (entitiesLocations.Any())
            {
                Log.Info($"Entity locations: {string.Join(", ", entitiesLocations)}");
            }
            else
            {
                Log.Warning("No entity locations were found.");
            }

            return entitiesLocations;
        }

        /// <summary>
        /// Finds all entity file paths within the specified directory.
        /// </summary>
        /// <param name="directory">The directory path to search for entities.</param>
        /// <returns>A list of file paths for entities found in the directory.</returns>
        protected virtual List<string> FindEntitiesInDirectory(string directory) => new List<string>();
    }
}
