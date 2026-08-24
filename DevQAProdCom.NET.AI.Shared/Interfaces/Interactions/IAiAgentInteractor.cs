using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Interfaces.Interactions
{
    public interface IAiAgentInteractor : IAsyncDisposable
    {
        public Task<IAiInteractionDataBank> InvokeAiAgentWithStreamingAsync(CancellationToken cancellationToken = default);

        public Task<IAiInteractionDataBank> InvokeAiAgentWithStreamingAsync(IAiInteractionRequest request,
            Func<IAiInteractionDataBank, IValidate>? responseValidationFunc = null,
            int maxAttempts = 1,
            CancellationToken cancellationToken = default);
    }
}
