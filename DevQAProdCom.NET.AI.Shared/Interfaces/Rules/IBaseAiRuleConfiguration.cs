namespace DevQAProdCom.NET.AI.Shared.Interfaces.Rules
{
    public interface IBaseAiRuleConfiguration : IAiEntityIdentifiers
    {
        public List<string> ApplyTo { get; set; }
    }
}
