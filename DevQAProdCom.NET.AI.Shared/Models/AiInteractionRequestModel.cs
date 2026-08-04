using DevQAProdCom.NET.AI.Shared.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Models
{
    public class AiInteractionRequestModel : IAiInteractionRequest
    {
        public string Prompt { get; set; }
    }
}
