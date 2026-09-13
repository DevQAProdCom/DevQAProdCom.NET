using DevQAProdCom.NET.AI.Shared.Enumerations;
using DevQAProdCom.NET.Global.Extensions;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.PermissionDecisions
{
    public class PlaywrightMcpPermissionDecisions : McpPermissionDecisions
    {
        private const string PlaywrightMcpIdentifier = "playwright";

        public PlaywrightMcpPermissionDecisions()
        {
        }

        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetApprovePlaywrightMcpAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                Permission.ApprovePlaywrightMcpToolsAll.GetDescriptionAttributeValue(),
                (request, invocation) => McpPermissionCheck(request, invocation, PermissionDecision.ApproveOnce(), PlaywrightMcpIdentifier, "*")
            );
        }

        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetDenyPlaywrightMcpAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                Permission.DenyPlaywrightMcpToolsAll.GetDescriptionAttributeValue(),
                (request, invocation) => McpPermissionCheck(request, invocation, PermissionDecision.Reject(GetMcpToolsNotAllowedMessage(PlaywrightMcpIdentifier, "*")), PlaywrightMcpIdentifier, "*")
            );
        }
    }
}
