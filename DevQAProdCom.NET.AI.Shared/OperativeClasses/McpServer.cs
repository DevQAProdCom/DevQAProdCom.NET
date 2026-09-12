using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public abstract class McpServer<T> : IMcpServer
        where T : class
    {
        protected T? McpServerConfiguration { get; set; }
        public abstract string? Class { get; set; }
        public string Type { get; set; }
        public abstract string Identifier { get; set; }
        public IList<string>? Tools { get; set; }
        public virtual void WithConfigurationFromJson(string configuration)
        {
            McpServerConfiguration = configuration.FromJson<T>();
        }

        public virtual void WithConfigurationFromYaml(string configuration)
        {
            throw new NotImplementedException();
        }

        public virtual void AssignToOrDefault<TMcpServer>(List<TMcpServer> list)
            where TMcpServer : class
        {
            if (list != null)
                if (typeof(TMcpServer) == typeof(T) || typeof(TMcpServer).IsAssignableTo(typeof(T)))
                {
                    var mcpServer = McpServerConfiguration as TMcpServer;
                    if (mcpServer != null)
                        list.Add(mcpServer);
                }
        }

        public virtual string ToJson()
        {
            return McpServerConfiguration.ToJson();
        }
    }
}
