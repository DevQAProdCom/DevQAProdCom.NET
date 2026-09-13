using DevQAProdCom.NET.Global.Utils;
using FluentAssertions;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models;

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
            var requestModel = new PlaywrightMcpAgentRequestModel
            {
                FilePath = filePath
            };

            //WHEN
            await using (var agent = AiAgentsLibrary.GetPlaywrightMcpAgentWithResponseValidator(tempWorkingDirectory, requestModel))
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
