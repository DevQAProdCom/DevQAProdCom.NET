using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.PermissionDecisions;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Permissions
{
    public class PlaywrightMcpTestPermissionDecisions : McpPermissionDecisions
    {
        public const string PlaywrightSdkBasedMcpServerIdentifier = "playwright-sdk-based-mcp-server";
        public const string PlaywrightMcpServerInMcpServersDirectoryIdentifier = "playwright-mcp-server-in-mcp-servers-directory";
        public const string PlaywrightMcpServerInMcpJsonFileIdentifier = "playwright-mcp-server-in-mcp-json-file";

        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetApprovePlaywrightSdkBasedMcpServerAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                PlaywrightSdkBasedMcpServerIdentifier,
                (request, invocation) => McpPermissionCheck(request, invocation, PermissionDecision.ApproveOnce(), PlaywrightSdkBasedMcpServerIdentifier, "*")
            );
        }

        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetApprovePlaywrightMcpServerInMcpServersDirectoryAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                PlaywrightMcpServerInMcpServersDirectoryIdentifier,
                (request, invocation) => McpPermissionCheck(request, invocation, PermissionDecision.ApproveOnce(), PlaywrightMcpServerInMcpServersDirectoryIdentifier, "*")
            );
        }

        public KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> GetApprovePlaywrightMcpServerInMcpJsonFileAllPermission()
        {
            return new KeyValuePair<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>(
                PlaywrightMcpServerInMcpJsonFileIdentifier,
                (request, invocation) => McpPermissionCheck(request, invocation, PermissionDecision.ApproveOnce(), PlaywrightMcpServerInMcpJsonFileIdentifier, "*")
            );
        }
    }
}
