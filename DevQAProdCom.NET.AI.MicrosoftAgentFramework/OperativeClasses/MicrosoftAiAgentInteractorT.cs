using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Builders;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses
{
    public abstract class MicrosoftAiAgentInteractorT<T> : IMicrosoftAiAgentInteractorT<T>
        where T : class, IMicrosoftAiAgentInteractorT<T>
    {
        protected AIAgent? AiAgent { get; set; }
        protected ILogger Logger { get; }

        protected string? Prompt = string.Empty;
        protected Func<IAiInteractionDataBank, IValidate>? ResponseValidationFunc = null;

        protected IMicrosoftAiAgentInteractor MicrosoftAiAgentInteractor;

        public MicrosoftAiAgentInteractorT(IMicrosoftAiAgentInteractor microsoftAiAgentInteractor, ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(logger);
            Logger = logger;
            MicrosoftAiAgentInteractor = microsoftAiAgentInteractor;
        }

        public MicrosoftAiAgentInteractorT(AIAgent aiAgent, IMicrosoftAiAgentInteractor microsoftAiAgentInteractor, ILogger logger, AgentRunOptions? agentRunOptions = null, params IAiContentHandler[] aiContentHandlers)
            : this(microsoftAiAgentInteractor, logger)
        {
            WithAiAgent(aiAgent);
            WithAgentRunOptions(agentRunOptions);
            WithAiContentHandlers(aiContentHandlers);
        }

        public T WithAiAgent(AIAgent aiAgent)
        {
            MicrosoftAiAgentInteractor.WithAiAgent(aiAgent);
            return this as T;
        }

        public T WithAiContentHandlers(params IAiContentHandler[] handlers)
        {
            MicrosoftAiAgentInteractor.WithAiContentHandlers(handlers);
            return this as T;
        }

        public T WithAgentRunOptions(AgentRunOptions? agentRunOptions)
        {
            MicrosoftAiAgentInteractor.WithAgentRunOptions(agentRunOptions);
            return this as T;
        }

        public T WithAgentRunOptions(Func<AgentRunOptionsBuilder, AgentRunOptionsBuilder> updateAgentRunOptionsFunc)
        {
            MicrosoftAiAgentInteractor.WithAgentRunOptions(updateAgentRunOptionsFunc);
            return this as T;
        }

        public T WithPrompt(string prompt)
        {
            MicrosoftAiAgentInteractor.WithPrompt(prompt);
            return this as T;
        }

        public T WithPromptInJsonFormat<TPromptInJsonFormat>(TPromptInJsonFormat promptData, string? promptPrefix = Const.Prompts.USE_THE_FOLLOWING_DATA_PROVIDED_IN_JSON_FORMAT)
        {
            MicrosoftAiAgentInteractor.WithPromptInJsonFormat(promptData, promptPrefix);
            return this as T;
        }

        public T WithMaxAttempts(int maxAttempts = 1)
        {
            MicrosoftAiAgentInteractor.WithMaxAttempts(maxAttempts);
            return this as T;
        }

        public T WithResponseValidationFunction(Func<IAiInteractionDataBank, IValidate>? responseValidationFunc)
        {
            MicrosoftAiAgentInteractor.WithResponseValidationFunction(responseValidationFunc);
            return this as T;
        }

        public T WithResponseValidator(IAiInteractionResultValidator responseValidator)
        {
            MicrosoftAiAgentInteractor.WithResponseValidator(responseValidator);
            return this as T;
        }

        public virtual async Task<IAiInteractionDataBank> InvokeAiAgentWithStreamingAsync(IAiInteractionRequest request,
            Func<IAiInteractionDataBank, IValidate>? responseValidationFunc = null,
            int maxAttempts = 1,
            CancellationToken cancellationToken = default)
        {
            AiAgent = await GetAiAgentAsync();
            MicrosoftAiAgentInteractor.WithAiAgent(AiAgent);
            return await MicrosoftAiAgentInteractor.InvokeAiAgentWithStreamingAsync(request, responseValidationFunc, maxAttempts, cancellationToken);
        }

        public virtual async Task<IAiInteractionDataBank> InvokeAiAgentWithStreamingAsync(CancellationToken cancellationToken = default)
        {
            AiAgent = await GetAiAgentAsync();
            MicrosoftAiAgentInteractor.WithAiAgent(AiAgent);
            return await MicrosoftAiAgentInteractor.InvokeAiAgentWithStreamingAsync();
        }

        public abstract Task<AIAgent> GetAiAgentAsync(CancellationToken cancellationToken = default);

        public virtual ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }
}
