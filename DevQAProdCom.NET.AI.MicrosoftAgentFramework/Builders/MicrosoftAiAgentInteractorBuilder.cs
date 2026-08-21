using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Agents.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Builders
{
    public abstract class MicrosoftAiAgentInteractorBuilder
    {
        protected AIAgent? AiAgent { get; set; }
        protected ILogger Logger { get; }

        public MicrosoftAiAgentInteractorBuilder(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
