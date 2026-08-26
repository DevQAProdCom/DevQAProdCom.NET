using System.Text;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations;
using DevQAProdCom.NET.Global.Utils;

namespace DevQAProdCom.NET.Global.Extensions.StringExtensions
{
    public static class IoStringExtensions
    {
        private const string XXX_TRUNCATION_INDICATOR = "xxx";

        public static Stream ToStream(this string @string)
        {
            if (string.IsNullOrEmpty(@string))
                throw new ArgumentException(nameof(@string));

            byte[] byteArray = Encoding.UTF8.GetBytes(@string);
            return new MemoryStream(byteArray);
        }

        public static string ToTrucatedFileNameOrDefault(this string @string, string? extension = null, int maxAmountOfChars = int.MaxValue, int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = 7)
        {
            if (@string == null)
                throw new ArgumentNullException(nameof(@string));

            var limits = RuntimeUtils.GetOsFileSystemLimits();
            return ToTruncatedFileNameOrDefault(@string, extension, maxAmountOfChars, limits.MaxFileNameWithExtensionLength, limits.Unit, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);
        }

        public static string ToFilePath(this string fileName, string extension, string directoryPath, int maxAmountOfCharsInFileName = int.MaxValue, int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = 7)
        {
            if (string.IsNullOrEmpty(directoryPath))
                throw new ArgumentException(nameof(directoryPath));

            var limits = RuntimeUtils.GetOsFileSystemLimits();
            var directoryPathSize = RuntimeUtils.GetSize(directoryPath, limits.Unit);
            var separatorSize = string.IsNullOrEmpty(directoryPath) ? 0 : RuntimeUtils.GetSize(Path.DirectorySeparatorChar.ToString(), limits.Unit);
            var remainingBudgetForFileNameAsPartOfheFullPath = limits.MaxPathLength - directoryPathSize - separatorSize;
            var fileNameBudgetSize = Math.Min(limits.MaxFileNameWithExtensionLength, remainingBudgetForFileNameAsPartOfheFullPath);

            var fileNameWithExtension = ToTruncatedFileNameOrDefault(fileName, extension, maxAmountOfCharsInFileName, fileNameBudgetSize, limits.Unit, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);

            return Path.Combine(directoryPath, fileNameWithExtension);
        }

        private static string ToTruncatedFileNameOrDefault(string @string, string? extension, int maxAmountOfCharsInFileName, int fileNameBudgetSizeInChars, FileSystemNamingLimitUnit fileNameSizeBudgetUnitOfMeasurement, int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = 7)
        {
            if (minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation < 1)
                throw new ArgumentException($"'{nameof(minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation)}' cannot be less than 1");

            var fileNameWithoutInvalidChars = WithoutInvalidFileNameChars(@string);
            var normalizedExtension = NormalizeExtension(extension);

            if (fileNameSizeBudgetUnitOfMeasurement == FileSystemNamingLimitUnit.Characters)
                return ToTruncatedFileNameByCharsOrDefault(fileNameWithoutInvalidChars, normalizedExtension, maxAmountOfCharsInFileName, fileNameBudgetSizeInChars, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);

            if (fileNameSizeBudgetUnitOfMeasurement == FileSystemNamingLimitUnit.Bytes)
            {
                var fileNameBudgetSizeInBytes = RuntimeUtils.GetSize(new string('*', fileNameBudgetSizeInChars), FileSystemNamingLimitUnit.Bytes);
                return ToTruncatedFileNameByBytesOrDefault(fileNameWithoutInvalidChars, normalizedExtension, maxAmountOfCharsInFileName, fileNameBudgetSizeInBytes, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);
            }

            throw new Exception($"Unsupported value/type of '{nameof(FileSystemNamingLimitUnit)}': '{fileNameSizeBudgetUnitOfMeasurement}'.");
        }

        private static string ToTruncatedFileNameByCharsOrDefault(string fileNameWithoutExtensionWithoutInvalidChars, string normalizedExtension, int maxAmountOfCharsInFileName, int fileNameBudgetSizeInChars, int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation)
        {
            var maxCharsBudgetForFileNameWithExtension = Math.Min(fileNameBudgetSizeInChars, maxAmountOfCharsInFileName + normalizedExtension.Length);

            if (IsEnoughFileNameSectionCharsToUseFullNameWithExtension(maxCharsBudgetForFileNameWithExtension, fileNameWithoutExtensionWithoutInvalidChars, normalizedExtension))
                return fileNameWithoutExtensionWithoutInvalidChars + normalizedExtension;

            //If not enough to use the full name with extension, we need to try to make truncation
            var maxCharsBudgetForFileNameWithoutExtension = maxCharsBudgetForFileNameWithExtension - normalizedExtension.Length;
            CheckIsEnoughFileNameSectionCharsBudgetForExtensionWithAtLeastMinAmountForFileName(maxCharsBudgetForFileNameWithoutExtension, normalizedExtension, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);
            var truncatedFileNameWithoutExtension = TruncateFileNameWithoutExtensionByChars(fileNameWithoutExtensionWithoutInvalidChars, maxCharsBudgetForFileNameWithoutExtension, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);

            return truncatedFileNameWithoutExtension + normalizedExtension;
        }

