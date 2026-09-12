using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Global.Extensions;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.McpServers
{
    public abstract class GitHubCopilotStdioMcpServer: McpServer<McpStdioServerConfig>
    {
        protected McpStdioServerConfig? McpServerConfiguration { get; set; }
        public string Type { get; set; }

        public virtual void WithConfigurationFromJson(string configuration)
        {

        }

        public virtual void WithConfigurationFromYaml(string configuration)
        {

        }

        public virtual void AssignToOrDefault<TMcpServer>(List<TMcpServer> list)
        {

        }

        public virtual string ToJson()
        {
            return McpServerConfiguration.ToJson();
        }
    }
}
