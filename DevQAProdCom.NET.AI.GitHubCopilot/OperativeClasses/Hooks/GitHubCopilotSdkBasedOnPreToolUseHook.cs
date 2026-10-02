using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public abstract class GitHubCopilotSdkBasedOnPreToolUseHook : GitHubCopilotSdkBasedSessionHook
    {
        public abstract Func<PreToolUseHookInput, HookInvocation, Task<PreToolUseHookOutput?>>? GetOnPreToolUseHook();

        public override void ApplyTo(SessionHooks sessionHooks)
        {
            sessionHooks.OnPreToolUse = GetOnPreToolUseHook();
        }
    }
}
