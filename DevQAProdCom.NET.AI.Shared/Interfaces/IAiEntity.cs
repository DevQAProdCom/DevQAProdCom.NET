namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiEntity : IEntityWithPrompt
    {
        public string? FilePath { get; set; }
        //public string? ToMdFile();
    }
}
