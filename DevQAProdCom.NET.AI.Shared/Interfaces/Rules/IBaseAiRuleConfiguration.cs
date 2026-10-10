namespace DevQAProdCom.NET.AI.Shared.Interfaces.Rules
{
    public interface IBaseAiRuleConfiguration : IAiEntityConfiguration
    {
        public List<string> ApplyTo { get; set; }
    }
}
