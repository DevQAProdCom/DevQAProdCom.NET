namespace DevQAProdCom.NET.AI.Shared.Interfaces.Hooks
{
    public interface IHook: IHookMetadata
    {
        public string? EventTriggerName { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; }
        public string? NameFromFile { get; }
        public string? ContentValue { get; set; }
    }
}
