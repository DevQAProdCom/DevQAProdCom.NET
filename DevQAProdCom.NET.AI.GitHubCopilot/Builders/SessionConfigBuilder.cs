using System.Globalization;
using DevQAProdCom.NET.AI.GitHubCopilot.Collections;
using DevQAProdCom.NET.AI.GitHubCopilot.Constants;
using DevQAProdCom.NET.AI.GitHubCopilot.Mappers;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations.Files;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Builders
{
    public class SessionConfigBuilder : IDisposable
    {
        private readonly SessionConfig _sessionConfig = new();

        private readonly Dictionary<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> _permissionDecisions = new();

        private PermissionDecisionsCollection? _permissionDecisionsCollection;
        private PermissionDecisionsCollection PermissionDecisionsCollection => _permissionDecisionsCollection ??= new();

        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>? _allAgentsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel> AllAgentsCollection => _allAgentsCollection ??= new GitHubCopilotAiAgentsCollection(_logger);

        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>? _sessionAgentsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel> SessionAgentsCollection => _sessionAgentsCollection ??= new GitHubCopilotAiAgentsCollection(_logger, initializeWithDefaultLocations: false);

        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel>? _allInstructionsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel> AllInstructionsCollection => _allInstructionsCollection ??= new GitHubCopilotAiInstructionsCollection(_logger);

        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel>? _sessionInstructionsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel> SessionInstructionsCollection => _sessionInstructionsCollection ??= new GitHubCopilotAiInstructionsCollection(_logger, initializeWithDefaultLocations: false);

        private readonly List<IAiEntityWithTYamlConfigurationType<GitHubCopilotAiInstructionYamlConfigurationModel>> _sessionInstructionEntities = new();

        private GitHubCopilotMappers? _gitHubCopilotMappers;
        private GitHubCopilotMappers GitHubCopilotMappers => _gitHubCopilotMappers ??= new();

        private readonly ILogger _logger;

        private string? _interactionConfigurationDirectory = null;

        public SessionConfigBuilder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public SessionConfigBuilder WithModel(string model)
        {
            LogSetting(nameof(_sessionConfig.Model), model);
            _sessionConfig.Model = model;
            return this;
        }

        public SessionConfigBuilder WithPrimaryAgent(string agentIdentifier)
        {
            WithAgent(agentIdentifier);
            var entityData = AllAgentsCollection.GetEntityDataByIdentifier(agentIdentifier);
            _sessionConfig.Agent = entityData.ConfigurationData.Name;
            LogSetting(nameof(_sessionConfig.Agent), _sessionConfig.Agent);
            WithModel(entityData.ConfigurationData.Model!);
            WithAvailableTools(entityData.ConfigurationData?.Tools?.ToArray()!);
            WithPermissions(entityData.ConfigurationData?.CustomPermissions?.ToArray()!);

            return this;
        }

        public SessionConfigBuilder WithPrimaryAgentFromFile(string filePath)
        {
            WithAgentFromFile(filePath);
            var entityData = SessionAgentsCollection.GetEntityDataByFilePath(filePath);
            _sessionConfig.Agent = entityData.ConfigurationData.Name;
            LogSetting(nameof(_sessionConfig.Agent), _sessionConfig.Agent);

            return this;
        }

        public SessionConfigBuilder WithAgent(string agentIdentifier)
        {
            _logger.Info("Loading {TypeName} '{PropertyName}' from agent identifier '{AgentIdentifier}'", nameof(SessionConfig), nameof(_sessionConfig.CustomAgents), agentIdentifier);
            var entityData = AllAgentsCollection.GetEntityDataByIdentifier(agentIdentifier);
            SessionAgentsCollection.AddEntityData(entityData);
            var customAgentConfig = GitHubCopilotMappers.ToCustomAgentConfig(entityData);
            WithCustomAgentConfig(customAgentConfig);

            return this;
        }

        public SessionConfigBuilder WithAgents(params string[] agentsIdentifiers)
        {
            foreach (var agentIdentifier in agentsIdentifiers)
            {
                WithAgent(agentIdentifier);
            }

            return this;
        }

        public SessionConfigBuilder WithAgentFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);
            var entityData = AllAgentsCollection.AddEntityDataFromFile(filePath);
            SessionAgentsCollection.AddEntityData(entityData);
            var customAgentConfig = GitHubCopilotMappers.ToCustomAgentConfig(entityData);
            WithCustomAgentConfig(customAgentConfig);
            return this;
        }

        public SessionConfigBuilder WithAgentsFromFiles(params string[] filePaths)
        {
            foreach (var filePath in filePaths)
            {
                WithAgentFromFile(filePath);
            }

            return this;
        }

        public SessionConfigBuilder WithAgentsFromDirectory(string directoryPath)
        {
            var entities = AllAgentsCollection.AddEntitiesDataFromDirectory(directoryPath);
            var sessionEntities = SessionAgentsCollection.AddEntitiesDataFromDirectory(directoryPath);

            foreach (var entityData in sessionEntities)
            {
                WithCustomAgentConfig(GitHubCopilotMappers.ToCustomAgentConfig(entityData));
            }

            return this;
        }

        public SessionConfigBuilder WithAgentsFromDirectories(params string[] directoriesPaths)
        {
            foreach (var directoryPath in directoriesPaths)
            {
                WithAgentsFromDirectory(directoryPath);
            }

            return this;
        }

        public SessionConfigBuilder WithInstruction(string instructionIdentifier)
        {
            _logger.Info("Loading {TypeName} instruction '{InstructionIdentifier}' from all instructions collection", nameof(SessionConfig), instructionIdentifier);
            var entityData = AllInstructionsCollection.GetEntityDataByIdentifier(instructionIdentifier);
            SessionInstructionsCollection.AddEntityData(entityData);

            return this;
        }

        public SessionConfigBuilder WithInstructions(params string[] instructionsIdentifiers)
        {
            foreach (var instructionIdentifier in instructionsIdentifiers)
            {
                WithInstruction(instructionIdentifier);
            }

            return this;
        }

        public SessionConfigBuilder WithInstruction(string instructionIdentifier, string prompt)
        {
            _logger.Info("Adding {TypeName} instruction '{InstructionIdentifier}' with custom prompt", nameof(SessionConfig), instructionIdentifier);

            var entityData = new AiEntityWithTYamlConfigurationTypeModel<GitHubCopilotAiInstructionYamlConfigurationModel>
            {
                ConfigurationData = new GitHubCopilotAiInstructionYamlConfigurationModel { Name = instructionIdentifier },
                Prompt = prompt
            };

            AllInstructionsCollection.AddEntityData(entityData);
            SessionInstructionsCollection.AddEntityData(entityData);

            return this;
        }

        public SessionConfigBuilder WithInstructionFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);
            var entityData = AllInstructionsCollection.AddEntityDataFromFile(filePath);
            SessionInstructionsCollection.AddEntityData(entityData);
            return this;
        }

        public SessionConfigBuilder WithInstructionsFromFiles(params string[] filePaths)
        {
            foreach (var filePath in filePaths)
            {
                WithInstructionFromFile(filePath);
            }

            return this;
        }

        public SessionConfigBuilder WithInstructionsFromDirectory(string directoryPath)
        {
            var entities = AllInstructionsCollection.AddEntitiesDataFromDirectory(directoryPath);
            SessionInstructionsCollection.AddEntitiesData(entities.ToArray());
            return this;
        }

        public SessionConfigBuilder WithInstructionsFromDirectories(params string[] directoryPaths)
        {
            foreach (var directoryPath in directoryPaths)
            {
                WithInstructionsFromDirectory(directoryPath);
            }

            return this;
        }

        public SessionConfigBuilder WithInstructionDirectories(params string[] instructionDirectories)
        {
            var directoryList = instructionDirectories.ToList();
            LogCollectionSetting(nameof(_sessionConfig.InstructionDirectories), directoryList);
            _sessionConfig.InstructionDirectories = directoryList;
            return this;
        }

        public SessionConfigBuilder WithWorkingDirectory(string workingDirectory)
        {
            LogSetting(nameof(_sessionConfig.WorkingDirectory), workingDirectory);
            _sessionConfig.WorkingDirectory = workingDirectory;
            return this;
        }

        public SessionConfigBuilder WithSystemMessage(string content, SystemMessageMode mode = SystemMessageMode.Append)
        {
            LogSystemMessage(mode, content);
            _sessionConfig.SystemMessage = new SystemMessageConfig { Content = content, Mode = mode };
            return this;
        }

        public SessionConfigBuilder WithAvailableTools(params string[] tools)
        {
            if (tools?.Length > 0)
            {
                var toolList = tools.ToList();
                LogCollectionSetting(nameof(_sessionConfig.AvailableTools), toolList);
                _sessionConfig.AvailableTools ??= new List<string>();

                foreach (var tool in toolList)
                {
                    if (!_sessionConfig.AvailableTools.Contains(tool))
                    {
                        _sessionConfig.AvailableTools.Add(tool);
                    }
                }
            }

            return this;
        }

        public SessionConfigBuilder WithStreaming(bool streaming = true)
        {
            LogSetting(nameof(_sessionConfig.Streaming), streaming);
            _sessionConfig.Streaming = streaming;
            return this;
        }

        public SessionConfigBuilder WithCustomAgentConfig(CustomAgentConfig config)
        {
            if (config == null)
            {
                _logger.Error("Attempted to add null {PropertyName} to {TypeName}", nameof(_sessionConfig.CustomAgents), nameof(SessionConfig));
                throw new ArgumentNullException(nameof(config), $"Custom agent configuration cannot be null when adding to {nameof(_sessionConfig.CustomAgents)}.");
            }

            _sessionConfig.CustomAgents ??= new List<CustomAgentConfig>();

            if (_sessionConfig.CustomAgents.Any(a => a.Name == config.Name))
            {
                _logger.Error("Agent with name '{AgentName}' already exists in {PropertyName} list", config.Name, nameof(_sessionConfig.CustomAgents));
                throw new InvalidOperationException($"Agent with name '{config.Name}' already exists in CustomAgentConfig list. Use {nameof(WithCustomAgentConfig)} to add a new agent config with a different name.");
            }

            _logger.Info("Adding {TypeName} '{PropertyName}' parameter as agent '{AgentName}'", nameof(SessionConfig), nameof(_sessionConfig.CustomAgents), config.Name);
            _sessionConfig.CustomAgents.Add(config);

            return this;
        }

        public SessionConfigBuilder WithMcpServer(string name, McpServerConfig config)
        {
            _logger.Info("Setting {TypeName} '{PropertyName}' parameter for server '{ServerName}'", nameof(SessionConfig), nameof(_sessionConfig.McpServers), name);
            _sessionConfig.McpServers ??= new Dictionary<string, McpServerConfig>();
            _sessionConfig.McpServers[name] = config;
            return this;
        }

        public SessionConfigBuilder WithSkipCustomInstructions(bool? skipCustomInstructions)
        {
            LogSetting(nameof(_sessionConfig.SkipCustomInstructions), skipCustomInstructions?.ToString() ?? "null");
            _sessionConfig.SkipCustomInstructions = skipCustomInstructions;
            return this;
        }

        public SessionConfigBuilder WithCustomAgentsLocalOnly(bool? customAgentsLocalOnly)
        {
            LogSetting(nameof(_sessionConfig.CustomAgentsLocalOnly), customAgentsLocalOnly?.ToString() ?? "null");
            _sessionConfig.CustomAgentsLocalOnly = customAgentsLocalOnly;
            return this;
        }

        public SessionConfigBuilder WithExcludedTools(params string[] excludedTools)
        {
            var toolList = excludedTools.ToList();
            LogCollectionSetting(nameof(_sessionConfig.ExcludedTools), toolList);
            _sessionConfig.ExcludedTools = toolList;
            return this;
        }

        public SessionConfigBuilder WithEnableSkills(bool? enableSkills)
        {
            LogSetting(nameof(_sessionConfig.EnableSkills), enableSkills?.ToString() ?? "null");
            _sessionConfig.EnableSkills = enableSkills;
            return this;
        }

        public SessionConfigBuilder WithIncludeSubAgentStreamingEvents(bool includeSubAgentStreamingEvents)
        {
            LogSetting(nameof(_sessionConfig.IncludeSubAgentStreamingEvents), includeSubAgentStreamingEvents);
            _sessionConfig.IncludeSubAgentStreamingEvents = includeSubAgentStreamingEvents;
            return this;
        }

        public SessionConfigBuilder WithSkillDirectories(params string[] skillDirectories)
        {
            var directoryList = skillDirectories.ToList();
            LogCollectionSetting(nameof(_sessionConfig.SkillDirectories), directoryList);
            _sessionConfig.SkillDirectories = directoryList;
            return this;
        }

        public SessionConfigBuilder WithDisabledSkills(params string[] disabledSkills)
        {
            var skillList = disabledSkills.ToList();
            LogCollectionSetting(nameof(_sessionConfig.DisabledSkills), skillList);
            _sessionConfig.DisabledSkills = skillList;
            return this;
        }

        public SessionConfigBuilder WithEnableConfigDiscovery(bool? enableConfigDiscovery)
        {
            LogSetting(nameof(_sessionConfig.EnableConfigDiscovery), enableConfigDiscovery?.ToString() ?? "null");
            _sessionConfig.EnableConfigDiscovery = enableConfigDiscovery;
            return this;
        }

        public SessionConfigBuilder WithOrganizationCustomInstructions(string? organizationCustomInstructions)
        {
            LogSetting(nameof(_sessionConfig.OrganizationCustomInstructions), $"Content={organizationCustomInstructions?.TruncateWithCount(50) ?? "null"}");
            _sessionConfig.OrganizationCustomInstructions = organizationCustomInstructions;
            return this;
        }

        public SessionConfigBuilder WithEnableOnDemandInstructionDiscovery(bool? enableOnDemandInstructionDiscovery)
        {
            LogSetting(nameof(_sessionConfig.EnableOnDemandInstructionDiscovery), enableOnDemandInstructionDiscovery?.ToString() ?? "null");
            _sessionConfig.EnableOnDemandInstructionDiscovery = enableOnDemandInstructionDiscovery;
            return this;
        }

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

        public SessionConfigBuilder WithPermission(string identifier)
        {
            ArgumentNullException.ThrowIfNull(identifier);

            _logger.Info("Setting {TypeName} permission '{Identifier}' from collection", nameof(SessionConfig), identifier);
            var permissionDecision = PermissionDecisionsCollection.GetByIdentifier(identifier);
            _permissionDecisions[identifier] = permissionDecision;
            return this;
        }

        public SessionConfigBuilder WithPermissions(params string[] identifiers)
        {
            if (identifiers?.Count() > 0)
                foreach (var identifier in identifiers)
                {
                    WithPermission(identifier);
                }

            return this;
        }

        public SessionConfigBuilder WithSessionConfig(Func<SessionConfig, SessionConfig> updateSessionConfig)
        {
            ArgumentNullException.ThrowIfNull(updateSessionConfig);

            _logger.Info("Applying custom {TypeName} update", nameof(SessionConfig));
            updateSessionConfig.Invoke(_sessionConfig);
            return this;
        }

        public SessionConfigBuilder WithInteractionConfigurationDirectory(string directoryPath)
        {
            LogSetting(nameof(_interactionConfigurationDirectory), directoryPath);
            _interactionConfigurationDirectory = directoryPath;
            return this;
        }

        public SessionConfig Build()
        {
            _logger.Info("Building {TypeName} Agent: {Agent}, (Model: {Model})", nameof(SessionConfig), _sessionConfig.Agent ?? "default", _sessionConfig.Model ?? "default");
            CreateFolderWithInteractionConfigurationData();
            SetUpOnPermissionRequest();
            SetUpInstructionDirectories();
            _logger.Info("{TypeName} built successfully Agent: {Agent}, (Model: {Model})", nameof(SessionConfig), _sessionConfig.Agent ?? "default", _sessionConfig.Model ?? "default");
            return _sessionConfig;
        }

        private void SetUpOnPermissionRequest()
        {
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
        }

        private void SetUpInstructionDirectories()
        {
            if (_sessionConfig.InstructionDirectories?.Count > 0)
                return;

            _sessionConfig.InstructionDirectories = new List<string>() { Path.Combine(_interactionConfigurationDirectory, Const.Directories.INSTRUCTIONS) };
        }

        private void CreateFolderWithInteractionConfigurationData()
        {
            if (string.IsNullOrEmpty(_interactionConfigurationDirectory))
                _interactionConfigurationDirectory = Path.Combine(Path.GetTempPath(), "AiInterationSession" + DateTime.UtcNow.ToString("yyyy-MM-dd_hh-mm-ss.fffffff", CultureInfo.InvariantCulture));
            else
                IoUtils.CleanDirectory(_interactionConfigurationDirectory);

            SaveAiAgents(_interactionConfigurationDirectory);
            SaveAiInstructions(_interactionConfigurationDirectory);
        }

        private void SaveAiAgents(string rootDirectory)
        {
            var agentsDirectory = Path.Combine(rootDirectory, Const.Directories.AGENTS);
            var primaryAgentsDirectory = Path.Combine(agentsDirectory, Const.Directories.PRIMARY);
            var subAgentsDirectory = Path.Combine(agentsDirectory, Const.Directories.SUB_AGENTS);

            IoUtils.CreateDirectory(primaryAgentsDirectory);
            IoUtils.CreateDirectory(subAgentsDirectory);

            var primaryAgentName = _sessionConfig.Agent;

            // Save Primary Agent
            if (!string.IsNullOrEmpty(primaryAgentName))
            {
                var primaryAgent = SessionAgentsCollection.GetEntityDataByIdentifier(primaryAgentName);
                SaveAgentFile(primaryAgent, primaryAgentsDirectory);
            }

            // Save Sub-Agents
            foreach (var agent in SessionAgentsCollection)
            {
                if (agent.ConfigurationData.Name == primaryAgentName)
                {
                    continue;
                }

                SaveAgentFile(agent, subAgentsDirectory);
            }
        }

        private void SaveAiInstructions(string rootDirectory)
        {
            var instructionsDirectory = Path.Combine(rootDirectory, Const.Directories.INSTRUCTIONS);
            IoUtils.CreateDirectory(instructionsDirectory);

            foreach (var instruction in _sessionInstructionEntities)
            {
                if (!string.IsNullOrEmpty(instruction.FilePath))
                {
                    var destinationFile = GetUniqueFilePathOrDefault(instructionsDirectory, Path.GetFileNameWithoutExtension(instruction.FilePath), Path.GetExtension(instruction.FilePath));
                    File.Copy(instruction.FilePath, destinationFile);
                }
                else
                {
                    var fileName = $"{instruction.ConfigurationData.Name}.instructions.md";
                    var destinationFile = Path.Combine(instructionsDirectory, fileName);

                    if (IoUtils.FileExists(destinationFile))
                    {
                        throw new InvalidOperationException(
                            $"Instruction with name '{instruction.ConfigurationData.Name}' already exists in '{instructionsDirectory}'. " +
                            $"Use {nameof(WithInstruction)}(string instructionIdentifier, string prompt) to add an instruction with a different name.");
                    }

                    File.WriteAllText(destinationFile, instruction.Prompt);
                }
            }
        }

        private void SaveAgentFile(IAiEntityWithTYamlConfigurationType<GitHubCopilotAiAgentYamlConfigurationModel> aiAgent, string destinationDirectory)
        {
            if (!string.IsNullOrEmpty(aiAgent.FilePath))
                IoUtils.FileCopy(aiAgent.FilePath, destinationDirectory);
            else
            {
                if (string.IsNullOrEmpty(aiAgent.ConfigurationData?.Name))
                {
                    throw new ArgumentException("Agent configuration name is null or empty, but must have a valid name to save the agent file.");
                }

                var destinationPath = GetUniqueFilePathOrDefault(destinationDirectory, aiAgent.ConfigurationData.Name, FileExtension.Md.GetDescriptionAttributeValue());
                IoUtils.WriteAllText(destinationPath, aiAgent.ToMdFileContent());
            }
        }

        private string GetUniqueFilePathOrDefault(string directory, string fileNameWithoutExtension, string extension)
        {
            var filePath = fileNameWithoutExtension.ToFilePathWithFileNameTruncationWithExtensionOrDefault(extension: extension, directoryPath: directory, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation: 10);

            if (!IoUtils.FileExists(filePath))
            {
                return filePath;
            }

            var counter = 1;

            while (true)
            {
                var fileNameWithouExtensionAfterTruncationOrDefault = Path.GetFileNameWithoutExtension(filePath);
                extension = Path.GetExtension(filePath);

                var duplicationSuffix = $"({counter})";
                var newFileNameWithoutExtension = fileNameWithouExtensionAfterTruncationOrDefault.Substring(0, Math.Max(0, fileNameWithouExtensionAfterTruncationOrDefault.Length - duplicationSuffix.Length)) + duplicationSuffix;

                var newFilePath = Path.Combine(directory, newFileNameWithoutExtension + extension);

                if (!IoUtils.FileExists(newFilePath))
                {
                    _logger.Warning($"Two files with the same name '{filePath}' were found; renaming to '{newFilePath}'");
                    return newFilePath;
                }

                counter++;
            }
        }

        public void Dispose()
        {
            if (!string.IsNullOrEmpty(_interactionConfigurationDirectory) && IoUtils.DirectoryExists(_interactionConfigurationDirectory))
            {
                IoUtils.DeleteDirectory(_interactionConfigurationDirectory);
            }
        }

        private void LogSetting(string propertyName, object value)
        {
            _logger.Info("Setting {TypeName} '{PropertyName}' parameter to '{Value}'", nameof(SessionConfig), propertyName, value);
        }

        private void LogCollectionSetting(string propertyName, IEnumerable<string> values)
        {
            _logger.Info("Setting {TypeName} '{PropertyName}' parameter to '[{Value}]'", nameof(SessionConfig), propertyName, string.Join(", ", values));
        }

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
