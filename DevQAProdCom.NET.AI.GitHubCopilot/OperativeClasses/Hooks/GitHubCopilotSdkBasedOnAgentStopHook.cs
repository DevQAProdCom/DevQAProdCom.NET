using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public abstract class GitHubCopilotSdkBasedOnAgentStopHook : GitHubCopilotSdkBasedSessionHook
    {
        public abstract Func<AgentStopHookInput, HookInvocation, Task<AgentStopHookOutput?>>? GetOnAgentStopHook();

        public override void ApplyTo(SessionHooks sessionHooks)
        {
            sessionHooks.OnAgentStop = GetOnAgentStopHook();
        }
    }
}
