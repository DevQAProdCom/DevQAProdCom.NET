using DevQAProdCom.NET.AI.GitHubCopilot.Collections;
using DevQAProdCom.NET.AI.GitHubCopilot.Mappers;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Builders
{
    /// <summary>
    /// Fluent builder for constructing a <see cref="SessionConfig"/> instance.
    /// </summary>
    public class SessionConfigBuilder
    {
        private readonly SessionConfig _sessionConfig = new();

        private readonly Dictionary<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> _permissionDecisions = new();

        private PermissionDecisionsCollection? _permissionDecisionsCollection;
        private PermissionDecisionsCollection PermissionDecisionsCollection => _permissionDecisionsCollection ??= new();

        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>? _aiAgentsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel> AiAgentsCollection => _aiAgentsCollection ??= new GitHubCopilotAiAgentsCollection();

        private GitHubCopilotMappers? _gitHubCopilotMappers;
        private GitHubCopilotMappers GitHubCopilotMappers => _gitHubCopilotMappers ??= new();

        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionConfigBuilder"/> class.
        /// </summary>
        /// <param name="logger">The logger used to emit diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> is <see langword="null"/>.</exception>
        public SessionConfigBuilder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.Model"/> parameter.
        /// </summary>
        /// <param name="model">The model identifier to use for the session (e.g., "gpt-4o").</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithModel(string model)
        {
            LogSetting(nameof(_sessionConfig.Model), model);
            _sessionConfig.Model = model;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.Agent"/> parameter by name.
        /// </summary>
        /// <param name="agentIdentifier">The name of the custom agent to activate.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithAgent(string agentIdentifier)
        {
            LogSetting(nameof(_sessionConfig.Agent), agentIdentifier);
            _sessionConfig.Agent = agentIdentifier;
            return this;
        }

        /// <summary>
        /// Loads a custom agent from the specified file and sets the
        /// <see cref="SessionConfigBase.Agent"/> parameter to the agent's name.
        /// </summary>
        /// <param name="filePath">The file containing the agent YAML configuration.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="filePath"/> is <see langword="null"/>.</exception>
        public SessionConfigBuilder WithAgent(FileInfo filePath)
        {
            ArgumentNullException.ThrowIfNull(filePath);

            _logger.Info("Loading {TypeName} '{PropertyName}' from file '{FilePath}'", nameof(SessionConfig), nameof(_sessionConfig.Agent), filePath.FullName);
            var entityData = AiAgentsCollection.AddEntityData(filePath.FullName);
            _sessionConfig.Agent = entityData.ConfigurationData.Name;
            LogSetting(nameof(_sessionConfig.Agent), _sessionConfig.Agent);
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.SystemMessage"/> parameter.
        /// </summary>
        /// <param name="content">The system message content.</param>
        /// <param name="mode">The system message mode. Defaults to <see cref="SystemMessageMode.Append"/>.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithSystemMessage(string content, SystemMessageMode mode = SystemMessageMode.Append)
        {
            LogSystemMessage(mode, content);
            _sessionConfig.SystemMessage = new SystemMessageConfig { Content = content, Mode = mode };
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.AvailableTools"/> parameter.
        /// </summary>
        /// <param name="tools">The tool names to make available to the session.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithAvailableTools(params string[] tools)
        {
            var toolList = tools.ToList();
            LogCollectionSetting(nameof(_sessionConfig.AvailableTools), toolList);
            _sessionConfig.AvailableTools = toolList;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.Streaming"/> parameter.
        /// </summary>
        /// <param name="streaming">
        /// <see langword="true"/> to enable streaming of assistant message and reasoning chunks;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithStreaming(bool streaming = true)
        {
            LogSetting(nameof(_sessionConfig.Streaming), streaming);
            _sessionConfig.Streaming = streaming;
            return this;
        }

        /// <summary>
        /// Adds or replaces a <see cref="SessionConfigBase.CustomAgents"/> entry.
        /// </summary>
        /// <param name="config">The custom agent configuration to register.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="config"/> is <see langword="null"/>.</exception>
        public SessionConfigBuilder WithCustomAgentConfig(CustomAgentConfig config)
        {
            ArgumentNullException.ThrowIfNull(config);

            _logger.Info("Setting {TypeName} '{PropertyName}' parameter for agent '{AgentName}'", nameof(SessionConfig), nameof(_sessionConfig.CustomAgents), config.Name);
            _sessionConfig.CustomAgents ??= new List<CustomAgentConfig>();
            var existingAgent = _sessionConfig.CustomAgents.FirstOrDefault(a => a.Name == config.Name);

            if (existingAgent != null)
            {
                _logger.Info("Replacing existing {TypeName} '{PropertyName}' entry for agent '{AgentName}'", nameof(SessionConfig), nameof(_sessionConfig.CustomAgents), config.Name);
                _sessionConfig.CustomAgents.Remove(existingAgent);
            }
            _sessionConfig.CustomAgents.Add(config);

            return this;
        }

        /// <summary>
        /// Loads a custom agent by identifier and adds it to
        /// <see cref="SessionConfigBase.CustomAgents"/>.
        /// </summary>
        /// <param name="agentIdentifier">The identifier of the agent to load.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="agentIdentifier"/> is <see langword="null"/>.</exception>
        public SessionConfigBuilder WithCustomAgentConfig(string agentIdentifier)
        {
            ArgumentNullException.ThrowIfNull(agentIdentifier);

            _logger.Info("Loading {TypeName} '{PropertyName}' from agent identifier '{AgentIdentifier}'", nameof(SessionConfig), nameof(_sessionConfig.CustomAgents), agentIdentifier);
            var entity = AiAgentsCollection.GetEntityData(agentIdentifier);
            var config = GitHubCopilotMappers.ToCustomAgentConfig(entity);
            return WithCustomAgentConfig(config);
        }

        /// <summary>
        /// Adds or replaces an entry in <see cref="SessionConfigBase.McpServers"/>.
        /// </summary>
        /// <param name="name">The server name.</param>
        /// <param name="config">The MCP server configuration.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> or <paramref name="config"/> is <see langword="null"/>.</exception>
        public SessionConfigBuilder WithMcpServer(string name, McpServerConfig config)
        {
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(config);

            _logger.Info("Setting {TypeName} '{PropertyName}' parameter for server '{ServerName}'", nameof(SessionConfig), nameof(_sessionConfig.McpServers), name);
            _sessionConfig.McpServers ??= new Dictionary<string, McpServerConfig>();
            _sessionConfig.McpServers[name] = config;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.SkipCustomInstructions"/> parameter.
        /// </summary>
        /// <param name="skipCustomInstructions">
        /// <see langword="true"/> to suppress loading of custom instruction files;
        /// <see langword="null"/> to let the SDK choose based on mode.
        /// </param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithSkipCustomInstructions(bool? skipCustomInstructions)
        {
            LogSetting(nameof(_sessionConfig.SkipCustomInstructions), skipCustomInstructions?.ToString() ?? "null");
            _sessionConfig.SkipCustomInstructions = skipCustomInstructions;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.CustomAgentsLocalOnly"/> parameter.
        /// </summary>
        /// <param name="customAgentsLocalOnly">
        /// <see langword="true"/> to restrict custom-agent discovery to the local working directory;
        /// <see langword="null"/> to let the SDK choose based on mode.
        /// </param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithCustomAgentsLocalOnly(bool? customAgentsLocalOnly)
        {
            LogSetting(nameof(_sessionConfig.CustomAgentsLocalOnly), customAgentsLocalOnly?.ToString() ?? "null");
            _sessionConfig.CustomAgentsLocalOnly = customAgentsLocalOnly;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.ExcludedTools"/> parameter.
        /// </summary>
        /// <param name="excludedTools">The tool names to exclude from the session.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithExcludedTools(params string[] excludedTools)
        {
            var toolList = excludedTools.ToList();
            LogCollectionSetting(nameof(_sessionConfig.ExcludedTools), toolList);
            _sessionConfig.ExcludedTools = toolList;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.EnableSkills"/> parameter.
        /// </summary>
        /// <param name="enableSkills">
        /// <see langword="true"/> to enable skill loading;
        /// <see langword="false"/> to disable skill loading regardless of other settings;
        /// <see langword="null"/> to let the SDK choose based on mode.
        /// </param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithEnableSkills(bool? enableSkills)
        {
            LogSetting(nameof(_sessionConfig.EnableSkills), enableSkills?.ToString() ?? "null");
            _sessionConfig.EnableSkills = enableSkills;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.IncludeSubAgentStreamingEvents"/> parameter.
        /// </summary>
        /// <param name="includeSubAgentStreamingEvents">
        /// <see langword="true"/> to forward sub-agent streaming delta events to this connection;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithIncludeSubAgentStreamingEvents(bool includeSubAgentStreamingEvents)
        {
            LogSetting(nameof(_sessionConfig.IncludeSubAgentStreamingEvents), includeSubAgentStreamingEvents);
            _sessionConfig.IncludeSubAgentStreamingEvents = includeSubAgentStreamingEvents;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.SkillDirectories"/> parameter.
        /// </summary>
        /// <param name="skillDirectories">The directories to load skills from.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithSkillDirectories(params string[] skillDirectories)
        {
            var directoryList = skillDirectories.ToList();
            LogCollectionSetting(nameof(_sessionConfig.SkillDirectories), directoryList);
            _sessionConfig.SkillDirectories = directoryList;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.InstructionDirectories"/> parameter.
        /// </summary>
        /// <param name="instructionDirectories">The additional directories to search for custom instruction files.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithInstructionDirectories(params string[] instructionDirectories)
        {
            var directoryList = instructionDirectories.ToList();
            LogCollectionSetting(nameof(_sessionConfig.InstructionDirectories), directoryList);
            _sessionConfig.InstructionDirectories = directoryList;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.DisabledSkills"/> parameter.
        /// </summary>
        /// <param name="disabledSkills">The skill names to disable.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithDisabledSkills(params string[] disabledSkills)
        {
            var skillList = disabledSkills.ToList();
            LogCollectionSetting(nameof(_sessionConfig.DisabledSkills), skillList);
            _sessionConfig.DisabledSkills = skillList;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.EnableConfigDiscovery"/> parameter.
        /// </summary>
        /// <param name="enableConfigDiscovery">
        /// <see langword="true"/> to automatically discover MCP server configurations and skill directories;
        /// <see langword="null"/> to let the SDK choose based on mode.
        /// </param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithEnableConfigDiscovery(bool? enableConfigDiscovery)
        {
            LogSetting(nameof(_sessionConfig.EnableConfigDiscovery), enableConfigDiscovery?.ToString() ?? "null");
            _sessionConfig.EnableConfigDiscovery = enableConfigDiscovery;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.OrganizationCustomInstructions"/> parameter.
        /// </summary>
        /// <param name="organizationCustomInstructions">
        /// Organization-level custom instructions to include in the system prompt.
        /// </param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithOrganizationCustomInstructions(string? organizationCustomInstructions)
        {
            LogSetting(nameof(_sessionConfig.OrganizationCustomInstructions), $"Content={organizationCustomInstructions?.TruncateWithCount(50) ?? "null"}");
            _sessionConfig.OrganizationCustomInstructions = organizationCustomInstructions;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="SessionConfigBase.EnableOnDemandInstructionDiscovery"/> parameter.
        /// </summary>
        /// <param name="enableOnDemandInstructionDiscovery">
        /// <see langword="true"/> to enable on-demand discovery of instruction files after successful file views;
        /// <see langword="null"/> to let the SDK choose based on mode.
        /// </param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithEnableOnDemandInstructionDiscovery(bool? enableOnDemandInstructionDiscovery)
        {
            LogSetting(nameof(_sessionConfig.EnableOnDemandInstructionDiscovery), enableOnDemandInstructionDiscovery?.ToString() ?? "null");
            _sessionConfig.EnableOnDemandInstructionDiscovery = enableOnDemandInstructionDiscovery;
            return this;
        }

        /// <summary>
        /// Applies isolation-safe defaults to the session configuration.
        /// <para>
        /// This sets <see cref="SessionConfigBase.SkipCustomInstructions"/> and
        /// <see cref="SessionConfigBase.CustomAgentsLocalOnly"/> to <see langword="true"/>,
        /// and <see cref="SessionConfigBase.EnableSkills"/>,
        /// <see cref="SessionConfigBase.EnableConfigDiscovery"/>,
        /// <see cref="SessionConfigBase.EnableOnDemandInstructionDiscovery"/>, and
        /// <see cref="SessionConfigBase.IncludeSubAgentStreamingEvents"/> to <see langword="false"/>.
        /// </para>
        /// </summary>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        public SessionConfigBuilder WithIsolation()
        {
            _logger.Info("Applying isolation configuration to {TypeName}", nameof(SessionConfig));
            return WithSkipCustomInstructions(true)
                .WithCustomAgentsLocalOnly(true)
                .WithEnableSkills(false)
                .WithEnableConfigDiscovery(false)
                .WithEnableOnDemandInstructionDiscovery(false)
                .WithIncludeSubAgentStreamingEvents(false);
        }

        /// <summary>
        /// Registers a custom permission decision handler.
        /// </summary>
        /// <param name="identifier">The permission identifier.</param>
        /// <param name="permissionFunc">The asynchronous delegate that evaluates permission requests.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="identifier"/> or <paramref name="permissionFunc"/> is <see langword="null"/>.</exception>
        public SessionConfigBuilder SetPermission(string identifier, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>> permissionFunc)
        {
            ArgumentNullException.ThrowIfNull(identifier);
            ArgumentNullException.ThrowIfNull(permissionFunc);

            _logger.Info("Setting {TypeName} permission decision for '{Identifier}'", nameof(SessionConfig), identifier);
            _permissionDecisions[identifier] = async (request, invocation) =>
            {
                var decision = await permissionFunc(request, invocation);

                return decision;
            };

            return this;
        }

        /// <summary>
        /// Registers a permission decision loaded from the built-in permission collection.
        /// </summary>
        /// <param name="identifier">The identifier of the permission to load.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="identifier"/> is <see langword="null"/>.</exception>
        public SessionConfigBuilder WithPermission(string identifier)
        {
            ArgumentNullException.ThrowIfNull(identifier);

            _logger.Info("Setting {TypeName} permission '{Identifier}' from collection", nameof(SessionConfig), identifier);
            var permissionDecision = PermissionDecisionsCollection.GetByIdentifier(identifier);
            _permissionDecisions[identifier] = permissionDecision;
            return this;
        }

        /// <summary>
        /// Applies a custom transformation to the underlying <see cref="SessionConfig"/>.
        /// </summary>
        /// <param name="updateSessionConfig">A delegate that mutates the session configuration.</param>
        /// <returns>The current <see cref="SessionConfigBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="updateSessionConfig"/> is <see langword="null"/>.</exception>
        public SessionConfigBuilder WithSessionConfig(Func<SessionConfig, SessionConfig> updateSessionConfig)
        {
            ArgumentNullException.ThrowIfNull(updateSessionConfig);

            _logger.Info("Applying custom {TypeName} update", nameof(SessionConfig));
            updateSessionConfig.Invoke(_sessionConfig);
            return this;
        }

        /// <summary>
        /// Builds and returns the configured <see cref="SessionConfig"/> instance.
        /// </summary>
        /// <returns>A fully configured <see cref="SessionConfig"/> instance.</returns>
        public SessionConfig Build()
        {
            _logger.Info("Building {TypeName} Agent: {Agent}, (Model: {Model})", nameof(SessionConfig), _sessionConfig.Agent ?? "default", _sessionConfig.Model ?? "default");

            if (_sessionConfig.Agent != null)
            {
                if (AiAgentsCollection.TryGetEntityData(_sessionConfig.Agent, out var entityData))
                {
                    var model = entityData!.ConfigurationData.Model;

                    if (!string.IsNullOrEmpty(model))
                    {
                        _logger.Info("Overriding {TypeName} '{PropertyName}' parameter to '{Value}' from agent configuration", nameof(SessionConfig), nameof(_sessionConfig.Model), model);
                        _sessionConfig.Model = model;
                    }
                    var agentTools = entityData.ConfigurationData.Tools;
                    _logger.Info("Setting {TypeName} '{PropertyName}' parameter from agent configuration to '[{Value}]'", nameof(SessionConfig), nameof(_sessionConfig.AvailableTools), string.Join(", ", agentTools ?? Enumerable.Empty<string>()));
                    _sessionConfig.AvailableTools = agentTools;

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

                if (_permissionDecisions?.Count > 0)
                {

                    foreach (var permissionDecision in _permissionDecisions)
                    {
                        try
                        {
                            if (permissionDecision.Value != null)
                            {
                                var result = await permissionDecision.Value(request, invocation);
                                if (result != null)
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

            _logger.Info("{TypeName} built successfully (Model: {Model}, Agent: {Agent})", nameof(SessionConfig), _sessionConfig.Model ?? "null", _sessionConfig.Agent ?? "null");
            return _sessionConfig;
        }

        /// <summary>
        /// Logs that a scalar <see cref="SessionConfig"/> parameter is being set.
        /// </summary>
        /// <param name="propertyName">The name of the parameter being set.</param>
        /// <param name="value">The value being assigned.</param>
        private void LogSetting(string propertyName, object value)
        {
            _logger.Info("Setting {TypeName} '{PropertyName}' parameter to '{Value}'", nameof(SessionConfig), propertyName, value);
        }

        /// <summary>
        /// Logs that a collection <see cref="SessionConfig"/> parameter is being set.
        /// </summary>
        /// <param name="propertyName">The name of the parameter being set.</param>
        /// <param name="values">The collection values being assigned.</param>
        private void LogCollectionSetting(string propertyName, IEnumerable<string> values)
        {
            _logger.Info("Setting {TypeName} '{PropertyName}' parameter to '[{Value}]'", nameof(SessionConfig), propertyName, string.Join(", ", values));
        }

        /// <summary>
        /// Logs that the <see cref="SessionConfigBase.SystemMessage"/> parameter is being set.
        /// </summary>
        /// <param name="mode">The system message mode.</param>
        /// <param name="content">The system message content.</param>
        private void LogSystemMessage(SystemMessageMode mode, string? content)
        {
            _logger.Info(
                "Setting {TypeName} '{PropertyName}' parameter to 'Mode={Mode}, Content={Content}'",
                nameof(SessionConfig),
                nameof(_sessionConfig.SystemMessage),
                mode,
                content?.TruncateWithCount(50) ?? "null");
        }
    }
}
