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
using Microsoft.EntityFrameworkCore.Storage;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Builders
{
    public class SessionConfigBuilder : IDisposable
    {
        private readonly SessionConfig _sessionConfig = new();

        private readonly Dictionary<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> _permissionDecisions = new();

        private PermissionDecisionsCollection? _permissionDecisionsCollection;
        private PermissionDecisionsCollection PermissionDecisionsCollection => _permissionDecisionsCollection ??= new();

        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>? _allAgentsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel> AllAgentsCollection => _allAgentsCollection ??= new GitHubCopilotAiAgentsCollection(_logger, collectionIdentifier: nameof(AllAgentsCollection));

        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>? _sessionAgentsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel> SessionAgentsCollection => _sessionAgentsCollection ??= new GitHubCopilotAiAgentsCollection(_logger, initializeWithDefaultLocations: false, collectionIdentifier: nameof(SessionAgentsCollection));

        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel>? _allInstructionsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel> AllInstructionsCollection => _allInstructionsCollection ??= new GitHubCopilotAiInstructionsCollection(_logger, collectionIdentifier: nameof(AllInstructionsCollection));

        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel>? _sessionInstructionsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel> SessionInstructionsCollection => _sessionInstructionsCollection ??= new GitHubCopilotAiInstructionsCollection(_logger, initializeWithDefaultLocations: false, collectionIdentifier: nameof(SessionInstructionsCollection));

        private IAiEntitiesCollection<GitHubCopilotAiSkillYamlConfigurationModel>? _allSkillsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiSkillYamlConfigurationModel> AllSkillsCollection => _allSkillsCollection ??= new GitHubCopilotAiSkillsCollection(_logger, collectionIdentifier: nameof(AllSkillsCollection));

        private IAiEntitiesCollection<GitHubCopilotAiSkillYamlConfigurationModel>? _sessionSkillsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiSkillYamlConfigurationModel> SessionSkillsCollection => _sessionSkillsCollection ??= new GitHubCopilotAiSkillsCollection(_logger, initializeWithDefaultLocations: false, collectionIdentifier: nameof(SessionSkillsCollection));

        private GitHubCopilotMappers? _gitHubCopilotMappers;
        private GitHubCopilotMappers GitHubCopilotMappers => _gitHubCopilotMappers ??= new();

        private readonly ILogger _logger;

        private string? _interactionConfigurationDirectory = null;

        //private string? _baseDirectory = null;

        public SessionConfigBuilder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //public SessionConfigBuilder WithBaseDirectory(string baseDirectory)
        //{
        //    _baseDirectory = baseDirectory;
        //    return this;
        //}

        public SessionConfigBuilder WithModel(string model)
        {
            LogSetting(nameof(_sessionConfig.Model), model);
            _sessionConfig.Model = model;
            return this;
        }


        #region Agents

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
            IoUtils.CheckFileMustExist(filePath);
            var entityData = AllAgentsCollection.AddEntityDataFromFile(filePath);
            WithPrimaryAgent(entityData.ConfigurationData.Name);

            return this;
        }

        public SessionConfigBuilder WithAgent(string agentIdentifier)
        {
            _logger.Info("{TypeName} Loading '{PropertyName}' from agent identifier '{AgentIdentifier}'", $"[{nameof(SessionConfigBuilder)}]", nameof(_sessionConfig.CustomAgents), agentIdentifier);
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

        #endregion Agents

        #region Instructions

        public SessionConfigBuilder WithInstruction(string instructionIdentifier)
        {
            _logger.Info("{TypeName} Loading instruction with identifier '{InstructionIdentifier}' from all instructions collection.", $"[{nameof(SessionConfigBuilder)}]", instructionIdentifier);
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
            _logger.Info("{TypeName} Adding instruction '{InstructionIdentifier}' with custom prompt.", $"[{nameof(SessionConfigBuilder)}]", instructionIdentifier);

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

        #endregion Instructions

        #region Skills

        public SessionConfigBuilder WithSkill(string skillIdentifier)
        {
            _logger.Info("{TypeName} Loading skill with identifier '{SkillIdentifier}' from all skills collection.", $"[{nameof(SessionConfigBuilder)}]", skillIdentifier);
            var entityData = AllSkillsCollection.GetEntityDataByIdentifier(skillIdentifier);
            SessionSkillsCollection.AddEntityData(entityData);

            return this;
        }

        public SessionConfigBuilder WithSkills(params string[] skillsIdentifiers)
        {
            foreach (var skillIdentifier in skillsIdentifiers)
            {
                WithSkill(skillIdentifier);
            }

            return this;
        }

        public SessionConfigBuilder WithSkill(string skillIdentifier, string prompt)
        {
            _logger.Info("{TypeName} Adding skill '{SkillIdentifier}' with custom prompt.", $"[{nameof(SessionConfigBuilder)}]", skillIdentifier);

            var entityData = new AiEntityWithTYamlConfigurationTypeModel<GitHubCopilotAiSkillYamlConfigurationModel>
            {
                ConfigurationData = new GitHubCopilotAiSkillYamlConfigurationModel { Name = skillIdentifier },
                Prompt = prompt
            };

            AllSkillsCollection.AddEntityData(entityData);
            SessionSkillsCollection.AddEntityData(entityData);

            return this;
        }

        public SessionConfigBuilder WithSkillFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);
            var entityData = AllSkillsCollection.AddEntityDataFromFile(filePath);
            SessionSkillsCollection.AddEntityData(entityData);
            return this;
        }

        public SessionConfigBuilder WithSkillsFromFiles(params string[] filePaths)
        {
            foreach (var filePath in filePaths)
            {
                WithSkillFromFile(filePath);
            }

            return this;
        }

        public SessionConfigBuilder WithSkillsFromDirectory(string directoryPath)
        {
            var entities = AllSkillsCollection.AddEntitiesDataFromDirectory(directoryPath);
            SessionSkillsCollection.AddEntitiesData(entities.ToArray());
            return this;
        }

        public SessionConfigBuilder WithSkillsFromDirectories(params string[] directoryPaths)
        {
            foreach (var directoryPath in directoryPaths)
            {
                WithSkillsFromDirectory(directoryPath);
            }

            return this;
        }


        #endregion Skills

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
                _logger.Error("{TypeName} Attempted to add null {PropertyName}.", nameof(_sessionConfig.CustomAgents), $"[{nameof(SessionConfigBuilder)}]");
                throw new ArgumentNullException(nameof(config), $"Custom agent configuration cannot be null when adding to {nameof(_sessionConfig.CustomAgents)}.");
            }

            _sessionConfig.CustomAgents ??= new List<CustomAgentConfig>();

            if (_sessionConfig.CustomAgents.Any(a => a.Name == config.Name))
            {
                _logger.Error("Agent with name '{AgentName}' already exists in {PropertyName} list", config.Name, nameof(_sessionConfig.CustomAgents));
                throw new InvalidOperationException($"Agent with name '{config.Name}' already exists in CustomAgentConfig list. Use {nameof(WithCustomAgentConfig)} to add a new agent config with a different name.");
            }

            _logger.Info("{TypeName} Adding '{PropertyName}' parameter as agent '{AgentName}'.", $"[{nameof(SessionConfigBuilder)}]", nameof(_sessionConfig.CustomAgents), config.Name);
            _sessionConfig.CustomAgents.Add(config);

            return this;
        }

        public SessionConfigBuilder WithMcpServer(string name, McpServerConfig config)
        {
            _logger.Info("{TypeName} Setting '{PropertyName}' parameter for server '{ServerName}'", $"[{nameof(SessionConfigBuilder)}]", nameof(_sessionConfig.McpServers), name);
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

        public SessionConfigBuilder WithEnableSkills(bool enableSkills)
        {
            LogSetting(nameof(_sessionConfig.EnableSkills), enableSkills);
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

        public SessionConfigBuilder WithFullIsolation()
        {
            _logger.Info("{TypeName} Applying Full Isolation configuration.", $"[{nameof(SessionConfigBuilder)}]");
            return WithSkipCustomInstructions(true)
                .WithCustomAgentsLocalOnly(true)
                .WithEnableSkills(false)
                .WithEnableConfigDiscovery(false)
                .WithEnableOnDemandInstructionDiscovery(false)
                .WithIncludeSubAgentStreamingEvents(false);
        }

        public SessionConfigBuilder WithSelectiveIsolation() //WithEnhancedIsolation//WithReinforcedIsolation
        {
            _logger.Info("{TypeName} Applying Selective Isolation configuration.", $"[{nameof(SessionConfigBuilder)}]");
            return WithSkipCustomInstructions(false)
                .WithEnableOnDemandInstructionDiscovery(true)
                .WithCustomAgentsLocalOnly(true)
                .WithEnableSkills(false)
                .WithEnableConfigDiscovery(false)
                .WithIncludeSubAgentStreamingEvents(false);
        }

        public SessionConfigBuilder SetPermission(string identifier, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>> permissionFunc)
        {
            ArgumentNullException.ThrowIfNull(identifier);
            ArgumentNullException.ThrowIfNull(permissionFunc);

            _logger.Info("{TypeName} Setting permission decision for '{Identifier}'.", $"[{nameof(SessionConfigBuilder)}]", identifier);
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

            _logger.Info("{TypeName} Setting permission '{Identifier}' from collection.", $"[{nameof(SessionConfigBuilder)}]", identifier);
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

            _logger.Info("{TypeName} Applying custom {TypeName} update.", $"[{nameof(SessionConfigBuilder)}]");
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
            _logger.Info("{TypeName} Building Agent: {Agent}, (Model: {Model}).", $"[{nameof(SessionConfigBuilder)}]", _sessionConfig.Agent ?? "default", _sessionConfig.Model ?? "default");
            CreateFolderWithInteractionConfigurationData();
            SetUpOnPermissionRequest();
            SetUpInstructionDirectories();
            SetUpSkillsDirectories();
            _logger.Info("{TypeName} Built successfully Agent: {Agent}, (Model: {Model}).", $"[{nameof(SessionConfigBuilder)}]", _sessionConfig.Agent ?? "default", _sessionConfig.Model ?? "default");
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
            if ((_sessionConfig.InstructionDirectories == null || _sessionConfig.InstructionDirectories.Count <= 0) && !string.IsNullOrEmpty(_interactionConfigurationDirectory))
            {
                var instructionsDirectory = Path.Combine(_interactionConfigurationDirectory);

                if (Directory.Exists(instructionsDirectory))
                {
                    _sessionConfig.InstructionDirectories = new List<string>() { instructionsDirectory };
                    _logger.Info("{TypeName} Setting '{PropertyName}' parameter to '[{Value}]'.", $"[{nameof(SessionConfigBuilder)}]", nameof(_sessionConfig.InstructionDirectories), string.Join(", ", _sessionConfig.InstructionDirectories));
                }
            }

            //if (_sessionConfig.InstructionDirectories?.Count > 0)
            //    foreach (var directory in _sessionConfig.InstructionDirectories)
            //    {
            //        IoUtils.DirectoryCopy(Path.Combine(directory, ".github", Const.Directories.INSTRUCTIONS), Path.Combine(_sessionConfig.WorkingDirectory, ".github", Const.Directories.INSTRUCTIONS), overwrite: true);
            //        IoUtils.DirectoryCopy(Path.Combine(directory, ".github", Const.Directories.INSTRUCTIONS), Path.Combine(_baseDirectory, ".github", Const.Directories.INSTRUCTIONS), overwrite: true);
            //    }
        }

        private void SetUpSkillsDirectories()
        {
            if ((_sessionConfig.SkillDirectories == null || _sessionConfig.SkillDirectories.Count <= 0) && !string.IsNullOrEmpty(_interactionConfigurationDirectory))
            {
                var skillsDirectory = Path.Combine(_interactionConfigurationDirectory);

                if (Directory.Exists(skillsDirectory))
                {
                    _sessionConfig.SkillDirectories = new List<string>() { skillsDirectory };
                    _logger.Info("{TypeName} Setting '{PropertyName}' parameter to '[{Value}]'.", $"[{nameof(SessionConfigBuilder)}]", nameof(_sessionConfig.SkillDirectories), string.Join(", ", _sessionConfig.SkillDirectories));
                }
            }
        }

        private void CreateFolderWithInteractionConfigurationData()
        {
            if (string.IsNullOrEmpty(_interactionConfigurationDirectory))
                _interactionConfigurationDirectory = Path.Combine(Path.GetTempPath(), "AiInterationSession" + DateTime.UtcNow.ToString("yyyy-MM-dd_hh-mm-ss.fffffff", CultureInfo.InvariantCulture));
            else
                IoUtils.CleanDirectory(_interactionConfigurationDirectory);

            SaveAiAgents(_interactionConfigurationDirectory);
            SaveAiInstructions(_interactionConfigurationDirectory);
            SaveAiSkills(_interactionConfigurationDirectory);
        }

        private void SaveAiAgents(string rootDirectory)
        {
            var agentsDirectory = Const.Directories.GetGitHubAgentsDirectory(rootDirectory);

            foreach (var agent in SessionAgentsCollection)
            {
                if (!string.IsNullOrEmpty(agent.FilePath))
                {
                    var destinationFilePath = GetUniqueFilePathOrDefault(agentsDirectory, Path.GetFileNameWithoutExtension(agent.FilePath), Path.GetExtension(agent.FilePath));
                    IoUtils.FileCopy(agent.FilePath, destinationFilePath);
                }
                else
                {
                    if (string.IsNullOrEmpty(agent.ConfigurationData?.Name))
                    {
                        throw new ArgumentException("Agent configuration name is either null or empty, but must have a valid name to save the agent file.");
                    }

                    var destinationFilePath = GetUniqueFilePathOrDefault(agentsDirectory, agent.ConfigurationData.Name, FilesConstants.AGENT_MD);
                    IoUtils.WriteAllText(destinationFilePath, agent.ToMdFileContent());
                }
            }
        }

        private void SaveAiInstructions(string rootDirectory)
        {
            var instructionsDirectory = Const.Directories.GetGitHubInstructionsDirectory(rootDirectory);

            foreach (var instruction in SessionInstructionsCollection)
            {
                if (!string.IsNullOrEmpty(instruction.FilePath))
                {
                    var destinationFilePath = GetUniqueFilePathOrDefault(instructionsDirectory, Path.GetFileNameWithoutExtension(instruction.FilePath), Path.GetExtension(instruction.FilePath));
                    IoUtils.FileCopy(instruction.FilePath, destinationFilePath);
                }
                else
                {
                    if (string.IsNullOrEmpty(instruction.ConfigurationData?.Name))
                    {
                        throw new ArgumentException("Instruction configuration name is either null or empty, but must have a valid name to save the instruction file.");
                    }

                    var destinationFilePath = GetUniqueFilePathOrDefault(instructionsDirectory, instruction.ConfigurationData.Name, FilesConstants.INSTRUCTIONS_MD);
                    IoUtils.WriteAllText(destinationFilePath, instruction.ToMdFileContent());
                }
            }
        }

        private void SaveAiSkills(string rootDirectory)
        {
            var skillsDirectory = Const.Directories.GetGitHubSkillsDirectory(rootDirectory);

            foreach (var skill in SessionSkillsCollection)
            {
                if (!string.IsNullOrEmpty(skill.FilePath))
                {
                    var skillDirectory = Path.GetDirectoryName(skill.FilePath);
                    var destinationDirectoryPath = Path.Combine(skillsDirectory, skillDirectory);

                    if(Directory.Exists(destinationDirectoryPath))
                        throw new InvalidOperationException($"Destination directory '{destinationDirectoryPath}' already exists. Cannot copy skill '{skill.ConfigurationData.Name}' directory. " +
                            $"Check if multiple skills have directories with the same name where their '{FilesConstants.SKILLS_MD}' files reside, as skills are copied as full directories with all files related to particular skills.");

                    IoUtils.DirectoryCopy(skillDirectory, destinationDirectoryPath);
                }
                else
                {
                    if (string.IsNullOrEmpty(skill.ConfigurationData?.Name))
                    {
                        throw new ArgumentException("Skill configuration name is either null or empty, but must have a valid name to save the skill file.");
                    }

                    var destinationPath = Path.Combine(skillsDirectory, skill.ConfigurationData.Name, FilesConstants.SKILLS_MD);
                    IoUtils.WriteAllText(destinationPath, skill.ToMdFileContent());
                }
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


           //TODO refactor in another way - it should first find if not such file already exists with the same name but with suffix
           //cause now i suppose it will add suffix (1)(1) etc, but i need it to be 2,3,4

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
            _logger.Info("{TypeName} Setting '{PropertyName}' parameter to '{Value}'.", $"[{nameof(SessionConfigBuilder)}]", propertyName, value);
        }

        private void LogCollectionSetting(string propertyName, IEnumerable<string> values)
        {
            _logger.Info("{TypeName} Setting '{PropertyName}' parameter to '[{Value}]'.", $"[{nameof(SessionConfigBuilder)}]", propertyName, string.Join(", ", values));
        }

        private void LogSystemMessage(SystemMessageMode mode, string? content)
        {
            _logger.Info("{TypeName} Setting '{PropertyName}' parameter to 'Mode={Mode}, Content={Content}'.", $"[{nameof(SessionConfigBuilder)}]", nameof(_sessionConfig.SystemMessage), mode, content?.TruncateWithCount(50) ?? "null");
        }
    }
}
