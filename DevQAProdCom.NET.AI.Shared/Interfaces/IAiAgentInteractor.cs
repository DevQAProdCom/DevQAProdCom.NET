using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiAgentInteractor: IAsyncDisposable
    {
        public Task<IAiInteractionDataBank> InvokeAiAgentWithStreamingAsync(IAiInteractionRequest request,
            Func<IAiInteractionDataBank, IValidate>? responseValidationFunc = null,
            int maxAttempts = 1,
            CancellationToken cancellationToken = default);

        public IAiAgentInteractor WithAgent(string agentIdentifier);
        public IAiAgentInteractor WithAgent(FileInfo filePath);
        public IAiAgentInteractor WithWorkingDirectory(string workingDirectory);
    }
}
