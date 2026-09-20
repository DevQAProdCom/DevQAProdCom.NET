namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public class DirectoryFilesDataModel : IDirectoryFilesData
    {
        public string? Directory { get; set; }
        public List<string>? Files { get; set; }
    }
}
