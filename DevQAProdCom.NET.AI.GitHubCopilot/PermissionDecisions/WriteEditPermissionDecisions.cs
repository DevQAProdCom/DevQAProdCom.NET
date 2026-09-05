using DevQAProdCom.NET.AI.Shared.Enumerations;
using DevQAProdCom.NET.Global.Extensions;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.PermissionDecisions
{
    public class WriteEditPermissionDecisions : BasePermissionDecisions
    {
        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetApproveWriteEditAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                Permission.ApproveWriteEditAll.GetDescriptionAttributeValue(),
                (request, invocation) => WriteEditPermissionCheck(request, invocation, PermissionDecision.ApproveOnce())
            );
        }

        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetDenyWriteEditAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                Permission.DenyWriteEditAll.GetDescriptionAttributeValue(),
                (request, invocation) => WriteEditPermissionCheck(request, invocation, PermissionDecision.Reject(GetToolsNotAllowedMessage("edit")))
            );
        }

        private Task<PermissionDecision?> WriteEditPermissionCheck(PermissionRequest request, PermissionInvocation invocation, PermissionDecision decision)
        {
            if (request.Kind != "edit")
            {
                return Task.FromResult<PermissionDecision?>(null);
            }

            return Task.FromResult<PermissionDecision?>(decision);
        }
    }
}
