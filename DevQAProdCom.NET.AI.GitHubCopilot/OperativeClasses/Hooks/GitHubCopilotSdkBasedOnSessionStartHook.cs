using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public abstract class GitHubCopilotSdkBasedOnSessionStartHook : GitHubCopilotSdkBasedSessionHook
    {
        public abstract Func<SessionStartHookInput, HookInvocation, Task<SessionStartHookOutput?>>? GetOnSessionStartHook();

        public override void ApplyTo(SessionHooks sessionHooks)
        {
            sessionHooks.OnSessionStart = GetOnSessionStartHook();
        }
    }
}
