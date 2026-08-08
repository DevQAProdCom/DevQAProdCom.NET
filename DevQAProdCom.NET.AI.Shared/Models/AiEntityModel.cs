using DevQAProdCom.NET.AI.Shared.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiEntityModel : IAiEntity
    {
        public string? FilePath { get; set; }
        public string Prompt { get; set; }
    }
}
