using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Models
{
    public class GitHubCopilotPermissionDecisionModel : IGitHubCopilotPermissionDecision
    {
        public string? Identifier { get; set; }
        public Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>> PermissionDecisionFunc { get; set; }

        public GitHubCopilotPermissionDecisionModel()
        {
            PermissionDecisionFunc = (_, _) => Task.FromResult<PermissionDecision?>(null);
        }
    }
}
