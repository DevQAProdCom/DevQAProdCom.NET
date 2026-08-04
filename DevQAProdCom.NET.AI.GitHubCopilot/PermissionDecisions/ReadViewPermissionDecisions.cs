using DevQAProdCom.NET.AI.Shared.Enumerations;
using DevQAProdCom.NET.Global.Extensions;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.PermissionDecisions
{
    internal class ReadViewPermissionDecisions : BasePermissionDecisions
    {
        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetApproveReadViewAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                Permission.ApproveReadViewAll.GetDescriptionAttributeValue(),
                (request, invocation) => ReadViewPermissionCheck(request, invocation, PermissionDecision.ApproveOnce())
            );
        }

        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetDenyReadViewAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                Permission.DenyReadViewAll.GetDescriptionAttributeValue(),
                (request, invocation) => ReadViewPermissionCheck(request, invocation, PermissionDecision.Reject(GetToolsNotAllowedMessage("read", "view")))
            );
        }

        private Task<PermissionDecision?> ReadViewPermissionCheck(PermissionRequest request, PermissionInvocation invocation, PermissionDecision decision)
        {
            if (request.Kind != "read")
            {
                return Task.FromResult<PermissionDecision?>(null);
            }

            return Task.FromResult<PermissionDecision?>(decision);
        }
    }
}
