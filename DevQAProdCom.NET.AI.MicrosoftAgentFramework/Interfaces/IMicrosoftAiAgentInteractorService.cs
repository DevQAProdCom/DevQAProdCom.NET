using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces
{
    public interface IMicrosoftAiAgentInteractorService : IAiAgentInteractorService
    {
    }

    public interface IMicrosoftAiAgentInteractorService<T> : IMicrosoftAiAgentInteractorService
        where T : IMicrosoftAiAgentInteractorService<T>
    {
    }
}
