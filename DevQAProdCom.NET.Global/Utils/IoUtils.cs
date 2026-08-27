using System.Text;
using DevQAProdCom.NET.Global.Constants;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations.Files;

namespace DevQAProdCom.NET.Global.Utils
{
    public static class IoUtils
    {
        public static List<FileInfo> GetFilesInDirectory(string directoryPath, string searchPattern = "*.*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            CheckDirectoryMustExist(directoryPath);
            return Directory.GetFiles(directoryPath, searchPattern, searchOption).Select(x => new FileInfo(x)).ToList();
        }

        public static void CleanDirectory(string? directoryPath, string searchPattern = "*.*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (string.IsNullOrEmpty(directoryPath) || !DirectoryExists(directoryPath))
                return;

            // Delete all files in the directory
            List<string> files = GetFilesInDirectory(directoryPath, searchPattern, searchOption).Select(x => x.FullName).ToList();
            foreach (string file in files)
                File.Delete(file);

            // Delete all subdirectories and their contents
            string[] subdirectories = Directory.GetDirectories(directoryPath);
            foreach (string subdirectory in subdirectories)
                CleanDirectory(subdirectory); // Recursive call to clean subdirectories

            // Finally, delete the directory itself
            if (!directoryPath.EndsWith("Logs")) //TODO : Pass as configurable parameter what to exclude from deletion
                Directory.Delete(directoryPath);
        }

        public static void CopyDirectory(string sourceDirectory, string targetDirectory, Func<string, bool>? filterFiles = null)
        {
            CheckDirectoryMustExist(sourceDirectory);
            CreateDirectory(targetDirectory);

            foreach (var file in Directory.GetFiles(sourceDirectory))
            {
                if (filterFiles == null || filterFiles(Path.GetFileName(file)))
                {
                    var destFile = Path.Combine(targetDirectory, Path.GetFileName(file));
                    File.Copy(file, destFile, true);
                }
            }

            foreach (var directory in Directory.GetDirectories(sourceDirectory))
            {
                var destDir = Path.Combine(targetDirectory, Path.GetFileName(directory));
                CopyDirectory(directory, destDir, filterFiles);
            }
        }

        public static string? GetNearestDirectoryAsCurrentOrParentWithFilesWithExtensions(string initialDirectory, params string[] extensions)
        {
            var extensionsSet = new HashSet<string>(extensions
                .Where(e => !string.IsNullOrWhiteSpace(e)).Select(e => e.StartsWith(".") ? e : $".{e}"), StringComparer.OrdinalIgnoreCase);

            if (extensionsSet.Count == 0)
                return null;

            if (!TryGetDirectory(initialDirectory, out var dir))
                return null;

            while (dir != null)
            {
                if (dir.EnumerateFiles("*", SearchOption.TopDirectoryOnly).Any(f => extensionsSet.Contains(f.Extension)))
                {
                    return dir.FullName;
                }

                dir = dir.Parent;
            }

            return null;
        }

        public static string GetNearestSolutionDirectoryAsCurrentOrParent(string? initialDirectory = null)
        {
            initialDirectory ??= Directory.GetCurrentDirectory();
            var solutionDirectory = GetNearestDirectoryAsCurrentOrParentWithFilesWithExtensions(initialDirectory, FileExtension.Sln.GetDescriptionAttributeValue());

            if (solutionDirectory == null)
                throw new DirectoryNotFoundException($"No solution directory (with '{FileExtension.Sln.GetDescriptionAttributeValue()}' file) found starting from '{initialDirectory}' and moving up the directory tree.");

            return solutionDirectory;
        }

        public static List<string> GetMarkdownFiles(string initialDirectory)
        {
            if (!DirectoryExists(initialDirectory))
                return new List<string>();

            return Directory.EnumerateFiles(initialDirectory, $"*{FileExtension.Md.GetDescriptionAttributeValue()}", SearchOption.AllDirectories).ToList();
        }

