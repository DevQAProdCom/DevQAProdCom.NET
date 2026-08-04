using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Extensions.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers.GitHubCopilot
{
    public class GitHubCopilotSessionEventAiContentHandler(IAiInteractionHandler handler, ILogger logger) : IAiContentHandler
    {
        public virtual void HandleEvent(AIContent content, IAiInteractionDataBank interactionDataBank)
        {
            if (content.RawRepresentation != null)
            {
                var rawRepresentation = content.ToJson();
                handler.HandleEvent(rawRepresentation, interactionDataBank);
            }
        }

        public virtual void Finally()
        {
            handler.Finally();
        }
    }
}
