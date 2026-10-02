using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Services;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.AI.GitHubCopilot.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddGitHubCopilotClientService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGitHubCopilotClientService, GitHubCopilotClientService>();
            return serviceCollection;
        }

        public static IServiceCollection AddGitHubCopilotHooksSearcher(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IFileBasedHooksSearcher, GitHubCopilotFileBasedHooksSearcher>();
            return serviceCollection;
        }
    }
}
