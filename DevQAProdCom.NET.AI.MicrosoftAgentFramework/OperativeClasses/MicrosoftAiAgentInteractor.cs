using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Builders;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses
{
    public class MicrosoftAiAgentInteractor : IMicrosoftAiAgentInteractor
    {
        protected AIAgent? AiAgent { get; set; }
        private AgentRunOptionsBuilder? _agentRunOptionsBuilder;
        private AgentRunOptions? _agentRunOptions;
        private List<IAiContentHandler> _aiContentHandlers = new();
        private AgentSession? _session;
        private int _maxAttempts = 1;
        private readonly TimeSpan _defaultInteractionTimeout = TimeSpan.FromMinutes(15);

        private string _prompt = string.Empty;
        private Func<IAiInteractionDataBank, IValidate>? _responseValidationFunc = null;
        private ILogger _logger { get; }

        public MicrosoftAiAgentInteractor(ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(logger);
            _logger = logger;
        }

        public MicrosoftAiAgentInteractor(AIAgent aiAgent, ILogger logger, List<IAiContentHandler>? aiContentHandlers = null, AgentRunOptions? agentRunOptions = null) : this(logger)
        {
            WithAiAgent(aiAgent);
            WithAgentRunOptions(agentRunOptions);
            WithAiContentHandlers(aiContentHandlers?.ToArray());
        }

        public IMicrosoftAiAgentInteractor WithAiAgent(AIAgent aiAgent)
        {
            AiAgent = aiAgent;
            return this;
        }

        public IMicrosoftAiAgentInteractor WithAiContentHandlers(params IAiContentHandler[]? handlers)
        {
            if (handlers?.Count() > 0)
            {
                var handlerTypes = string.Join(", ", handlers.Select(h => h.GetType().Name));
                _logger.Info($"Adding {handlers.Length} AI Content Handler(s): {handlerTypes}");
                _aiContentHandlers.AddRange(handlers);
            }

            return this;
        }

        public IMicrosoftAiAgentInteractor WithAgentRunOptions(AgentRunOptions? agentRunOptions)
        {
            _agentRunOptions = agentRunOptions;
            return this;
        }

        public IMicrosoftAiAgentInteractor WithAgentRunOptions(Func<AgentRunOptionsBuilder, AgentRunOptionsBuilder> updateAgentRunOptionsFunc)
        {
            _agentRunOptionsBuilder ??= new();
            updateAgentRunOptionsFunc.Invoke(_agentRunOptionsBuilder);
            return this;
        }


        public IMicrosoftAiAgentInteractor WithPrompt(string prompt)
        {
            _prompt = prompt;
            return this;
        }

        public IMicrosoftAiAgentInteractor WithResponseValidationFunction(Func<IAiInteractionDataBank, IValidate>? responseValidationFunc)
        {
            _responseValidationFunc = responseValidationFunc;
            return this;
        }

        public IMicrosoftAiAgentInteractor WithResponseValidator(IAiInteractionResultValidator responseValidator)
        {
            _responseValidationFunc = responseValidator.Validate;
            return this;
        }

        public IMicrosoftAiAgentInteractor WithMaxAttempts(int maxAttempts = 1)
        {
            _maxAttempts = maxAttempts;
            return this;
        }

        public virtual async Task<IAiInteractionDataBank> InvokeAiAgentWithStreamingAsync(IAiInteractionRequest request,
            Func<IAiInteractionDataBank, IValidate>? responseValidationFunc = null,
            int maxAttempts = 1,
            CancellationToken cancellationToken = default)
        {
            IAiInteractionDataBank interactionDataBank = new AiInteractionDataBank();
            IValidate? responseValidationModel = null;

            using var cts = CreateLinkedCancellationTokenSourceOrDefault(cancellationToken);

            if (AiAgent == null)
            {
                throw new InvalidOperationException("AI Agent is not set. Call WithAiAgent before invoking the agent.");
            }

            if (_session == null)
                try
                {
                    _session = await AiAgent.CreateSessionAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    var errorMessage = $"Failed to create agent session for '{AiAgent.Name}' agent. Exception: {ex.Message}";
                    _logger.Error(errorMessage);
                    throw new Exception(errorMessage, ex);
                }

            var currentPropmt = request.Prompt;

            if (_agentRunOptionsBuilder != null)
            {
                _agentRunOptions = _agentRunOptionsBuilder.Build();
            }


            try
            {
                bool isAssertionSuccessful = false;

                for (int attempt = 1; attempt <= maxAttempts; attempt++)
                {
                    var attemptLogStatement = $"Attempt #{attempt}";
                    var attemptLogStatementWithIcon = $"🔁 {attemptLogStatement}";

                    _logger.Info($"🔁--- {attemptLogStatement} Started ---");

                    #region Agent Interaction

                    _logger.Info($"{attemptLogStatementWithIcon} -> Agent Interaction Started");

                    var streamingResponse = AiAgent.RunStreamingAsync(currentPropmt, _session, options: _agentRunOptions, cancellationToken: cancellationToken);

                    await foreach (AgentResponseUpdate update in streamingResponse)
                    {
                        foreach (AIContent content in update.Contents)
                        {
                            try
                            {
                                foreach (var handler in _aiContentHandlers)
                                {
                                    handler.HandleEvent(content, interactionDataBank);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.Error($"Error processing content. {ex.Message}");

                            }
                        }
                    }

                    foreach (var handler in _aiContentHandlers)
                    {
                        handler.Finally();
                    }

                    _logger.Info($"{attemptLogStatementWithIcon} -> Agent Interaction Finished");

                    #endregion Agent Interaction

                    #region Agent Response Validation
                    if (responseValidationFunc != null)
                    {
                        _logger.Info($"{attemptLogStatementWithIcon} -> Agent Response Validation Started");
                        responseValidationModel = responseValidationFunc.Invoke(interactionDataBank);

                        if (responseValidationModel.IsSuccessful)
                        {
                            _logger.Info($"{attemptLogStatementWithIcon} -> ✅ Assertion Passed! Agent Execution finished Successful");
                            isAssertionSuccessful = true;
                            break;
                        }
                        else
                        {
                            _logger.Warning($"{attemptLogStatementWithIcon} -> ❌ Assertion Failed! Agent Execution finished with Validation Errors: {responseValidationModel.Error}");

                            // Feed the validation error explicitly back to the model for the next loop attempt
                            currentPropmt = $"Your previous output failed validation. Error details: {responseValidationModel.Error}.\nRecheck and fix all issues with available tools.";
                        }
                    }
                    else
                    {
                        // If no validation delegate was provided, assume success and exit the loop
                        isAssertionSuccessful = true;
                        break;
                    }
                    #endregion Agent Response Validation

                    _logger.Info($"🔁--- {attemptLogStatement} Finished ---");
                }

                if (!isAssertionSuccessful)
                {
                    _logger.Error($"🔴 Agent Workflow stopped: maximum of {maxAttempts} iterative corrections reached without passing validation.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"🔴 Agent Workflow stopped because of exception: {ex.Message}.");
            }
            finally
            {
                cts?.Dispose();

            }

            if (responseValidationFunc != null && responseValidationModel?.IsSuccessful != true)
            {
                throw new Exception($"🔴 Agent Workflow failed validation checks after {maxAttempts} attempts.");
            }

            return interactionDataBank;
        }

        public async Task<IAiInteractionDataBank> InvokeAiAgentWithStreamingAsync(CancellationToken cancellationToken = default)
        {
            var request = new AiInteractionRequestModel
            {
                Prompt = _prompt
            };

            return await InvokeAiAgentWithStreamingAsync(request, _responseValidationFunc, _maxAttempts, cancellationToken);
        }

        private CancellationTokenSource CreateLinkedCancellationTokenSourceOrDefault(CancellationToken cancellationToken)
        {
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            if (cancellationToken == default)
                cts.CancelAfter(_defaultInteractionTimeout);

            return cts;
        }

        public virtual ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }
}
