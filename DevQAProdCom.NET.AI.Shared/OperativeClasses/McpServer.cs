using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.Utils;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public abstract class McpServer<T> : IMcpServer
        where T : class, new()
    {
        public abstract string? Class { get; }
        public string? FilePath { get; set; }

        public string? Type { get; set; }
        public abstract string Identifier { get; set; }
        public List<string>? Tools { get; set; }

        protected T? McpServerConfiguration { get; set; }

        public McpServer() { }

        public virtual void WithConfigurationFromJson(string configuration)
        {
            McpServerConfiguration = configuration.FromJson<T>();
            JsonUtils.PopulateConfigurationProperties(this, configuration);
        }

        public virtual void WithConfigurationFromYaml(string configuration)
        {
            throw new NotImplementedException();
        }

        //public virtual void AssignToOrDefault<TMcpServer>(List<TMcpServer> list)
        //    where TMcpServer : class, new()
        //{
        //    if (list != null)
        //        if (typeof(TMcpServer) == typeof(T) || typeof(TMcpServer).IsAssignableFrom(typeof(T)))
        //        {
        //            var mcpServer = BuildMcpServerConfiguration() as TMcpServer;
        //            if (mcpServer != null)
        //                list.Add(mcpServer);
        //        }
        //}

        public virtual string ToJson()
        {
            return McpServerConfiguration.ToJson(); //TODO Check if this is goind to work with null
        }

        public virtual bool TryGet<TMcpServer>(out TMcpServer? mcpServer) where TMcpServer : class
        {
            if (typeof(TMcpServer) == typeof(T) || typeof(TMcpServer).IsAssignableFrom(typeof(T)))
            {
                mcpServer = BuildMcpServerConfiguration() as TMcpServer;
                if (mcpServer != null)
                {
                    return true;
                }
            }

            mcpServer = null;
            return false;
        }

        protected virtual T? BuildMcpServerConfiguration()
        {
            return McpServerConfiguration;
        }
    }
}
