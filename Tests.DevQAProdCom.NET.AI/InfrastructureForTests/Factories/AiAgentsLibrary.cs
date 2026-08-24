using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using Tests.DevQAProdCom.NET.AI.Constants;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Validators;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Factories
{
    internal class AiAgentsLibrary
    {
        public GitHubCopilotAiAgentInteractor GetBaseReadWriteAgent(string workingDirectory)
        {
            return DiContainer.Instance.MicrosoftAiAgentsInteractorsFactory
                .GetGitHubCopilotAiAgentInteractor()
                .WithPrimaryAgent(Const.AiAgents.Names.READ_WRITE_AGENT)
                .WithSessionConfig(config => config.WithModel("claude-haiku-4.5"))
                .WithIsolation()
                .WithWorkingDirectory(workingDirectory)
                .WithDefaultContentHandlers();
        }

        public GitHubCopilotAiAgentInteractor GetReadWriteAgentWithResponseValidator(string workingDirectory, string inputFilePath, string expectedOutputFilePath, string inputContent)
        {
            var responseValidator = new ReadWriteAgentResponseValidator(inputFilePath, expectedOutputFilePath, inputContent);
            var baseReadWriteAgent = GetBaseReadWriteAgent(workingDirectory);

            return baseReadWriteAgent.WithResponseValidator(responseValidator);
        }
    }
}
