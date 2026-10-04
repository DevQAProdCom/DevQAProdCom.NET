using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Global.Utils;
using FluentAssertions;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Permissions;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class McpServersTests : BaseTest
    {
        private const string CheckCustomMcpServersFieldMcpJsonFileWithMcpServersTopLevelObjectAgent = "check-custom-mcp-servers-field-mcp-json-file-with-mcp-servers-top-level-object-agent";
        private const string CheckCustomMcpServersFieldMcpServersDirectoryWithGeneralTopLevelObjectAgent = "check-custom-mcp-servers-field-mcp-servers-directory-with-general-top-level-object-agent";
        private const string CheckCustomMcpServersFieldSdkBasedAgent = "check-custom-mcp-servers-field-sdk-based-agent";
        private const string SdkBasedMcpServerIdentifier = "playwright-sdk-based-mcp-server";
        private const string ExpectedOutputFileName = "youtube_url.txt";
        private const string ExpectedYoutubeUrl = "https://www.youtube.com/results?search_query=Best+of+The+Voice";

        [Test]
        public async Task Should_FileBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_McpServers_Field_By_Identifier_From_GitHubDirectory_McpJsonFile_With_McpServers_TopLevelObject()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_FileBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_McpServers_Field_By_Identifier_From_GitHubDirectory_McpJsonFile_With_McpServers_TopLevelObject));
            var filePath = Path.Combine(workingDirectory, ExpectedOutputFileName);
            var requestModel = new PlaywrightMcpAgentRequestModel
            {
                FilePath = filePath
            };
            var permissionDecisions = new PlaywrightMcpTestPermissionDecisions();

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    mcpServersDefaultLocationsProvider: CreateFileBasedMcpServersDefaultLocationsProvider())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(CheckCustomMcpServersFieldMcpJsonFileWithMcpServersTopLevelObjectAgent)
                .WithWorkingDirectory(workingDirectory)
                .WithPromptInJsonFormat(requestModel)
                .WithSessionConfig(builder => builder.SetPermission(
                    PlaywrightMcpTestPermissionDecisions.PlaywrightMcpServerInMcpJsonFileIdentifier,
                    permissionDecisions.GetApprovePlaywrightMcpServerInMcpJsonFileAllPermission().Value)))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            var actualFileContent = File.ReadAllText(filePath);
            actualFileContent.Should().Be(ExpectedYoutubeUrl);

            IoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task Should_FileBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_McpServers_Field_By_Identifier_From_McpServersDirectory_JsonFile_With_General_TopLevelObject()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_FileBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_McpServers_Field_By_Identifier_From_McpServersDirectory_JsonFile_With_General_TopLevelObject));
            var filePath = Path.Combine(workingDirectory, ExpectedOutputFileName);
            var requestModel = new PlaywrightMcpAgentRequestModel
            {
                FilePath = filePath
            };
            var permissionDecisions = new PlaywrightMcpTestPermissionDecisions();

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    mcpServersDefaultLocationsProvider: CreateFileBasedMcpServersDefaultLocationsProvider())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(CheckCustomMcpServersFieldMcpServersDirectoryWithGeneralTopLevelObjectAgent)
                .WithWorkingDirectory(workingDirectory)
                .WithPromptInJsonFormat(requestModel)
                .WithSessionConfig(builder => builder.SetPermission(
                    PlaywrightMcpTestPermissionDecisions.PlaywrightMcpServerInMcpServersDirectoryIdentifier,
                    permissionDecisions.GetApprovePlaywrightMcpServerInMcpServersDirectoryAllPermission().Value)))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            var actualFileContent = File.ReadAllText(filePath);
            actualFileContent.Should().Be(ExpectedYoutubeUrl);

            IoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task Should_SdkBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_McpServers_Field_By_Identifier()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_SdkBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_McpServers_Field_By_Identifier));
            var filePath = Path.Combine(workingDirectory, ExpectedOutputFileName);
            var requestModel = new PlaywrightMcpAgentRequestModel
            {
                FilePath = filePath
            };
            var permissionDecisions = new PlaywrightMcpTestPermissionDecisions();

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    allSdkBasedMcpServersCollection: CreateSdkBasedMcpServersCollection())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(CheckCustomMcpServersFieldSdkBasedAgent)
                .WithWorkingDirectory(workingDirectory)
                .WithPromptInJsonFormat(requestModel)
                .WithSessionConfig(builder => builder.SetPermission(
                    PlaywrightMcpTestPermissionDecisions.PlaywrightSdkBasedMcpServerIdentifier,
                    permissionDecisions.GetApprovePlaywrightSdkBasedMcpServerAllPermission().Value)))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            var actualFileContent = File.ReadAllText(filePath);
            actualFileContent.Should().Be(ExpectedYoutubeUrl);

            IoUtils.DeleteDirectory(workingDirectory);
        }

        private IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer> CreateSdkBasedMcpServersCollection()
        {
            var collection = new IdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>(Log);
            var sdkMcpServer = new GitHubCopilotPlaywrightMcpServer(Log);
            sdkMcpServer.Identifier = SdkBasedMcpServerIdentifier;
            collection.Add(sdkMcpServer);
            return collection;
        }

        private ILocationsProvider CreateFileBasedMcpServersDefaultLocationsProvider() =>
            new GitHubCopilotFileBasedMcpServersDefaultLocationsProvider(useExtendedSearch: true);
    }
}
