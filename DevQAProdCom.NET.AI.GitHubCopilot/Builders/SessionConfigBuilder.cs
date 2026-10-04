using System.Text.Json;
using System.Text.Json.Nodes;
using DevQAProdCom.NET.AI.GitHubCopilot.Constants;
using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.Mappers;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Collections;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.AI.Shared.Interfaces.McpServers;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.AI.Shared.Utils;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.Constans;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;
using Microsoft.Agents.AI;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Builders
{
    public class SessionConfigBuilder : IDisposable
    {
        private readonly SessionConfig _sessionConfig = new();


        private GitHubCopilotPermissionDecisionsCollection? _allPermissionDecisionsCollection;
        private GitHubCopilotPermissionDecisionsCollection AllPermissionDecisionsCollection => _allPermissionDecisionsCollection ??= new();

        private readonly Dictionary<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> _sessionPermissionDecisions = new();


        private bool _useAgentsExtendedSearch = false;

        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>? _allAgentsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel> AllAgentsCollection => _allAgentsCollection ??= new GitHubCopilotAiAgentsCollection(_logger, initializeFromDefaultLocations: true, collectionIdentifier: nameof(AllAgentsCollection), useExtendedSearch: _useAgentsExtendedSearch);

        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel>? _sessionAgentsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiAgentYamlConfigurationModel> SessionAgentsCollection => _sessionAgentsCollection ??= new GitHubCopilotAiAgentsCollection(_logger, collectionIdentifier: nameof(SessionAgentsCollection));


        private bool _useInstructionsExtendedSearch = false;

        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel>? _allInstructionsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel> AllInstructionsCollection => _allInstructionsCollection ??= new GitHubCopilotAiInstructionsCollection(_logger, initializeFromDefaultLocations: true, collectionIdentifier: nameof(AllInstructionsCollection), useExtendedSearch: _useInstructionsExtendedSearch);

        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel>? _sessionInstructionsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiInstructionYamlConfigurationModel> SessionInstructionsCollection => _sessionInstructionsCollection ??= new GitHubCopilotAiInstructionsCollection(_logger, collectionIdentifier: nameof(SessionInstructionsCollection));

        private bool _useSkillsExtendedSearch = false;

        private IAiEntitiesCollection<GitHubCopilotAiSkillYamlConfigurationModel>? _allSkillsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiSkillYamlConfigurationModel> AllSkillsCollection => _allSkillsCollection ??= new GitHubCopilotAiSkillsCollection(_logger, initializeFromDefaultLocations: true, collectionIdentifier: nameof(AllSkillsCollection), useExtendedSearch: _useSkillsExtendedSearch);

        private IAiEntitiesCollection<GitHubCopilotAiSkillYamlConfigurationModel>? _sessionSkillsCollection;
        private IAiEntitiesCollection<GitHubCopilotAiSkillYamlConfigurationModel> SessionSkillsCollection => _sessionSkillsCollection ??= new GitHubCopilotAiSkillsCollection(_logger, collectionIdentifier: nameof(SessionSkillsCollection));

        private IFileBasedHooksCollection _allFileBasedHooksCollection;
        private IFileBasedHooksCollection _sessionFileBasedHooksCollection;

        private IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook> _allSdkBasedSessionHooksCollection;
        private IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook> _sessionSdkBasedSessionHooksCollection;
        private SessionHooksBuilder _sessionHooksBuilder;

        private IFileBasedMcpServersCollection _allFileBasedMcpServersCollection;
        private IFileBasedMcpServersCollection _sessionFileBasedMcpServersCollection;

        private IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer> _allSdkBasedMcpServersCollection;
        private IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer> _sessionSdkBasedMcpServersCollection;

        private IGitHubCopilotMappers _gitHubCopilotMappers;

        private readonly ILogger _logger;

        private string? _directoryForInteractionConfigurationData = null;

        private CopilotClientMode _copilotClientMode = CopilotClientMode.Empty;

        private bool _gitHubDirectoryExistedAtStart;
        private bool _gitHubInitialDirectoryExistedAtStart;
        private bool _isGitHubDirectoryInitialReserveCopyCreated;

        public SessionConfigBuilder(
            ILogger logger,

            IGitHubCopilotMappers? gitHubCopilotMappers = null,
            IFileBasedMcpServersSearcher? mcpServersSearcher = null,
            ILocationsProvider? mcpServersDefaultLocationsProvider = null,

            IFileBasedMcpServersCollection? allFileBasedMcpServersCollection = null,
            IFileBasedMcpServersCollection? sessionFileBasedMcpServersCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>? allSdkBasedMcpServersCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>? sessionSdkBasedMcpServersCollection = null,

            IFileBasedHooksSearcher? hooksSearcher = null,
            ILocationsProvider? hooksDefaultLocationsProvider = null,
            IFileBasedHooksCollection? allFileBasedHooksCollection = null,
            IFileBasedHooksCollection? sessionFileBasedHooksCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>? allSdkBasedSessionHooksCollection = null,
            IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>? sessionSdkBasedSessionHooksCollection = null)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _gitHubCopilotMappers = gitHubCopilotMappers ?? new GitHubCopilotMappers(_logger);

            WithClientMode(CopilotClientMode.Empty);

            //McpServers
            var fileBasedMcpServersSearcher = mcpServersSearcher ?? new GitHubCopilotFileBasedMcpServersSearcher(_logger);
            _allFileBasedMcpServersCollection = allFileBasedMcpServersCollection ?? new FileBasedMcpServersCollection(_logger, fileBasedMcpServersSearcher, mcpServersDefaultLocationsProvider, nameof(_allFileBasedMcpServersCollection).ToNameOf());
            _sessionFileBasedMcpServersCollection = sessionFileBasedMcpServersCollection ?? new FileBasedMcpServersCollection(_logger, fileBasedMcpServersSearcher, collectionIdentifier: nameof(_sessionFileBasedMcpServersCollection).ToNameOf());

            _allSdkBasedMcpServersCollection = allSdkBasedMcpServersCollection ?? new IdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>(_logger, collectionIdentifier: nameof(_allSdkBasedMcpServersCollection).ToNameOf());
            _sessionSdkBasedMcpServersCollection = sessionSdkBasedMcpServersCollection ?? new IdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>(_logger, collectionIdentifier: nameof(_sessionSdkBasedMcpServersCollection).ToNameOf());

            //Hooks
            var fileBasedHooksSearcher = hooksSearcher ?? new GitHubCopilotFileBasedHooksSearcher(_logger);
            _allFileBasedHooksCollection = allFileBasedHooksCollection ?? new FileBasedHooksCollection(_logger, fileBasedHooksSearcher, hooksDefaultLocationsProvider, nameof(_allFileBasedHooksCollection).ToNameOf());
            _sessionFileBasedHooksCollection = sessionFileBasedHooksCollection ?? new FileBasedHooksCollection(_logger, fileBasedHooksSearcher, collectionIdentifier: nameof(_sessionFileBasedHooksCollection).ToNameOf());

            _allSdkBasedSessionHooksCollection = allSdkBasedSessionHooksCollection ?? new IdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>(_logger, collectionIdentifier: nameof(_allSdkBasedSessionHooksCollection).ToNameOf());
            _sessionSdkBasedSessionHooksCollection = sessionSdkBasedSessionHooksCollection ?? new IdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>(_logger, collectionIdentifier: nameof(_sessionSdkBasedSessionHooksCollection).ToNameOf());
        }

        //private string? _baseDirectory = null;

        //public SessionConfigBuilder WithBaseDirectory(string baseDirectory)
        //{
        //    _baseDirectory = baseDirectory;
        //    return this;
        //}

        private string _permissionRequestLogFile;
        public SessionConfigBuilder WithPermissionRequestLogFile(string filePath)
        {
            _permissionRequestLogFile = filePath;
            return this;
        }

        public SessionConfigBuilder WithModel(string model)
        {
            LogSetting(nameof(_sessionConfig.Model), model);
            _sessionConfig.Model = model;
            return this;
        }

        #region Agents

        //public SessionConfigBuilder WithPrimaryAgent(string agentIdentifier)
        //{
        //    WithAgent(agentIdentifier);
        //    var entityData = AllAgentsCollection.GetEntityDataByIdentifier(agentIdentifier);
        //    _sessionConfig.Agent = entityData.ConfigurationData.Name;
        //    LogSetting(nameof(_sessionConfig.Agent), _sessionConfig.Agent);
        //    WithModel(entityData.ConfigurationData.Model!);

        //    //if (entityData.ConfigurationData?.Tools?.ToArray()?.Count() > 0)
        //    //{
        //    //    WithAvailableTools(entityData.ConfigurationData?.Tools?.ToArray());
        //    //}
        //    //else
        //    //{
        //    //    var toolSet = new ToolSet().AddBuiltIn("*");
        //    //    WithAvailableTools(toolSet.ToArray());

        //    //    entityData.ConfigurationData.Tools = toolSet;
        //    //}

        //    WithPermissions(entityData.ConfigurationData?.CustomPermissions?.ToArray());
        //    WithInstructions(entityData.ConfigurationData?.CustomInstructions?.ToArray());
        //    WithSkills(entityData.ConfigurationData?.CustomSkills?.ToArray());

        //    return this;
        //}

        public SessionConfigBuilder WithPrimaryAgent(string agentIdentifier)
        {
            WithAgent(agentIdentifier);
            var entityData = AllAgentsCollection.GetByIdentifier(agentIdentifier);
            _sessionConfig.Agent = entityData.ConfigurationData.Name;
            LogSetting($"{nameof(_sessionConfig.Agent)} (Primary Agent)", _sessionConfig.Agent);

            //if (entityData.ConfigurationData?.Tools?.ToArray()?.Count() > 0)
            //{
            //    WithAvailableTools(entityData.ConfigurationData?.Tools?.ToArray());
            //}
            //else
            //{
            //    var toolSet = new ToolSet().AddBuiltIn("*");
            //    WithAvailableTools(toolSet.ToArray());

            //    entityData.ConfigurationData.Tools = toolSet;
            //}

            return this;
        }

        public SessionConfigBuilder WithClientMode(CopilotClientMode copilotClientMode)
        {
            _copilotClientMode = copilotClientMode;
            LogSetting(nameof(_copilotClientMode), _copilotClientMode);
            return this;
        }

        public SessionConfigBuilder WithPrimaryAgentFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);
            var entityData = AllAgentsCollection.AddFromFile(filePath);
            WithPrimaryAgent(entityData.ConfigurationData.Name);

            return this;
        }

        public SessionConfigBuilder WithAgent(string agentIdentifier)
        {
            _logger.Info("{TypeName} Loading '{PropertyName}' from agent identifier '{AgentIdentifier}'", $"[{nameof(SessionConfigBuilder)}]", nameof(_sessionConfig.CustomAgents), agentIdentifier);
            var entityData = AllAgentsCollection.GetByIdentifier(agentIdentifier);
            SessionAgentsCollection.Add(entityData);
            //WithPermissions(entityData.ConfigurationData?.CustomPermissions?.ToArray());
            //WithInstructions(entityData.ConfigurationData?.CustomInstructions?.ToArray());
            //WithSkills(entityData.ConfigurationData?.CustomSkills?.ToArray());
            //WithAgents(entityData.ConfigurationData?.CustomSubagents?.ToArray());

            return this;
        }

        public SessionConfigBuilder WithAgents(params string[]? agentsIdentifiers)
        {
            if (agentsIdentifiers?.Count() > 0)
                foreach (var agentIdentifier in agentsIdentifiers)
                {
                    WithAgent(agentIdentifier);
                }

            return this;
        }

        public SessionConfigBuilder WithAgentFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);
            var entityData = AllAgentsCollection.AddFromFile(filePath);
            SessionAgentsCollection.Add(entityData);
            return this;
        }

        public SessionConfigBuilder WithAgentsFromFiles(params string[]? filePaths)
        {
            if (filePaths?.Count() > 0)
                foreach (var filePath in filePaths)
                {
                    WithAgentFromFile(filePath);
                }

            return this;
        }

        public SessionConfigBuilder WithAgentsFromDirectory(string directoryPath)
        {
            var entities = AllAgentsCollection.AddFromDirectory(directoryPath);
            var sessionEntities = SessionAgentsCollection.AddFromDirectory(directoryPath);
            return this;
        }

        public SessionConfigBuilder WithAgentsFromDirectories(params string[]? directoriesPaths)
        {
            if (directoriesPaths?.Count() > 0)
                foreach (var directoryPath in directoriesPaths)
                {
                    WithAgentsFromDirectory(directoryPath);
                }

            return this;
        }

        public SessionConfigBuilder WithAgentsExtendedSearch(bool useExtendedSearch)
        {
            _logger.Info("{TypeName} Enabling extended search for agents.", $"[{nameof(SessionConfigBuilder)}]");
            _useAgentsExtendedSearch = useExtendedSearch;
            return this;
        }

        #endregion Agents

        #region Instructions

        public SessionConfigBuilder WithInstruction(string instructionIdentifier)
        {
            _logger.Info("{TypeName} Loading instruction with identifier '{InstructionIdentifier}' from all instructions collection.", $"[{nameof(SessionConfigBuilder)}]", instructionIdentifier);
            var entityData = AllInstructionsCollection.GetByIdentifier(instructionIdentifier);
            SessionInstructionsCollection.Add(entityData);

            return this;
        }

        public SessionConfigBuilder WithInstructions(params string[]? instructionsIdentifiers)
        {
            if (instructionsIdentifiers?.Count() > 0)
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

            AllInstructionsCollection.Add(entityData);
            SessionInstructionsCollection.Add(entityData);

            return this;
        }

        public SessionConfigBuilder WithInstructionFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);
            var entityData = AllInstructionsCollection.AddFromFile(filePath);
            SessionInstructionsCollection.Add(entityData);
            return this;
        }

        public SessionConfigBuilder WithInstructionsFromFiles(params string[]? filePaths)
        {
            if (filePaths?.Count() > 0)
                foreach (var filePath in filePaths)
                {
                    WithInstructionFromFile(filePath);
                }

            return this;
        }

        public SessionConfigBuilder WithInstructionsFromDirectory(string directoryPath)
        {
            var entities = AllInstructionsCollection.AddFromDirectory(directoryPath);
            SessionInstructionsCollection.Add(entities.ToArray());
            return this;
        }

        public SessionConfigBuilder WithInstructionsFromDirectories(params string[]? directoryPaths)
        {
            if (directoryPaths?.Count() > 0)
                foreach (var directoryPath in directoryPaths)
                {
                    WithInstructionsFromDirectory(directoryPath);
                }

            return this;
        }

        public SessionConfigBuilder WithInstructionDirectories(params string[]? instructionDirectories)
        {
            if (instructionDirectories?.Count() > 0)
            {
                LogCollectionSetting(nameof(_sessionConfig.InstructionDirectories), instructionDirectories);
                _sessionConfig.InstructionDirectories = instructionDirectories;
            }

            return this;
        }

        #endregion Instructions

        #region Skills

        public SessionConfigBuilder WithSkill(string skillIdentifier)
        {
            _logger.Info("{TypeName} Loading skill with identifier '{SkillIdentifier}' from all skills collection.", $"[{nameof(SessionConfigBuilder)}]", skillIdentifier);
            var entityData = AllSkillsCollection.GetByIdentifier(skillIdentifier);
            SessionSkillsCollection.Add(entityData);

            return this;
        }

        public SessionConfigBuilder WithSkills(params string[]? skillsIdentifiers)
        {
            if (skillsIdentifiers?.Count() > 0)
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

            AllSkillsCollection.Add(entityData);
            SessionSkillsCollection.Add(entityData);

            return this;
        }

        public SessionConfigBuilder WithSkillFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);
            var entityData = AllSkillsCollection.AddFromFile(filePath);
            SessionSkillsCollection.Add(entityData);
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
            var entities = AllSkillsCollection.AddFromDirectory(directoryPath);
            SessionSkillsCollection.Add(entities.ToArray());
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

        #region MCP Servers

        public SessionConfigBuilder WithMcpServer(string mcpServerIdentifier)
        {
            ArgumentNullException.ThrowIfNull(mcpServerIdentifier);

            var fileBasedMcpServerExists = _allFileBasedMcpServersCollection.TryGetByIdentifier(mcpServerIdentifier, out var fileBasedMcpServer);
            var sdkBasedMcpServerExists = _allSdkBasedMcpServersCollection.TryGetByIdentifier(mcpServerIdentifier, out var sdkBasedMcpServer);

            if (fileBasedMcpServerExists && sdkBasedMcpServerExists)
                throw new Exception($"[{nameof(SessionConfigBuilder)}] MCP server with identifier '{mcpServerIdentifier}' was found in both {nameof(_allFileBasedMcpServersCollection).ToNameOf()} and {nameof(_allSdkBasedMcpServersCollection).ToNameOf()}. " +
                    $"Please ensure that the identifier is unique across both collections.");

            if (fileBasedMcpServerExists)
                WithFileBasedMcpServer(mcpServerIdentifier);

            if (sdkBasedMcpServerExists)
                WithSdkBasedMcpServer(mcpServerIdentifier);

            if (fileBasedMcpServerExists || sdkBasedMcpServerExists)
                return this;

            throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] MCP server with identifier '{mcpServerIdentifier}' was not found in neither '{nameof(_allFileBasedMcpServersCollection).ToNameOf()}' nor '{nameof(_allSdkBasedMcpServersCollection).ToNameOf()}'.");
        }

        public SessionConfigBuilder WithMcpServers(params string[]? mcpServersIdentifiers)
        {
            if (mcpServersIdentifiers?.Count() > 0)
                foreach (var mcpServerIdentifier in mcpServersIdentifiers)
                {
                    WithMcpServer(mcpServerIdentifier);
                }

            return this;
        }

        public SessionConfigBuilder WithFileBasedMcpServer(string mcpServerIdentifier)
        {
            _logger.Info("{TypeName} Adding MCP server with identifier '{McpServerIdentifier}' from all file-based MCP servers collection.", $"[{nameof(SessionConfigBuilder)}]", mcpServerIdentifier);
            var mcpServerData = _allFileBasedMcpServersCollection.GetByIdentifier(mcpServerIdentifier);
            _sessionFileBasedMcpServersCollection.Add(mcpServerData);
            return this;
        }

        public SessionConfigBuilder WithFileBasedMcpServers(params string[]? mcpServersIdentifiers)
        {
            if (mcpServersIdentifiers?.Count() > 0)
                foreach (var mcpServerIdentifier in mcpServersIdentifiers)
                {
                    WithFileBasedMcpServer(mcpServerIdentifier);
                }

            return this;
        }

        public SessionConfigBuilder WithFileBasedMcpServer(string mcpServerIdentifier, string mcpServerInJsonFormat)
        {
            _logger.Info("{TypeName} Adding MCP server '{McpServerIdentifier}' from JSON string.", $"[{nameof(SessionConfigBuilder)}]", mcpServerIdentifier);

            var mcpServerData = new FileBasedMcpServerModel
            {
                Identifier = mcpServerIdentifier,
                ContentValue = mcpServerInJsonFormat
            };

            _allFileBasedMcpServersCollection.Add(mcpServerData);
            _sessionFileBasedMcpServersCollection.Add(mcpServerData);
            return this;
        }

        public SessionConfigBuilder WithFileBasedMcpServer<T>(string mcpServerIdentifier, T mcpServer) where T : class
        {
            _logger.Info("{TypeName} Adding MCP server '{McpServerIdentifier}' from typed object.", $"[{nameof(SessionConfigBuilder)}]", mcpServerIdentifier);

            ArgumentNullException.ThrowIfNull(mcpServer);

            var mcpServerData = new FileBasedMcpServerModel
            {
                Identifier = mcpServerIdentifier,
                ContentValue = mcpServer.ToJson()
            };

            _allFileBasedMcpServersCollection.Add(mcpServerData);
            _sessionFileBasedMcpServersCollection.Add(mcpServerData);

            return this;
        }

        public SessionConfigBuilder WithFileBasedMcpServersFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);
            var mcpServers = _allFileBasedMcpServersCollection.AddFromFile(filePath);
            _sessionFileBasedMcpServersCollection.Add(mcpServers.ToArray());
            return this;
        }

        public SessionConfigBuilder WithFileBasedMcpServersFromFiles(params string[]? filePaths)
        {
            if (filePaths?.Count() > 0)
                foreach (var filePath in filePaths)
                {
                    WithFileBasedMcpServersFromFile(filePath);
                }

            return this;
        }

        public SessionConfigBuilder WithFileBasedMcpServersFromDirectory(string directoryPath)
        {
            var mcpServers = _allFileBasedMcpServersCollection.AddFromDirectory(directoryPath);
            _sessionFileBasedMcpServersCollection.Add(mcpServers.ToArray());
            return this;
        }

        public SessionConfigBuilder WithFileBasedMcpServersFromDirectories(params string[]? directoriesPaths)
        {
            if (directoriesPaths?.Count() > 0)
                foreach (var directoryPath in directoriesPaths)
                {
                    WithFileBasedMcpServersFromDirectory(directoryPath);
                }

            return this;
        }

        public SessionConfigBuilder WithSdkBasedMcpServer(string identifier)
        {
            ArgumentNullException.ThrowIfNull(identifier);
            _logger.Info("{TypeName} Adding SDK based MCP server with identifier '{McpServerIdentifier}' from '{CollectionName}' collection.", $"[{nameof(SessionConfigBuilder)}]", identifier, nameof(_allSdkBasedMcpServersCollection).ToNameOf());
            var mcpServer = _allSdkBasedMcpServersCollection.GetByIdentifier(identifier);
            _sessionSdkBasedMcpServersCollection.Add(mcpServer);

            return this;
        }

        public SessionConfigBuilder WithSdkBasedMcpServers(params string[]? mcpServersIdentifiers)
        {
            if (mcpServersIdentifiers?.Count() > 0)
                foreach (var mcpServerIdentifier in mcpServersIdentifiers)
                {
                    WithSdkBasedMcpServer(mcpServerIdentifier);
                }

            return this;
        }

        #endregion MCP Servers

        #region Hooks

        public SessionConfigBuilder WithEnableFileHooks(bool enableFileHooks)
        {
            LogSetting(nameof(_sessionConfig.EnableFileHooks), enableFileHooks);
            _sessionConfig.EnableFileHooks = enableFileHooks;
            return this;
        }

        public SessionConfigBuilder WithHooks(string hookIdentifier)
        {
            ArgumentNullException.ThrowIfNull(hookIdentifier);

            var fileBasedHookExists = _allFileBasedHooksCollection.TryGetByIdentifier(hookIdentifier, out var fileBasedHook);
            if (fileBasedHookExists)
                WithFileBasedHook(hookIdentifier);

            var sdkBasedHookExists = _allSdkBasedSessionHooksCollection.TryGetByIdentifier(hookIdentifier, out var sdkBasedSessionHook);
            if (sdkBasedHookExists)
                WithSdkBasedHook(hookIdentifier);

            if (fileBasedHookExists || sdkBasedHookExists)
                return this;

            throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] Hook with identifier '{hookIdentifier}' was not found in neither {nameof(_allFileBasedHooksCollection).ToNameOf()} nor {nameof(_allSdkBasedSessionHooksCollection).ToNameOf()}.");
        }

        public SessionConfigBuilder WithHooks(params string[]? hooksIdentifiers)
        {
            if (hooksIdentifiers?.Count() > 0)
                foreach (var hookIdentifier in hooksIdentifiers)
                {
                    WithHooks(hookIdentifier);
                }

            return this;
        }

        public SessionConfigBuilder WithFileBasedHook(string hookIdentifier)
        {
            _logger.Info("{TypeName} Loading hook with identifier '{HookIdentifier}' from all hooks collection.", $"[{nameof(SessionConfigBuilder)}]", hookIdentifier);
            var hookData = _allFileBasedHooksCollection.GetByIdentifier(hookIdentifier);
            _sessionFileBasedHooksCollection.Add(hookData);
            return this;
        }

        public SessionConfigBuilder WithFileBasedHooks(params string[]? hooksIdentifiers)
        {
            if (hooksIdentifiers?.Count() > 0)
                foreach (var hookIdentifier in hooksIdentifiers)
                {
                    WithFileBasedHook(hookIdentifier);
                }

            return this;
        }

        public SessionConfigBuilder WithFileBasedHook(string hookIdentifier, string hookInJsonFormat)
        {
            _logger.Info("{TypeName} Adding hook '{HookIdentifier}' from JSON string.", $"[{nameof(SessionConfigBuilder)}]", hookIdentifier);
            ArgumentNullException.ThrowIfNullOrEmpty(hookInJsonFormat);

            var hookData = new HookModel
            {
                Identifier = hookIdentifier,
                ContentValue = hookInJsonFormat
            };

            _allFileBasedHooksCollection.Add(hookData);
            _sessionFileBasedHooksCollection.Add(hookData);
            return this;
        }

        public SessionConfigBuilder WithFileBasedHook<T>(string hookIdentifier, T hook) where T : class
        {
            _logger.Info("{TypeName} Adding hook '{HookIdentifier}' from typed object.", $"[{nameof(SessionConfigBuilder)}]", hookIdentifier);
            ArgumentNullException.ThrowIfNull(hook);

            var hookData = new HookModel
            {
                Identifier = hookIdentifier,
                ContentValue = hook.ToJson()
            };

            _allFileBasedHooksCollection.Add(hookData);
            _sessionFileBasedHooksCollection.Add(hookData);

            return this;
        }

        public SessionConfigBuilder WithFileBasedHooksFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);
            var hooks = _allFileBasedHooksCollection.AddFromFile(filePath);
            _sessionFileBasedHooksCollection.Add(hooks.ToArray());
            return this;
        }

        public SessionConfigBuilder WithFileBasedHooksFromFiles(params string[]? filePaths)
        {
            if (filePaths?.Count() > 0)
                foreach (var filePath in filePaths)
                {
                    WithFileBasedHooksFromFile(filePath);
                }

            return this;
        }

        public SessionConfigBuilder WithFileBasedHooksFromDirectory(string directoryPath)
        {
            var hooks = _allFileBasedHooksCollection.AddFromDirectory(directoryPath);
            _sessionFileBasedHooksCollection.Add(hooks.ToArray());
            return this;
        }

        public SessionConfigBuilder WithFileBasedHooksFromDirectories(params string[]? directoriesPaths)
        {
            if (directoriesPaths?.Count() > 0)
                foreach (var directoryPath in directoriesPaths)
                {
                    WithFileBasedHooksFromDirectory(directoryPath);
                }

            return this;
        }

        public SessionConfigBuilder WithSdkBasedHook(string identifier)
        {
            ArgumentNullException.ThrowIfNull(identifier);
            _logger.Info("🛠️[{LogArea}] ⚙️[{TypeName}] Adding SDK based hook with identifier '{HookIdentifier}' from '{CollectionName}' collection.", $"{SharedLoggingConstants.Area.Config}", nameof(SessionConfigBuilder), identifier, nameof(_allSdkBasedSessionHooksCollection).ToNameOf());
            var hook = _allSdkBasedSessionHooksCollection.GetByIdentifier(identifier);
            _sessionSdkBasedSessionHooksCollection.Add(hook);

            return this;
        }

        public SessionConfigBuilder WithSdkBasedHooks(params string[]? hooksIdentifiers)
        {
            if (hooksIdentifiers?.Count() > 0)
                foreach (var hookIdentifier in hooksIdentifiers)
                {
                    WithSdkBasedHook(hookIdentifier);
                }

            return this;
        }

        public SessionConfigBuilder WithSdkBasedHooks(SessionHooks sessionHooks)
        {
            ArgumentNullException.ThrowIfNull(sessionHooks);
            _sessionConfig.Hooks = sessionHooks;
            LogComplexObjectSetting(nameof(_sessionConfig.Hooks));
            return this;
        }

        public SessionConfigBuilder WithSdkBasedHooks(Func<SessionHooksBuilder, SessionHooksBuilder> updateSessionHooks)
        {
            ArgumentNullException.ThrowIfNull(updateSessionHooks);
            _logger.Info("🛠️[{LogArea}] ⚙️[{TypeName}] Configuring SDK based hooks via '{BuilderType}'.", $"{SharedLoggingConstants.Area.Config}", nameof(SessionConfigBuilder), nameof(SessionHooksBuilder));

            _sessionHooksBuilder ??= new SessionHooksBuilder(_logger);
            _sessionHooksBuilder = updateSessionHooks.Invoke(_sessionHooksBuilder);

            return this;
        }

        #endregion Hooks

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

        public SessionConfigBuilder WithAvailableTools(params string[]? tools)
        {
            if (tools?.Count() > 0)
            {
                LogCollectionSetting(nameof(_sessionConfig.AvailableTools), tools);

                _sessionConfig.AvailableTools ??= new List<string>();

                foreach (var tool in tools)
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
                .WithEnableSkills(true)
                .WithEnableConfigDiscovery(false)
                .WithIncludeSubAgentStreamingEvents(false);
        }

        public SessionConfigBuilder SetPermission(string identifier, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>> permissionFunc)
        {
            ArgumentNullException.ThrowIfNull(identifier);
            ArgumentNullException.ThrowIfNull(permissionFunc);

            _logger.Info("{TypeName} Setting permission decision for '{Identifier}'.", $"[{nameof(SessionConfigBuilder)}]", identifier);
            _sessionPermissionDecisions[identifier] = async (request, invocation) =>
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
            var permissionDecision = AllPermissionDecisionsCollection.GetByIdentifier(identifier);
            _sessionPermissionDecisions[identifier] = permissionDecision;
            return this;
        }

        public SessionConfigBuilder WithPermissions(params string[]? identifiers)
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

        public SessionConfigBuilder WithDirectoryForInteractionConfigurationData(string directoryPath)
        {
            LogSetting(nameof(_directoryForInteractionConfigurationData), directoryPath);
            _directoryForInteractionConfigurationData = directoryPath;
            return this;
        }

        //public SessionConfig Build()
        //{
        //    _logger.Info("{TypeName} Building Agent: {Agent}, (Model: {Model}).", $"[{nameof(SessionConfigBuilder)}]", _sessionConfig.Agent ?? "default", _sessionConfig.Model ?? "default");
        //    //_sessionConfig.AvailableTools.Add("read-agent");
        //    //_sessionConfig.AvailableTools.Add("write-agent");
        //    LogCollectionSetting(nameof(_sessionConfig.AvailableTools), _sessionConfig.AvailableTools);
        //    CreateFolderWithInteractionConfigurationData();
        //    SetUpOnPermissionRequest();
        //    SetUpInstructionDirectories();
        //    SetUpSkillsDirectories();
        //    _logger.Info("{TypeName} Built successfully Agent: {Agent}, (Model: {Model}).", $"[{nameof(SessionConfigBuilder)}]", _sessionConfig.Agent ?? "default", _sessionConfig.Model ?? "default");
        //    return _sessionConfig;
        //}

        public SessionConfig Build(CopilotClientMode? copilotClientMode = null)
        {
            var agent = _sessionConfig.Agent ?? "default";
            var model = _sessionConfig.Model ?? "default";

            _logger.Info("{TypeName} Building Agent: {Agent}, (Model: {Model}).", $"[{nameof(SessionConfigBuilder)}]", agent, model);

            if (copilotClientMode != null)
                WithClientMode(copilotClientMode.Value);

            CheckWorkingDirectoryMustExistOrCreate();

            var directoryForInteractionConfigurationData = SetupDirectoryForInteractionConfigurationData();

            ConfigureModel();
            ConfigureAgents(directoryForInteractionConfigurationData);
            ConfigureTools(_copilotClientMode);
            ConfigureInstructions(directoryForInteractionConfigurationData);
            ConfigureSkills(directoryForInteractionConfigurationData);
            ConfigureMcpServers(directoryForInteractionConfigurationData);

            ConfigurePermissions(directoryForInteractionConfigurationData);
            ConfigureOnPermissionRequest();
            ConfigureHooks(directoryForInteractionConfigurationData);

            ConfigureDataInGitHubDirectory(directoryForInteractionConfigurationData);
            _logger.Info("{TypeName} Built successfully Agent: {Agent}, (Model: {Model}).", $"[{nameof(SessionConfigBuilder)}]", agent, model);

            return _sessionConfig;
        }

        private void ConfigureModel()
        {
            if (!string.IsNullOrEmpty(_sessionConfig.Agent))
            {
                var primaryAgentEntityData = SessionAgentsCollection.GetByIdentifier(_sessionConfig.Agent);
                WithModel(primaryAgentEntityData.ConfigurationData.Model!);
            }

            if (string.IsNullOrEmpty(_sessionConfig.Model))
                throw new InvalidOperationException("The session configuration does not have a model specified. Please ensure that the configuration includes a valid model.");
        }

        private void ConfigureAgents(string directoryForInteractionConfigurationData)
        {
            //Aggregate data on subagents for all agents added to the session. This is required because some agents may require subagents that are not available in other agents, so the session must have all subagents available to be able to run all agents in the session.
            var sessionSubagents = SessionAgentsCollection.Where(x => x.ConfigurationData?.CustomMetadata?.Subagents?.Count() > 0).SelectMany(x => x.ConfigurationData?.CustomMetadata?.Subagents!).Distinct().ToArray();
            WithAgents(sessionSubagents);

            foreach (var entityData in SessionAgentsCollection)
            {
                var customAgentConfig = _gitHubCopilotMappers.ToCustomAgentConfig(entityData, fileBasedMcpServersCollection: _allFileBasedMcpServersCollection, sdkBasedMcpServersCollection: _allSdkBasedMcpServersCollection);
                WithCustomAgentConfig(customAgentConfig); //TODO Make sure that all CustomAgentConfig entries are logged, for use case, when those where added manuall, not through SessionAgentsCollection, so that they are not logged in the WithAgent method.
            }

            SaveAiAgents(directoryForInteractionConfigurationData);
        }

        /// <remarks>
        /// One of the modes is <see cref="CopilotClientMode.Cli"/>. 
        /// When set to <see cref="CopilotClientMode.Empty"/>, the SDK validates that the app has supplied the required configuration (<see cref="BaseDirectory"/> or <see cref="SessionFs"/>, plus <see cref="SessionConfigBase.AvailableTools"/> on each session).
        /// Null or empty <see cref="SessionConfigBase.AvailableTools"/> are not allowed. So default toolset is created with <see cref="BuiltInTools.Isolated"/> entries.
        /// </remarks>
        private void ConfigureTools(CopilotClientMode? copilotClientMode = null)
        {
            //Session is setup with all tools available from all agents in the session. This is required because some agents may require tools that are not available in other agents, so the session must have all tools available to be able to run all agents in the session.
            var tools = SessionAgentsCollection.Where(agent => agent.ConfigurationData?.Tools?.Count() > 0)
                .SelectMany(agent => agent.ConfigurationData.Tools!)
                .Distinct()
                .ToList();

            _sessionConfig.AvailableTools = tools;

            if (copilotClientMode == CopilotClientMode.Empty)
            {
                var toolSet = new ToolSet().AddBuiltIn(BuiltInTools.Isolated);
                _logger.Info("{TypeName} Adding default BuiltInTools.Isolated toolset '{ToolSet}' to '{PropertyName}' because the session is in '{Mode}' mode.", $"[{nameof(SessionConfigBuilder)}]", string.Join(", ", toolSet.ToArray()), nameof(_sessionConfig.AvailableTools), copilotClientMode);
                WithAvailableTools(toolSet.ToArray());
            }

            //If call of subagents is required then session must have the "agent" tool available. This is required for the session to be able to call subagents.
            if (SessionAgentsCollection.Count() > 1 && !_sessionConfig.AvailableTools.Contains(Const.Tools.AGENT))
            {
                _logger.Info("{TypeName} Adding '{Tool}' to '{PropertyName}' because multiple agents are configured in the session, but it was not already available.", $"[{nameof(SessionConfigBuilder)}]", Const.Tools.AGENT, nameof(_sessionConfig.AvailableTools));
                WithAvailableTools(Const.Tools.AGENT);
            }
        }

        private void ConfigureInstructions(string directoryForInteractionConfigurationData)
        {
            // Aggregate data on instructions from all agents in the session
            var sessionInstructions = SessionAgentsCollection.Where(x => x.ConfigurationData?.CustomMetadata?.Instructions?.Count() > 0).SelectMany(x => x.ConfigurationData?.CustomMetadata?.Instructions!).Distinct().ToArray();
            WithInstructions(sessionInstructions);
            SaveAiInstructions(directoryForInteractionConfigurationData);

            if ((_sessionConfig.InstructionDirectories == null || _sessionConfig.InstructionDirectories.Count <= 0) && !string.IsNullOrEmpty(directoryForInteractionConfigurationData))
            {
                var instructionsDirectory = Const.Directories.GetGitHubInstructionsDirectory(directoryForInteractionConfigurationData);

                if (Directory.Exists(instructionsDirectory))
                {
                    _sessionConfig.InstructionDirectories = new List<string>() { instructionsDirectory };
                    _logger.Info("{TypeName} Setting '{PropertyName}' parameter to '[{Value}]'.", $"[{nameof(SessionConfigBuilder)}]", nameof(_sessionConfig.InstructionDirectories), string.Join(", ", _sessionConfig.InstructionDirectories));
                }
            }
            else
                throw new Exception();

            //if (_sessionConfig.InstructionDirectories?.Count > 0)
            //    foreach (var directory in _sessionConfig.InstructionDirectories)
            //    {
            //        IoUtils.DirectoryCopy(Path.Combine(directory, ".github", Const.Directories.INSTRUCTIONS), Path.Combine(_sessionConfig.WorkingDirectory, ".github", Const.Directories.INSTRUCTIONS), overwrite: true);
            //        IoUtils.DirectoryCopy(Path.Combine(directory, ".github", Const.Directories.INSTRUCTIONS), Path.Combine(_baseDirectory, ".github", Const.Directories.INSTRUCTIONS), overwrite: true);
            //    }
        }

        private void ConfigureSkills(string directoryForInteractionConfigurationData)
        {
            // Aggregate data on skills from all agents in the session
            var sessionSkills = SessionAgentsCollection.Where(x => x.ConfigurationData?.CustomMetadata?.Skills?.Count() > 0).SelectMany(x => x.ConfigurationData?.CustomMetadata?.Skills!).Distinct().ToArray();

            //TODO Check that no duplication happens between skills added through WithSkills and skills added through WithSkillDirectories. If duplication happens, then throw an exception.
            WithSkills(sessionSkills);
            SaveAiSkills(directoryForInteractionConfigurationData);

            if ((_sessionConfig.SkillDirectories == null || _sessionConfig.SkillDirectories.Count <= 0) && !string.IsNullOrEmpty(directoryForInteractionConfigurationData))
            {
                var rootDirectoryWithSkills = Const.Directories.GetGitHubSkillsDirectory(directoryForInteractionConfigurationData);

                if (Directory.Exists(rootDirectoryWithSkills))
                {
                    var specificDirectoriesOfSkills = Directory.GetDirectories(rootDirectoryWithSkills);

                    if (specificDirectoriesOfSkills.Count() > 0)
                    {
                        _sessionConfig.SkillDirectories ??= new List<string>();

                        foreach (var skillDirectory in specificDirectoriesOfSkills)
                        {
                            _sessionConfig.SkillDirectories.Add(skillDirectory);
                            _logger.Info("{TypeName} Setting '{PropertyName}' parameter to '[{Value}]'.", $"[{nameof(SessionConfigBuilder)}]", nameof(_sessionConfig.SkillDirectories), string.Join(", ", _sessionConfig.SkillDirectories));
                        }
                    }
                }
            }

            //This setup is required because it is not enough to just set SkillsDirectories of the SessionConfig, but names of skills should be added to the CustomAgentConfig.
            if (!string.IsNullOrEmpty(_sessionConfig.Agent) && _sessionConfig.SkillDirectories?.Count() > 0)
            {
                //Add directly to the CustomAgentConfig of the SessionConfig
                var primaryAgentFromSessionConfig = _sessionConfig.CustomAgents?.SingleOrDefault(x => x.Name == _sessionConfig.Agent);
                if (primaryAgentFromSessionConfig != null)
                    primaryAgentFromSessionConfig.Skills = new List<string>();

                //Add through the SessionAgentsCollection entity mapped to the CustomAgentConfig of the SessionConfig.
                //Plus for consistency, the same skills are added to the SessionAgentsCollection entity, so that it is consistent with the CustomAgentConfig of the SessionConfig.
                var primaryAgentFroSessionAgentsCollection = SessionAgentsCollection.GetByIdentifier(_sessionConfig.Agent);
                if (primaryAgentFroSessionAgentsCollection != null && primaryAgentFroSessionAgentsCollection.ConfigurationData != null)
                    primaryAgentFroSessionAgentsCollection.ConfigurationData.CustomMetadata!.Skills = new List<string>();

                foreach (var directory in _sessionConfig.SkillDirectories.Select(x => new DirectoryInfo(x)))
                {
                    primaryAgentFromSessionConfig?.Skills?.Add(directory.Name);
                    primaryAgentFroSessionAgentsCollection?.ConfigurationData?.CustomMetadata?.Skills?.Add(directory.Name);
                }
            }
        }

        public void ConfigureMcpServers(string directoryForInteractionConfigurationData)
        {
            //Fill the session MCP servers collection with all MCP servers from all agents in the session.
            //This is required because some agents may require MCP servers that are not available in other agents, so the session must have all MCP servers available to be able to run all agents in the session.
            var agentsMcpServers = SessionAgentsCollection
                .Where(x => x.ConfigurationData?.CustomMetadata?.McpServers?.Count > 0)
                .SelectMany(x => x.ConfigurationData!.CustomMetadata!.McpServers!)
                .Distinct()
                .ToArray();

            foreach (var agentMcpServer in agentsMcpServers)
            {
                if (_sessionFileBasedMcpServersCollection.Any(x => x.Identifier == agentMcpServer))
                {
                    _logger.Info("{TypeName} MCP server '{McpServerIdentifier}' is already added to the session from file-based collection.", $"[{nameof(SessionConfigBuilder)}]", agentMcpServer);
                    continue;
                }
                else
                    WithFileBasedMcpServer(agentMcpServer);

                if (_sessionSdkBasedMcpServersCollection.Any(x => x.Identifier == agentMcpServer))
                {
                    _logger.Info("{TypeName} MCP server '{McpServerIdentifier}' is already added to the session from SDK-based collection.", $"[{nameof(SessionConfigBuilder)}]", agentMcpServer);
                    continue;
                }
                else
                    WithSdkBasedMcpServer(agentMcpServer);
            }

            //Check no 2 MCP servers have the same identifier, one from file-based and one from SDK-based. If so, throw an exception.
            var fileBasedIdentifiers = _sessionFileBasedMcpServersCollection.Select(x => x.Identifier!).ToList();
            var sdkBasedIdentifiers = _sessionSdkBasedMcpServersCollection.Select(x => x.Identifier).ToList();
            var duplicatedIdentifiers = sdkBasedIdentifiers.Intersect(fileBasedIdentifiers).ToList();

            if (duplicatedIdentifiers.Count > 0)
            {
                throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] There are conflicts of identifiers between SDK-based and file-based MCP servers: {string.Join(", ", duplicatedIdentifiers)}.");
            }

            _sessionConfig.McpServers ??= new Dictionary<string, McpServerConfig>();

            foreach (var fileBasedMcpServer in _sessionFileBasedMcpServersCollection)
            {
                var config = _gitHubCopilotMappers.ToMcpServerConfig(fileBasedMcpServer);
                _sessionConfig.McpServers[fileBasedMcpServer.Identifier!] = config;
            }

            foreach (var sdkBasedMcpServer in _sessionSdkBasedMcpServersCollection)
            {
                sdkBasedMcpServer.AddTo(_sessionConfig.McpServers);
            }

            SaveMcpServers(directoryForInteractionConfigurationData);
        }

        private void SaveMcpServers(string directoryForInteractionConfigurationData)
        {
            if (!_sessionFileBasedMcpServersCollection.Any() && !_sessionSdkBasedMcpServersCollection.Any())
            {
                return;
            }

            var mcpServersDirectory = Const.Directories.GetGitHubMcpServersDirectory(directoryForInteractionConfigurationData);
            IoUtils.CleanDirectory(mcpServersDirectory);

            var fileBasedGroups = _sessionFileBasedMcpServersCollection.GroupBy(mcpServer => mcpServer.FilePath);

            foreach (var group in fileBasedGroups)
            {
                var serversByIdentifier = new JsonObject();

                foreach (var mcpServer in group)
                {
                    var identifier = mcpServer.Identifier ?? throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] File-based MCP server identifier is not set.");
                    if (string.IsNullOrEmpty(mcpServer.ContentValue))
                        throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] Content of file-based MCP server '{identifier}' is empty.");

                    serversByIdentifier[identifier] = JsonNode.Parse(mcpServer.ContentValue);
                }

                //For use case when was added via WithFileBasedMcpServer(string identifier, string mcpServerInJsonFormat) or WithFileBasedMcpServer<T>(string identifier, T mcpServer) methods, the FilePath property is null or empty, so the file name will be "file-based-mcp-servers.json".
                var fileNameWithoutExtension = string.IsNullOrEmpty(group.Key)
                    ? "file-based-mcp-servers"
                    : Path.GetFileNameWithoutExtension(group.Key);

                var destinationFilePath = IoUtils.GetUniqueFilePathOrDefault(mcpServersDirectory, fileNameWithoutExtension, ".json");
                IoUtils.WriteAllText(destinationFilePath, serversByIdentifier.ToJsonString());
            }

            foreach (var sdkBasedMcpServer in _sessionSdkBasedMcpServersCollection)
            {
                var identifier = sdkBasedMcpServer.Identifier ?? throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] SDK-based MCP server identifier is not set.");
                var content = sdkBasedMcpServer.ToJson();

                var destinationFilePath = IoUtils.GetUniqueFilePathOrDefault(mcpServersDirectory, identifier, ".json");
                IoUtils.WriteAllText(destinationFilePath, content);
            }
        }

        private void ConfigureHooks(string directoryForInteractionConfigurationData)
        {
            var sessionHooks = SessionAgentsCollection
                .Where(x => x.ConfigurationData?.CustomMetadata?.Hooks?.Count > 0)
                .SelectMany(x => x.ConfigurationData!.CustomMetadata!.Hooks!)
                .Distinct()
                .ToArray();

            WithHooks(sessionHooks); // this overload configures both file-based and SDK-based hooks based on the identifiers found in the agents' metadata.

            //Required configuration for File Based Hooks
            SaveHooks(directoryForInteractionConfigurationData);

            //Required configuration for SDK Based Hooks
            // If the session configuration does not already have hooks set, build them from the builder if available
            if (_sessionConfig.Hooks == null)
            {
                if (_sessionHooksBuilder != null)
                {
                    _sessionConfig.Hooks = _sessionHooksBuilder.Build();
                }
            }

            _sessionConfig.Hooks ??= new();

            foreach (var sdkBasedHook in _sessionSdkBasedSessionHooksCollection)
            {
                sdkBasedHook.AddTo(_sessionConfig.Hooks);
            }
        }

        public void ConfigurePermissions(string directoryForInteractionConfigurationData)
        {
            // Aggregate data on skills from all agents in the session
            var sessionPermissions = SessionAgentsCollection.Where(x => x.ConfigurationData?.CustomMetadata?.Permissions?.Count() > 0).SelectMany(x => x.ConfigurationData?.CustomMetadata?.Permissions!).Distinct().ToArray();
            WithPermissions(sessionPermissions);
            //TODO Save Permissions Configuration to directoryForInteractionConfigurationData if needed, similar to how agents, instructions, and skills are saved.
        }

        private void ConfigureOnPermissionRequest()
        {
            _sessionConfig.OnPermissionRequest = async (request, invocation) =>
            {
                var message = $"Permission Request:\nType= {request.ToString()}\nBody = {request.ToJson()}";
                _logger.Info(message);

                if (!string.IsNullOrEmpty(_permissionRequestLogFile))
                    IoUtils.AppendAllText(_permissionRequestLogFile, message);

                if (_sessionPermissionDecisions?.Count > 0)
                {
                    foreach (var permissionDecision in _sessionPermissionDecisions)
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

        private string SetupDirectoryForInteractionConfigurationData()
        {
            if (string.IsNullOrEmpty(_directoryForInteractionConfigurationData))
                _directoryForInteractionConfigurationData = SharedAiIoUtils.GetTempAiInterationSessionFolder();

            if (IoUtils.NormalizeFilePath(_directoryForInteractionConfigurationData) == IoUtils.NormalizeFilePath(_sessionConfig.WorkingDirectory))
                throw new Exception("Directory with Interaction Configuration Data should not be the same as Working Directory");

            IoUtils.CreateDirectory(_directoryForInteractionConfigurationData);

            return _directoryForInteractionConfigurationData;
        }

        private void SaveAiAgents(string rootDirectory)
        {
            var agentsDirectory = Const.Directories.GetGitHubAgentsDirectory(rootDirectory);
            SaveAiEntities(SessionAgentsCollection, agentsDirectory, Const.Files.Extensions.AGENT_MD, "Agent");
        }

        private void SaveAiInstructions(string rootDirectory)
        {
            var instructionsDirectory = Const.Directories.GetGitHubInstructionsDirectory(rootDirectory);
            SaveAiEntities(SessionInstructionsCollection, instructionsDirectory, Const.Files.Extensions.INSTRUCTIONS_MD, "Instruction");
        }

        //private void SetupOnPermissionRequest()
        //{
        //    _sessionConfig.OnPermissionRequest = async (request, invocation) =>
        //    {
        //        var message = $"Permission Request:\nType= {request.ToString()}\nBody = {request.ToJson()}";
        //        _logger.Info(message);

        //        if (!string.IsNullOrEmpty(_permissionRequestLogFile))
        //            IoUtils.AppendAllText(_permissionRequestLogFile, message);

        //        return PermissionDecision.ApproveOnce();
        //    };
        //}

        private void SaveAiSkills(string rootDirectory)
        {
            var skillsDirectory = Const.Directories.GetGitHubSkillsDirectory(rootDirectory);

            foreach (var skill in SessionSkillsCollection)
            {
                if (!string.IsNullOrEmpty(skill.FilePath))
                {
                    var skillDirectory = new DirectoryInfo(Path.GetDirectoryName(skill.FilePath));
                    var destinationDirectoryPath = Path.Combine(skillsDirectory, skillDirectory.Name);

                    if (Directory.Exists(destinationDirectoryPath))
                        throw new InvalidOperationException($"Destination directory '{destinationDirectoryPath}' already exists. Cannot copy skill '{skill.ConfigurationData.Name}' directory. " +
                            $"Check if multiple skills have directories with the same name where their '{Const.Files.Extensions.SKILL_MD}' files reside, as skills are copied as full directories with all files related to particular skills.");

                    IoUtils.DirectoryCopy(skillDirectory.FullName, destinationDirectoryPath);
                }
                else
                {
                    if (string.IsNullOrEmpty(skill.ConfigurationData?.Name))
                    {
                        throw new ArgumentException("Skill configuration name is either null or empty, but must have a valid name to save the skill file.");
                    }

                    var destinationPath = Path.Combine(skillsDirectory, skill.ConfigurationData.Name, Const.Files.Extensions.SKILL_MD);
                    IoUtils.WriteAllText(destinationPath, skill.ToMdFileContent());
                }
            }
        }

        private void SaveAiEntities<TConfiguration>(
            IEnumerable<IAiEntityWithTYamlConfigurationType<TConfiguration>> entities,
            string directory,
            string defaultExtension,
            string entityTypeName)
            where TConfiguration : IAiEntityYamlConfiguration, new()
        {
            IoUtils.CleanDirectory(directory);

            var entityInfos = entities.Select(entity =>
            {
                if (!string.IsNullOrEmpty(entity.FilePath))
                {
                    return new
                    {
                        Entity = entity,
                        FileNameWithoutExtension = Path.GetFileNameWithoutExtension(entity.FilePath),
                        Extension = Path.GetExtension(entity.FilePath),
                        InitialFilePath = (string?)entity.FilePath
                    };
                }

                if (string.IsNullOrEmpty(entity.ConfigurationData?.Name))
                {
                    throw new ArgumentException($"'{entityTypeName}' configuration name is either null or empty, but must have a valid name to save the '{entityTypeName}' file.");
                }

                return new
                {
                    Entity = entity,
                    FileNameWithoutExtension = entity.ConfigurationData.Name,
                    Extension = defaultExtension,
                    InitialFilePath = (string?)null
                };
            });

            var groups = entityInfos.GroupBy(info => IoUtils.WithoutInvalidFileNameChars(info.FileNameWithoutExtension) + IoUtils.NormalizeExtension(info.Extension));

            foreach (var group in groups)
            {
                var items = group.ToList();

                if (items.Count > 1)
                {
                    var initialFilePaths = items.Select(item => item.InitialFilePath ?? $"The entity of '{entityTypeName}' kind was created dynamically via code and has no initial file path. By default its name is used as eventual file name '{item.FileNameWithoutExtension}'");
                    _logger.Warning("{TypeName} Several {EntityType} entities could have the same eventual name '{EventualName}' inside directory '{Directory}' after copy. Additional numerical index will be applied to file name of each entity to avoid naming duplication. " +
                        "Initial file paths: {InitialFilePaths}.", $"[{nameof(SessionConfigBuilder)}]", entityTypeName, group.Key, directory, string.Join(", ", initialFilePaths));
                }

                foreach (var item in items)
                {
                    var destinationFilePath = IoUtils.GetUniqueFilePathOrDefault(directory, item.FileNameWithoutExtension, item.Extension);
                    var entityFilePath = item.Entity.FilePath;

                    if (!string.IsNullOrEmpty(entityFilePath))
                    {
                        IoUtils.FileCopy(entityFilePath, destinationFilePath);
                    }
                    else
                    {
                        IoUtils.WriteAllText(destinationFilePath, item.Entity.ToMdFileContent());
                    }
                }
            }
        }

        private void CheckWorkingDirectoryMustExistOrCreate()
        {
            if (string.IsNullOrEmpty(_sessionConfig.WorkingDirectory))
            {
                throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] Working directory is not set. Please ensure that the working directory is configured before performing file system operations.");
            }

            if (!IoUtils.DirectoryExists(_sessionConfig.WorkingDirectory))
            {
                IoUtils.CreateDirectory(_sessionConfig.WorkingDirectory);
            }
        }

        //private void CheckInteractiondirectoryForInteractionConfigurationDataMustExist()
        //{
        //    if (string.IsNullOrEmpty(_interactiondirectoryForInteractionConfigurationData))
        //    {
        //        var message = $"[{nameof(SessionConfigBuilder)}] Interaction configuration directory is not set.";
        //        throw new InvalidOperationException(message);
        //    }

        //    if (!IoUtils.DirectoryExists(_interactiondirectoryForInteractionConfigurationData))
        //    {
        //        var message = $"[{nameof(SessionConfigBuilder)}] Interaction configuration directory '{_interactiondirectoryForInteractionConfigurationData}' does not exist.";
        //        throw new DirectoryNotFoundException(message);
        //    }
        //}

        private void SetUpGitHubDirectory()
        {
            CheckWorkingDirectoryMustExistOrCreate();

            var gitHubDirectory = Path.Combine(_sessionConfig.WorkingDirectory!, Const.Directories.GITHUB);
            var gitHubInitialDirectory = Path.Combine(_sessionConfig.WorkingDirectory!, $"{Const.Directories.GITHUB}-initial");

            _gitHubDirectoryExistedAtStart = IoUtils.DirectoryExists(gitHubDirectory);
            _gitHubInitialDirectoryExistedAtStart = IoUtils.DirectoryExists(gitHubInitialDirectory);

            if (_gitHubDirectoryExistedAtStart && _gitHubInitialDirectoryExistedAtStart)
            {
                _logger.Warning("[{TypeName}] Both '{GitHubDirectory}' and '{GitHubInitialDirectory}' directories exist in working directory '{WorkingDirectory}'. The '{GitHubDirectory}' directory will be removed and '{GitHubInitialDirectory}' will be left as is.", nameof(SessionConfigBuilder), Const.Directories.GITHUB, $"{Const.Directories.GITHUB}-initial", _sessionConfig.WorkingDirectory!);
                IoUtils.DeleteDirectory(gitHubDirectory);
            }
            else if (_gitHubDirectoryExistedAtStart)
            {
                _logger.Warning("[{TypeName}] Only '{GitHubDirectory}' directory exists in working directory '{WorkingDirectory}'. It will be renamed to '{GitHubInitialDirectory}' and a new empty '{GitHubDirectory}' directory will be created.", nameof(SessionConfigBuilder), Const.Directories.GITHUB, _sessionConfig.WorkingDirectory!, $"{Const.Directories.GITHUB}-initial");
                Directory.Move(gitHubDirectory, gitHubInitialDirectory);
                _isGitHubDirectoryInitialReserveCopyCreated = true;

                IoUtils.CreateDirectory(gitHubDirectory);
            }
            else
            {
                IoUtils.CreateDirectory(gitHubDirectory);
            }
        }

        private void RestoreGitHubDirectory()
        {
            if (string.IsNullOrEmpty(_sessionConfig.WorkingDirectory))
            {
                return;
            }

            var gitHubDirectory = Path.Combine(_sessionConfig.WorkingDirectory, Const.Directories.GITHUB);
            var gitHubInitialDirectory = Path.Combine(_sessionConfig.WorkingDirectory, $"{Const.Directories.GITHUB}-initial");

            if (IoUtils.DirectoryExists(gitHubInitialDirectory))
            {
                if (IoUtils.DirectoryExists(gitHubDirectory))
                {
                    IoUtils.DeleteDirectory(gitHubDirectory);
                }

                if (_isGitHubDirectoryInitialReserveCopyCreated)
                {
                    Directory.Move(gitHubInitialDirectory, gitHubDirectory);
                }
            }
            else if (!_gitHubDirectoryExistedAtStart && IoUtils.DirectoryExists(gitHubDirectory))
            {
                IoUtils.DeleteDirectory(gitHubDirectory);
            }
        }

        private void SaveHooks(string directoryForInteractionConfigurationData)
        {
            if (_sessionFileBasedHooksCollection.Any())
            {
                IoUtils.CheckDirectoryMustExist(directoryForInteractionConfigurationData);

                var hooksDirectory = Const.Directories.GetGitHubHooksDirectory(directoryForInteractionConfigurationData);
                var hooksWithFilePath = _sessionFileBasedHooksCollection.Where(h => !string.IsNullOrEmpty(h.FilePath)).ToList();
                var hooksWithoutFilePath = _sessionFileBasedHooksCollection.Where(h => string.IsNullOrEmpty(h.FilePath)).ToList();
                var writtenFileNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (var group in hooksWithFilePath.GroupBy(h => IoUtils.NormalizeFilePath(h.FilePath!)))
                {
                    var sourceFilePath = group.Key;
                    var fileName = IoUtils.GetFileName(sourceFilePath);

                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = $"{Guid.NewGuid()}.hooks.json";
                    }

                    if (writtenFileNames.Contains(fileName))
                    {
                        throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] Multiple hook files would have the same file name '{fileName}' in directory '{hooksDirectory}'. Please ensure that hook file names are unique.");
                    }

                    writtenFileNames.Add(fileName);
                    var destinationFilePath = Path.Combine(hooksDirectory, fileName);
                    var hooksByEvent = group.GroupBy(h => h.EventTriggerName).ToDictionary(g => g.Key ?? throw new Exception($"{nameof(HookModel.EventTriggerName)} of hook is not set."), g => g.Select(h => h.ContentValue).Where(hook => !string.IsNullOrEmpty(hook)).ToList());
                    WriteHooksFile(destinationFilePath, hooksByEvent);
                    CopyHookDataDirectories(group, hooksDirectory);
                }

                foreach (var hook in hooksWithoutFilePath)
                {
                    if (string.IsNullOrEmpty(hook.Identifier))
                    {
                        throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] Programmatically added hook must have an identifier. The provided hook does not have an identifier.");
                    }

                    var fileName = $"{hook.Identifier}.hooks.json";

                    if (writtenFileNames.Contains(fileName))
                    {
                        throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] Multiple programmatic hooks would have the same file name '{fileName}' in directory '{hooksDirectory}'. Please ensure that hook identifiers are unique.");
                    }

                    writtenFileNames.Add(fileName);
                    var destinationFilePath = Path.Combine(hooksDirectory, fileName);
                    var hooksByEvent = new Dictionary<string, List<string?>>
                    {
                        { hook.EventTriggerName ?? throw new Exception($"{nameof(HookModel.EventTriggerName)} of hook is not set."), new List<string?> { hook.ContentValue } }
                    };

                    WriteHooksFile(destinationFilePath, hooksByEvent);
                    CopyHookDataDirectories(new[] { hook }, hooksDirectory);
                }
            }
        }

        private void WriteHooksFile(string filePath, Dictionary<string, List<string?>> hooksByEvent)
        {
            var hooksObject = new JsonObject();

            foreach (var eventEntry in hooksByEvent)
            {
                var hookArray = new JsonArray();

                foreach (var hookJson in eventEntry.Value)
                {
                    if (string.IsNullOrWhiteSpace(hookJson))
                    {
                        continue;
                    }

                    hookArray.Add(JsonNode.Parse(hookJson));
                }

                hooksObject[eventEntry.Key] = hookArray;
            }

            var output = new JsonObject { ["hooks"] = hooksObject };
            var options = new JsonSerializerOptions { WriteIndented = true };

            IoUtils.WriteAllText(filePath, output.ToJsonString(options));
        }

        private void CopyHookDataDirectories(IEnumerable<IFileBasedHook> hooks, string hooksDirectory)
        {
            foreach (var hook in hooks)
            {
                if (hook.Data == null)
                {
                    continue;
                }

                var hookFileDirectory = string.IsNullOrEmpty(hook.FilePath) ? null : Path.GetDirectoryName(hook.FilePath);

                foreach (var directoryFilesData in hook.Data)
                {
                    if (string.IsNullOrEmpty(directoryFilesData.Directory) || directoryFilesData.Files == null)
                    {
                        continue;
                    }

                    var destinationDirectory = Path.Combine(hooksDirectory, directoryFilesData.Directory);
                    IoUtils.CreateDirectory(destinationDirectory);
                    var copiedFileNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    foreach (var file in directoryFilesData.Files)
                    {
                        var sourceFilePath = file;

                        if (!Path.IsPathRooted(sourceFilePath) && !string.IsNullOrEmpty(hookFileDirectory))
                        {
                            sourceFilePath = Path.Combine(hookFileDirectory, sourceFilePath);
                        }

                        var fileName = IoUtils.GetFileName(sourceFilePath);

                        if (string.IsNullOrEmpty(fileName))
                        {
                            throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] Unable to extract file name from source file path '{sourceFilePath}'. Hook data file path must be a valid file with a name.");
                        }

                        if (copiedFileNames.Contains(fileName))
                        {
                            throw new InvalidOperationException($"[{nameof(SessionConfigBuilder)}] Duplicate file name '{fileName}' detected in directory '{destinationDirectory}'. Hook data files must have unique names within each directory.");
                        }

                        copiedFileNames.Add(fileName);
                        var destinationFilePath = Path.Combine(destinationDirectory, fileName);
                        IoUtils.FileCopy(sourceFilePath, destinationFilePath);
                    }
                }
            }
        }

        private void ConfigureFileBasedHooksInGitHubDirectory(string interactiondirectoryForInteractionConfigurationData)
        {
            var sourceHooksDirectory = Const.Directories.GetGitHubHooksDirectory(interactiondirectoryForInteractionConfigurationData);

            if (!IoUtils.DirectoryExists(sourceHooksDirectory))
            {
                var fallbackSourceHooksDirectory = Path.Combine(interactiondirectoryForInteractionConfigurationData, $"{Const.Directories.GITHUB}-initial", Const.Directories.HOOKS);

                if (IoUtils.DirectoryExists(fallbackSourceHooksDirectory))
                {
                    sourceHooksDirectory = fallbackSourceHooksDirectory;
                }
                else
                {
                    return;
                }
            }

            if (_sessionFileBasedHooksCollection.Any() && _sessionConfig.EnableFileHooks != true)
            {
                _logger.Info("[{TypeName}] EnableFileHooks was not set via {WithEnableFileHooksMethod}, but file-based hooks were added to the session. EnableFileHooks will be set to true automatically.", nameof(SessionConfigBuilder), nameof(WithEnableFileHooks));
                _sessionConfig.EnableFileHooks = true;
            }

            var destinationHooksDirectory = Const.Directories.GetGitHubHooksDirectory(_sessionConfig.WorkingDirectory);

            if (IoUtils.NormalizeFilePath(sourceHooksDirectory).Equals(IoUtils.NormalizeFilePath(destinationHooksDirectory), StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            IoUtils.DirectoryCopy(sourceHooksDirectory, destinationHooksDirectory);
        }

        private void ConfigureDataInGitHubDirectory(string directoryForInteractionConfigurationData)
        {
            SetUpGitHubDirectory();
            ConfigureFileBasedHooksInGitHubDirectory(directoryForInteractionConfigurationData);
        }

        public void Dispose()
        {
            RestoreGitHubDirectory();

            if (!string.IsNullOrEmpty(_directoryForInteractionConfigurationData) && IoUtils.DirectoryExists(_directoryForInteractionConfigurationData))
            {
                IoUtils.DeleteDirectory(_directoryForInteractionConfigurationData);
            }
        }

        private void LogSetting(string propertyName, object value)
        {
            _logger.Info("🛠️[{LogArea}] ⚙️[{TypeName}] Setting 🔧'{PropertyName}' parameter to '{Value}'.", $"{SharedLoggingConstants.Area.Config}", $"{nameof(SessionConfigBuilder)}", propertyName, value ?? "null");
        }

        private void LogComplexObjectSetting(string propertyName)
        {
            _logger.Info("🛠️[{LogArea}] ⚙️[{TypeName}] Setting 🔧'{PropertyName}' parameter.", $"{SharedLoggingConstants.Area.Config}", $"{nameof(SessionConfigBuilder)}", propertyName);
        }

        private void LogComplexObjectSetting(string propertyName, string context)
        {
            _logger.Info("🛠️[{LogArea}] ⚙️[{TypeName}] Setting 🔧'{PropertyName}' parameter for '{Context}'.", $"{SharedLoggingConstants.Area.Config}", $"{nameof(SessionConfigBuilder)}", propertyName, context);
        }

        private void LogCollectionSetting(string propertyName, IEnumerable<string> values)
        {
            _logger.Info("🛠️[{LogArea}] ⚙️[{TypeName}] Setting 🔧'{PropertyName}' parameter to '[{Value}]'.", $"{SharedLoggingConstants.Area.Config}", $"{nameof(SessionConfigBuilder)}", propertyName, string.Join(", ", values ?? new[] { "null" }));
        }

        private void LogSystemMessage(SystemMessageMode mode, string? content)
        {
            _logger.Info("🛠️[{LogArea}] ⚙️[{TypeName}] Setting 🔧'{PropertyName}' parameter to 'Mode={Mode}, Content={Content}'.", $"{SharedLoggingConstants.Area.Config}", $"{nameof(SessionConfigBuilder)}", nameof(_sessionConfig.SystemMessage), mode, content?.TruncateWithCount(50) ?? "null");
        }
    }
}
