using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.AI.Shared.Utils;
using DevQAProdCom.NET.Global.Utils;

namespace DevQAProdCom.NET.AI.Shared.Collections
{
    public class AiEntitiesCollection<TAiEntityYamlConfiguration> : IAiEntitiesCollection<TAiEntityYamlConfiguration>
        where TAiEntityYamlConfiguration : IAiEntityYamlConfiguration, new()
    {
        public string? BaseDirectory { get; set; }

        protected List<IAiEntityWithTYamlConfigurationType<TAiEntityYamlConfiguration>> Entities = new();

        public AiEntitiesCollection()
        {
            InitializeCollection();
        }

        public AiEntitiesCollection(string baseFolder) : this()
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
                throw new InvalidOperationException($"Multiple entities with identifier/name '{entityIdentifier}' found in the collection.");
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
                var entityData = YamlUtils.SplitEntityDataAndYamlMetaData<AiEntityWithTYamlConfigurationTypeModel<TAiEntityYamlConfiguration>, TAiEntityYamlConfiguration>(filePath);
                entityData.FilePath = filePath;
                Entities.Add(entityData);
                return entityData;
            }

            throw new Exception($"File '{filePath}' with entity is not found.");
        }

        protected List<string> GetEntitiesLocations()
        {
            var entitiesLocations = new List<string>();

            if (!string.IsNullOrEmpty(BaseDirectory))
            {
                var entities = FindEntitiesInDirectory(BaseDirectory);
                entitiesLocations.AddRange(entities);
            }
            else
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var entities = FindEntitiesInDirectory(currentDirectory);
                entitiesLocations.AddRange(entities);

                var solutionFolder = IoUtils.GetNearestSolutionDirectoryAsCurrentOrParent(currentDirectory);
                entities = FindEntitiesInDirectory(solutionFolder);
                entitiesLocations.AddRange(entities);
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
