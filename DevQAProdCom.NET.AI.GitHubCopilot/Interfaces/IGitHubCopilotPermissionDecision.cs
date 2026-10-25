using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotPermissionDecision : IHaveStringIdentifier
    {
        public Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>> PermissionDecisionFunc { get; set; }
    }
}
