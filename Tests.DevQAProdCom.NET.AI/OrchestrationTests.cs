using DevQAProdCom.NET.AI.GitHubCopilot.Utils;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers;
using DevQAProdCom.NET.Global.Extensions;
using FluentAssertions;
using NUnit.Framework;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

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
            var workingDirectory = Path.Combine(mainTempDir, nameof(Should_Check_Streaming_From_Subagents), dateTime, $"WorkingDirectory");
            var baseDirectory = Path.Combine(mainTempDir, nameof(Should_Check_Streaming_From_Subagents), dateTime, $"BaseDirectory");
            var logFile = Path.Combine(mainTempDir, nameof(Should_Check_Streaming_From_Subagents), dateTime, "ai-content-log.json");
            var logAllAiContentHandler = new LogAllAiContentHandler(logFile, Log);

            GlobalIoUtils.CreateDirectory(workingDirectory);
            GlobalIoUtils.CreateDirectory(baseDirectory);

            var (requestModel, expectedData) = await PrepareOrchestratorReadWriteAgentTestFilesAsync(workingDirectory);

            await using (var agent = AiAgentsLibrary
                .GetOrchestratorReadWriteAgentWithResponseValidator(workingDirectory, requestModel, expectedData)
                .WithAiContentHandlers(logAllAiContentHandler)
                .WithSelectiveIsolation()
                .WithCopilotClientBaseDirectory(baseDirectory)
                .WithMaxAttempts(1)
                .WithSessionConfig(config => config.WithIncludeSubAgentStreamingEvents(true).WithPermissionRequestLogFile(logFile)))
            {
                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();
                await act.Should().NotThrowAsync();
            }

            //IoUtils.DeleteDirectory(workingDirectory);
        }
    }
}
