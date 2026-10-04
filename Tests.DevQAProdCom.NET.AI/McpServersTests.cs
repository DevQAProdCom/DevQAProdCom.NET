using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Global.Utils;
using FluentAssertions;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class McpServersTests : BaseTest
    {
        private const string FileBasedMcpServerMcpJsonFileWithMcpServersTopLevelObjectAgentName = "check-custom-mcp-server-field-mcp-json-file-with-mcp-servers-top-level-object-agent";
        private const string FileBasedMcpServerMcpServersDirectoryAgentName = "playwright-file-based-mcp-server-mcp-servers-directory-general-top-level-object-agent";
        private const string SdkBasedMcpServerAgentName = "check-custom-mcp-servers-field-sdk-based-agent";
        private const string SdkBasedMcpServerIdentifier = "playwright-sdk-based-mcp-server";

        [Test]
        public async Task Should_FileBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier_From_GitHubDirectory_McpJsonFile_With_McpServers_TopLevelObject()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_FileBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier_From_GitHubDirectory_McpJsonFile_With_McpServers_TopLevelObject));
            var filePath = Path.Combine(workingDirectory, "youtube_url.txt");
            var requestModel = new PlaywrightMcpAgentRequestModel
            {
                FilePath = filePath
            };

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    mcpServersDefaultLocationsProvider: CreateFileBasedMcpServersDefaultLocationsProvider())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(FileBasedMcpServerMcpJsonFileWithMcpServersTopLevelObjectAgentName)
                .WithWorkingDirectory(workingDirectory)
                .WithPromptInJsonFormat(requestModel))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            var actualFileContent = File.ReadAllText(filePath);
            actualFileContent.Should().Be("https://www.youtube.com/results?search_query=Best+of+The+Voice");

            IoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task Should_FileBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier_From_McpServersDirectory_JsonFile_With_General_TopLevelObject()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_FileBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier_From_McpServersDirectory_JsonFile_With_General_TopLevelObject));
            var filePath = Path.Combine(workingDirectory, "youtube_url.txt");
            var requestModel = new PlaywrightMcpAgentRequestModel
            {
                FilePath = filePath
            };

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    mcpServersDefaultLocationsProvider: CreateFileBasedMcpServersDefaultLocationsProvider())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(FileBasedMcpServerMcpServersDirectoryAgentName)
                .WithWorkingDirectory(workingDirectory)
                .WithPromptInJsonFormat(requestModel))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            var actualFileContent = File.ReadAllText(filePath);
            actualFileContent.Should().Be("https://www.youtube.com/results?search_query=Best+of+The+Voice");

            IoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task Should_SdkBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_SdkBasedMcpServer_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier));
            var filePath = Path.Combine(workingDirectory, "youtube_url.txt");
            var requestModel = new PlaywrightMcpAgentRequestModel
            {
                FilePath = filePath
            };

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    allSdkBasedMcpServersCollection: CreateSdkBasedMcpServersCollection())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(SdkBasedMcpServerAgentName)
                .WithWorkingDirectory(workingDirectory)
                .WithPromptInJsonFormat(requestModel))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            var actualFileContent = File.ReadAllText(filePath);
            actualFileContent.Should().Be("https://www.youtube.com/results?search_query=Best+of+The+Voice");

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
            new GitHubCopilotFileBasedMcpServersDefaultLocationsProvider(useExtendedSearch: false);
    }
}
