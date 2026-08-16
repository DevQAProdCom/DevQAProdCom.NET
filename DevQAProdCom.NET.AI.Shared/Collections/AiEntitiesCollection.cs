using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.AI.Shared.Utils;
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

        public AiEntitiesCollection(ILogger logger, bool initializeWithDefaultLocations = true)
        {
            ArgumentNullException.ThrowIfNull(logger);
            Log = logger;

            if (initializeWithDefaultLocations)
            {
                InitializeCollectionFromDefaultLocations();
            }
        }

        public AiEntitiesCollection(string baseFolder, ILogger logger, bool initializeWithDefaultLocations = true) : this(logger, initializeWithDefaultLocations)
        {
            BaseDirectory = baseFolder;
        }

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> GetEntityDataByIdentifier(string entityIdentifier)
        {
            if (TryGetEntityDataByIdentifier(entityIdentifier, out var entityData))
            {
                return entityData!;
            }

            throw new InvalidOperationException($"Entity with identifier/name '{entityIdentifier}' is not found in the collection.");
        }

        public bool TryGetEntityDataByIdentifier(string entityIdentifier, out IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>? entityData)
        {
            var matchingEntities = Entities.Where(x => x.ConfigurationData.Name == entityIdentifier).ToList();

            if (matchingEntities.Count > 1)
            {
                var filePaths = matchingEntities
                    .Select(x => x.FilePath)
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToList();

                throw new InvalidOperationException(
                    $"There are several entities with the same name '{entityIdentifier}' under several file paths: " +
                    $"{string.Join(", ", filePaths)}. " +
                    "Please get the entity by file path instead of by name.");
            }

            if (matchingEntities.Count == 1)
            {
                entityData = matchingEntities.First();
                return true;
            }

            entityData = default;
            return false;
        }

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> AddEntityDataFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);

            Log.Info($"Adding entity data from file: {filePath}");
            var entityData = YamlUtils.SplitEntityDataAndYamlMetaData<AiEntityWithTYamlConfigurationTypeModel<TAiEntityYamlConfiguration>, TAiEntityYamlConfiguration>(filePath);
            entityData.FilePath = filePath;

            var addedEntityNormalizedFilePath = IoUtils.NormalizeFilePath(filePath);
            var existingEntityByFilePath = Entities.FirstOrDefault(x => IoUtils.NormalizeFilePath(x.FilePath) == addedEntityNormalizedFilePath);

            if (existingEntityByFilePath != null)
            {
                Log.Warning($"Entity with file path '{filePath}' already exists in the collection and will be replaced with the new one.");
                Entities.Remove(existingEntityByFilePath);
            }

            var duplicateNameEntities = Entities
                .Where(x => x.ConfigurationData.Name == entityData.ConfigurationData.Name)
                .Where(x => IoUtils.NormalizeFilePath(x.FilePath) != addedEntityNormalizedFilePath)
                .ToList();

            if (duplicateNameEntities.Any())
            {
                var duplicateFilePaths = duplicateNameEntities
                    .Select(x => x.FilePath)
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToList();

                Log.Warning(
                    $"Entity with name '{entityData.ConfigurationData.Name}' is already present in the collection " +
                    $"under different file path(s): {string.Join(", ", duplicateFilePaths)}. " +
                    $"Adding another entity with the same name from file path '{filePath}'.");
            }

            Entities.Add(entityData);
            Log.Info($"Successfully added entity with name '{entityData.ConfigurationData.Name}' from file: {filePath}");
            return entityData;
        }

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>[] AddEntitiesDataFromFiles(params string[] filePaths)
        {
            var addedEntities = new List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>>();

            foreach (var filePath in filePaths)
            {
                addedEntities.Add(AddEntityDataFromFile(filePath));
            }

            return addedEntities.ToArray();
        }

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>[] AddEntitiesDataFromDirectories(params string[] directoriesPaths)
        {
            var addedEntities = new List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>>();

            foreach (var directoryPath in directoriesPaths)
            {
                IoUtils.CheckDirectoryMustExist(directoryPath);
                var mdFiles = IoUtils.GetMarkdownFiles(directoryPath);

                foreach (var mdFile in mdFiles)
                {
                    addedEntities.Add(AddEntityDataFromFile(mdFile));
                }
            }

            return addedEntities.ToArray();
        }

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> GetEntityDataByFilePath(string filePath)
        {
            if (TryGetEntityDataByFilePath(filePath, out var entityData))
            {
                return entityData!;
            }

            throw new InvalidOperationException($"Entity with file path '{filePath}' is not found in the collection.");
        }

        public bool TryGetEntityDataByFilePath(string filePath, out IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>? entityData)
        {
            var normalizedFilePath = IoUtils.NormalizeFilePath(filePath);
            entityData = Entities.FirstOrDefault(x => IoUtils.NormalizeFilePath(x.FilePath) == normalizedFilePath);
            return entityData != null;
        }

        private void InitializeCollectionFromDefaultLocations()
        {
            var baseEntitiesLocations = GetEntitiesLocations();
            var mdFiles = IoUtils.GetMarkdownFiles(baseEntitiesLocations);

            foreach (var mdFile in mdFiles)
            {
                AddEntityDataFromFile(mdFile);
            }
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

        protected virtual List<string> FindEntitiesInDirectory(string directory) => new List<string>();
    }
}
