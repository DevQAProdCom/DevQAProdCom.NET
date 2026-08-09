using DevQAProdCom.NET.Global.Enumerations.Files;
using DevQAProdCom.NET.Global.Extensions;

namespace DevQAProdCom.NET.Global.Utils
{
    public static class IoUtils
    {
        public static List<FileInfo> GetFilesInDirectory(string directoryPath, string searchPattern = "*.*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"No such directory exists: '{directoryPath}'.");

            return Directory.GetFiles(directoryPath, searchPattern, searchOption).Select(x => new FileInfo(x)).ToList();
        }

        public static void CleanDirectory(string directoryPath, string searchPattern = "*.*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (Directory.Exists(directoryPath))
            {
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
        }

        public static void CopyDirectory(string sourceDirectory, string targetDirectory, Func<string, bool>? filterFiles = null)
        {
            Directory.CreateDirectory(targetDirectory);

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

        public static void WriteToFile(List<List<string>> data, string filePath, string delimiter = ";")
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("Data cannot be null or empty.", nameof(data));
            }

            using (var writer = new StreamWriter(filePath))
                foreach (var row in data)
                    writer.WriteLine(string.Join(delimiter, row));
        }

        public static string? GetNearestDirectoryAsCurrentOrParentWithFilesWithExtensions(string initialDirectory, params string[] extensions)
        {
            var extensionsSet = new HashSet<string>(extensions
                .Where(e => !string.IsNullOrWhiteSpace(e)).Select(e => e.StartsWith(".") ? e : $".{e}"), StringComparer.OrdinalIgnoreCase);

            if (extensionsSet.Count == 0)
                return null;

            var dir = new DirectoryInfo(initialDirectory);

            while (dir != null)
            {
                if (dir.Exists && dir.EnumerateFiles("*", SearchOption.TopDirectoryOnly).Any(f => extensionsSet.Contains(f.Extension)))
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
            return Directory.EnumerateFiles(initialDirectory, $"*{FileExtension.Md.GetDescriptionAttributeValue()}", SearchOption.AllDirectories).ToList();
        }

        public static List<string> GetMarkdownFiles(List<string> entries)
        {
            var result = new List<string>();
            foreach (var entry in entries)
            {
                if (File.Exists(entry) && Path.GetExtension(entry).Equals(FileExtension.Md.GetDescriptionAttributeValue(), StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(entry);
                }
                else if (Directory.Exists(entry))
                {
                    var mdFiles = GetMarkdownFiles(entry);
                    result.AddRange(mdFiles);
                }
            }

            return result;
        }
    }
}