        public static List<string> GetMarkdownFiles(List<string> entries)
        {
            var result = new List<string>();
            foreach (var entry in entries)
            {
                if (FileExists(entry) && Path.GetExtension(entry).Equals(FileExtension.Md.GetDescriptionAttributeValue(), StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(entry);
                }
                else if (DirectoryExists(entry))
                {
                    var mdFiles = GetMarkdownFiles(entry);
                    result.AddRange(mdFiles);
                }
            }

            return result;
        }

        public static void DeleteDirectory(string directoryPath, bool recursive = true)
        {
            if (DirectoryExists(directoryPath))
                Directory.Delete(directoryPath, recursive);
        }

        public static bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public static DirectoryInfo CheckDirectoryMustExist(string directoryPath)
        {
            if (!TryGetDirectory(directoryPath, out var directory))
                throw new DirectoryNotFoundException($"No such directory exists: '{directoryPath}'.");

            return directory!;
        }

        public static bool TryGetDirectory(string directoryPath, out DirectoryInfo? directory)
        {
            if (DirectoryExists(directoryPath))
            {
                directory = new DirectoryInfo(directoryPath);
                return true;
            }

            directory = null;
            return false;
        }

        public static DirectoryInfo CreateDirectory(string path)
        {
            if (TryGetDirectory(path, out var directory))
                return directory!;

            return Directory.CreateDirectory(path);
        }

        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static FileInfo CheckFileMustExist(string filePath)
        {
            if (!TryGetFile(filePath, out var file))
                throw new FileNotFoundException($"No such file exists: '{filePath}'.", filePath);

            return file!;
        }

        public static bool TryGetFile(string filePath, out FileInfo? file)
        {
            if (FileExists(filePath))
            {
                file = new FileInfo(filePath);
                return true;
            }

            file = null;
            return false;
        }

        public static string NormalizeFilePath(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return string.Empty;
            }

            return Path.GetFullPath(filePath).TrimEnd('\\', '/');
        }

        public static void FileCopy(string sourceFilePath, string destinationFilePath, bool overwrite = true)
        {
            CheckFileMustExist(sourceFilePath);

            var destinationDirectory = Path.GetDirectoryName(destinationFilePath);
            if (!string.IsNullOrEmpty(destinationDirectory))
            {
                CreateDirectory(destinationDirectory);
            }

            File.Copy(sourceFilePath, destinationFilePath, overwrite);
        }

        public static void WriteAllText(string filePath, string content)
        {
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                CreateDirectory(directory);
            }

            File.WriteAllText(filePath, content);
        }

        public static string ToTruncatedFileNameWithExtensionOrDefault(string fileNameWithoutExtension, string? extension = null,
            int? maxAmountOfCharsInFileName = null,
            int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = GlobalConst.Io.MINIMUM_REQUIRED_CHARS_LENGTH_OF_FILE_NAME_WITHOUT_EXTENSION_TO_APPLY_TRUNCATION)
        {
            if (fileNameWithoutExtension == null)
                throw new ArgumentNullException(nameof(fileNameWithoutExtension));

            ValidateMaxAmountOfCharsInFileName(maxAmountOfCharsInFileName, extension);

            var limits = RuntimeUtils.GetOsFileSystemLimits();
            return ToTruncatedFileNameWithExtensionOrDefault(fileNameWithoutExtension, extension, limits.MaxFileNameWithExtensionLength, limits.Unit,
                maxAmountOfCharsInFileName: maxAmountOfCharsInFileName,
                minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation: minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);
        }

        public static string ToFilePathWithFileNameTruncationWithExtensionOrDefault(string fileNameWithoutExtension, string? extension, string directoryPath,
            int? maxAmountOfCharsInFileName = null,
            int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = GlobalConst.Io.MINIMUM_REQUIRED_CHARS_LENGTH_OF_FILE_NAME_WITHOUT_EXTENSION_TO_APPLY_TRUNCATION)
        {
            if (string.IsNullOrEmpty(directoryPath))
                throw new ArgumentException(nameof(directoryPath));

            var limits = RuntimeUtils.GetOsFileSystemLimits();
            var directoryPathSize = RuntimeUtils.GetSize(directoryPath, limits.Unit);
            var separatorSize = RuntimeUtils.GetSize(Path.DirectorySeparatorChar.ToString(), limits.Unit);
            var remainingBudgetForFileNameAsPartOfheFullPath = limits.MaxPathLength - directoryPathSize - separatorSize;

            if (remainingBudgetForFileNameAsPartOfheFullPath <= 0)
                throw new Exception($"Directory path '{directoryPath}' exceeds the maximum allowed path length of {limits.MaxPathLength} {limits.Unit.ToString().ToLowerInvariant()}.");

            var fileNameBudgetSize = Math.Min(limits.MaxFileNameWithExtensionLength, remainingBudgetForFileNameAsPartOfheFullPath);
            var fileNameWithExtension = ToTruncatedFileNameWithExtensionOrDefault(fileNameWithoutExtension, extension, fileNameBudgetSize, limits.Unit,
                maxAmountOfCharsInFileName: maxAmountOfCharsInFileName,
                minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation: minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);

            return Path.Combine(directoryPath, fileNameWithExtension);
        }

