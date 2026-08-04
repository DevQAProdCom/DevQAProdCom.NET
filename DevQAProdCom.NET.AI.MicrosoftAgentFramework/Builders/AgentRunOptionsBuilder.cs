using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Builders
{
    public class AgentRunOptionsBuilder
    {
        private readonly AgentRunOptions _options = new();

        public AgentRunOptionsBuilder WithContinuationToken(ResponseContinuationToken? continuationToken)
        {
            _options.ContinuationToken = continuationToken;
            return this;
        }

        public AgentRunOptionsBuilder WithAllowBackgroundResponses(bool allowBackgroundResponses)
        {
            _options.AllowBackgroundResponses = allowBackgroundResponses;
            return this;
        }

        public AgentRunOptionsBuilder WithAdditionalProperties(AdditionalPropertiesDictionary? additionalProperties)
        {
            _options.AdditionalProperties = additionalProperties;
            return this;
        }

        public AgentRunOptionsBuilder WithResponseFormat(ChatResponseFormat? responseFormat)
        {
            _options.ResponseFormat = responseFormat;
            return this;
        }

        public AgentRunOptions Build()
        {
            return _options;
        }
    }
}
