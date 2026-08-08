using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Builders;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses
{
    public abstract class MicrosoftAiAgentInteractor : IMicrosoftAiAgentInteractor
    {
        protected AIAgent? AiAgent { get; set; }
        protected AgentRunOptionsBuilder? _agentRunOptionsBuilder;
        protected List<IAiContentHandler> AiContentHandlers = new();
        protected ILogger Logger { get; }

        public MicrosoftAiAgentInteractor(ILogger logger)
        {
            Logger = logger;
        }

        protected async Task<AIAgent> GetAiAgentAsync(CancellationToken cancellationToken = default)
        {
            if (AiAgent == null)
                AiAgent = await BuildAiAgentAsync(cancellationToken);

            return AiAgent;
        }

        protected abstract Task<AIAgent> BuildAiAgentAsync(CancellationToken cancellationToken = default);
        public virtual IMicrosoftAiAgentInteractor WithAiAgent(AIAgent aiAgent)
        {
            AiAgent = aiAgent;
            return this;
        }

        public abstract IAiEntityWithTYamlConfigurationTypeInteractor WithAgent(string agentIdentifier);
        public abstract IAiEntityWithTYamlConfigurationTypeInteractor WithAgent(FileInfo filePath);
        public abstract IAiEntityWithTYamlConfigurationTypeInteractor WithWorkingDirectory(string workingDirectory);

        public IAiEntityWithTYamlConfigurationTypeInteractor WithAiContentHandlers(params IAiContentHandler[] handlers)
        {
            AiContentHandlers.AddRange(handlers);
            return this;
        }

        public IAiEntityWithTYamlConfigurationTypeInteractor WithAgentRunOptions(Func<AgentRunOptionsBuilder, AgentRunOptionsBuilder> updateAgentRunOptionsFunc)
        {
            _agentRunOptionsBuilder ??= new();
            updateAgentRunOptionsFunc.Invoke(_agentRunOptionsBuilder);
            return this;
        }

        public async Task<IAiInteractionDataBank> InvokeAiAgentWithStreamingAsync(IAiInteractionRequest request,
            Func<IAiInteractionDataBank, IValidate>? responseValidationFunc = null,
            int maxAttempts = 1,
            CancellationToken cancellationToken = default)
        {
            IAiInteractionDataBank interactionDataBank = new AiInteractionDataBank();
            IValidate? responseValidationModel = null;

            CancellationTokenSource? cts = null;

            if (cancellationToken == default)
            {
                cts = new CancellationTokenSource(TimeSpan.FromMinutes(15));
                cancellationToken = cts.Token;
            }

            var agent = await GetAiAgentAsync();

            AgentSession session;

            try
            {
                session = await agent.CreateSessionAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to create agent session for '{agent.Name}' agent. Exception: {ex.Message}";
                Logger.Error(errorMessage);
                throw new Exception(errorMessage, ex);
            }

            var currentPropmt = request.Prompt;

            AgentRunOptions? options = null;
            if (_agentRunOptionsBuilder != null)
            {
                options = _agentRunOptionsBuilder.Build();
            }

            try
            {
                bool isAssertionSuccessful = false;

                for (int attempt = 1; attempt <= maxAttempts; attempt++)
                {
                    var attemptLogStatement = $"Attempt #{attempt}";
                    var attemptLogStatementWithIcon = $"🔁 {attemptLogStatement}";

                    Logger.Info($"🔁--- {attemptLogStatement} Started ---");

                    #region Agent Interaction

                    Logger.Info($"{attemptLogStatementWithIcon} -> Agent Interaction Started");

                    var streamingResponse = agent.RunStreamingAsync(currentPropmt, session, options: options, cancellationToken: cancellationToken);

                    await foreach (AgentResponseUpdate update in streamingResponse)
                    {
                        foreach (AIContent content in update.Contents)
                        {
                            try
                            {
                                foreach (var handler in AiContentHandlers)
                                {
                                    handler.HandleEvent(content, interactionDataBank);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.Error($"Error processing content: {ex.Message}");

                            }
                        }
                    }

                    foreach (var handler in AiContentHandlers)
                    {
                        handler.Finally();
                    }

                    Logger.Info($"{attemptLogStatementWithIcon} -> Agent Interaction Finished");

                    #endregion Agent Interaction

                    #region Agent Response Validation
                    if (responseValidationFunc != null)
                    {
                        Logger.Info($"{attemptLogStatementWithIcon} -> Agent Response Validation Started");
                        responseValidationModel = responseValidationFunc.Invoke(interactionDataBank);

                        if (responseValidationModel.IsSuccessful)
                        {
                            Logger.Info($"{attemptLogStatementWithIcon} -> ✅ Assertion Passed! Agent Execution finished Successful");
                            isAssertionSuccessful = true;
                            break;
                        }
                        else
                        {
                            Logger.Warning($"{attemptLogStatementWithIcon} -> ❌ Assertion Failed! Agent Execution finished with Validation Errors: {responseValidationModel.Error}");

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

                    Logger.Info($"🔁--- {attemptLogStatement} Finished ---");
                }

                if (!isAssertionSuccessful)
                {
                    Logger.Error($"🔴 Agent Workflow stopped: maximum of {maxAttempts} iterative corrections reached without passing validation.");
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

        public abstract ValueTask DisposeAsync();
    }
}
