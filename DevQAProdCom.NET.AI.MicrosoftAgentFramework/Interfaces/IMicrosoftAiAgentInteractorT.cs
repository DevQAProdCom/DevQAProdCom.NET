using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Builders;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Constants;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces
{
    public interface IMicrosoftAiAgentInteractorT<T> : IAiAgentInteractor
    {
        public Task<AIAgent> GetAiAgentAsync(CancellationToken cancellationToken = default);
        public T WithAiAgent(AIAgent aiAgent);
        public T WithAiContentHandlers(params IAiContentHandler[] handlers);
        public T WithAgentRunOptions(Func<AgentRunOptionsBuilder, AgentRunOptionsBuilder> updateAgentRunOptionsFunc);
        public T WithPrompt(string prompt);
        public T WithPromptInJsonFormat<TPromptInJsonFormat>(TPromptInJsonFormat promptData, string? promptPrefix = Const.Prompts.USE_THE_FOLLOWING_DATA_PROVIDED_IN_JSON_FORMAT);
        public T WithResponseValidationFunction(Func<IAiInteractionDataBank, IValidate>? responseValidationFunc);
        public T WithResponseValidator(IAiInteractionResultValidator responseValidator);
        public T WithMaxAttempts(int maxAttempts = 1);
    }
}
