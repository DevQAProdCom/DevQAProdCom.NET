using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks
{
    public abstract class GitHubCopilotSdkBasedSessionHook : IGitHubCopilotSdkBasedSessionHook
    {
        public abstract string? Identifier { get; set; }
        public virtual string? Description { get; set; }
        public abstract void AddTo(SessionHooks sessionHooks);
    }
}
