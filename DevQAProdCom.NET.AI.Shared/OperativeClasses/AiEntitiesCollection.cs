using System.Collections;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class AiEntitiesCollection<TAiEntityConfiguration> : IAiEntitiesCollection<TAiEntityConfiguration>
        where TAiEntityConfiguration : IAiEntityIdentifiers
    {
        public string CollectionIdentifier { get; }

        protected readonly List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> Entities = new();
        protected readonly ILogger _logger;
        private readonly ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> _entitiesSearcher;

        public AiEntitiesCollection(ILogger logger, ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> searcher, ILocationsProvider? defaultLocationsProvider = null, string? collectionIdentifier = null)
        {
            CollectionIdentifier = collectionIdentifier ?? Guid.NewGuid().ToString();
            ArgumentNullException.ThrowIfNull(logger);
            _logger = logger;
            _entitiesSearcher = searcher ?? throw new ArgumentNullException(nameof(searcher));
            InitializeCollectionFromDefaultLocations(defaultLocationsProvider);
        }

        public virtual IAiEntityWithTConfigurationType<TAiEntityConfiguration> Add(IAiEntityWithTConfigurationType<TAiEntityConfiguration> entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var addedEntityName = entity.ConfigurationData?.Name?.ToLower();

            if (string.IsNullOrEmpty(addedEntityName))
            {
                throw new InvalidOperationException($"[{CollectionIdentifier}] Entity must have a valid name in its YAML configuration. The provided entity has an empty or null name.");
            }

            entity.FilePath = string.IsNullOrWhiteSpace(entity.FilePath) ? null : IoUtils.NormalizeFilePath(entity.FilePath);
            var addedEntityFilePath = entity.FilePath;

            var existingEntityWithTheSameNameAndSameFilePath = Entities.SingleOrDefault(existingEntityInCollection =>
                existingEntityInCollection.ConfigurationData.Name.ToLower() == addedEntityName &&
                NormalizeFilePath(existingEntityInCollection.FilePath) == addedEntityFilePath);

            if (existingEntityWithTheSameNameAndSameFilePath != null)
            {
                Entities.Remove(existingEntityWithTheSameNameAndSameFilePath);
                Entities.Add(entity);
                _logger.Debug("[{CollectionIdentifier}] Entity with name '{entityName}' and file path '{filePath}' already exists in the collection. It has been replaced with the new one.", $"{CollectionIdentifier}", addedEntityName, addedEntityFilePath ?? "null");
                return entity;
            }

            IAiEntityWithTConfigurationType<TAiEntityConfiguration>? entityWithDifferentNameAndSameFilePath = null;
            if (!string.IsNullOrEmpty(addedEntityFilePath))
            {
                entityWithDifferentNameAndSameFilePath = Entities.SingleOrDefault(existingEntityInCollection =>
                    NormalizeFilePath(existingEntityInCollection.FilePath) == addedEntityFilePath &&
                    existingEntityInCollection.ConfigurationData.Name.ToLower() != addedEntityName);
            }

            var existingEntitiesWithTheSameName = Entities.Where(existingEntityInCollection =>
                existingEntityInCollection.ConfigurationData.Name.ToLower() == addedEntityName).ToList();

            var allSameNameEntities = new List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>>(existingEntitiesWithTheSameName);
            allSameNameEntities.Add(entity);

            if (allSameNameEntities.Any(e => !string.IsNullOrEmpty(e.FilePath)) && allSameNameEntities.Any(e => string.IsNullOrEmpty(e.FilePath)))
            {
                throw new InvalidOperationException($"[{CollectionIdentifier}] There are entities with the same name '{addedEntityName}' in the collection, some with file paths and some without. This is not allowed. Please ensure that all entities with the same name have file paths set.");
            }

            var existingEntitiesWithTheSameNameAndDifferentSetFilePaths = existingEntitiesWithTheSameName
                .Where(existingEntityInCollection =>
                    !string.IsNullOrEmpty(existingEntityInCollection.FilePath) &&
                    NormalizeFilePath(existingEntityInCollection.FilePath) != addedEntityFilePath)
                .ToList();

            if (entityWithDifferentNameAndSameFilePath != null)
            {
                Entities.Remove(entityWithDifferentNameAndSameFilePath);
                _logger.Debug("[{CollectionIdentifier}] Entity with the same file path '{filePath}', but different name '{oldEntityName}' already exists in the collection. It has been replaced with the new one.", $"{CollectionIdentifier}", addedEntityFilePath ?? "null", entityWithDifferentNameAndSameFilePath.ConfigurationData.Name);
            }

            if (existingEntitiesWithTheSameNameAndDifferentSetFilePaths.Count > 0)
            {
                Entities.Add(entity);
                _logger.Warning(
                    "{CollectionIdentifier} Entity with name '{entityName}' is already present in the collection under different file path(s): {existingFilePaths}. " +
                    "Adding another entity with the same name from file path '{addedEntityFilePath}'.",
                    $"[{CollectionIdentifier}]", addedEntityName, string.Join(", ", existingEntitiesWithTheSameNameAndDifferentSetFilePaths.Select(x => x.FilePath)), addedEntityFilePath ?? "null");
                return entity;
            }

            Entities.Add(entity);
            return entity;
        }

        private static string? NormalizeFilePath(string? filePath) => string.IsNullOrWhiteSpace(filePath) ? null : IoUtils.NormalizeFilePath(filePath);

        public List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> Add(params IAiEntityWithTConfigurationType<TAiEntityConfiguration>[] entities)
        {
            if (entities == null || entities.Length == 0)
            {
                throw new ArgumentException("At least one entity must be provided.", nameof(entities));
            }

            var addedEntities = new List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>>();

            foreach (var entity in entities)
            {
                addedEntities.Add(Add(entity));
            }

            return addedEntities;
        }

        public virtual IAiEntityWithTConfigurationType<TAiEntityConfiguration> AddFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);

            _logger.Info("{CollectionIdentifier} Adding entity data from file: {filePath}", $"[{CollectionIdentifier}]", filePath);

            var entities = _entitiesSearcher.Search(filePath);

            if (entities.Count == 0)
            {
                throw new InvalidOperationException($"[{CollectionIdentifier}] No entities found in file '{filePath}'.");
            }

            if (entities.Count > 1)
            {
                throw new InvalidOperationException($"[{CollectionIdentifier}] Multiple entities found in file '{filePath}'. Only one entity per file is expected when adding from file.");
            }

            return Add(entities[0]);
        }

        public List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> AddFromFiles(params string[] filesPaths)
        {
            var addedEntities = new List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>>();

            foreach (var filePath in filesPaths)
            {
                addedEntities.Add(AddFromFile(filePath));
            }

            return addedEntities;
        }

        public List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> AddFromDirectory(string directoryPath)
        {
            IoUtils.CheckDirectoryMustExist(directoryPath);

            var entities = _entitiesSearcher.Search(directoryPath);
            return Add(entities.ToArray());
        }

        public List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> AddFromDirectories(params string[] directoriesPaths)
        {
            var addedEntities = new List<IAiEntityWithTConfigurationType<TAiEntityConfiguration>>();

            foreach (var directoryPath in directoriesPaths)
            {
                addedEntities.AddRange(AddFromDirectory(directoryPath));
            }

            return addedEntities;
        }

        public virtual IAiEntityWithTConfigurationType<TAiEntityConfiguration> GetByIdentifier(string entityIdentifier)
        {
            if (TryGetByIdentifier(entityIdentifier, out var entityData))
            {
                return entityData!;
            }

            throw new InvalidOperationException($"[{CollectionIdentifier}] Entity with identifier/name '{entityIdentifier}' is not found in the collection.");
        }

        public virtual bool TryGetByIdentifier(string entityIdentifier, out IAiEntityWithTConfigurationType<TAiEntityConfiguration>? entityData)
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

        public virtual IAiEntityWithTConfigurationType<TAiEntityConfiguration> GetByFilePath(string filePath)
        {
            if (TryGetByFilePath(filePath, out var entityData))
            {
                return entityData!;
            }

            throw new InvalidOperationException($"[{CollectionIdentifier}] Entity with file path '{filePath}' is not found in the collection.");
        }

        public virtual bool TryGetByFilePath(string filePath, out IAiEntityWithTConfigurationType<TAiEntityConfiguration>? entityData)
        {
            var normalizedFilePath = IoUtils.NormalizeFilePath(filePath);
            entityData = Entities.FirstOrDefault(x => IoUtils.NormalizeFilePath(x.FilePath) == normalizedFilePath);
            return entityData != null;
        }

        private void InitializeCollectionFromDefaultLocations(ILocationsProvider? defaultLocationsProvider = null)
        {
            if (defaultLocationsProvider != null)
            {
                foreach (var location in defaultLocationsProvider)
                {
                    var entities = _entitiesSearcher.Search(location);
                    Add(entities.ToArray());
                }
            }
        }

        public IEnumerator<IAiEntityWithTConfigurationType<TAiEntityConfiguration>> GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
