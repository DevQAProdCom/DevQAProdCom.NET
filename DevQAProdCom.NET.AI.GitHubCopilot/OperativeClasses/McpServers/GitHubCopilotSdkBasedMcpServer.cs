using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.Global.Extensions;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers
{
    public abstract class GitHubCopilotSdkBasedMcpServer<T> : IGitHubCopilotSdkBasedMcpServer where T : McpServerConfig, new()
    {
        public abstract string? Identifier { get; set; }

        public virtual string? Description { get; set; }

        protected T McpServerConfiguration { get; set; } = new T();

        public virtual void ApplyTo(List<McpServerConfig> mcpServerConfigs)
        {
            ArgumentNullException.ThrowIfNull(mcpServerConfigs);
            mcpServerConfigs.Add(McpServerConfiguration);
        }

        public virtual string ToJson()
        {
            return McpServerConfiguration.ToJson();
        }
    }
}
