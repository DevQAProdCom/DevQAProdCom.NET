using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public abstract class GitHubCopilotSdkBasedOnUserPromptTransformedHook : GitHubCopilotSdkBasedSessionHook
    {
        public abstract Func<UserPromptTransformedHookInput, HookInvocation, Task<UserPromptTransformedHookOutput?>>? GetOnUserPromptTransformedHook();

        public override void ApplyTo(SessionHooks sessionHooks)
        {
            sessionHooks.OnUserPromptTransformed = GetOnUserPromptTransformedHook();
        }
    }
}
