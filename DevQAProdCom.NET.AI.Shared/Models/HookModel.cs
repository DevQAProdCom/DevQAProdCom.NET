using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class HookModel : IHook
    {
        #region Hook Custom Metadata

        public string? Identifier { get; set; }
        public string? Description { get; set; }
        public string? EventTriggerName { get; set; }
        public List<IKeyValuesData>? Data { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; }
        public string? NameFromFile { get; }

        #endregion Hook Custom Metadata

        #region Provider Specific Hook Content
        public string? Hook { get; set; }
        #endregion Provider Specific Hook Content
    }
}
