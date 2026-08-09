using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Factories
{
    public class MicrosoftAgentFrameworkAiAgentInteractorsFactory(IServiceProvider serviceProvider) : IMicrosoftAgentFrameworkAiAgentInteractorsFactory
    {
        public GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor()
        {
            return serviceProvider.GetService<GitHubCopilotAiAgentInteractor>() ?? throw new InvalidOperationException($"{nameof(GitHubCopilotAiAgentInteractor)} is not registered in the {nameof(IServiceProvider)}.");
        }
    }
}
