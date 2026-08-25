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

        public static string ToFileName(this string @string, string directoryPath, string? extension = null, int maxAmountOfChars = int.MaxValue, int minimumFileNameSizeToApplyTruncation = 7)
        {
            if (@string == null)
                throw new ArgumentNullException(nameof(@string));

            var limits = RuntimeUtils.GetOsFileSystemLimits();
            return ToFileName(@string, extension, maxAmountOfChars, limits.MaxFileNameLength, limits.Unit, minimumFileNameSizeToApplyTruncation);
        }

        public static string ToFilePath(this string fileName, string directoryPath, string extension, int maxAmountOfCharsInFileName = int.MaxValue, int minimumFileNameSizeToApplyTruncation = 7)
        {
            var limits = RuntimeUtils.GetOsFileSystemLimits();
            var safeDirectoryPath = directoryPath ?? string.Empty;
            var directoryPathSize = RuntimeUtils.GetSize(safeDirectoryPath, limits.Unit);
            var separatorSize = string.IsNullOrEmpty(safeDirectoryPath) ? 0 : RuntimeUtils.GetSize(Path.DirectorySeparatorChar.ToString(), limits.Unit);
            var pathBudget = limits.MaxPathLength - directoryPathSize - separatorSize;
            var effectiveMaxFileNameSize = Math.Min(limits.MaxFileNameLength, pathBudget);

            var fileNameWithExtension = ToFileName(fileName, extension, maxAmountOfCharsInFileName, effectiveMaxFileNameSize, limits.Unit, minimumFileNameSizeToApplyTruncation);

            return Path.Combine(safeDirectoryPath, fileNameWithExtension);
        }

        private static string ToFileName(string @string, string? extension, int maxAmountOfChars, int osMaxFileNameSize, FileSystemNamingLimitUnit unit, int minimumFileNameSizeToApplyTruncation = 7)
        {
            if (minimumFileNameSizeToApplyTruncation < 1)
                throw new ArgumentException($"'{nameof(minimumFileNameSizeToApplyTruncation)}' cannot be less than 1");

            var fileNameWithoutInvalidChars = WithoutInvalidFileNameChars(@string);
            var normalizedExtensionChars = NormalizeExtension(extension);

            if (unit == FileSystemNamingLimitUnit.Characters)
            {
                var maxAvailableTotalCharsForFileNameWithExtension = Math.Min(osMaxFileNameSize, maxAmountOfChars);
                var maxAvailableTotalCharsForFileNameWithoutExtension = maxAvailableTotalCharsForFileNameWithExtension - normalizedExtensionChars.Length;

                if (maxAvailableTotalCharsForFileNameWithoutExtension < 1)
                    throw new ArgumentException("Not enough space for the filename and extension within the specified max OS allowed size limits.", nameof(extension));

                var truncatedFileNameWithoutExtension = TruncateWithXxxByChars(value: fileNameWithoutInvalidChars, maxLength: maxAvailableTotalCharsForFileNameWithoutExtension,
                    minimumFileNameSizeToApplyTruncation: minimumFileNameSizeToApplyTruncation);
                return truncatedFileNameWithoutExtension + normalizedExtensionChars;
            }
            else if (unit == FileSystemNamingLimitUnit.Bytes)
            {
                var maxAvailableTotalCharsForFileNameWithoutExtension = maxAmountOfChars - normalizedExtensionChars.Length;

                if (maxAvailableTotalCharsForFileNameWithoutExtension < 1)
                    throw new ArgumentException("Not enough space for the filename and extension within the specified limits.", nameof(extension));

                var baseNameInChars = TruncateWithXxxByChars(value: fileNameWithoutInvalidChars, maxLength: maxAvailableTotalCharsForFileNameWithoutExtension, minimumFileNameSizeToApplyTruncation: minimumFileNameSizeToApplyTruncation);

                var extensionSizeInBytes = RuntimeUtils.GetSize(normalizedExtensionChars, unit);
                var totalSizeOfFileNameWithExtensionInBytes = RuntimeUtils.GetSize(baseNameInChars, unit) + extensionSizeInBytes;

                if (totalSizeOfFileNameWithExtensionInBytes > osMaxFileNameSize)
                {
                    var baseNameMaxSize = osMaxFileNameSize - extensionSizeInBytes;

                    if (TruncateToByteLength(fileNameWithoutInvalidChars, baseNameMaxSize).Length < 1)
                        throw new ArgumentException("Not enough space for the filename and extension within the specified limits.", nameof(extension));

                    baseNameInChars = TruncateWithXxxByBytes(value: fileNameWithoutInvalidChars, maxBytes: baseNameMaxSize, minimumFileNameSizeToApplyTruncation: minimumFileNameSizeToApplyTruncation);
                }

                return baseNameInChars + normalizedExtensionChars;
            }

            throw new Exception($"Unsupported value/type of '{nameof(FileSystemNamingLimitUnit)}': '{unit}'.");
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

        private static string TruncateWithXxxByChars(string value, int maxLength, int minimumFileNameSizeToApplyTruncation)
        {
            if (maxLength < 0)
                throw new ArgumentException($"'{nameof(maxLength)}' cannot be negative");

            if (value.Length <= maxLength)
                return value;

            if (maxLength < minimumFileNameSizeToApplyTruncation + XXX.Length)
                return TruncateByCharsOrDefault(value, maxLength);

            return value.Substring(0, maxLength - XXX.Length) + XXX;
        }

        private static string TruncateByCharsOrDefault(string value, int maxLength)
        {
            if (maxLength <= 0)
                throw new ArgumentException($"'{nameof(maxLength)}' cannot be negative");

            if (value.Length <= maxLength)
                return value;

            return value.Substring(0, maxLength);
        }

        private static string TruncateWithXxxByBytes(string value, int maxBytes, int minimumFileNameSizeToApplyTruncation)
        {
            if (maxBytes < 0)
                throw new ArgumentException($"'{nameof(maxBytes)}' cannot be negative");

            if (Encoding.UTF8.GetByteCount(value) <= maxBytes)
                return value;

            var rawPartMaxBytes = maxBytes - XXX.Length;
            if (rawPartMaxBytes < 0)
                return TruncateToByteLength(value, maxBytes);

            var rawPart = TruncateToByteLength(value, rawPartMaxBytes);
            if (rawPart.Length >= minimumFileNameSizeToApplyTruncation)
                return rawPart + XXX;

            return TruncateToByteLength(value, maxBytes);
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
