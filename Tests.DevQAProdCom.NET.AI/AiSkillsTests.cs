using DevQAProdCom.NET.Global.Utils;
using FluentAssertions;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models;
using Tests.DevQAProdCom.NET.AI.TestData;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class AiSkillsTests : BaseTest
    {
        [Test]
        public async Task Should_Skill_Be_Used_Using_SDK_Configuration_By_Identifier()
        {
            //GIVEN
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_Skill_Be_Used_Using_SDK_Configuration_By_Identifier));

            var requestModel = GetAnswerQuestionsAgentRequestModel(tempWorkingDirectory, new List<string> { ExpectedValues.WHAT_IS_MY_FAVORITE_ANIMAL });
            var expectedResponse = new AnswerQuestionsAgentReponseModel()
            {
                QuestionsAndAnswers = new List<QuestionAnswersModel> {
                    new QuestionAnswersModel {
                        Question = ExpectedValues.WHAT_IS_MY_FAVORITE_ANIMAL,
                        Answers = new(){ ExpectedValues.GetMyFavoriteAnimalIsLion (Const.AiSkills.Names.SKILL_ANSWER_QUESTIONS_SET_1)} } }
            };

            //WHEN
            await using (var agent = AiAgentsLibrary.GetBaseAnswerQuestionAgent(tempWorkingDirectory, requestModel.FilePathToWriteResponseTo, requestModel)
                .WithSessionConfig(config => config
                .WithSkill(Const.AiSkills.Names.SKILL_ANSWER_QUESTIONS_SET_1)))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            //THEN
            var actualResponse = new AnswerQuestionsAgentReponseModel(requestModel.FilePathToWriteResponseTo);
            actualResponse.Should().BeEquivalentTo(expectedResponse);

            //TEAR DOWN
            IoUtils.DeleteDirectory(tempWorkingDirectory);
        }
    }
}
