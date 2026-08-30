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
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_Skill_Be_Used_Using_SDK_Configuration_By_Identifier));

            await using (var agent = AiAgentsLibrary
                .GetBaseAgent(workingDirectory)
                .WithSelectiveIsolation()
                .WithPrimaryAgent(Const.AiAgents.Names.ANSWER_QUESTIONS_AGENT)
                .WithSessionConfig(config => config
                .WithSkill(Const.AiSkills.Names.SKILL_ANSWER_QUESTIONS_SET_1))
                .WithPrompt(Const.AiAgents.Prompts.ANSWER_QUESTION_WHAT_IS_MY_FAVORITE_ANIMAL)
                .WithMaxAttempts(1))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            IoUtils.DeleteDirectory(workingDirectory);
        }
    }
}
