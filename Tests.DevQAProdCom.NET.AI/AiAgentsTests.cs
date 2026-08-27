using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Tests.DevQAProdCom.NET.AI.Constants;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class AiAgentsTests : BaseTest
    {
        [Test]
        public async Task ReadWriteAgentTest()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(ReadWriteAgentTest));
            var (inputFilePath, inputContent, outputFolderPath, expectedOutputFilePath) =
                await PrepareReadWriteAgentTestFilesAsync(workingDirectory);

            await using (var agent = AiAgentsLibrary
                .GetReadWriteAgentWithResponseValidator(workingDirectory, inputFilePath, outputFolderPath, expectedOutputFilePath, inputContent)
                .WithMaxAttempts(3))
            {
                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();
                await act.Should().NotThrowAsync();
            }

            GlobalIoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task ReadWriteAgentWithInstructionsTest()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(ReadWriteAgentWithInstructionsTest));
            var (inputFilePath, inputContent, outputFolderPath, expectedOutputFilePath) =
                await PrepareReadWriteAgentTestFilesAsync(workingDirectory);

            inputContent += $" {Const.AiRules.CUSTOM_INSTRUCTION_CHECK}";

            await using (var agent = AiAgentsLibrary
              .GetReadWriteAgentWithResponseValidator(workingDirectory, inputFilePath, outputFolderPath, expectedOutputFilePath, inputContent)
              .WithSessionConfig(config => config.WithInstruction(Const.AiRules.Names.APPEND_CUSTOM_INSTRUCTION_CHECK_TO_READ_WRITE_AGENT_CONTENT_PROPERTY))
              .WithMaxAttempts(1))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            //await using (var agent = AiAgentsLibrary
            //    .GetReadWriteAgentWithResponseValidator(workingDirectory, inputFilePath, outputFolderPath, expectedOutputFilePath, inputContent)
            //    .WithSessionConfig(config => config.WithInstruction(Const.AiRules.Names.APPEND_CUSTOM_INSTRUCTION_CHECK_TO_READ_WRITE_AGENT_CONTENT_PROPERTY))
            //    .WithMaxAttempts(3))
            //{
            //    var act = async () => await agent.InvokeAiAgentWithStreamingAsync();
            //    await act.Should().NotThrowAsync();
            //}

            GlobalIoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task ShowSessionInstructionsAgentsTest()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(ShowSessionInstructionsAgentsTest));

            await using (var agent = AiAgentsLibrary
              .GetBaseShowInstructionsAgent(workingDirectory)
              .WithFullIsolation()
              .WithSessionConfig(config => config
              .WithInstruction(Const.AiRules.Names.APPEND_CUSTOM_INSTRUCTION_CHECK_TO_READ_WRITE_AGENT_CONTENT_PROPERTY))
              .WithPrompt("Use 'Append CUSTOM INSTRUCTION CHECK to Read Write Agent content property' instruction to answer question 'What is my favorite animal?'")
              .WithMaxAttempts(1))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            GlobalIoUtils.DeleteDirectory(workingDirectory);
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

            GlobalIoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task MemorizeNumberTest()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(MemorizeNumberTest));

            await using (var aiAgent = GetGitHubCopilotAiAgentInteractor()
                .WithDefaultContentHandlers()
                .WithSessionConfig(config => config.WithModel("claude-haiku-4.5"))
                .WithWorkingDirectory(workingDirectory))
            {
                var randomNumber = new Random().Next(1000, 9999);

                await aiAgent
                    .WithPrompt($"Memorize number {randomNumber}.")
                    .InvokeAiAgentWithStreamingAsync();

                await aiAgent
                    .WithPrompt($"Subtract 1 from previously memorized number.")
                    .InvokeAiAgentWithStreamingAsync();
            }
            GlobalIoUtils.DeleteDirectory(workingDirectory);
        }
    }
}