        private static bool IsEnoughFileNameSectionCharsToUseFullNameWithExtension(int budgetSizeInChars, string fileName, string extension)
        {
            return fileName.Length + extension.Length <= budgetSizeInChars;
        }

        private static bool CheckIsEnoughFileNameSectionCharsBudgetForExtensionWithAtLeastMinAmountForFileName(int budgetSizeInChars, string extension, int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = 10)
        {
            var extensionSizeInChars = extension.Length;
            var remainingBudgetForFileNameWithoutExtensionInChars = budgetSizeInChars - extensionSizeInChars;

            if (remainingBudgetForFileNameWithoutExtensionInChars < minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation)
                throw new Exception($"Not enough space for the filename and extension within the specified limits. Extension requires {extensionSizeInChars} chars, but only {budgetSizeInChars} chars available. " +
                    $"Minimum filename size required: {minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation} chars.");

            return true;
        }

        private static string TruncateFileNameWithoutExtensionByChars(string fileName, int budgetSizeInChars, int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation)
        {
            if (fileName.Length <= budgetSizeInChars)
                return fileName;

            //If fileName.Length > budgetSizeInChars, then truncation is needed. But we need to check if the fileName is long enough to apply truncation.

            //Make checks to assure that fileName.Length >= minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation >= XXX_TRUNCATION_INDICATOR.Length, so it is safe to truncate and append the truncation indicator.
            if (minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation < XXX_TRUNCATION_INDICATOR.Length)
                throw new Exception($"Minimum length required for truncation '{minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation}' is less than the length of the truncation indicator '{XXX_TRUNCATION_INDICATOR.Length}'.");

            if (fileName.Length < minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation)
                throw new Exception($"File name '{fileName}' is too short to apply truncation. Minimum length required: {minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation} chars.");

            //Previous checks assure that:
            //1) fileName.Length is greater than or equal budgetSizeInChars
            //2) fileName.Length is greater than or equal XXX_TRUNCATION_INDICATOR.Length (of 3 symbols)
            //3) fileName.Length is greater than or equal minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation
            // So eventually, as far as fileName.Length >= budgetSizeInChars >= minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation >= XXX_TRUNCATION_INDICATOR.Length it is safe to truncate and append the truncation indicator
            return fileName.Substring(0, budgetSizeInChars - XXX_TRUNCATION_INDICATOR.Length) + XXX_TRUNCATION_INDICATOR;
        }

        private static string ToTruncatedFileNameByBytesOrDefault(string fileNameWithoutInvalidChars, string normalizedExtension, int maxAmountOfCharsInFileName, int fileNameBudgetSizeInBytes, int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation)
        {
            var maxAmountOfCharsInFileNameSizeInBytes = RuntimeUtils.GetSize(new string('*', maxAmountOfCharsInFileName), FileSystemNamingLimitUnit.Bytes);
            var fileNameWithoutInvalidCharsSizeInBytes = RuntimeUtils.GetSize(fileNameWithoutInvalidChars, FileSystemNamingLimitUnit.Bytes);
            var normalizedExtensionSizeInBytes = RuntimeUtils.GetSize(normalizedExtension, FileSystemNamingLimitUnit.Bytes);

            var maxBudgetForFileNameWithExtensionInBytes = Math.Min(fileNameBudgetSizeInBytes, maxAmountOfCharsInFileNameSizeInBytes + normalizedExtensionSizeInBytes);

            if (IsEnoughFileNameSectionInBytesToUseFullNameWithExtension(maxBudgetForFileNameWithExtensionInBytes, fileNameWithoutInvalidChars, normalizedExtension))
                return fileNameWithoutInvalidChars + normalizedExtension;

            //If not enough to use the full name with extension, we need to try to make truncation
            var maxBudgetForFileNameWithoutExtensionInBytes = maxBudgetForFileNameWithExtensionInBytes - normalizedExtensionSizeInBytes;
            CheckIsEnoughFileNameSectionBytesBudgetForExtensionWithAtLeastMinAmountForFileName(maxBudgetForFileNameWithExtensionInBytes, normalizedExtension, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);
            var truncatedFileNameWithoutExtension = TruncateFileNameWithoutExtensionByBytes(fileNameWithoutInvalidChars, maxBudgetForFileNameWithoutExtensionInBytes, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);

            return truncatedFileNameWithoutExtension + normalizedExtension;
        }

