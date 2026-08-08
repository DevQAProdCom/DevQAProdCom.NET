namespace DevQAProdCom.NET.AI.Shared.Interfaces.Rules
{
    public interface IBaseAiRuleYamlConfiguration : IAiEntityYamlConfiguration
    {
        public List<string> ApplyTo { get; set; }
    }
}
