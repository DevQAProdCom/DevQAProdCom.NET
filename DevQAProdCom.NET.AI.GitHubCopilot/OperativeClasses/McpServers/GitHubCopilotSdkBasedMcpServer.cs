using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.Global.Extensions;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers
{
    public abstract class GitHubCopilotSdkBasedMcpServer<T> : IGitHubCopilotSdkBasedMcpServer where T : McpServerConfig, new()
    {
        public abstract string? Identifier { get; set; }

        public virtual string? Description { get; set; }

        protected T? McpServerConfiguration { get; set; }

        public virtual void ApplyTo(IDictionary<string, McpServerConfig>? mcpServerConfigs)
        {
            ArgumentNullException.ThrowIfNull(mcpServerConfigs);
            ArgumentNullException.ThrowIfNullOrEmpty(Identifier);
            ArgumentNullException.ThrowIfNull(McpServerConfiguration);
            mcpServerConfigs.Add(Identifier, McpServerConfiguration);
        }

        public virtual string ToJson()
        {
            ArgumentNullException.ThrowIfNull(McpServerConfiguration);
            return McpServerConfiguration.ToJson();
        }
    }
}
