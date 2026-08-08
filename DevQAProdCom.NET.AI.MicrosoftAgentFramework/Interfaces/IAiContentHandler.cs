using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using Microsoft.Extensions.AI;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces
{
    public interface IAiContentHandler
    {
        public void HandleEvent(AIContent content, IAiInteractionDataBank interactionDataBank);
        public void Finally();
    }
}
