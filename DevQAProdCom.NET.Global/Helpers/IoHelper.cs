namespace DevQAProdCom.NET.Global.Helpers
{
    public static class IoHelper
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
                if (!directoryPath.EndsWith("Logs"))
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
    }
}
