using System.Reflection;
using System.Text.RegularExpressions;
using DevQAProdCom.NET.AI.GitHubCopilot.Handlers;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class AssistantUsageEventHandlerTests
    {
        private const string SessionId = "copilot-session-123";

        [Test]
        public void Should_Log_Accumulated_Token_Consumption_For_Primary_And_Subagent()
        {
            var (logger, capturingLogger) = CreateLogger();
            var handler = new AssistantUsageEventHandler(logger);
            var dataBank = new AiInteractionDataBank();

            HandleSessionStart(handler, dataBank);
            handler.HandleEvent("{\"type\":\"subagent.started\",\"agentId\":\"agent-1\",\"data\":{\"agentDescription\":\"Read files\",\"agentDisplayName\":\"Reader\",\"agentName\":\"reader\",\"toolCallId\":\"tool-1\"}}", dataBank);
            handler.HandleEvent("{\"type\":\"assistant.usage\",\"data\":{\"model\":\"gpt-5\",\"inputTokens\":100,\"outputTokens\":20,\"cacheReadTokens\":30,\"cacheWriteTokens\":10,\"reasoningTokens\":5,\"copilotUsage\":{\"totalNanoAiu\":1000000000}}}", dataBank);
            handler.HandleEvent("{\"type\":\"assistant.usage\",\"agentId\":\"agent-1\",\"data\":{\"model\":\"gpt-5\",\"inputTokens\":50,\"outputTokens\":10,\"cacheReadTokens\":15,\"cacheWriteTokens\":5,\"reasoningTokens\":2,\"copilotUsage\":{\"totalNanoAiu\":500000000}}}", dataBank);
            handler.Finally();

            AssertLog(capturingLogger, "TOKEN CONSUMPTION (Model={Model})", ("SessionId", SessionId), ("AgentName", "primary"), ("AgentId", "main"), ("Model", "gpt-5"), ("TotalTokens", 120L), ("InputTokens", 100L), ("OutputTokens", 20L), ("CacheReadTokens", 30L), ("CacheWriteTokens", 10L), ("ReasoningTokens", 5L), ("AiCredits", "1"));
            AssertLog(capturingLogger, "TOTAL TOKEN CONSUMPTION", ("SessionId", SessionId), ("AgentName", "primary"), ("AgentId", "main"), ("TotalTokens", 120L), ("InputTokens", 100L), ("OutputTokens", 20L), ("CacheReadTokens", 30L), ("CacheWriteTokens", 10L), ("ReasoningTokens", 5L), ("AiCredits", "1"));
            AssertLog(capturingLogger, "TOTAL TOKEN CONSUMPTION", ("SessionId", SessionId), ("AgentName", "reader"), ("AgentId", "agent-1"), ("TotalTokens", 60L), ("InputTokens", 50L), ("OutputTokens", 10L), ("CacheReadTokens", 15L), ("CacheWriteTokens", 5L), ("ReasoningTokens", 2L), ("AiCredits", "0.5"));
            AssertLog(capturingLogger, "[session] TOTAL TOKEN CONSUMPTION", ("SessionId", SessionId), ("TotalTokens", 180L), ("InputTokens", 150L), ("OutputTokens", 30L), ("CacheReadTokens", 45L), ("CacheWriteTokens", 15L), ("ReasoningTokens", 7L), ("AiCredits", "1.5"));
            capturingLogger.InfoMessages.Should().OnlyContain(message => message.Template.Contains("{SessionId}", StringComparison.Ordinal) && !message.Template.Contains(SessionId, StringComparison.Ordinal));
        }

        [Test]
        public void Should_Use_Subagent_Total_When_Assistant_Usage_Is_Not_Streamed()
        {
            var (logger, capturingLogger) = CreateLogger();
            var handler = new AssistantUsageEventHandler(logger);
            var dataBank = new AiInteractionDataBank();

            HandleSessionStart(handler, dataBank);
            handler.HandleEvent("{\"type\":\"subagent.completed\",\"agentId\":\"agent-1\",\"data\":{\"agentDisplayName\":\"Reader\",\"agentName\":\"reader\",\"toolCallId\":\"tool-1\",\"totalTokens\":75}}", dataBank);
            handler.Finally();

            AssertLog(capturingLogger, "TOTAL TOKEN CONSUMPTION", ("SessionId", SessionId), ("AgentName", "reader"), ("AgentId", "agent-1"), ("TotalTokens", 75L), ("AiCredits", "unknown"));
            AssertLog(capturingLogger, "[session] TOTAL TOKEN CONSUMPTION", ("SessionId", SessionId), ("TotalTokens", 75L), ("AiCredits", "unknown"));
        }

        [Test]
        public void Should_Log_Authoritative_Totals_From_Session_Shutdown()
        {
            var (logger, capturingLogger) = CreateLogger();
            var handler = new AssistantUsageEventHandler(logger);
            var dataBank = new AiInteractionDataBank();
            var mainModelMetrics = "{\"gpt-5\":{\"requests\":{},\"usage\":{\"cacheReadTokens\":30,\"cacheWriteTokens\":10,\"inputTokens\":100,\"outputTokens\":20,\"reasoningTokens\":5}}}";
            var subagentModelMetrics = "{\"gpt-5\":{\"requests\":{},\"usage\":{\"cacheReadTokens\":15,\"cacheWriteTokens\":5,\"inputTokens\":50,\"outputTokens\":10,\"reasoningTokens\":2}}}";
            var sessionModelMetrics = "{\"gpt-5\":{\"requests\":{},\"usage\":{\"cacheReadTokens\":45,\"cacheWriteTokens\":15,\"inputTokens\":150,\"outputTokens\":30,\"reasoningTokens\":7}}}";
            var shutdownEvent = "{\"type\":\"session.shutdown\",\"data\":{\"agentMetrics\":{\"main\":{\"modelMetrics\":" + mainModelMetrics + ",\"totalApiDurationMs\":0,\"totalNanoAiu\":1000000000},\"agent-1\":{\"agentName\":\"reader\",\"agentDisplayName\":\"Reader\",\"modelMetrics\":" + subagentModelMetrics + ",\"totalApiDurationMs\":0,\"totalNanoAiu\":500000000}},\"codeChanges\":{\"filesModified\":[],\"linesAdded\":0,\"linesRemoved\":0},\"modelMetrics\":" + sessionModelMetrics + ",\"totalNanoAiu\":1500000000,\"sessionStartTime\":0,\"shutdownType\":\"routine\",\"totalApiDurationMs\":0}}";

            HandleSessionStart(handler, dataBank);
            handler.HandleEvent(shutdownEvent, dataBank);

            AssertLog(capturingLogger, "SESSION TOTAL TOKEN CONSUMPTION", ("SessionId", SessionId), ("AgentName", "primary"), ("AgentId", "main"), ("TotalTokens", 120L), ("InputTokens", 100L), ("OutputTokens", 20L), ("CacheReadTokens", 30L), ("CacheWriteTokens", 10L), ("ReasoningTokens", 5L), ("AiCredits", "1"));
            AssertLog(capturingLogger, "SESSION TOTAL TOKEN CONSUMPTION", ("SessionId", SessionId), ("AgentName", "reader"), ("AgentId", "agent-1"), ("TotalTokens", 60L), ("InputTokens", 50L), ("OutputTokens", 10L), ("CacheReadTokens", 15L), ("CacheWriteTokens", 5L), ("ReasoningTokens", 2L), ("AiCredits", "0.5"));
            AssertLog(capturingLogger, "[SessionShutdownEvent] SESSION TOTAL TOKEN CONSUMPTION", ("SessionId", SessionId), ("TotalTokens", 180L), ("InputTokens", 150L), ("OutputTokens", 30L), ("CacheReadTokens", 45L), ("CacheWriteTokens", 15L), ("ReasoningTokens", 7L), ("AiCredits", "1.5"));
        }

        private static void HandleSessionStart(AssistantUsageEventHandler handler, AiInteractionDataBank dataBank)
        {
            handler.HandleEvent("{\"type\":\"session.start\",\"data\":{\"sessionId\":\"" + SessionId + "\",\"copilotVersion\":\"1.0.0\",\"producer\":\"test\",\"startTime\":\"2026-11-15T12:00:00Z\",\"version\":1}}", dataBank);
        }

        private static (ILogger Logger, CapturingLogger CapturingLogger) CreateLogger()
        {
            var logger = DispatchProxy.Create<ILogger, CapturingLogger>();
            return (logger, (CapturingLogger)(object)logger);
        }

        private static void AssertLog(CapturingLogger logger, string templatePart, params (string Name, object? Value)[] expectedProperties)
        {
            var matchingLogs = logger.InfoMessages.Where(message => message.Template.Contains(templatePart, StringComparison.Ordinal)).ToList();
            matchingLogs.Should().NotBeEmpty(templatePart);
            matchingLogs.Select(GetProperties).Should().Contain(properties => MatchesExpected(properties, expectedProperties), templatePart);
        }

        private static bool MatchesExpected(Dictionary<string, object?> properties, IEnumerable<(string Name, object? Value)> expectedProperties)
        {
            foreach (var expected in expectedProperties)
            {
                if (!properties.TryGetValue(expected.Name, out var value) || !Equals(value, expected.Value))
                    return false;
            }

            return true;
        }

        private static Dictionary<string, object?> GetProperties(CapturedLogMessage message)
        {
            var names = Regex.Matches(message.Template, @"\{(?<name>[A-Za-z][A-Za-z0-9]*)\}").Select(match => match.Groups["name"].Value).ToList();
            var properties = new Dictionary<string, object?>(StringComparer.Ordinal);

            for (var index = 0; index < names.Count && index < message.Parameters.Length; index++)
                properties[names[index]] = message.Parameters[index];

            return properties;
        }
    }

    public sealed record CapturedLogMessage(string Template, object?[] Parameters);

    public class CapturingLogger : DispatchProxy
    {
        public List<CapturedLogMessage> InfoMessages { get; } = new();

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (targetMethod?.Name == nameof(ILogger.Info) && args?.FirstOrDefault() is string template)
            {
                var parameters = args.Length > 1 ? args[1] as object?[] : null;
                InfoMessages.Add(new CapturedLogMessage(template, parameters ?? Array.Empty<object?>()));
            }

            if (targetMethod?.ReturnType.IsValueType == true && targetMethod.ReturnType != typeof(void))
                return Activator.CreateInstance(targetMethod.ReturnType);

            return null;
        }
    }
}
