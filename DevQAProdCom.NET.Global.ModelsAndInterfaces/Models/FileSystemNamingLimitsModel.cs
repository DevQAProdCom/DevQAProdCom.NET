using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations;

namespace DevQAProdCom.NET.Global.ModelsAndInterfaces.Models
{
    public class FileSystemNamingLimitsModel
    {
        public int MaxFileNameWithExtensionLength { get; set; }
        public int MaxPathLength { get; set; }
        public FileSystemNamingLimitUnit Unit { get; set; }

        public int GetMaxFileNameWithoutExtensionLength(string? extension = null)
        {
            var extensionLength = extension?.Length ?? 0;
            return MaxFileNameWithExtensionLength - extensionLength;
        }
    }
}
