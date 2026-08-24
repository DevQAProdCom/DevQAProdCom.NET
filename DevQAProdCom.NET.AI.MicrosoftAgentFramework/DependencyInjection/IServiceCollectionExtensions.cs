using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Services;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Factories;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.AI.MicrosoftAgentFramework.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMicrosoftAiAgentInteractor(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMicrosoftAiAgentInteractor, MicrosoftAiAgentInteractor>();
            return serviceCollection;
        }

        public static IServiceCollection AddGitHubCopilotAiAgentInteractor(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGitHubCopilotClientService, GitHubCopilotClientService>();
            serviceCollection.AddTransient<GitHubCopilotAiAgentInteractor>();
            return serviceCollection;
        }

        public static IServiceCollection AddMicrosoftAgentFrameworkAiAgentInteractorsFactory(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MicrosoftAgentFrameworkAiAgentInteractorsFactory>();
            serviceCollection.AddSingleton<IMicrosoftAgentFrameworkAiAgentInteractorsFactory, MicrosoftAgentFrameworkAiAgentInteractorsFactory>();
            return serviceCollection;
        }

        public static IServiceCollection AddMicrosoftAgentFrameworkDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMicrosoftAiAgentInteractor();
            serviceCollection.AddGitHubCopilotAiAgentInteractor();
            serviceCollection.AddMicrosoftAgentFrameworkAiAgentInteractorsFactory();

            return serviceCollection;
        }
    }
}
