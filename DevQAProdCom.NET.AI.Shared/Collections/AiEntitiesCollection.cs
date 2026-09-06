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
        public string CollectionIdentifier { get; }
        public string? BaseDirectory { get; set; }
        protected List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> Entities = new();
        protected ILogger Log;

        protected readonly bool UseExtendedSearch = false;

        public AiEntitiesCollection(ILogger logger, bool initializeWithDefaultLocations = true, string? collectionIdentifier = null, bool useExtendedSearch = false)
        {
            collectionIdentifier ??= Guid.NewGuid().ToString();
            CollectionIdentifier = collectionIdentifier;
            ArgumentNullException.ThrowIfNull(logger);
            Log = logger;
            UseExtendedSearch = useExtendedSearch;

            if (initializeWithDefaultLocations)
            {
                InitializeCollectionFromDefaultLocations();
            }
        }

        public AiEntitiesCollection(string baseFolder, ILogger logger, bool initializeWithDefaultLocations = true, string? collectionIdentifier = null, bool useExtendedSearch = false) : this(logger, initializeWithDefaultLocations, collectionIdentifier, useExtendedSearch)
        {
            BaseDirectory = baseFolder;
        }

        public IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> AddEntityData(IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration> entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var entityName = entity.ConfigurationData?.Name;
            var addedEntityFilePath = entity.FilePath;

            // Validate that the entity has a valid name in its YAML configuration
            if (string.IsNullOrEmpty(entityName))
            {
                throw new InvalidOperationException($"[{CollectionIdentifier}] Entity must have a valid name in its YAML configuration. The provided entity has an empty or null name.");
            }

            //Validate existing entity with the same name and file path and replace it if found
            var existingEntity = Entities.SingleOrDefault(x => x.ConfigurationData.Name == entityName && x.FilePath == addedEntityFilePath);
            if (existingEntity != null)
            {
                Entities.Remove(existingEntity);
                Entities.Add(entity);
                Log.Debug("[{CollectionIdentifier}] Entity with name '{entityName}' and file path '{filePath}' already exists in the collection. It has been replaced with the new one.", $"{CollectionIdentifier}", entity.ConfigurationData.Name, entity.FilePath ?? "null");
                return existingEntity;
            }

            //For entity added from a file, check for existing entities with and handle duplicates if any
            if (!string.IsNullOrEmpty(addedEntityFilePath))
            {
                IoUtils.CheckFileMustExist(addedEntityFilePath);
                var addedEntityNormalizedFilePath = IoUtils.NormalizeFilePath(addedEntityFilePath);
                var existingEntityByFilePath = Entities.FirstOrDefault(x => !string.IsNullOrEmpty(x.FilePath) && IoUtils.NormalizeFilePath(x.FilePath) == addedEntityNormalizedFilePath);

                if (existingEntityByFilePath != null)
                {
                    Log.Warning("{CollectionIdentifier} Entity with file path '{addedEntityFilePath}' already exists in the collection and will be replaced with the new one.", $"[{CollectionIdentifier}]", addedEntityFilePath);
                    Entities.Remove(existingEntityByFilePath);
                }

                Entities.Add(entity);
                Log.Info("{CollectionIdentifier} Successfully added entity with name '{entityName}' from file: {addedEntityFilePath}", $"[{CollectionIdentifier}]", entityName, addedEntityFilePath);

                // Add warning if collection contains entities with the same name but different file paths
                var enitiesWithSameNameButDifferentFilePaths = Entities
                    .Where(x => x.ConfigurationData.Name == entityName)
                    .Where(x => string.IsNullOrEmpty(x.FilePath) || IoUtils.NormalizeFilePath(x.FilePath) != addedEntityNormalizedFilePath)
                    .ToList();

                if (enitiesWithSameNameButDifferentFilePaths.Any())
                {
                    var duplicateFilePaths = enitiesWithSameNameButDifferentFilePaths
                        .Select(x => x.FilePath)
                        .Where(x => !string.IsNullOrEmpty(x))
                        .ToList();

                    Log.Warning(
                        "{CollectionIdentifier} Entity with name '{entityName}' is already present in the collection " +
                        "under different file path(s): {duplicateFilePaths}. " +
                        "Adding another entity with the same name from file path '{addedEntityFilePath}'.",
                        $"[{CollectionIdentifier}]", entityName, string.Join(", ", duplicateFilePaths), addedEntityFilePath!);
                }

                return entity;
            }

            //For entity added dynamically without creating an md file (file path may be null or empty)
            else
            {
                if (Entities.Any(x => x.ConfigurationData.Name == entityName))
                {
                    throw new InvalidOperationException(
                        $"[{CollectionIdentifier}] Entity with name '{entityName}' already exists in the collection. " +
                        $"Entities added dynamically without a file path must have a unique identifier.");
                }

                Entities.Add(entity);
                Log.Info("{CollectionIdentifier} Successfully added dynamic entity with name '{entityName}' without a file path.", $"[{CollectionIdentifier}]", entityName);

                return entity;
            }
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

            Log.Info("{CollectionIdentifier} Adding entity data from file: {filePath}", $"[{CollectionIdentifier}]", filePath);
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

            throw new InvalidOperationException($"[{CollectionIdentifier}] Entity with identifier/name '{entityIdentifier}' is not found in the collection.");
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

                throw new InvalidOperationException($"[{CollectionIdentifier}] There are several entities with the same name '{entityIdentifier}' under several file paths: {string.Join(", ", filePaths)}. " +
                    $"Please get the entity by file path instead of by name.");
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

            throw new InvalidOperationException($"[{CollectionIdentifier}] Entity with file path '{filePath}' is not found in the collection.");
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

            Log.Info("{CollectionIdentifier} Starting to gather entity locations.", $"[{CollectionIdentifier}]");

            if (!string.IsNullOrEmpty(BaseDirectory))
            {
                Log.Info("{CollectionIdentifier} Using specified BaseDirectory: {baseDirectory}", $"[{CollectionIdentifier}]", BaseDirectory);
                var entities = FindEntitiesInDirectory(BaseDirectory, UseExtendedSearch);
                entitiesLocations.AddRange(entities);
                Log.Info("{CollectionIdentifier} Found {entitiesCount} entities in BaseDirectory.", $"[{CollectionIdentifier}]", entities.Count);
            }
            else
            {
                Log.Info("{CollectionIdentifier} BaseDirectory not specified, searching in current directory and solution folder.", $"[{CollectionIdentifier}]");
                var currentDirectory = Directory.GetCurrentDirectory();
                Log.Info("{CollectionIdentifier} Current directory: {currentDirectory}", $"[{CollectionIdentifier}]", currentDirectory);
                var entities = FindEntitiesInDirectory(currentDirectory, UseExtendedSearch);
                entitiesLocations.AddRange(entities);
                Log.Info("{CollectionIdentifier} Found {entitiesCount} entities in current directory.", $"[{CollectionIdentifier}]", entities.Count);

                if (IoUtils.TryGetNearestSolutionDirectoryAsCurrentOrParent(out var solutionDirectory, currentDirectory) && solutionDirectory != currentDirectory)
                {
                    Log.Info("{CollectionIdentifier} Solution folder: {solutionDirectory}", $"[{CollectionIdentifier}]", solutionDirectory!);
                    entities = FindEntitiesInDirectory(solutionDirectory, UseExtendedSearch);
                    entitiesLocations.AddRange(entities);
                    Log.Info("{CollectionIdentifier} Found {entitiesCount} entities in solution folder.", $"[{CollectionIdentifier}]", entities.Count);
                }
            }

            Log.Info("{CollectionIdentifier} Total entity locations found: {entitiesLocationsCount}", $"[{CollectionIdentifier}]", entitiesLocations.Count);
            if (entitiesLocations.Any())
            {
                Log.Info("{CollectionIdentifier} Entity locations: {entityLocations}", $"[{CollectionIdentifier}]", string.Join(", ", entitiesLocations));
            }
            else
            {
                Log.Warning("{CollectionIdentifier} No entity locations were found.", $"[{CollectionIdentifier}]");
            }

            return entitiesLocations;
        }

        protected virtual List<string> FindEntitiesInDirectory(string directory, bool useExtendedSearch = false) => new List<string>();
    }
}
