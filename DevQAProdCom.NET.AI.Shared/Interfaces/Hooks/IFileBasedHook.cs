using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Interfaces.Hooks
{
    public interface IFileBasedHook: IHaveStringIdentifier, IHaveDescription
    {
        public List<IDirectoryFilesData>? Data { get; set; }
        public string? EventTriggerName { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; }
        public string? NameFromFile { get; }
        public string? ContentValue { get; set; }
    }
}
