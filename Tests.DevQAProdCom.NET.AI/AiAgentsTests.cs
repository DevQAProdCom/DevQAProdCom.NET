using DevQAProdCom.NET.Global.Utils;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

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

            IoUtils.DeleteDirectory(workingDirectory);
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

            IoUtils.DeleteDirectory(workingDirectory);
        }
    }
}
