namespace DevQAProdCom.NET.AI.Shared.Interfaces.Hooks
{
    public interface IHookMetadata
    {
        public string? Identifier { get; set; }
        public string? Description { get; set; }
        public List<IDirectoryFilesData>? Data { get; set; }
    }
}
