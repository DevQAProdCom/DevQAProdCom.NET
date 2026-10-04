using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public abstract class GitHubCopilotSdkBasedOnPostToolUseFailureHook : GitHubCopilotSdkBasedSessionHook
    {
        public abstract Func<PostToolUseFailureHookInput, HookInvocation, Task<PostToolUseFailureHookOutput?>>? GetOnPostToolUseFailureHook();

        public override void AddTo(SessionHooks sessionHooks)
        {
            sessionHooks.OnPostToolUseFailure = GetOnPostToolUseFailureHook();
        }
    }
}
