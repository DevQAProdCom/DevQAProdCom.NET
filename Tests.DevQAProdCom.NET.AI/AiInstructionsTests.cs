using DevQAProdCom.NET.Global.Utils;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class AiInstructionsTests : BaseTest
    {
        [Test]
        public async Task ShowSessionInstructionsAgentsTest()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(ShowSessionInstructionsAgentsTest));

            await using (var agent = AiAgentsLibrary
              .GetBaseShowInstructionsAgent(workingDirectory)
              .WithFullIsolation()
              .WithSessionConfig(config => config
              .WithInstruction(Const.AiInstructions.Names.APPEND_CUSTOM_INSTRUCTION_CHECK_TO_READ_WRITE_AGENT_CONTENT_PROPERTY))
              .WithPrompt("Use 'Append CUSTOM INSTRUCTION CHECK to Read Write Agent content property' instruction to answer question 'What is my favorite animal?'")
              .WithMaxAttempts(1))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            IoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task ShowCustomConfiguredSessionInstructionsAgentsTest()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(ShowCustomConfiguredSessionInstructionsAgentsTest));

            await using (var agent = GetGitHubCopilotAiAgentInteractor()
                .WithPrimaryAgentFromFile("C:\\Files\\Dev\\DevQAProdCom.NET\\.github\\agents\\show-instructions.agent.md")
                .WithSessionConfig(config => config.WithModel("claude-haiku-4.5"))
                .WithWorkingDirectory(workingDirectory)
                .WithDefaultContentHandlers()
                .WithSelectiveIsolation()
                .WithSessionConfig(config => config
                .WithInstructionFromFile("C:\\Files\\Dev\\DevQAProdCom.NET\\.github\\instructions\\generate-random-data.instructions.md")
                .WithInstructionFromFile("C:\\Files\\Dev\\DevQAProdCom.NET\\.github\\instructions\\answer-questions.instructions.md"))
                //.WithPrompt("Use github instruction 'Append CUSTOM INSTRUCTION CHECK to Read Write Agent content property' to answer question 'What is my favorite animal?'. This instruction should have been added to the session.")
                .WithPrompt("Answer question 'What is my favorite animal?'. If several answers appear to be applicable then show array of answers.")
                .WithMaxAttempts(1))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            IoUtils.DeleteDirectory(workingDirectory);
        }
    }
}
