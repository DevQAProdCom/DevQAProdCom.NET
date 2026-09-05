using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Validators;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Factories
{
    internal class AiAgentsLibrary
    {
        public GitHubCopilotAiAgentInteractor GetBaseOrchestratorReadWriteAgent(string workingDirectory, OrchestratorReadWriteAgentRequestModel requestModel)
        {
            return GetBaseAgent(workingDirectory)
                .WithPrimaryAgent(Const.AiAgents.Names.ORCHESTRATOR_READ_WRITE_AGENT)
                .WithSessionConfig(config => config
                    .WithAgents(Const.AiAgents.Names.READ_AGENT, Const.AiAgents.Names.WRITE_AGENT))
                .WithPromptInJsonFormat(requestModel);
        }

        public GitHubCopilotAiAgentInteractor GetOrchestratorReadWriteAgentWithResponseValidator(string workingDirectory, OrchestratorReadWriteAgentRequestModel requestModel, IEnumerable<string> expectedData)
        {
            var responseValidator = new OrchestratorReadWriteAgentResponseValidator(requestModel, expectedData);
            var baseOrchestratorAgent = GetBaseOrchestratorReadWriteAgent(workingDirectory, requestModel);

            return baseOrchestratorAgent.WithResponseValidator(responseValidator);
        }

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

        public GitHubCopilotAiAgentInteractor GetBaseAgent(string workingDirectory)
        {
            return DiContainer.Instance.MicrosoftAiAgentsInteractorsFactory
                .GetGitHubCopilotAiAgentInteractor()
                .WithSessionConfig(config => config.WithModel(Const.ProviderModel.CLAUDE_HAIKU_4_5))
                .WithWorkingDirectory(workingDirectory)
                .WithDefaultContentHandlers();
        }

        public GitHubCopilotAiAgentInteractor GetBaseAnswerQuestionAgent(string workingDirectory, string filePathToWrite, AnswerQuestionsAgentRequestModel requestModel)
        {
            var validator = new AnswerQuestionsAgentReponseValidator(filePathToWrite, requestModel.Questions);

            return GetBaseAgent(workingDirectory)
                .WithSelectiveIsolation()
                .WithPrimaryAgent(Const.AiAgents.Names.ANSWER_QUESTIONS_AGENT)
                .WithPromptInJsonFormat(requestModel)
                .WithResponseValidator(validator)
                .WithMaxAttempts(1);
        }

        public GitHubCopilotAiAgentInteractor GetCheckCustomSkillsFieldAnswerQuestionsAgent(string workingDirectory, string filePathToWrite, AnswerQuestionsAgentRequestModel requestModel)
        {
            var validator = new AnswerQuestionsAgentReponseValidator(filePathToWrite, requestModel.Questions);

            return GetBaseAgent(workingDirectory)
                .WithSelectiveIsolation()
                .WithPrimaryAgent(Const.AiAgents.Names.CHECK_CUSTOM_SKILLS_FIELD_ANSWER_QUESTIONS_AGENT)
                .WithPromptInJsonFormat(requestModel)
                .WithResponseValidator(validator)
                .WithMaxAttempts(1);
        }

        public GitHubCopilotAiAgentInteractor GetCheckCustomInstructionsFieldAnswerQuestionsAgent(string workingDirectory, string filePathToWrite, AnswerQuestionsAgentRequestModel requestModel)
        {
            var validator = new AnswerQuestionsAgentReponseValidator(filePathToWrite, requestModel.Questions);

            return GetBaseAgent(workingDirectory)
                .WithSelectiveIsolation()
                .WithPrimaryAgent(Const.AiAgents.Names.CHECK_CUSTOM_INSTRUCTIONS_FIELD_ANSWER_QUESTIONS_AGENT)
                .WithPromptInJsonFormat(requestModel)
                .WithResponseValidator(validator)
                .WithMaxAttempts(1);
        }
    }
}
