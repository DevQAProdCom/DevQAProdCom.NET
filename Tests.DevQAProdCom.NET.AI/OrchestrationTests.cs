using NUnit.Framework;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class OrchestrationTests : BaseTest
    {
        [Test]
        public async Task Should_Check_Streaming_From_Subagents()
        {
            //GIVEN
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_Check_Streaming_From_Subagents));

            //WHEN
            await using (var agent = AiAgentsLibrary.GetBaseAgent(workingDirectory)
                .WithSessionConfig(config => config.WithIncludeSubAgentStreamingEvents(true)))
            {
                {
                    await agent.InvokeAiAgentWithStreamingAsync();
                }

                //THEN





                //TEAR DOWN
            }
        }
    }
}
