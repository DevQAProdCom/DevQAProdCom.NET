using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.PermissionDecisions;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Permissions
{
    public class FilesystemMcpTestPermissionDecisions : McpPermissionDecisions
    {
        public const string FilesystemSdkBasedMcpServerIdentifier = "filesystem-sdk-based-mcp-server";

        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetApproveFilesystemSdkBasedMcpServerAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                FilesystemSdkBasedMcpServerIdentifier,
                (request, invocation) => McpPermissionCheck(request, invocation, PermissionDecision.ApproveOnce(), FilesystemSdkBasedMcpServerIdentifier, "*")
            );
        }
    }
}
