using System.Text.Json.Serialization;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models
{
    public class ContentModel
    {
        [JsonPropertyName("content")]
        public string? Content { get; set; }
    }
}
