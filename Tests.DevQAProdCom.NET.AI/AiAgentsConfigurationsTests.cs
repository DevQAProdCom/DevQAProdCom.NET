using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers;
using DevQAProdCom.NET.AI.Shared.Constants;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Global.Utils;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.LocationsProviders;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Permissions;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Validators;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class AiAgentsConfigurationsTests : BaseTest
    {
        [Test]
        public async Task Should_Default_AiAgentConfiguration_Be_Used_From_AiAgentMdFile()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_Default_AiAgentConfiguration_Be_Used_From_AiAgentMdFile));
            var (inputFilePath, inputContent, _, _) = await PrepareReadWriteAgentTestFilesAsync(workingDirectory);

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor()
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(Const.AiAgents.Names.READ_AGENT_WITH_2_CONFIGURATIONS, SharedAiConstants.Configurations.DEFAULT)
                .WithWorkingDirectory(workingDirectory)
                .WithPrompt(Const.AiAgents.Prompts.GetReadAgentPrompt(inputFilePath))
                .WithResponseValidator(new ReadAgentResponseValidator(inputContent))
                .WithMaxAttempts(3))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            IoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task Should_AiAgentConfiguration_Be_Used_From_AiAgentMdFile_By_Configuration_Identifier()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_AiAgentConfiguration_Be_Used_From_AiAgentMdFile_By_Configuration_Identifier));
            var (inputFilePath, inputContent, _, _) = await PrepareReadWriteAgentTestFilesAsync(workingDirectory);
            var permissionDecisions = new FilesystemMcpTestPermissionDecisions();

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    allSdkBasedMcpServersCollection: CreateFilesystemSdkBasedMcpServersCollection(workingDirectory))
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(Const.AiAgents.Names.READ_AGENT_WITH_2_CONFIGURATIONS, Const.Configurations.FilesystemMcpMdFileConfigurationIdentifier)
                .WithWorkingDirectory(workingDirectory)
                .WithPrompt(Const.AiAgents.Prompts.GetReadAgentPrompt(inputFilePath))
                .WithSessionConfig(builder => builder.SetPermission(
                    permissionDecisions.GetApproveFilesystemSdkBasedMcpServerAllPermission().Key,
                    permissionDecisions.GetApproveFilesystemSdkBasedMcpServerAllPermission().Value))
                .WithResponseValidator(new ReadAgentResponseValidator(inputContent))
                .WithMaxAttempts(3))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            IoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task Should_AiAgentConfiguration_Be_Used_From_JsonConfigurationFile_By_Configuration_Identifier()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_AiAgentConfiguration_Be_Used_From_JsonConfigurationFile_By_Configuration_Identifier));
            var (inputFilePath, inputContent, _, _) = await PrepareReadWriteAgentTestFilesAsync(workingDirectory);
            var permissionDecisions = new FilesystemMcpTestPermissionDecisions();

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    agentsConfigurationsSearcher: CreateAgentConfigurationsSearcher(),
                    agentsDefaultConfigurationsLocationsProvider: CreateAgentConfigurationsLocationsProvider(),
                    allSdkBasedMcpServersCollection: CreateFilesystemSdkBasedMcpServersCollection(workingDirectory))
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(Const.AiAgents.Names.READ_AGENT_WITH_2_CONFIGURATIONS, Const.Configurations.FilesystemMcpJsonFileConfigurationIdentifier)
                .WithWorkingDirectory(workingDirectory)
                .WithPrompt(Const.AiAgents.Prompts.GetReadAgentPrompt(inputFilePath))
                .WithSessionConfig(builder => builder.SetPermission(
                    permissionDecisions.GetApproveFilesystemSdkBasedMcpServerAllPermission().Key,
                    permissionDecisions.GetApproveFilesystemSdkBasedMcpServerAllPermission().Value))
                .WithResponseValidator(new ReadAgentResponseValidator(inputContent))
                .WithMaxAttempts(3))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            IoUtils.DeleteDirectory(workingDirectory);
        }

        private IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer> CreateFilesystemSdkBasedMcpServersCollection(string workingDirectory)
        {
            var collection = new IdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>(Log);
            var server = new GitHubCopilotFilesystemMcpServer(Log, new[] { workingDirectory });
            server.Identifier = FilesystemMcpTestPermissionDecisions.FilesystemSdkBasedMcpServerIdentifier;
            collection.Add(server);
            return collection;
        }

        private ISearchEntitiesInLocations<IAiAgentConfiguration> CreateAgentConfigurationsSearcher()
        {
            return new AiAgentsConfigurationsSearcher(Log);
        }

        private ILocationsProvider CreateAgentConfigurationsLocationsProvider()
        {
            return new AiAgentsConfigurationsLocationsProvider();
        }
    }
}
