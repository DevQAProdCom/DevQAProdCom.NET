using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public abstract class GitHubCopilotSdkBasedOnUserPromptSubmittedHook : GitHubCopilotSdkBasedSessionHook
    {
        public abstract Func<UserPromptSubmittedHookInput, HookInvocation, Task<UserPromptSubmittedHookOutput?>>? GetOnUserPromptSubmittedHook();

        public override void ApplyTo(SessionHooks sessionHooks)
        {
            sessionHooks.OnUserPromptSubmitted = GetOnUserPromptSubmittedHook();
        }
    }
}