        private static string ToTruncatedFileNameWithExtensionOrDefault(string fileNameWithoutExtension, string? extension, int fileNameBudgetSize, FileSystemNamingLimitUnit fileNameSizeBudgetUnitOfMeasurement,
            int? maxAmountOfCharsInFileName = null,
            int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = GlobalConst.Io.MINIMUM_REQUIRED_CHARS_LENGTH_OF_FILE_NAME_WITHOUT_EXTENSION_TO_APPLY_TRUNCATION)
        {
            ValidateMaxAmountOfCharsInFileName(maxAmountOfCharsInFileName, extension);

            if (minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation < 1)
                throw new ArgumentException($"'{nameof(minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation)}' cannot be less than 1");

            var fileNameWithoutInvalidChars = WithoutInvalidFileNameChars(fileNameWithoutExtension);
            var normalizedExtension = NormalizeExtension(extension);

            if (fileNameSizeBudgetUnitOfMeasurement == FileSystemNamingLimitUnit.Characters)
                return ToTruncatedFileNameByCharsOrDefault(fileNameWithoutInvalidChars, normalizedExtension, fileNameBudgetSize,
                    maxAmountOfCharsInFileName: maxAmountOfCharsInFileName,
                    minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation: minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);

            if (fileNameSizeBudgetUnitOfMeasurement == FileSystemNamingLimitUnit.Bytes)
                return ToTruncatedFileNameByBytesOrDefault(fileNameWithoutInvalidChars, normalizedExtension, fileNameBudgetSize,
                    maxAmountOfCharsInFileName: maxAmountOfCharsInFileName,
                    minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation: minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);

            throw new Exception($"Unsupported value/type of '{nameof(FileSystemNamingLimitUnit)}': '{fileNameSizeBudgetUnitOfMeasurement}'.");
        }

        private static string ToTruncatedFileNameByCharsOrDefault(string fileNameWithoutExtension, string? extension, int fileNameBudgetSizeInChars,
            int? maxAmountOfCharsInFileName = null,
            int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = GlobalConst.Io.MINIMUM_REQUIRED_CHARS_LENGTH_OF_FILE_NAME_WITHOUT_EXTENSION_TO_APPLY_TRUNCATION)
        {
            var fileNameWithoutExtensionWithoutInvalidChars = WithoutInvalidFileNameChars(fileNameWithoutExtension);
            var normalizedExtension = NormalizeExtension(extension);

            int maxCharsBudgetForFileNameWithExtension = 0;

            if (maxAmountOfCharsInFileName > 0)
                maxCharsBudgetForFileNameWithExtension = Math.Min(fileNameBudgetSizeInChars, maxAmountOfCharsInFileName.Value + normalizedExtension.Length);
            else
                maxCharsBudgetForFileNameWithExtension = fileNameBudgetSizeInChars;

            if (IsEnoughFileNameSectionCharsToUseFullNameWithExtension(maxCharsBudgetForFileNameWithExtension, fileNameWithoutExtensionWithoutInvalidChars, normalizedExtension))
                return fileNameWithoutExtensionWithoutInvalidChars + normalizedExtension;

            //If not enough to use the full name with extension, we need to try to make truncation
            CheckIsEnoughFileNameSectionCharsBudgetForExtensionWithAtLeastMinAmountForFileName(maxCharsBudgetForFileNameWithExtension, normalizedExtension, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);

            var maxCharsBudgetForFileNameWithoutExtension = maxCharsBudgetForFileNameWithExtension - normalizedExtension.Length;
            var truncatedFileNameWithoutExtension = TruncateFileNameWithoutExtensionByChars(fileNameWithoutExtensionWithoutInvalidChars, maxCharsBudgetForFileNameWithoutExtension, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);

            return truncatedFileNameWithoutExtension + normalizedExtension;
        }

        private static bool IsEnoughFileNameSectionCharsToUseFullNameWithExtension(int budgetSizeInChars, string fileName, string? extension)
        {
            return fileName.Length + NormalizeExtension(extension).Length <= budgetSizeInChars;
        }

