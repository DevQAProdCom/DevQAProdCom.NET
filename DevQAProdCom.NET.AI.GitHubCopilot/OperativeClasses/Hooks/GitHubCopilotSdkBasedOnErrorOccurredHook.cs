using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public abstract class GitHubCopilotSdkBasedOnErrorOccurredHook : GitHubCopilotSdkBasedSessionHook
    {
        public abstract Func<ErrorOccurredHookInput, HookInvocation, Task<ErrorOccurredHookOutput?>>? GetOnErrorOccurredHook();

        public override void ApplyTo(SessionHooks sessionHooks)
        {
            sessionHooks.OnErrorOccurred = GetOnErrorOccurredHook();
        }
    }
}
