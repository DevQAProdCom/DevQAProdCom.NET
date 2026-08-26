using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Models;

namespace DevQAProdCom.NET.Global.Constants
{
    public static partial class GlobalConst
    {
        public partial class Runtime
        {
            public readonly static FileSystemNamingLimitsModel WINDOWS_FILESYSTEM_LIMITS_MODEL = new FileSystemNamingLimitsModel() { MaxFileNameWithExtensionLength = 255, MaxPathLength = 260, Unit = FileSystemNamingLimitUnit.Characters };
            public readonly static FileSystemNamingLimitsModel LINUX_FILESYSTEM_LIMITS_MODEL = new FileSystemNamingLimitsModel() { MaxFileNameWithExtensionLength = 255, MaxPathLength = 4096, Unit = FileSystemNamingLimitUnit.Bytes };
            public readonly static FileSystemNamingLimitsModel OSX_FILESYSTEM_LIMITS_MODEL = new FileSystemNamingLimitsModel() { MaxFileNameWithExtensionLength = 255, MaxPathLength = 1024, Unit = FileSystemNamingLimitUnit.Bytes };
        }
    }
}
