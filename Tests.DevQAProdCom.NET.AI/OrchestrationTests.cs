using DevQAProdCom.NET.Global.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class OrchestrationTests : BaseTest
    {
        [Test]
        public async Task Should_Check_Streaming_From_Subagents()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_Check_Streaming_From_Subagents));
            var (requestModel, expectedData) = await PrepareOrchestratorReadWriteAgentTestFilesAsync(workingDirectory);

            await using (var agent = AiAgentsLibrary
                .GetOrchestratorReadWriteAgentWithResponseValidator(workingDirectory, requestModel, expectedData)
                .WithMaxAttempts(3)
                .WithSessionConfig(config => config.WithIncludeSubAgentStreamingEvents(true)))
            {
                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();
                await act.Should().NotThrowAsync();
            }

            IoUtils.DeleteDirectory(workingDirectory);
        }
    }
}
