namespace DevQAProdCom.NET.AI.Shared.Interfaces.Rules
{
    public interface IAiRulesCollection<TAiRuleYamlConfiguration> where TAiRuleYamlConfiguration : IBaseAiRuleYamlConfiguration, new()
    {
        public IAiEntityWithTYamlConfigurationType<TAiRuleYamlConfiguration> GetRuleData(string agentIdentifier);
        public bool TryGetRuleData(string agentIdentifier, out IAiEntityWithTYamlConfigurationType<TAiRuleYamlConfiguration>? agent);
        public IAiEntityWithTYamlConfigurationType<TAiRuleYamlConfiguration> AddRuleData(string filePath);
    }
}
