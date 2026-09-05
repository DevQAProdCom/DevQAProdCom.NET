using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations.Files;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Factories;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Models;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class BaseTest
    {
        protected ILogger Log => DiContainer.Instance.Log;
        protected AiAgentsLibrary AiAgentsLibrary => DiContainer.Instance.AiAgentsLibrary;
        protected IMicrosoftAgentFrameworkAiAgentInteractorsFactory AiAgentsInteractorsFactory => DiContainer.Instance.MicrosoftAiAgentsInteractorsFactory;
        protected GitHubCopilotAiAgentInteractor GetGitHubCopilotAiAgentInteractor() => AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor();

        protected string PrepareTempTestWorkingDirectory(string? testName)
        {
            testName ??= TestContext.CurrentContext.Test.MethodName;

            if (string.IsNullOrEmpty(testName))
                throw new ArgumentNullException("Unable to create temporary test folder using Test Name. Test Name is not specified.");

            var testDirectory = Path.Combine(Path.GetTempPath(), testName);

            if (Directory.Exists(testDirectory))
            {
                IoUtils.DeleteDirectory(testDirectory, recursive: true);
            }

            IoUtils.CreateDirectory(testDirectory);

            return testDirectory;
        }

        protected async Task<(string inputFilePath, string inputContent, string outputFolderPath, string expectedOutputFilePath)>
            PrepareReadWriteAgentTestFilesAsync(string testDirectory)
        {
            var timestamp = DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds();

            var inputFileName = $"file_to_read_{timestamp}.txt";
            var inputFilePath = Path.Combine(testDirectory, inputFileName);
            var inputContent = $"Random content for {timestamp} - {Guid.NewGuid()}";
            await File.WriteAllTextAsync(inputFilePath, inputContent);

            var outputFolderName = $"output_folder_to_write_{timestamp}";
            var outputFolderPath = Path.Combine(testDirectory, outputFolderName);
            Directory.CreateDirectory(outputFolderPath);

            var expectedOutputFileName = $"file_to_read_{timestamp}_copilot.txt";
            var expectedOutputFilePath = Path.Combine(outputFolderPath, expectedOutputFileName);

            return (inputFilePath, inputContent, outputFolderPath, expectedOutputFilePath);
        }

        protected string GetTempFilePath(string workingDirectory, FileExtension extension = FileExtension.Json)
        {
            var timestamp = DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds();
            var fileName = $"temp_file_{timestamp}_copilot{extension.GetDescriptionAttributeValue()}";
            return Path.Combine(workingDirectory, fileName);
        }

        protected AnswerQuestionsAgentRequestModel GetAnswerQuestionsAgentRequestModel(string workingDirectory, List<string> questions)
        {
            return new AnswerQuestionsAgentRequestModel
            {
                FilePathToWriteResponseTo = GetTempFilePath(workingDirectory),
                Questions = questions
            };
        }

        protected async Task<(OrchestratorReadWriteAgentRequestModel requestModel, List<string> fileContents)> PrepareOrchestratorReadWriteAgentTestFilesAsync(
            string testDirectory,
            int fileCount = 3)
        {
            var filePathsToRead = new List<string>();
            var fileContents = new List<string>();

            for (int i = 0; i < fileCount; i++)
            {
                var timestamp = DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds();
                var inputFileName = $"file_to_read_{i}_{timestamp}.txt";
                var inputFilePath = Path.Combine(testDirectory, inputFileName);
                var inputContent = $"Random content for {timestamp} - index {i} - {Guid.NewGuid()}";
                await File.WriteAllTextAsync(inputFilePath, inputContent);

                filePathsToRead.Add(inputFilePath);
                fileContents.Add(inputContent);
            }

            var requestModel = new OrchestratorReadWriteAgentRequestModel
            {
                FilePathsToRead = filePathsToRead,
                OutputFilePathToWrite = GetTempFilePath(testDirectory)
            };

            return (requestModel, fileContents);
        }
    }
}
