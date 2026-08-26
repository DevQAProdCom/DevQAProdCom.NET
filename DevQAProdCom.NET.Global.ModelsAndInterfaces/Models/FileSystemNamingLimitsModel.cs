using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations;

namespace DevQAProdCom.NET.Global.ModelsAndInterfaces.Models
{
    public class FileSystemNamingLimitsModel
    {
        public int MaxFileNameWithExtensionLength { get; set; }
        public int MaxPathLength { get; set; }
        public FileSystemNamingLimitUnit Unit { get; set; }
    }
}
