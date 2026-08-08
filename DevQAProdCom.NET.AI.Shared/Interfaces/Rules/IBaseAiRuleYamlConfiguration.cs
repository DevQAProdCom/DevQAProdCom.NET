namespace DevQAProdCom.NET.AI.Shared.Interfaces.Rules
{
    public interface IBaseAiRuleYamlConfiguration : IBaseAiEntityYamlConfiguration
    {
        public List<string> ApplyTo { get; set; }
    }
}
