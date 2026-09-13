using DevQAProdCom.NET.Global.Utils;
using FluentAssertions;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class McpServersTests : BaseTest
    {
        [Test]
        public async Task Should_McpServer_Be_Used_Using_SDK_Configuration_By_Identifier()
        {
            //GIVEN
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_McpServer_Be_Used_Using_SDK_Configuration_By_Identifier));
            var filePath = Path.Combine(tempWorkingDirectory, "youtube_url.txt");

            //WHEN
            await using (var agent = AiAgentsLibrary.GetBaseAgent(tempWorkingDirectory)
                .WithPrimaryAgent(Const.AiAgents.Names.PLAYWRIGHT_MCP_AGENT)
                .WithPrompt($"filepath = {filePath}")
                .WithSessionConfig(config => config.WithMcpServer("playwright")))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            //THEN
            var actualFileContent = File.ReadAllText(filePath);
            actualFileContent.Should().Be("https://www.youtube.com/results?search_query=Best+of+The+Voice");

            //TEAR DOWN
            IoUtils.DeleteDirectory(tempWorkingDirectory);
        }
    }
}
