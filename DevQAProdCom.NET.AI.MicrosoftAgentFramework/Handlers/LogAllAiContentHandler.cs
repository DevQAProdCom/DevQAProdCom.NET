using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Extensions.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Handlers
{
    public class LogAllAiContentHandler(string filePath, ILogger logger) : IAiContentHandler
    {
        public virtual void HandleEvent(AIContent content, IAiInteractionDataBank interactionDataBank)
        {
            if (content.RawRepresentation != null)
            {
                var rawRepresentation = content.RawRepresentation.ToJson();
            }
        }

        public virtual void Finally()
        {

        }
    }
}
