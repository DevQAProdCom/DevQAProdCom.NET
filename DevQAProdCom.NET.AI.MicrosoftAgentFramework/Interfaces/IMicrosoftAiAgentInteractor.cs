using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Builders;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Constants;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces
{
    public interface IMicrosoftAiAgentInteractor : IAiAgentInteractor
    {
        public IMicrosoftAiAgentInteractor WithAiAgent(AIAgent aiAgent);
        public IMicrosoftAiAgentInteractor WithAiContentHandlers(params IAiContentHandler[] handlers);
        public IMicrosoftAiAgentInteractor WithAgentRunOptions(AgentRunOptions? agentRunOptions);
        public IMicrosoftAiAgentInteractor WithAgentRunOptions(Func<AgentRunOptionsBuilder, AgentRunOptionsBuilder> updateAgentRunOptionsFunc);
        public IMicrosoftAiAgentInteractor WithPrompt(string prompt);
        public IMicrosoftAiAgentInteractor WithPromptInJsonFormat<TPromptInJsonFormat>(TPromptInJsonFormat promptData, string? promptPrefix = Const.Prompts.USE_THE_FOLLOWING_DATA_PROVIDED_IN_JSON_FORMAT);
        public IMicrosoftAiAgentInteractor WithResponseValidationFunction(Func<IAiInteractionDataBank, IValidate>? responseValidationFunc);
        public IMicrosoftAiAgentInteractor WithResponseValidator(IAiInteractionResultValidator responseValidator);
        public IMicrosoftAiAgentInteractor WithMaxAttempts(int maxAttempts = 1);
    }
}
