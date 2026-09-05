using DevQAProdCom.NET.Global.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class OrchestrationTests : BaseTest
    {
        [Test]
        public async Task Should_Check_Streaming_From_Subagents()
        {
            //var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_Check_Streaming_From_Subagents));
            var mainTempDir = "C:\\Files\\temp\\1";
            var dateTime = DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds();
            var workingDirectory = Path.Combine(mainTempDir, nameof(Should_Check_Streaming_From_Subagents), $"WorkingDirectory{dateTime}");
            var baseDirectory = Path.Combine(mainTempDir, nameof(Should_Check_Streaming_From_Subagents), $"BaseDirectory{dateTime}");

            var (requestModel, expectedData) = await PrepareOrchestratorReadWriteAgentTestFilesAsync(workingDirectory);

            await using (var agent = AiAgentsLibrary
                .GetOrchestratorReadWriteAgentWithResponseValidator(workingDirectory, requestModel, expectedData)
                .WithCopilotClientBaseDirectory(baseDirectory)
                .WithSelectiveIsolation()
                .WithMaxAttempts(3)
                .WithSessionConfig(config => config.WithIncludeSubAgentStreamingEvents(true)))
            {
                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();
                await act.Should().NotThrowAsync();
            }

            //IoUtils.DeleteDirectory(workingDirectory);
        }
    }
}
