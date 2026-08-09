using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using Tests.DevQAProdCom.NET.AI.Constants;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Interfaces;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Services
{
    public class AiAgentsService(IMicrosoftAgentFrameworkAiAgentInteractorsFactory aiAgentsInteractorsFactory) : IAiAgentsService
    {
        public async Task ExecuteReadWriteAgent(
            string workingDirectory,
            string filePathToRead,
            string outputFolderToWrite,
            Func<IAiInteractionDataBank, IValidate>? responseValidationFunc = null,
            int maxAttempts = 3)
        {
            var aiAgentInteractor = aiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor();

            var request = new AiInteractionRequestModel
            {
                Prompt = Const.AiAgents.Prompts.GetReadWriteAgentPrompt(filePathToRead, outputFolderToWrite)
            };

            await aiAgentInteractor
                .WithAgent(Const.AiAgents.Names.READ_WRITE_AGENT)
                .WithIsolation()
                .WithWorkingDirectory(workingDirectory)
                .InvokeAiAgentWithStreamingAsync(request, responseValidationFunc: responseValidationFunc, maxAttempts: maxAttempts);
        }
    }
}
