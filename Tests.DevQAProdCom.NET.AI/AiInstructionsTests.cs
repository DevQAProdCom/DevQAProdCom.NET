using DevQAProdCom.NET.Global.Utils;
using FluentAssertions;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models;
using Tests.DevQAProdCom.NET.AI.TestData;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class AiInstructionsTests : BaseTest
    {
        [Test]
        public async Task Should_Instruction_Be_Used_Using_SDK_Configuration_By_Identifier()
        {
            //GIVEN
            var (tempWorkingDirectory, requestModel, expectedResponse) = PrepareAnswerQuestionsAgentTestDataForAnswerQuestionsSet1Instructions(nameof(Should_Instruction_Be_Used_Using_SDK_Configuration_By_Identifier));

            //WHEN
            await using (var agent = AiAgentsLibrary.GetBaseAnswerQuestionAgent(tempWorkingDirectory, requestModel.FilePathToWriteResponseTo, requestModel)
                .WithSessionConfig(config => config
                .WithInstruction(Const.AiInstructions.Names.ANSWER_QUESTIONS_SET_1_INSTRUCTIONS)))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            //THEN
            var actualResponse = new AnswerQuestionsAgentReponseModel(requestModel.FilePathToWriteResponseTo);
            actualResponse.Should().BeEquivalentTo(expectedResponse);

            //TEAR DOWN
            IoUtils.DeleteDirectory(tempWorkingDirectory);
        }

        [Test]
        public async Task Should_Instruction_Be_Used_Using_Agent_Custom_Instructions_Field_By_Identifier()
        {
            //GIVEN
            var (tempWorkingDirectory, requestModel, expectedResponse) = PrepareAnswerQuestionsAgentTestDataForAnswerQuestionsSet1Instructions(nameof(Should_Instruction_Be_Used_Using_Agent_Custom_Instructions_Field_By_Identifier));

            //WHEN
            await using (var agent = AiAgentsLibrary.GetCheckCustomInstructionsFieldAnswerQuestionsAgent(tempWorkingDirectory, requestModel.FilePathToWriteResponseTo, requestModel)
                .WithSessionConfig(config => config
                .WithInstruction(Const.AiInstructions.Names.ANSWER_QUESTIONS_SET_1_INSTRUCTIONS)))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            //THEN
            var actualResponse = new AnswerQuestionsAgentReponseModel(requestModel.FilePathToWriteResponseTo);
            actualResponse.Should().BeEquivalentTo(expectedResponse);

            //TEAR DOWN
            IoUtils.DeleteDirectory(tempWorkingDirectory);
        }

        private (string TempWorkingDirectory, AnswerQuestionsAgentRequestModel RequestModel, AnswerQuestionsAgentReponseModel ExpectedResponse) PrepareAnswerQuestionsAgentTestDataForAnswerQuestionsSet1Instructions(string testMethodName)
        {
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(testMethodName);
            var requestModel = GetAnswerQuestionsAgentRequestModel(tempWorkingDirectory, new List<string> { ExpectedValues.WHAT_IS_MY_FAVORITE_ANIMAL });
            var expectedResponse = new AnswerQuestionsAgentReponseModel()
            {
                QuestionsAndAnswers = new List<QuestionAnswersModel> {
                    new QuestionAnswersModel {
                        Question = ExpectedValues.WHAT_IS_MY_FAVORITE_ANIMAL,
                        Answers = new(){ ExpectedValues.GetMyFavoriteAnimalIsWolf(Const.AiInstructions.Names.ANSWER_QUESTIONS_SET_1_INSTRUCTIONS) } } }
            };
            return (tempWorkingDirectory, requestModel, expectedResponse);
        }
    }
}
