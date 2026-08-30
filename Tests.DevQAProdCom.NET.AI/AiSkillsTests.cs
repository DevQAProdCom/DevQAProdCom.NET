using DevQAProdCom.NET.Global.Utils;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class AiSkillsTests : BaseTest
    {
        [Test]
        public async Task Should_Skill_Be_Used_Using_SDK_Configuration_By_Identifier()
        {
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_Skill_Be_Used_Using_SDK_Configuration_By_Identifier));
            var tempFilePathToWrite = GetTempFilePath(tempWorkingDirectory);

            await using (var agent = AiAgentsLibrary.GetBaseAnswerQuestionAgent(tempWorkingDirectory, tempFilePathToWrite, Const.AiAgents.Prompts.ANSWER_QUESTION_WHAT_IS_MY_FAVORITE_ANIMAL)
                .WithSessionConfig(config => config
                .WithSkill(Const.AiSkills.Names.SKILL_ANSWER_QUESTIONS_SET_1))
                )
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            IoUtils.DeleteDirectory(tempWorkingDirectory);
        }
    }
}
