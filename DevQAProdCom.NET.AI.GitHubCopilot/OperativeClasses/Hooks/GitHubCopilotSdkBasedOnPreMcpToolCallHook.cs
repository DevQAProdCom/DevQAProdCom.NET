using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public abstract class GitHubCopilotSdkBasedOnPreMcpToolCallHook : GitHubCopilotSdkBasedSessionHook
    {
        public abstract Func<PreMcpToolCallHookInput, HookInvocation, Task<PreMcpToolCallHookOutput?>>? GetOnPreMcpToolCallHook();

        public override void AddTo(SessionHooks sessionHooks)
        {
            sessionHooks.OnPreMcpToolCall = GetOnPreMcpToolCallHook();
        }
    }
}
