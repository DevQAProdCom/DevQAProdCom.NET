namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models
{
    internal class QuestionAnswersModel
    {
        public string? Question { get; set; }
        public List<string>? Answers { get; set; } = new();
    }
}
