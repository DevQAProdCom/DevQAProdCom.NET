using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Interfaces.Hooks
{
    public interface IHook
    {
        public string? Identifier { get; set; }
        public string? Description { get; set; }
        public string? EventTriggerName { get; set; }
        public List<IKeyValuesData>? Data { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; }
        public string? NameFromFile { get; }
        public string? Hook { get; set; }
    }
}
