using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public abstract class GitHubCopilotSdkBasedOnPostToolUseHook : GitHubCopilotSdkBasedSessionHook
    {
        public abstract Func<PostToolUseHookInput, HookInvocation, Task<PostToolUseHookOutput?>>? GetOnPostToolUseHook();

        public override void AddTo(SessionHooks sessionHooks)
        {
            sessionHooks.OnPostToolUse = GetOnPostToolUseHook();
        }
    }
}
