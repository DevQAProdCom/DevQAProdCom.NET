using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Interfaces.Interactions
{
    public interface IAiEntityWithTYamlConfigurationTypeInteractor: IAsyncDisposable
    {
        public Task<IAiInteractionDataBank> InvokeAiAgentWithStreamingAsync(IAiInteractionRequest request,
            Func<IAiInteractionDataBank, IValidate>? responseValidationFunc = null,
            int maxAttempts = 1,
            CancellationToken cancellationToken = default);

        public IAiEntityWithTYamlConfigurationTypeInteractor WithAgent(string agentIdentifier);
        public IAiEntityWithTYamlConfigurationTypeInteractor WithAgent(FileInfo filePath);
        public IAiEntityWithTYamlConfigurationTypeInteractor WithWorkingDirectory(string workingDirectory);
    }
}
