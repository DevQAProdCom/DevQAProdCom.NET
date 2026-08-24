using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using Tests.DevQAProdCom.NET.AI.Constants;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Factories;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Interfaces;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Services
{
    internal class AiAgentsService(AiAgentsLibrary aiAgentsLibrary) : IAiAgentsService
    {
        public async Task ExecuteReadWriteAgent(
            string workingDirectory,
            string filePathToRead,
            string outputFolderToWrite,
            Func<IAiInteractionDataBank, IValidate>? responseValidationFunc = null,
            int maxAttempts = 3)
        {
            await using var agent = aiAgentsLibrary
                 .GetReadWriteAgent(workingDirectory)
                 .WithPrompt(Const.AiAgents.Prompts.GetReadWriteAgentPrompt(filePathToRead, outputFolderToWrite))
                 .WithMaxAttempts(3);

            await agent.InvokeAiAgentWithStreamingAsync();
        }
    }
}
