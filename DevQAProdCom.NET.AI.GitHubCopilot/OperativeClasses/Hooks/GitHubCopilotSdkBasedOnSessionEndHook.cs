using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public abstract class GitHubCopilotSdkBasedOnSessionEndHook : GitHubCopilotSdkBasedSessionHook
    {
        public abstract Func<SessionEndHookInput, HookInvocation, Task<SessionEndHookOutput?>>? GetOnSessionEndHook();

        public override void ApplyTo(SessionHooks sessionHooks)
        {
            sessionHooks.OnSessionEnd = GetOnSessionEndHook();
        }
    }
}
