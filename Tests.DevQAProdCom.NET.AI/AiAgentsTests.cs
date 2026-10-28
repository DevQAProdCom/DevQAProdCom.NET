using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class AiAgentsTests : BaseTest
    {
        [Test]
        public async Task Should_Read_Write_Agent_Execute()
        {
            //SETUP
            CheckGitHubCopilotAuthenticationSetup();

            //GIVEN
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_Read_Write_Agent_Execute));
            var (inputFilePath, inputContent, outputFolderPath, expectedOutputFilePath) =
                await PrepareReadWriteAgentTestFilesAsync(tempWorkingDirectory);

            //WHEN
            await using (var agent = AiAgentsLibrary
                .GetReadWriteAgentWithResponseValidator(tempWorkingDirectory, inputFilePath, outputFolderPath, expectedOutputFilePath, inputContent)
                .WithMaxAttempts(3))
            {
                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();

                //THEN
                await act.Should().NotThrowAsync();
            }

            //TEAR DOWN
            CleanUpTempWorkingDirectory(tempWorkingDirectory);
        }
    }
}
