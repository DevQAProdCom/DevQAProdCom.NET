using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Interfaces
{
    public interface IAiAgentsService
    {
        Task ExecuteReadWriteAgent(
            string workingDirectory,
            string filePathToRead,
            string outputFolderToWrite,
            Func<IAiInteractionDataBank, IValidate>? responseValidationFunc = null,
            int maxAttempts = 3);
    }
}
