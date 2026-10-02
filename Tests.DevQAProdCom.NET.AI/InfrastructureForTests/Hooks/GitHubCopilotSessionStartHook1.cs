using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks;
using DevQAProdCom.NET.Global.Utils;
using GitHub.Copilot;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Hooks
{
    public class GitHubCopilotSessionStartHook1 : GitHubCopilotSdkBasedOnSessionStartHook
    {
        public override string? Identifier { get; set; } = $"{nameof(GitHubCopilotSessionStartHook1)}";

        public override Func<SessionStartHookInput, HookInvocation, Task<SessionStartHookOutput?>>? GetOnSessionStartHook()
        {
            return async (input, invocation) =>
            {
                var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                var content = $"session-start-hook-1 {timestamp}";
                IoUtils.WriteAllText("session-start-hook-1.txt", content);
                return await Task.FromResult<SessionStartHookOutput?>(null);
            };
        }
    }
}
