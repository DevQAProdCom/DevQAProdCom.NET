using DevQAProdCom.NET.Global.Extensions;
using FluentAssertions;
using NUnit.Framework;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Handlers;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class OrchestrationTests : BaseTest
    {
        [Test]
        public async Task Should_Streaming_From_Subagents_Be_Applied()
        {
            //SETUP
            CheckGitHubCopilotAuthenticationSetup();

            //GIVEN
            var tempOperationalDirectory = "C:\\Files\\temp\\1"; //PrepareTempTestWorkingDirectory(nameof(Should_Check_Streaming_From_Subagents));
            var dateTime = DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds();
            var workingDirectory = Path.Combine(tempOperationalDirectory, nameof(Should_Streaming_From_Subagents_Be_Applied), dateTime, $"WorkingDirectory");
            var baseDirectory = Path.Combine(tempOperationalDirectory, nameof(Should_Streaming_From_Subagents_Be_Applied), dateTime, $"BaseDirectory");
            var logFile = Path.Combine(tempOperationalDirectory, nameof(Should_Streaming_From_Subagents_Be_Applied), dateTime, "ai-content-log.json");
            var logAllAiContentHandler = new LogAllAiContentHandler(logFile, Log);
            var eventsCollectorHandler = new SubagentAssistantMessageEventsCollectorHandler(Log);
            var eventsCollectorAiContentHandler = new GitHubCopilotSubagentAssistantMessageEventsAiContentHandler(eventsCollectorHandler, Log);

            GlobalIoUtils.CreateDirectory(workingDirectory);
            GlobalIoUtils.CreateDirectory(baseDirectory);

            var (requestModel, expectedData) = await PrepareOrchestratorReadWriteAgentTestFilesAsync(workingDirectory);

            //WHEN
            await using (var agent = AiAgentsLibrary
                .GetOrchestratorReadWriteAgentWithResponseValidator(workingDirectory, requestModel, expectedData)
                .WithAiContentHandlers(logAllAiContentHandler, eventsCollectorAiContentHandler)
                .WithSelectiveIsolation()
                .WithCopilotClientBaseDirectory(baseDirectory)
                .WithMaxAttempts(1)
                .WithSessionConfig(config => config.WithIncludeSubAgentStreamingEvents(true).WithPermissionRequestLogFile(logFile)))
            {
               await agent.InvokeAiAgentWithStreamingAsync();
            }

            //THEN
            var assistantMessageToolCallIds = eventsCollectorHandler.AssistantMessageEvents
                .SelectMany(m => m.Data?.ToolRequests?.Select(t => t.ToolCallId) ?? Enumerable.Empty<string?>())
                .Where(t => !string.IsNullOrEmpty(t))
                .ToHashSet(StringComparer.Ordinal);

            var distinctSubagentIds = eventsCollectorHandler.SubagentStartedEvents
                .Where(s => !string.IsNullOrEmpty(s.Data?.ToolCallId) && assistantMessageToolCallIds.Contains(s.Data.ToolCallId))
                .Select(s => s.AgentId ?? s.Data?.ToolCallId)
                .Where(id => !string.IsNullOrEmpty(id))
                .Distinct()
                .ToList();
            
            distinctSubagentIds.Should().HaveCountGreaterOrEqualTo(2, "no streaming from subagents is found");

            //TEAR DOWN
            //CleanUpTempWorkingDirectory(tempCommonWorkingDirectory);
        }
    }
}
