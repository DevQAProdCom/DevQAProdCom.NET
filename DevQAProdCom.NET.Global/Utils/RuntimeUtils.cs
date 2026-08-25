using System.Runtime.InteropServices;
using System.Text;
using DevQAProdCom.NET.Global.Constants;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Models;

namespace DevQAProdCom.NET.Global.Utils
{
    public static class RuntimeUtils
    {
        public static OsPlatform GetOsPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return OsPlatform.Windows;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return OsPlatform.Linux;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return OsPlatform.OSX;

            return OsPlatform.Unknown;
        }

        public static FileSystemNamingLimitsModel GetOsFileSystemLimits()
        {
            switch (RuntimeUtils.GetOsPlatform())
            {
                case OsPlatform.Windows:
                    return GlobalConst.Runtime.WINDOWS_FILESYSTEM_LIMITS_MODEL;
                case OsPlatform.Linux:
                    return GlobalConst.Runtime.LINUX_FILESYSTEM_LIMITS_MODEL;
                case OsPlatform.OSX:
                    return GlobalConst.Runtime.OSX_FILESYSTEM_LIMITS_MODEL;
                default:
                    return new FileSystemNamingLimitsModel() { MaxFileNameLength = 255, MaxPathLength = 260, Unit = FileSystemNamingLimitUnit.Bytes };
            }
        }

        public static int GetSize(string value, FileSystemNamingLimitUnit unit)
        {
            return unit == FileSystemNamingLimitUnit.Bytes
                ? Encoding.UTF8.GetByteCount(value)
                : value.Length;
        }
    }
}
