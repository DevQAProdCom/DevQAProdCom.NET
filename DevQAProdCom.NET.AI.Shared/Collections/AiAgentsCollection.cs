using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.AI.Shared.Utils;
using DevQAProdCom.NET.Global.Utils;

namespace DevQAProdCom.NET.AI.Shared.Collections
{
    public class AiAgentsCollection<TAiAgentYamlConfiguration> : IAiEntityWithTYamlConfigurationTypesCollection<TAiAgentYamlConfiguration>
        where TAiAgentYamlConfiguration : IBaseAiAgentYamlConfiguration, new()
    {
        public string? BaseFolder { get; set; }
        public static List<string> SharedAgentsLocations { get; set; } = new();

        protected List<IAiEntityWithTYamlConfigurationType<TAiAgentYamlConfiguration>> Agents = new();

        public AiAgentsCollection()
        {
            InitializeCollection();
        }

        public AiAgentsCollection(string baseFolder) : this()
        {
            BaseFolder = baseFolder;
        }

        public IAiEntityWithTYamlConfigurationType<TAiAgentYamlConfiguration> GetAgentData(string agentIdentifier)
        {
            if (TryGetAgentData(agentIdentifier, out var agentData))
            {
                return agentData!;
            }

            throw new InvalidOperationException($"Agent with identifier/name '{agentIdentifier}' is not found in the collection.");
        }


        public bool TryGetAgentData(string agentIdentifier, out IAiEntityWithTYamlConfigurationType<TAiAgentYamlConfiguration>? agentData)
        {
            var matchingAgents = Agents.Where(x => x.ConfigurationData.Name == agentIdentifier).ToList();

            if (matchingAgents.Count > 0)
            {
                throw new InvalidOperationException($"Multiple agents with identifier/name '{agentIdentifier}' found in the collection.");
            }

            if (matchingAgents.Count == 1)
            {
                agentData = matchingAgents.First();
                return true;
            }

            agentData = default;
            return false;
        }

        private void InitializeCollection()
        {
            var baseAgentsLocations = GetBaseAgentsLocations();
            var allAgentsLocations = new List<string>(SharedAgentsLocations);
            allAgentsLocations.AddRange(baseAgentsLocations);
            var mdFiles = IoUtils.GetMarkdownFiles(allAgentsLocations);

            foreach (var mdFile in mdFiles)
            {
                AddAgentData(mdFile);
            }
        }

        public IAiEntityWithTYamlConfigurationType<TAiAgentYamlConfiguration> AddAgentData(string filePath)
        {
            if (File.Exists(filePath))
            {
                var agentData = YamlUtils.SplitEntityDataAndYamlMetaData<AiEntityWithTYamlConfigurationTypeModel<TAiAgentYamlConfiguration>, TAiAgentYamlConfiguration>(filePath);
                agentData.FilePath = filePath;
                Agents.Add(agentData);
                return agentData;
            }

            throw new Exception($"File '{filePath}' with agent is not found.");
        }

        protected virtual List<string> GetBaseAgentsLocations() => new();
    }
}
