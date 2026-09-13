using DevQAProdCom.NET.Global.Utils;
using NUnit.Framework;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class McpServersTests : BaseTest
    {
        [Test]
        public async Task Should_McpServer_Be_Used_Using_SDK_Configuration_By_Identifier()
        {
            //GIVEN
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_McpServer_Be_Used_Using_SDK_Configuration_By_Identifier));

            //WHEN
            await using (var agent = AiAgentsLibrary.GetBaseAgent(tempWorkingDirectory)
                .WithSessionConfig(config => config.WithMcpServer("playwright")))
            {
                await agent.InvokeAiAgentWithStreamingAsync();
            }

            //TEAR DOWN
            IoUtils.DeleteDirectory(tempWorkingDirectory);
        }
    }
}
