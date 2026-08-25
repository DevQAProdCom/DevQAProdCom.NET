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
            var testDirectory = PrepareTempTestWorkingDirectory();
            var (inputFilePath, inputContent, outputFolderPath, expectedOutputFilePath) =
                await PrepareReadWriteAgentTestFilesAsync(testDirectory);

            await using (var agent = AiAgentsLibrary
                .GetReadWriteAgentWithResponseValidator(testDirectory, inputFilePath, outputFolderPath, expectedOutputFilePath, inputContent)
                .WithMaxAttempts(3))
            {
                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();
                await act.Should().NotThrowAsync();
            }

            GlobalIoUtils.DeleteDirectory(testDirectory);
        }

        [Test]
        public async Task ReadWriteAgentWithInstructionsTest()
        {
            var testDirectory = PrepareTempTestWorkingDirectory();
            var (inputFilePath, inputContent, outputFolderPath, expectedOutputFilePath) =
                await PrepareReadWriteAgentTestFilesAsync(testDirectory);

            inputContent += $" {Const.AiRules.CUSTOM_INSTRUCTION_CHECK}";

            await using (var agent = AiAgentsLibrary
                .GetReadWriteAgentWithResponseValidator(testDirectory, inputFilePath, outputFolderPath, expectedOutputFilePath, inputContent)
                .WithSessionConfig(config => config.WithInstruction(Const.AiRules.Names.APPEND_CUSTOM_INSTRUCTION_CHECK_TO_READ_WRITE_AGENT_CONTENT_PROPERTY))
                .WithMaxAttempts(3))
            {
                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();
                await act.Should().NotThrowAsync();
            }

            GlobalIoUtils.DeleteDirectory(testDirectory);
        }

        [Test]
        public async Task MemorizeNumberTest()
        {
            var testDirectory = PrepareTempTestWorkingDirectory(nameof(MemorizeNumberTest));

            await using (var aiAgent = GetGitHubCopilotAiAgentInteractor()
                .WithDefaultContentHandlers()
                .WithSessionConfig(config => config.WithModel("claude-haiku-4.5"))
                .WithWorkingDirectory(testDirectory))
            {
                var randomNumber = new Random().Next(1000, 9999);

                await aiAgent
                    .WithPrompt($"Memorize number {randomNumber}.")
                    .InvokeAiAgentWithStreamingAsync();

                await aiAgent
                    .WithPrompt($"Subtract 1 from previously memorized number.")
                    .InvokeAiAgentWithStreamingAsync();
            }
            GlobalIoUtils.DeleteDirectory(testDirectory);
        }

    }
}
