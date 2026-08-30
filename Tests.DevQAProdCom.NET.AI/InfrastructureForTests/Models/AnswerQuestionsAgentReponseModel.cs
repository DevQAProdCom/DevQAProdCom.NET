using System.Text.Json.Serialization;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models
{
    internal class AnswerQuestionsAgentReponseModel
    {
        [JsonPropertyName("questionsAndAnswers")]
        public List<QuestionAnswersModel> QuestionsAndAnswers { get; set; } = new List<QuestionAnswersModel>();
    }
}
