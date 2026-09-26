using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class HookMetadataModel: IHookMetadata
    {
        public string? Identifier { get; set; }
        public string? Description { get; set; }
        public List<IDirectoryFilesData>? Data { get; set; }
    }
}
