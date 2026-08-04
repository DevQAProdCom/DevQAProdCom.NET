using DevQAProdCom.NET.AI.Shared.Enumerations;
using DevQAProdCom.NET.Global.Extensions;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.PermissionDecisions
{
    public class WriteCreatePermissionDecisions: BasePermissionDecisions
    {
        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetApproveWriteCreateAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                Permission.ApproveWriteCreateAll.GetDescriptionAttributeValue(),
                (request, invocation) => WriteCreatePermissionCheck(request, invocation, PermissionDecision.ApproveOnce())
            );
        }

        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetDenyWriteCreateAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                Permission.DenyWriteCreateAll.GetDescriptionAttributeValue(),
                (request, invocation) => WriteCreatePermissionCheck(request, invocation, PermissionDecision.Reject(GetToolsNotAllowedMessage("create")))
            );
        }

        private Task<PermissionDecision?> WriteCreatePermissionCheck(PermissionRequest request, PermissionInvocation invocation, PermissionDecision decision)
        {
            if (request.Kind != "write")
            {
                return Task.FromResult<PermissionDecision?>(null);
            }

            return Task.FromResult<PermissionDecision?>(decision);
        }
    }
}
