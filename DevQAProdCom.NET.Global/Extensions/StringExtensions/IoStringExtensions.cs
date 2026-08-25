using System.ComponentModel.DataAnnotations;
using System.Text;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations;
using DevQAProdCom.NET.Global.Utils;

namespace DevQAProdCom.NET.Global.Extensions.StringExtensions
{
    public static class IoStringExtensions
    {
        private const string XXX = "xxx";

        public static Stream ToStream(this string @string)
        {
            if (string.IsNullOrEmpty(@string))
                throw new ArgumentException(nameof(@string));

            byte[] byteArray = Encoding.UTF8.GetBytes(@string);
            return new MemoryStream(byteArray);
        }

        public static string ToFileName(this string @string, string directoryPath, string? extension = null, int maxAmountOfChars = int.MaxValue)
        {
            if (@string == null)
                throw new ArgumentNullException(nameof(@string));

            return ToFileName(@string, extension, maxAmountOfChars);
        }

        public static string ToFilePath(this string fileName, string directoryPath, string extension, int maxAmountOfCharsInFileName = int.MaxValue)
        {
            var limits = RuntimeUtils.GetOsFileSystemLimits();
            var safeDirectoryPath = directoryPath ?? string.Empty;
            var directoryPathSize = RuntimeUtils.GetSize(safeDirectoryPath, limits.Unit);
            var separatorSize = string.IsNullOrEmpty(safeDirectoryPath) ? 0 : RuntimeUtils.GetSize(Path.DirectorySeparatorChar.ToString(), limits.Unit);
            var pathBudget = limits.MaxPathLength - directoryPathSize - separatorSize;
            var effectiveMaxFileNameSize = Math.Min(limits.MaxFileNameLength, pathBudget);

            var fileNameWithExtension = ToFileName(fileName, extension, maxAmountOfCharsInFileName, effectiveMaxFileNameSize, limits.Unit);

            return Path.Combine(safeDirectoryPath, fileNameWithExtension);
        }

        private static string ToFileName(string @string, string? extension, int maxAmountOfChars)
        {
            var limits = RuntimeUtils.GetOsFileSystemLimits();

            var fileNameWithoutInvalidChars = WithoutInvalidFileNameChars(@string);
            var normalizedExtensionChars = NormalizeExtension(extension);

            if (limits.Unit == FileSystemNamingLimitUnit.Characters)
            {
                // Calculate the maximum allowed characters for the file name with extension. Either the user-defined 'maxAmountOfChars' or the OS Max limit, whichever is smaller.
                // This always ensures that the total length of the file name (including the extension) does not exceed the OS limit.
                var maxAllowedTotalCharsForFileNameWithExtension = Math.Min(limits.MaxFileNameLength, maxAmountOfChars);
                var maxAllowedTotalCharsForFileNameWithoutExtension = Math.Max(0, maxAllowedTotalCharsForFileNameWithExtension - normalizedExtensionChars.Length);
                var truncatedFileNameWithoutExtension = TruncateWithXxxByChars(value: fileNameWithoutInvalidChars, maxLength: maxAllowedTotalCharsForFileNameWithoutExtension);
                return truncatedFileNameWithoutExtension + normalizedExtensionChars;
            }
            else if (limits.Unit == FileSystemNamingLimitUnit.Bytes)
            {
                var maxAllowedTotalCharsForFileNameWithoutExtension = Math.Max(0, maxAmountOfChars - normalizedExtensionChars.Length);
                var baseNameInChars = TruncateWithXxxByChars(value: fileNameWithoutInvalidChars, maxLength: maxAllowedTotalCharsForFileNameWithoutExtension);

                var extensionSizeInBytes = RuntimeUtils.GetSize(normalizedExtensionChars, limits.Unit);
                var totalSizeOfFileNameWithExtensionInBytes = RuntimeUtils.GetSize(baseNameInChars, limits.Unit) + extensionSizeInBytes;

                if (totalSizeOfFileNameWithExtensionInBytes > limits.MaxFileNameLength)
                {
                    var baseNameMaxSize = Math.Max(0, limits.MaxFileNameLength - extensionSizeInBytes);
                    baseNameInChars = TruncateWithXxxByBytes(value: fileNameWithoutInvalidChars, maxBytes: baseNameMaxSize);
                }

                return baseNameInChars + normalizedExtensionChars;
            }

            throw new Exception($"Unsupported value/type of '{nameof(FileSystemNamingLimitUnit)}': '{limits.Unit}'.");
        }

        public static string WithoutInvalidFileNameChars(string @string)
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

        private static string TruncateWithXxxByChars(string value, int maxLength)
        {
            if (maxLength < 0)
                throw new ArgumentException($"'{nameof(maxLength)}' cannot be negative");

            if (value.Length <= maxLength)
                return value;

            if (maxLength <= XXX.Length)
                return value.Substring(0, maxLength);

            return value.Substring(0, maxLength - XXX.Length) + XXX;
        }

        private static string TruncateWithXxxByBytes(string value, int maxBytes)
        {
            if (maxBytes < 0)
                throw new ArgumentException($"'{nameof(maxBytes)}' cannot be negative");

            if (Encoding.UTF8.GetByteCount(value) <= maxBytes)
                return value;

            if (maxBytes <= XXX.Length)
                return TruncateToByteLength(value, maxBytes);

            var withoutXxx = TruncateToByteLength(value, maxBytes - XXX.Length);
            return withoutXxx + XXX;
        }

        private static string TruncateToByteLength(string value, int maxBytes)
        {
            if (maxBytes <= 0)
                return string.Empty;

            int byteCount = 0;
            int charCount = 0;

            foreach (var c in value)
            {
                var charBytes = Encoding.UTF8.GetByteCount(new[] { c });
                if (byteCount + charBytes > maxBytes)
                    break;

                byteCount += charBytes;
                charCount++;
            }

            return value.Substring(0, charCount);
        }



        private static string NormalizeExtension(string? extension)
        {
            if (string.IsNullOrEmpty(extension))
                return string.Empty;

            return extension.StartsWith(".") ? extension : $".{extension}";
        }
    }
}
