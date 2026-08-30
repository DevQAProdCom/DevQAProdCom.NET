using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Validators;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Factories
{
    internal class AiAgentsLibrary
    {
        public GitHubCopilotAiAgentInteractor GetBaseReadWriteAgent(string workingDirectory, string inputFilePath, string outputFolderPath)
        {
            return DiContainer.Instance.MicrosoftAiAgentsInteractorsFactory
                .GetGitHubCopilotAiAgentInteractor()
                .WithPrimaryAgent(Const.AiAgents.Names.READ_WRITE_AGENT)
                .WithSessionConfig(config => config.WithModel("claude-haiku-4.5"))
                .WithSelectiveIsolation()
                .WithWorkingDirectory(workingDirectory)
                .WithDefaultContentHandlers()
                .WithPrompt(Const.AiAgents.Prompts.GetReadWriteAgentPrompt(inputFilePath, outputFolderPath));
        }

        public GitHubCopilotAiAgentInteractor GetReadWriteAgentWithResponseValidator(string workingDirectory, string inputFilePath, string outputFolderPath, string expectedOutputFilePath, string inputContent)
        {
            var responseValidator = new ReadWriteAgentResponseValidator(inputFilePath, expectedOutputFilePath, inputContent);
            var baseReadWriteAgent = GetBaseReadWriteAgent(workingDirectory, inputFilePath, outputFolderPath);

            return baseReadWriteAgent.WithResponseValidator(responseValidator);
        }

        public GitHubCopilotAiAgentInteractor GetBaseShowInstructionsAgent(string workingDirectory)
        {
            return DiContainer.Instance.MicrosoftAiAgentsInteractorsFactory
                .GetGitHubCopilotAiAgentInteractor()
                .WithPrimaryAgent(Const.AiAgents.Names.ANSWER_QUESTIONS_AGENT)
                .WithSessionConfig(config => config.WithModel(Const.ProviderModel.CLAUDE_HAIKU_4_5))
                .WithWorkingDirectory(workingDirectory)
                .WithDefaultContentHandlers()
                .WithPrompt($"Execute agent 'Show Instructions Agent'. And answer 'What is My favorite animal?");
            //.WithPrompt($"Execute '{Const.AiAgents.Names.SHOW_INSTRUCTIONS_AGENT}'. Describe the name of the agent that is executed. Its description. And what agent must do. Execute what agent must do.");
        }

        public GitHubCopilotAiAgentInteractor GetBaseAgent(string workingDirectory)
        {
            return DiContainer.Instance.MicrosoftAiAgentsInteractorsFactory
                .GetGitHubCopilotAiAgentInteractor()
                .WithSessionConfig(config => config.WithModel(Const.ProviderModel.CLAUDE_HAIKU_4_5))
                .WithWorkingDirectory(workingDirectory)
                .WithDefaultContentHandlers();
        }
    }
}
