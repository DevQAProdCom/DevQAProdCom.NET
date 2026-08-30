using System.Text.Json.Serialization;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models
{
    internal class QuestionAnswersModel
    {
        [JsonPropertyName("question")]
        public string? Question { get; set; }

        [JsonPropertyName("answers")]
        public List<string>? Answers { get; set; } = new();
    }
}
