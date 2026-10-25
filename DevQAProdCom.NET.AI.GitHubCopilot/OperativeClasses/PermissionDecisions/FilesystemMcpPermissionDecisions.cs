using DevQAProdCom.NET.AI.Shared.Enumerations;
using DevQAProdCom.NET.Global.Extensions;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.PermissionDecisions
{
    public class FilesystemMcpPermissionDecisions : McpPermissionDecisions
    {
        private const string FilesystemMcpIdentifier = "filesystem-sdk-based-mcp-server";

        public FilesystemMcpPermissionDecisions()
        {
        }

        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetApproveFilesystemMcpAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                Permission.ApproveFilesystemMcpToolsAll.GetDescriptionAttributeValue(),
                (request, invocation) => McpPermissionCheck(request, invocation, PermissionDecision.ApproveOnce(), FilesystemMcpIdentifier, "*")
            );
        }
    }
}
