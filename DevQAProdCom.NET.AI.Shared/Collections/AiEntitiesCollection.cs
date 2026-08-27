using System.Collections;
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

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> AddEntityData(IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            //TODO Add validation that Each Entity has Yaml Configuration with Name // or add random generation of name if case not exists for use cases when one dont have access to files in those folders
            //or add config to not intialize such rules at all without names and log warning - then this method should be nullable

            var entityName = entity.ConfigurationData.Name;
            var addedEntityFilePath = entity.FilePath;
            var hasFilePath = !string.IsNullOrEmpty(addedEntityFilePath);

            if (hasFilePath)
            {
                IoUtils.CheckFileMustExist(addedEntityFilePath!);

                var addedEntityNormalizedFilePath = IoUtils.NormalizeFilePath(addedEntityFilePath);

                var existingEntityByFilePath = Entities
                    .FirstOrDefault(x => !string.IsNullOrEmpty(x.FilePath) && IoUtils.NormalizeFilePath(x.FilePath) == addedEntityNormalizedFilePath);

                if (existingEntityByFilePath != null)
                {
                    Log.Warning($"Entity with file path '{addedEntityFilePath}' already exists in the collection and will be replaced with the new one.");
                    Entities.Remove(existingEntityByFilePath);
                }

                var duplicateNameEntities = Entities
                    .Where(x => x.ConfigurationData.Name == entityName)
                    .Where(x => string.IsNullOrEmpty(x.FilePath) || IoUtils.NormalizeFilePath(x.FilePath) != addedEntityNormalizedFilePath)
                    .ToList();

                if (duplicateNameEntities.Any())
                {
                    var allHaveDifferentFilePaths = duplicateNameEntities.All(x => !string.IsNullOrEmpty(x.FilePath));

                    if (!allHaveDifferentFilePaths)
                    {
                        throw new InvalidOperationException(
                            $"Entity with name '{entityName}' already exists in the collection with a missing or identical file path. " +
                            $"Entities with the same identifier can only be added when they have different file paths.");
                    }

                    var duplicateFilePaths = duplicateNameEntities
                        .Select(x => x.FilePath)
                        .Where(x => !string.IsNullOrEmpty(x))
                        .ToList();

                    Log.Warning(
                        $"Entity with name '{entityName}' is already present in the collection " +
                        $"under different file path(s): {string.Join(", ", duplicateFilePaths)}. " +
                        $"Adding another entity with the same name from file path '{addedEntityFilePath}'.");
                }

                Entities.Add(entity);
                Log.Info($"Successfully added entity with name '{entityName}' from file: {addedEntityFilePath}");
                return entity;
            }

            // Entity added dynamically without creating an md file (file path is null or empty)
            if (Entities.Any(x => x.ConfigurationData.Name == entityName))
            {
                throw new InvalidOperationException(
                    $"Entity with name '{entityName}' already exists in the collection. " +
                    $"Entities added dynamically without a file path must have a unique identifier.");
            }

            Entities.Add(entity);
            Log.Info($"Successfully added dynamic entity with name '{entityName}' without a file path.");
            return entity;
        }

        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> AddEntitiesData(params IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>[] entities)
        {
            if (entities == null || entities.Length == 0)
            {
                throw new ArgumentException("At least one entity must be provided.", nameof(entities));
            }

            var addedEntities = new List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>>();

            foreach (var entity in entities)
            {
                addedEntities.Add(AddEntityData(entity));
            }

            return addedEntities;
        }

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> AddEntityDataFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);

            Log.Info($"Adding entity data from file: {filePath}");
            var entityData = YamlUtils.SplitEntityDataAndYamlMetaData<AiEntityWithTYamlConfigurationTypeModel<TAiEntityYamlConfiguration>, TAiEntityYamlConfiguration>(filePath);
            entityData.FilePath = filePath;

            return AddEntityData(entityData);
        }

        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> AddEntitiesDataFromFiles(params string[] filesPaths)
        {
            var addedEntities = new List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>>();

            foreach (var filePath in filesPaths)
            {
                addedEntities.Add(AddEntityDataFromFile(filePath));
            }

            return addedEntities;
        }

        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> AddEntitiesDataFromDirectory(string directoryPath)
        {
            IoUtils.CheckDirectoryMustExist(directoryPath);
            var mdFiles = IoUtils.GetMarkdownFiles(directoryPath);
            var addedEntities = new List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>>();

            foreach (var mdFile in mdFiles)
            {
                addedEntities.Add(AddEntityDataFromFile(mdFile));
            }

            return addedEntities;
        }

        public List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> AddEntitiesDataFromDirectories(params string[] directoriesPaths)
        {
            var addedEntities = new List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>>();

            foreach (var directoryPath in directoriesPaths)
            {
                addedEntities.AddRange(AddEntitiesDataFromDirectory(directoryPath));
            }

            return addedEntities;
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

        public IEnumerator<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

                if (IoUtils.TryGetNearestSolutionDirectoryAsCurrentOrParent(out var solutionDirectory, currentDirectory) && solutionDirectory != currentDirectory)
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
