using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.McpServers;
using DevQAProdCom.NET.AI.Shared.Models;
using FluentAssertions;
using GitHub.Copilot;
using NUnit.Framework;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class SessionConfigBuilderMcpServersTests : BaseTest
    {
        [Test]
        public void Should_Map_Stdio_FileBasedMcpServer_To_McpStdioServerConfig()
        {
            var fileBasedMcpServer = new FileBasedMcpServerModel
            {
                Identifier = "stdio-server",
                ContentValue = "{ \"type\": \"stdio\", \"command\": \"npx\", \"args\": [\"@playwright/mcp@latest\"], \"cwd\": \"/work\" }"
            };

            var mapper = new GitHubCopilotMcpServersMappers();
            var config = mapper.ToMcpServerConfig(fileBasedMcpServer);

            config.Should().BeOfType<McpStdioServerConfig>();
            var stdioConfig = (McpStdioServerConfig)config;
            stdioConfig.Command.Should().Be("npx");
            stdioConfig.Args.Should().ContainSingle().Which.Should().Be("@playwright/mcp@latest");
            stdioConfig.WorkingDirectory.Should().Be("/work");
        }

        [Test]
        public void Should_Map_Http_FileBasedMcpServer_To_McpHttpServerConfig()
        {
            var fileBasedMcpServer = new FileBasedMcpServerModel
            {
                Identifier = "http-server",
                ContentValue = "{ \"type\": \"http\", \"url\": \"https://example.com/mcp\" }"
            };

            var mapper = new GitHubCopilotMcpServersMappers();
            var config = mapper.ToMcpServerConfig(fileBasedMcpServer);

            config.Should().BeOfType<McpHttpServerConfig>();
            var httpConfig = (McpHttpServerConfig)config;
            httpConfig.Url.Should().Be("https://example.com/mcp");
        }

        [Test]
        public void Should_Map_Sse_FileBasedMcpServer_To_McpHttpServerConfig()
        {
            var fileBasedMcpServer = new FileBasedMcpServerModel
            {
                Identifier = "sse-server",
                ContentValue = "{ \"type\": \"sse\", \"url\": \"https://example.com/sse\" }"
            };

            var mapper = new GitHubCopilotMcpServersMappers();
            var config = mapper.ToMcpServerConfig(fileBasedMcpServer);

            config.Should().BeOfType<McpHttpServerConfig>();
        }

        [Test]
        public void Should_Map_Local_FileBasedMcpServer_To_McpStdioServerConfig()
        {
            var fileBasedMcpServer = new FileBasedMcpServerModel
            {
                Identifier = "local-server",
                ContentValue = "{ \"type\": \"local\", \"command\": \"node\" }"
            };

            var mapper = new GitHubCopilotMcpServersMappers();
            var config = mapper.ToMcpServerConfig(fileBasedMcpServer);

            config.Should().BeOfType<McpStdioServerConfig>();
        }

        [Test]
        public void Should_Throw_When_FileBasedMcpServer_Type_Is_Unsupported()
        {
            var fileBasedMcpServer = new FileBasedMcpServerModel
            {
                Identifier = "unknown-server",
                ContentValue = "{ \"type\": \"unknown\" }"
            };

            var mapper = new GitHubCopilotMcpServersMappers();
            Action act = () => mapper.ToMcpServerConfig(fileBasedMcpServer);

            act.Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Should_ConfigureMcpServers_Throw_When_Identifiers_Conflict()
        {
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_ConfigureMcpServers_Throw_When_Identifiers_Conflict));
            var sessionConfigBuilder = new SessionConfigBuilder(Log, new GitHubCopilotFileBasedHooksSearcher(Log))
                .WithWorkingDirectory(workingDirectory)
                .WithModel("claude-haiku-4.5")
                .WithFileBasedMcpServer("playwright", "{ \"type\": \"stdio\", \"command\": \"node\" }")
                .WithSdkBasedMcpServer("playwright");

            Action act = () => sessionConfigBuilder.Build();

            act.Should().Throw<InvalidOperationException>().WithMessage("*conflicts of identifiers*");
        }
    }
}
