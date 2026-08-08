namespace DevQAProdCom.NET.AI.Shared.Interfaces.Rules
{
    public interface IAiRulesCollection<TAiRuleYamlConfiguration> where TAiRuleYamlConfiguration : IBaseAiRuleYamlConfiguration, new()
    {
        public IAiRule<TAiRuleYamlConfiguration> GetRuleData(string agentIdentifier);
        public bool TryGetRuleData(string agentIdentifier, out IAiRule<TAiRuleYamlConfiguration>? agent);
        public IAiRule<TAiRuleYamlConfiguration> AddRuleData(string filePath);
    }
}
