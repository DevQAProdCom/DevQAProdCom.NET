using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.Global.Utils;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class HookModel : IHook
    {
        #region Hook Custom Metadata

        public string? Identifier { get; set; }
        public string? Description { get; set; }
        public string? EventTriggerName { get; set; }
        public List<IDirectoryFilesData>? Data { get; set; }
        public string? FilePath { get; set; }
        public string? FileName => IoUtils.GetFileName(FilePath);
        public string? NameFromFile
        {
            get
            {
                var fileName = FileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    return null;
                }

                var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                if (nameWithoutExtension.EndsWith(".hooks", StringComparison.OrdinalIgnoreCase))
                {
                    return nameWithoutExtension.Substring(0, nameWithoutExtension.Length - ".hooks".Length).Trim();
                }

                if (nameWithoutExtension.EndsWith(".hook", StringComparison.OrdinalIgnoreCase))
                {
                    return nameWithoutExtension.Substring(0, nameWithoutExtension.Length - ".hook".Length).Trim();
                }

                return nameWithoutExtension.Trim();
            }
        }

        #endregion Hook Custom Metadata

        #region Provider Specific Hook Content
        public string? Hook { get; set; }
        #endregion Provider Specific Hook Content
    }
}
