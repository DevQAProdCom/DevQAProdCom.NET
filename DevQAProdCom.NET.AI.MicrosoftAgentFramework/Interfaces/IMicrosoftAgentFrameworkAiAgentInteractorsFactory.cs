using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces
{
    public interface IMicrosoftAgentFrameworkAiAgentInteractorsFactory
    {
        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor();
    }
}
