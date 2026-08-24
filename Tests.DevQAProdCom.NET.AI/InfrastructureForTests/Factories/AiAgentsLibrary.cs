using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using Tests.DevQAProdCom.NET.AI.Constants;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Factories
{
    internal class AiAgentsLibrary
    {
        public GitHubCopilotAiAgentInteractor GetReadWriteAgent(string workingDirectory)
        {
            return DiContainer.Instance.AiAgentsInteractorsFactory
                .GetGitHubCopilotAiAgentInteractor()
                .WithPrimaryAgent(Const.AiAgents.Names.READ_WRITE_AGENT)
                .WithIsolation()
                .WithWorkingDirectory(workingDirectory);
        }
    }
}
