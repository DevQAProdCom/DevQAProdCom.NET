using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class BaseTest
    {
        private IMicrosoftAgentFrameworkAiAgentInteractorsFactory _aiAgentsInteractorsFactory = DiContainer.Instance.GetRequiredService<IMicrosoftAgentFrameworkAiAgentInteractorsFactory>();
        protected IMicrosoftAgentFrameworkAiAgentInteractorsFactory AiAgentsInteractorsFactory => _aiAgentsInteractorsFactory ??= DiContainer.Instance.GetRequiredService<IMicrosoftAgentFrameworkAiAgentInteractorsFactory>();
    }
}