        private static bool CheckIsEnoughFileNameSectionCharsBudgetForExtensionWithAtLeastMinAmountForFileName(int budgetSizeInChars, string extension,
            int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = GlobalConst.Io.MINIMUM_REQUIRED_CHARS_LENGTH_OF_FILE_NAME_WITHOUT_EXTENSION_TO_APPLY_TRUNCATION)
        {
            var extensionSizeInChars = extension.Length;
            var remainingBudgetForFileNameWithoutExtensionInChars = budgetSizeInChars - extensionSizeInChars;

            if (remainingBudgetForFileNameWithoutExtensionInChars < minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation)
                throw new Exception($"Not enough space for the filename and extension within the specified limits. Extension requires {extensionSizeInChars} chars, but only {budgetSizeInChars} chars available. " +
                    $"Minimum filename size required: {minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation} chars.");

            return true;
        }

        private static string TruncateFileNameWithoutExtensionByChars(string fileNameWithoutExtension, int budgetSizeInChars,
            int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = GlobalConst.Io.MINIMUM_REQUIRED_CHARS_LENGTH_OF_FILE_NAME_WITHOUT_EXTENSION_TO_APPLY_TRUNCATION)
        {
            if (fileNameWithoutExtension.Length <= budgetSizeInChars)
                return fileNameWithoutExtension;

            //If fileName.Length > budgetSizeInChars, then truncation is needed. But we need to check if the fileName is long enough to apply truncation.

            //Make checks to assure that fileName.Length >= minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation >= GlobalConst.XXX_TRUNCATION_INDICATOR.Length, so it is safe to truncate and append the truncation indicator.
            if (minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation < GlobalConst.XXX_TRUNCATION_INDICATOR.Length)
                throw new Exception($"Minimum length required for truncation '{minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation}' is less than the length of the truncation indicator '{GlobalConst.XXX_TRUNCATION_INDICATOR.Length}'.");

            if (fileNameWithoutExtension.Length < minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation)
                throw new Exception($"File name '{fileNameWithoutExtension}' is too short to apply truncation. Minimum length required: {minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation} chars.");

            //Previous checks assure that:
            //1) fileName.Length is greater than or equal budgetSizeInChars
            //2) fileName.Length is greater than or equal GlobalConst.XXX_TRUNCATION_INDICATOR.Length (of 3 symbols)
            //3) fileName.Length is greater than or equal minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation
            // So eventually, as far as fileName.Length >= budgetSizeInChars >= minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation >= GlobalConst.XXX_TRUNCATION_INDICATOR.Length it is safe to truncate and append the truncation indicator
            return fileNameWithoutExtension.Substring(0, budgetSizeInChars - GlobalConst.XXX_TRUNCATION_INDICATOR.Length) + GlobalConst.XXX_TRUNCATION_INDICATOR;
        }

        private static string ToTruncatedFileNameByBytesOrDefault(string fileNameWithoutExtension, string extension, int fileNameBudgetSizeInBytes,
            int? maxAmountOfCharsInFileName = null,
            int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = GlobalConst.Io.MINIMUM_REQUIRED_CHARS_LENGTH_OF_FILE_NAME_WITHOUT_EXTENSION_TO_APPLY_TRUNCATION)
        {
            var fileNameWithoutExtensionWithoutInvalidChars = WithoutInvalidFileNameChars(fileNameWithoutExtension);
            var normalizedExtension = NormalizeExtension(extension);

            int maxAmountOfCharsInFileNameSizeInBytes = 0;

            if (maxAmountOfCharsInFileName > 0)
                maxAmountOfCharsInFileNameSizeInBytes = Math.Min(RuntimeUtils.GetSize(new string('*', maxAmountOfCharsInFileName.Value), FileSystemNamingLimitUnit.Bytes), fileNameBudgetSizeInBytes);
            else
                maxAmountOfCharsInFileNameSizeInBytes = fileNameBudgetSizeInBytes;

            var fileNameWithoutInvalidCharsSizeInBytes = RuntimeUtils.GetSize(fileNameWithoutExtensionWithoutInvalidChars, FileSystemNamingLimitUnit.Bytes);
            var normalizedExtensionSizeInBytes = RuntimeUtils.GetSize(normalizedExtension, FileSystemNamingLimitUnit.Bytes);

            var maxBudgetForFileNameWithExtensionInBytes = Math.Min(fileNameBudgetSizeInBytes, maxAmountOfCharsInFileNameSizeInBytes + normalizedExtensionSizeInBytes);

            if (IsEnoughFileNameSectionInBytesToUseFullNameWithExtension(maxBudgetForFileNameWithExtensionInBytes, fileNameWithoutExtensionWithoutInvalidChars, normalizedExtension))
                return fileNameWithoutExtensionWithoutInvalidChars + normalizedExtension;

            //If not enough to use the full name with extension, we need to try to make truncation
            CheckIsEnoughFileNameSectionBytesBudgetForExtensionWithAtLeastMinAmountForFileName(maxBudgetForFileNameWithExtensionInBytes, normalizedExtension, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);

            var maxBudgetForFileNameWithoutExtensionInBytes = maxBudgetForFileNameWithExtensionInBytes - normalizedExtensionSizeInBytes;
            var truncatedFileNameWithoutExtension = TruncateFileNameWithoutExtensionByBytes(fileNameWithoutExtensionWithoutInvalidChars, maxBudgetForFileNameWithoutExtensionInBytes, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);

            return truncatedFileNameWithoutExtension + normalizedExtension;
        }