        private static bool IsEnoughFileNameSectionInBytesToUseFullNameWithExtension(int budgetSizeInBytes, string fileName, string extension)
        {
            return RuntimeUtils.GetSize(fileName, FileSystemNamingLimitUnit.Bytes) + RuntimeUtils.GetSize(extension, FileSystemNamingLimitUnit.Bytes) <= budgetSizeInBytes;
        }

        private static bool CheckIsEnoughFileNameSectionBytesBudgetForExtensionWithAtLeastMinAmountForFileName(int budgetSizeInBytes, string extension, int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = 7)
        {
            var extensionSizeInBytes = RuntimeUtils.GetSize(extension, FileSystemNamingLimitUnit.Bytes);
            var remainingBudgetForFileNameWithoutExtensionInBytes = budgetSizeInBytes - extensionSizeInBytes;
            var minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncationInBytes = RuntimeUtils.GetSize(new string('*', minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation), FileSystemNamingLimitUnit.Bytes);

            if (remainingBudgetForFileNameWithoutExtensionInBytes < minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncationInBytes)
            {
                var bytesPerChar = minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncationInBytes / (double)minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation;
                throw new Exception($"Not enough space for the filename and extension within the specified limits. " +
                    $"Extension requires {extensionSizeInBytes} bytes ({extension.Length} chars), but only {budgetSizeInBytes} bytes available. " +
                    $"Minimum filename size required: {minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation} chars ({minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncationInBytes} bytes, ~{bytesPerChar:F2} bytes per char). " +
                    $"Remaining budget for filename: {remainingBudgetForFileNameWithoutExtensionInBytes} bytes.");
            }

            return true;
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

        private static string TruncateFileNameWithoutExtensionByBytes(string fileName, int budgetSizeInBytes, int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation)
        {
            var fileNameSizeInBytes = RuntimeUtils.GetSize(fileName, FileSystemNamingLimitUnit.Bytes);

            if (fileNameSizeInBytes <= budgetSizeInBytes)
                return fileName;

            //If fileNameSizeInBytes > budgetSizeInBytes, then truncation is needed. But we need to check if the fileName is long enough to apply truncation.

            //Make checks to assure that fileNameSizeInBytes >= minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation >= XXX_TRUNCATION_INDICATOR.Length, so it is safe to truncate and append the truncation indicator.
            var xxxTruncationIndicatorSizeInBytes = RuntimeUtils.GetSize(XXX_TRUNCATION_INDICATOR, FileSystemNamingLimitUnit.Bytes);
            var minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncationSizeInBytes = RuntimeUtils.GetSize(new string('*', minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation), FileSystemNamingLimitUnit.Bytes);

            if (minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncationSizeInBytes < xxxTruncationIndicatorSizeInBytes)
                throw new Exception($"Minimum length required for truncation ('{minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation}' chars = {minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncationSizeInBytes} bytes) is less than the truncation indicator size ({xxxTruncationIndicatorSizeInBytes} bytes).");

            if (fileNameSizeInBytes < minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncationSizeInBytes)
                throw new Exception($"File name '{fileName}' ({fileNameSizeInBytes} bytes) is too short to apply truncation. Minimum length required: {minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation} chars ({minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncationSizeInBytes} bytes).");

            //Previous checks assure that:
            //1) fileNameSizeInBytes is greater than or equal budgetSizeInBytes
            //2) fileNameSizeInBytes is greater than or equal xxxTruncationIndicatorSizeInBytes (of 3 symbols)
            //3) fileNameSizeInBytes is greater than or equal minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncationSizeInBytes
            // So eventually, as far as fileNameSizeInBytes >= budgetSizeInBytes >= minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncationSizeInBytes >= xxxTruncationIndicatorSizeInBytes it is safe to truncate and append the truncation indicator
            return TruncateToByteLength(fileName, budgetSizeInBytes - xxxTruncationIndicatorSizeInBytes) + XXX_TRUNCATION_INDICATOR;
        }

        private static string TruncateToByteLength(string value, int maxBytes)
        {
            if (maxBytes <= 0)
                return string.Empty;

            int byteCount = 0;
            int charCount = 0;

            foreach (var @char in value)
            {
                var charBytes = Encoding.UTF8.GetByteCount(new[] { @char });
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
