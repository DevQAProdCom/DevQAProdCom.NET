using System.Text.Json.Serialization;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models
{
    public class DataModel
    {
        [JsonPropertyName("data")]
        public List<string> Data { get; set; } = new List<string>();
    }
}
