using System.Text.Json.Serialization;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models
{
    internal class AnswerQuestionsAgentReponseModel
    {
        [JsonPropertyName("questionsAndAnswers")]
        public List<QuestionAnswersModel> QuestionsAndAnswers { get; set; } = new List<QuestionAnswersModel>();


        public AnswerQuestionsAgentReponseModel() { }

        public AnswerQuestionsAgentReponseModel(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            var model = jsonString.FromJson<AnswerQuestionsAgentReponseModel>();
            this.QuestionsAndAnswers = model.QuestionsAndAnswers;
        }
    }
}
