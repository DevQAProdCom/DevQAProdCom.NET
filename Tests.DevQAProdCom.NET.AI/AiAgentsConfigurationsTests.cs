using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers;
using DevQAProdCom.NET.AI.Shared.Constants;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Agents;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using FluentAssertions;
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
            //SETUP
            CheckGitHubCopilotAuthenticationSetup();

            //GIVEN            
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_Default_AiAgentConfiguration_Be_Used_From_AiAgentMdFile));
            var (inputFilePath, inputContent, outputFolderPath, expectedOutputFilePath) = await PrepareReadWriteAgentTestFilesAsync(tempWorkingDirectory);

            //WHEN
            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor()
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(Const.AiAgents.Names.READ_WRITE_AGENT_WITH_2_CONFIGURATIONS, SharedAiConstants.Configurations.DEFAULT)
                .WithWorkingDirectory(tempWorkingDirectory)
                .WithPrompt(Const.AiAgents.Prompts.GetReadWriteAgentPrompt(inputFilePath, outputFolderPath))
                .WithResponseValidator(new ReadWriteAgentResponseValidator(inputFilePath, expectedOutputFilePath, inputContent))
                .WithMaxAttempts(3))
            {
                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();

                //THEN
                await act.Should().NotThrowAsync();
            }

            //TEAR DOWN
            CleanUpTempWorkingDirectory(tempWorkingDirectory);
        }

        [Test]
        public async Task Should_AiAgentConfiguration_Be_Used_From_AiAgentMdFile_By_Configuration_Identifier()
        {
            //SETUP
            CheckGitHubCopilotAuthenticationSetup();

            //GIVEN 
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_AiAgentConfiguration_Be_Used_From_AiAgentMdFile_By_Configuration_Identifier));
            var (inputFilePath, inputContent, outputFolderPath, expectedOutputFilePath) = await PrepareReadWriteAgentTestFilesAsync(tempWorkingDirectory);
            var permissionDecisions = new FilesystemMcpTestPermissionDecisions();

            //WHEN
            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    allSdkBasedMcpServersCollection: CreateFilesystemSdkBasedMcpServersCollection(tempWorkingDirectory, outputFolderPath))
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(Const.AiAgents.Names.READ_WRITE_AGENT_WITH_2_CONFIGURATIONS, Const.Configurations.FilesystemMcpMdFileConfigurationIdentifier)
                .WithWorkingDirectory(tempWorkingDirectory)
                .WithPrompt(Const.AiAgents.Prompts.GetReadWriteAgentPrompt(inputFilePath, outputFolderPath))
                .WithSessionConfig(builder => builder.SetPermission(
                    permissionDecisions.GetApproveFilesystemSdkBasedMcpServerAllPermission().Key,
                    permissionDecisions.GetApproveFilesystemSdkBasedMcpServerAllPermission().Value))
                .WithResponseValidator(new ReadWriteAgentResponseValidator(inputFilePath, expectedOutputFilePath, inputContent))
                .WithMaxAttempts(2))
            {

                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();

                //THEN
                await act.Should().NotThrowAsync();
            }

            //TEAR DOWN
            CleanUpTempWorkingDirectory(tempWorkingDirectory);
        }

        [Test]
        public async Task Should_AiAgentConfiguration_Be_Used_From_JsonConfigurationFile_By_Configuration_Identifier()
        {
            //SETUP
            CheckGitHubCopilotAuthenticationSetup();

            //GIVEN 
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_AiAgentConfiguration_Be_Used_From_JsonConfigurationFile_By_Configuration_Identifier));
            var (inputFilePath, inputContent, outputFolderPath, expectedOutputFilePath) = await PrepareReadWriteAgentTestFilesAsync(tempWorkingDirectory);
            var permissionDecisions = new FilesystemMcpTestPermissionDecisions();

            //WHEN
            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    agentsConfigurationsSearcher: CreateAgentConfigurationsSearcher(),
                    agentsDefaultConfigurationsLocationsProvider: CreateAgentConfigurationsLocationsProvider(),
                    allSdkBasedMcpServersCollection: CreateFilesystemSdkBasedMcpServersCollection(tempWorkingDirectory))
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(Const.AiAgents.Names.READ_WRITE_AGENT_WITH_2_CONFIGURATIONS, Const.Configurations.FilesystemMcpJsonFileConfigurationIdentifier)
                .WithWorkingDirectory(tempWorkingDirectory)
                .WithPrompt(Const.AiAgents.Prompts.GetReadWriteAgentPrompt(inputFilePath, outputFolderPath))
                .WithSessionConfig(builder => builder.SetPermission(
                    permissionDecisions.GetApproveFilesystemSdkBasedMcpServerAllPermission().Key,
                    permissionDecisions.GetApproveFilesystemSdkBasedMcpServerAllPermission().Value))
                .WithResponseValidator(new ReadWriteAgentResponseValidator(inputFilePath, expectedOutputFilePath, inputContent))
                .WithMaxAttempts(3))
            {
                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();

                //THEN
                await act.Should().NotThrowAsync();
            }

            //TEAR DOWN
            CleanUpTempWorkingDirectory(tempWorkingDirectory);
        }

        private IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer> CreateFilesystemSdkBasedMcpServersCollection(params string[] allowedDirectories)
        {
            var collection = new IdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>(Log);
            var server = new GitHubCopilotFilesystemMcpServer(Log, allowedDirectories);
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
