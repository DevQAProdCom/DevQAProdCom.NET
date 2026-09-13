using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.PermissionDecisions
{
    public class McpPermissionDecisions : BasePermissionDecisions
    {
        //private readonly string _mcpName;
        //private readonly string[] _allowedTools;

        //public McpPermissionDecisions(string mcpName, params string[] allowedTools)
        //{
        //    if (string.IsNullOrWhiteSpace(mcpName))
        //        throw new ArgumentException("MCP name must be specified.", nameof(mcpName));

        //    if (allowedTools == null || allowedTools.Length == 0)
        //        throw new ArgumentException("At least one allowed tool must be specified.", nameof(allowedTools));

        //    _mcpName = mcpName;
        //    _allowedTools = allowedTools;
        //}

        protected Task<PermissionDecision?> McpPermissionCheck(PermissionRequest request, PermissionInvocation invocation, PermissionDecision decision, string mcpName, params string[] tools)
        {
            if (request.Kind != "mcp")
            {
                return Task.FromResult<PermissionDecision?>(null);
            }

            if (request is not PermissionRequestMcp mcpRequest)
            {
                return Task.FromResult<PermissionDecision?>(null);
            }

            if (!string.Equals(mcpRequest.ServerName, mcpName, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult<PermissionDecision?>(null);
            }

            if (!tools.Contains("*", StringComparer.OrdinalIgnoreCase) &&
                !tools.Contains(mcpRequest.ToolName, StringComparer.OrdinalIgnoreCase))
            {
                return Task.FromResult<PermissionDecision?>(null);
            }

            return Task.FromResult<PermissionDecision?>(decision);
        }

        protected string GetMcpToolsNotAllowedMessage(string mcpName, params string[] allowedTools)
        {
            if (allowedTools.Contains("*", StringComparer.OrdinalIgnoreCase))
            {
                return $"Usage of '{mcpName}' MCP server tools is not allowed.";
            }

            var toolOrTools = allowedTools.Length > 1 ? "tools are" : "tool is";
            return $"Usage of '{mcpName}' MCP server tools is not allowed. Only '{string.Join(", ", allowedTools)}' {toolOrTools} allowed.";
        }
    }
}
