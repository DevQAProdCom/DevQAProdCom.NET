using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Factories;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddGitHubCopilotAiAgentInteractor(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<GitHubCopilotAiAgentInteractorService>();
            serviceCollection.AddTransient<IAiAgentInteractorService, GitHubCopilotAiAgentInteractorService>();
            return serviceCollection;
        }

        public static IServiceCollection AddMicrosoftAgentFrameworkAiAgentInteractorsFactory(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MicrosoftAgentFrameworkAiAgentInteractorsFactory>();
            serviceCollection.AddSingleton<IMicrosoftAgentFrameworkAiAgentInteractorsFactory, MicrosoftAgentFrameworkAiAgentInteractorsFactory>();
            return serviceCollection;
        }
    }
}
