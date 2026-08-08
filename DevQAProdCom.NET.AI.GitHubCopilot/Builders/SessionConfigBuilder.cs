using DevQAProdCom.NET.AI.GitHubCopilot.Collections;
using DevQAProdCom.NET.AI.GitHubCopilot.Mappers;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Builders
{
    public class SessionConfigBuilder
    {
        private readonly SessionConfig _sessionConfig = new();

        private readonly Dictionary<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> _permissionDecisions = new();

        private PermissionDecisionsCollection? _permissionDecisionsCollection;
        private PermissionDecisionsCollection? PermissionDecisionsCollection => _permissionDecisionsCollection ??= new();

        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>? _aiAgentsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel> AiAgentsCollection => _aiAgentsCollection ??= new GitHubCopilotAiAgentsCollection();

        private GitHubCopilotMappers? _gitHubCopilotMappers;
        private GitHubCopilotMappers GitHubCopilotMappers => _gitHubCopilotMappers ??= new();

        private readonly ILogger _logger;

        public SessionConfigBuilder(ILogger logger)
        {
            _logger = logger;
        }

        public SessionConfigBuilder WithModel(string model)
        {
            _sessionConfig.Model = model;
            return this;
        }

        public SessionConfigBuilder WithAgent(string agentIdentifier)
        {
            _sessionConfig.Agent = agentIdentifier;
            return this;
        }

        public SessionConfigBuilder WithAgent(FileInfo filePath)
        {
            var entityData = AiAgentsCollection.AddEntityData(filePath.FullName);
            _sessionConfig.Agent = entityData.ConfigurationData.Name;
            return this;
        }

        public SessionConfigBuilder WithSystemMessage(string content, SystemMessageMode mode = SystemMessageMode.Append)
        {
            var systemMessage = new SystemMessageConfig { Content = content, Mode = mode };
            _sessionConfig.SystemMessage = systemMessage;
            return this;
        }

        public SessionConfigBuilder WithAvailableTools(params string[] tools)
        {
            _sessionConfig.AvailableTools = tools.ToList();
            return this;
        }

        public SessionConfigBuilder WithStreaming(bool streaming = true)
        {
            _sessionConfig.Streaming = streaming;
            return this;
        }

        public SessionConfigBuilder WithCustomAgentConfig(CustomAgentConfig config)
        {
            _sessionConfig.CustomAgents ??= new List<CustomAgentConfig>();
            var existingAgent = _sessionConfig.CustomAgents.FirstOrDefault(a => a.Name == config.Name);

            if (existingAgent != null)
            {
                _sessionConfig.CustomAgents.Remove(existingAgent);
            }
            _sessionConfig.CustomAgents.Add(config);

            return this;
        }

        public SessionConfigBuilder WithCustomAgentConfig(string agentIdentifier)
        {
            var entity = AiAgentsCollection.GetEntityData(agentIdentifier);
            var config = GitHubCopilotMappers.ToCustomAgentConfig(entity);
            return WithCustomAgentConfig(config);
        }

        public SessionConfigBuilder WithMcpServer(string name, McpServerConfig config)
        {
            _sessionConfig.McpServers ??= new Dictionary<string, McpServerConfig>();
            _sessionConfig.McpServers[name] = config;
            return this;
        }

        public SessionConfigBuilder SetPermission(string identifier, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>> permissionFunc)
        {
            _permissionDecisions[identifier] = async (request, invocation) =>
            {
                var decision = await permissionFunc(request, invocation);

                return decision;
            };

            return this;
        }

        public SessionConfigBuilder WithPermission(string identifier)
        {
            var permissionDecision = PermissionDecisionsCollection.GetByIdentifier(identifier);
            _permissionDecisions[identifier] = permissionDecision;
            return this;
        }

        public SessionConfigBuilder WithSessionConfig(Func<SessionConfig, SessionConfig> updateSessionConfig)
        {
            updateSessionConfig.Invoke(_sessionConfig);
            return this;
        }

        public SessionConfig Build()
        {
            if (_sessionConfig.Agent != null)
            {
                if (AiAgentsCollection.TryGetEntityData(_sessionConfig.Agent, out var entityData))
                {
                    var model = entityData!.ConfigurationData.Model;

                    if (!string.IsNullOrEmpty(model))
                    {
                        _sessionConfig.Model = model;
                    }
                    _sessionConfig.AvailableTools = entityData.ConfigurationData.Tools;

                    WithCustomAgentConfig(_sessionConfig.Agent);

                    if (entityData.ConfigurationData.CustomPermissions?.Count() > 0)
                    {
                        foreach (var permission in entityData.ConfigurationData.CustomPermissions)
                        {
                            WithPermission(permission);
                        }
                    }
                }
            }

            _sessionConfig.OnPermissionRequest = async (request, invocation) =>
            {
                _logger.Info($"Permission Request:\nType= {request.ToString()}\nBody = {request.ToJson()}");

                if(_permissionDecisions?.Count>0)
                {

                    foreach(var permissionDecision in _permissionDecisions)
                    {
                        try
                        {
                            if(permissionDecision.Value != null)
                            {
                                var result = await permissionDecision.Value(request, invocation);
                                if(result!=null)
                                {
                                    return result!;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Error($"Error processing permission decision '{permissionDecision.Key}': {ex.Message}");
                        }
                    }

                }

                return PermissionDecision.Reject("Review the available tools and use only those permitted to complete the task. If no suitable tools are found, list all available tools and indicate that the requested tool cannot be executed.");
            };

            return _sessionConfig;
        }
    }
}
