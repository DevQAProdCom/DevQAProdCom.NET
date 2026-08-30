namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models
{
    public class AnswerQuestionsAgentRequestModel
    {
        public List<string> Questions { get; set; } = new List<string>();
        public string FilePathToWriteResponseTo { get; set; }
    }
}
