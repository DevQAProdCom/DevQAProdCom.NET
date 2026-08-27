using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Factories;

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
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd_hh-mm-ss");

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
    }
}
