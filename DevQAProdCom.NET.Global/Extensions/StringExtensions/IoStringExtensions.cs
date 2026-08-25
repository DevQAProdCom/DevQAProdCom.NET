using System.Runtime.InteropServices;
using System.Text;
using DevQAProdCom.NET.Global.Constants;

namespace DevQAProdCom.NET.Global.Extensions.StringExtensions
{
    public static class IoStringExtensions
    {
        public static Stream ToStream(this string @string)
        {
            if (string.IsNullOrEmpty(@string))
                throw new ArgumentException(nameof(@string));

            byte[] byteArray = Encoding.UTF8.GetBytes(@string);
            return new MemoryStream(byteArray);
        }

        private static string CleanFileName(string @string)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var cleaned = new StringBuilder(@string.Length);

            foreach (var c in @string)
            {
                if (invalidChars.Contains(c))
                    cleaned.Append('_');
                else
                    cleaned.Append(c);
            }

            return cleaned.ToString();
        }

        private static string NormalizeExtension(string? extension)
        {
            if (string.IsNullOrEmpty(extension))
                return string.Empty;

            var cleaned = CleanFileName(extension);
            return cleaned.StartsWith(".") ? cleaned : "." + cleaned;
        }



        private static int GetOsMaxFileNameLength()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? 255 : 255;
        }

        private static int GetOsMaxPathLength()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? 260 : 4096;
        }


        public static string ToFileName(this string @string, string directoryPath, string? extension = null, int maxAmountOfSymbols = GlobalConst.Io.MAX_WINDOWS_FILE_NAME_SIZE_IN_CHARACTERS)
        {
            if (@string == null)
                throw new ArgumentNullException(nameof(@string));

            var cleaned = CleanFileName(@string);
            var normalizedExtension = NormalizeExtension(extension);
            var osMaxFileNameLength = GetOsMaxFileNameLength();
            var effectiveMax = Math.Min(maxAmountOfSymbols, osMaxFileNameLength);

            if (!string.IsNullOrEmpty(normalizedExtension))
                effectiveMax = Math.Max(0, effectiveMax - normalizedExtension.Length);

            var fileNamePart = TruncateWithXXX(cleaned, effectiveMax);
            return fileNamePart + normalizedExtension;
        }

        public static string ToFilePath(this string fileName, string directoryPath, string extension, int maxAmountOfSymbols = GlobalConst.Io.MAX_WINDOWS_FILE_NAME_SIZE_IN_CHARACTERS)
        {
            var normalizedExtension = NormalizeExtension(extension);
            var osMaxFileNameLength = GetOsMaxFileNameLength();
            var osMaxPathLength = GetOsMaxPathLength();
            var separatorLength = string.IsNullOrEmpty(directoryPath) ? 0 : 1;
            var maxFileNameFromPath = osMaxPathLength - directoryPath.Length - separatorLength - normalizedExtension.Length;
            var effectiveMax = Math.Min(maxAmountOfSymbolsInFileName, maxFileNameFromPath);
            effectiveMax = Math.Min(effectiveMax, osMaxFileNameLength - normalizedExtension.Length);

            return Path.Combine(directoryPath, fileName.ToFileName(directoryPath, extension, effectiveMax));
        }

        public static string TruncateWithXXX(string value, int maxLength)
        {
            if (maxLength <= 0 || value.Length <= maxLength)
                return value;

            if (maxLength <= 3)
                return value.Substring(0, maxLength);

            return value.Substring(0, maxLength - 3) + "xxx";
        }
    }
}
