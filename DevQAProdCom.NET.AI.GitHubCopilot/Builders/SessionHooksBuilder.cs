using GitHub.Copilot;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.Logging.Shared.Constans;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Builders
{
    public class SessionHooksBuilder
    {
        private readonly ILogger _logger;
        private readonly SessionHooks _sessionHooks = new();

        public SessionHooksBuilder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public SessionHooksBuilder WithOnPreToolUse(Func<PreToolUseHookInput, HookInvocation, Task<PreToolUseHookOutput?>>? handler)
        {
            _sessionHooks.OnPreToolUse = handler;
            LogSetting(nameof(_sessionHooks.OnPreToolUse), handler != null ? "set" : "null");
            return this;
        }

        public SessionHooksBuilder WithOnPreMcpToolCall(Func<PreMcpToolCallHookInput, HookInvocation, Task<PreMcpToolCallHookOutput?>>? handler)
        {
            _sessionHooks.OnPreMcpToolCall = handler;
            LogSetting(nameof(_sessionHooks.OnPreMcpToolCall), handler != null ? "set" : "null");
            return this;
        }

        public SessionHooksBuilder WithOnPostToolUse(Func<PostToolUseHookInput, HookInvocation, Task<PostToolUseHookOutput?>>? handler)
        {
            _sessionHooks.OnPostToolUse = handler;
            LogSetting(nameof(_sessionHooks.OnPostToolUse), handler != null ? "set" : "null");
            return this;
        }

        public SessionHooksBuilder WithOnPostToolUseFailure(Func<PostToolUseFailureHookInput, HookInvocation, Task<PostToolUseFailureHookOutput?>>? handler)
        {
            _sessionHooks.OnPostToolUseFailure = handler;
            LogSetting(nameof(_sessionHooks.OnPostToolUseFailure), handler != null ? "set" : "null");
            return this;
        }

        public SessionHooksBuilder WithOnUserPromptSubmitted(Func<UserPromptSubmittedHookInput, HookInvocation, Task<UserPromptSubmittedHookOutput?>>? handler)
        {
            _sessionHooks.OnUserPromptSubmitted = handler;
            LogSetting(nameof(_sessionHooks.OnUserPromptSubmitted), handler != null ? "set" : "null");
            return this;
        }

        public SessionHooksBuilder WithOnSessionStart(Func<SessionStartHookInput, HookInvocation, Task<SessionStartHookOutput?>>? handler)
        {
            _sessionHooks.OnSessionStart = handler;
            LogSetting(nameof(_sessionHooks.OnSessionStart), handler != null ? "set" : "null");
            return this;
        }

        public SessionHooksBuilder WithOnSessionEnd(Func<SessionEndHookInput, HookInvocation, Task<SessionEndHookOutput?>>? handler)
        {
            _sessionHooks.OnSessionEnd = handler;
            LogSetting(nameof(_sessionHooks.OnSessionEnd), handler != null ? "set" : "null");
            return this;
        }

        public SessionHooksBuilder WithOnErrorOccurred(Func<ErrorOccurredHookInput, HookInvocation, Task<ErrorOccurredHookOutput?>>? handler)
        {
            _sessionHooks.OnErrorOccurred = handler;
            LogSetting(nameof(_sessionHooks.OnErrorOccurred), handler != null ? "set" : "null");
            return this;
        }

        public SessionHooksBuilder WithSessionHooks(Func<SessionHooks, SessionHooks> updateSessionHooks)
        {
            ArgumentNullException.ThrowIfNull(updateSessionHooks);
            updateSessionHooks.Invoke(_sessionHooks);
            LogSetting(nameof(_sessionHooks), "updated via custom configuration");
            return this;
        }

        public SessionHooks Build()
        {
            return _sessionHooks;
        }

        private void LogSetting(string propertyName, object value)
        {
            _logger.Info("🛠️[{LogArea}] ⚙️[{TypeName}] Setting 🔧'{PropertyName}' parameter to '{Value}'.", $"{SharedLoggingConstants.Area.Config}", $"{nameof(SessionHooksBuilder)}", propertyName, value);
        }
    }
}
