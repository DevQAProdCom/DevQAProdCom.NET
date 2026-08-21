using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.Factories
{
    public class MicrosoftAgentFrameworkAiAgentInteractorsFactory(IServiceProvider serviceProvider) : IMicrosoftAgentFrameworkAiAgentInteractorsFactory
    {
        public GitHubCopilotAiAgentInteractorService GetGitHubCopilotAiAgentInteractor()
        {
            return serviceProvider.GetService<GitHubCopilotAiAgentInteractorService>() ?? throw new InvalidOperationException($"{nameof(GitHubCopilotAiAgentInteractorService)} is not registered in the {nameof(IServiceProvider)}.");
        }
    }
}
