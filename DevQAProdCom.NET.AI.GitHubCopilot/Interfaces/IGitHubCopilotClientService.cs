using DevQAProdCom.NET.AI.GitHubCopilot.Builders;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Services;
using GitHub.Copilot;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Interfaces
{
    public interface IGitHubCopilotClientService : IAsyncDisposable
    {
        public GitHubCopilotClientService WithCopilotClientOptions(Func<CopilotClientOptionsBuilder, CopilotClientOptionsBuilder> updateCopilotClientOptionsFunc);
        public CopilotClient GetGitHubCopilotClient();
    }
}