        private static bool IsEnoughFileNameSectionInBytesToUseFullNameWithExtension(int budgetSizeInBytes, string fileName, string extension)
        {
            return RuntimeUtils.GetSize(fileName, FileSystemNamingLimitUnit.Bytes) + RuntimeUtils.GetSize(extension, FileSystemNamingLimitUnit.Bytes) <= budgetSizeInBytes;
        }

        private static bool CheckIsEnoughFileNameSectionBytesBudgetForExtensionWithAtLeastMinAmountForFileName(int budgetSizeInBytes, string extension,
            int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = GlobalConst.Io.MINIMUM_REQUIRED_CHARS_LENGTH_OF_FILE_NAME_WITHOUT_EXTENSION_TO_APPLY_TRUNCATION)
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

        private static string TruncateFileNameWithoutExtensionByBytes(string fileName, int budgetSizeInBytes,
            int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = GlobalConst.Io.MINIMUM_REQUIRED_CHARS_LENGTH_OF_FILE_NAME_WITHOUT_EXTENSION_TO_APPLY_TRUNCATION)
        {
            var fileNameSizeInBytes = RuntimeUtils.GetSize(fileName, FileSystemNamingLimitUnit.Bytes);

            if (fileNameSizeInBytes <= budgetSizeInBytes)
                return fileName;

            //If fileNameSizeInBytes > budgetSizeInBytes, then truncation is needed. But we need to check if the fileName is long enough to apply truncation.

            //Make checks to assure that fileNameSizeInBytes >= minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation >= GlobalConst.XXX_TRUNCATION_INDICATOR.Length, so it is safe to truncate and append the truncation indicator.
            var xxxTruncationIndicatorSizeInBytes = RuntimeUtils.GetSize(GlobalConst.XXX_TRUNCATION_INDICATOR, FileSystemNamingLimitUnit.Bytes);
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
            return TruncateToByteLength(fileName, budgetSizeInBytes - xxxTruncationIndicatorSizeInBytes) + GlobalConst.XXX_TRUNCATION_INDICATOR;
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

        public static string NormalizeExtension(string? extension)
        {
            if (string.IsNullOrEmpty(extension))
                return string.Empty;

            return extension.StartsWith(".") ? extension : $".{extension}";
        }

        private static void ValidateMaxAmountOfCharsInFileName(int? maxAmountOfCharsInFileName, string? extension)
        {
            if (maxAmountOfCharsInFileName == null)
                return;

            var limits = RuntimeUtils.GetOsFileSystemLimits();
            var maxBudgetSizeForAmountOfCharsInFileName = limits.GetMaxFileNameWithoutExtensionLength(extension);

            if (maxAmountOfCharsInFileName < 1 || maxAmountOfCharsInFileName > maxBudgetSizeForAmountOfCharsInFileName)
                throw new ArgumentException($"'{nameof(maxAmountOfCharsInFileName)}' must be between 1 and {maxBudgetSizeForAmountOfCharsInFileName}. The specified value {maxAmountOfCharsInFileName} is not valid. The maximum allowed length for a file name without extension on this operating system is {maxBudgetSizeForAmountOfCharsInFileName} characters. " +
                                            $"(Calculated as {limits.MaxFileNameWithExtensionLength} characters for the full file name with extension, minus {extension?.Length ?? 0} characters for the extension.)");
        }
    }
}
