using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Tests.DevQAProdCom.NET.AI.Constants;
using GlobalIoUtils = DevQAProdCom.NET.Global.Utils.IoUtils;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class AiAgentsTests : BaseTest
    {
        [Test]
        public async Task ReadWriteAgentTest()
        {
            var testDirectory = PrepareTempTestWorkingDirectory();
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

            await using (var agent = AiAgentsLibrary
                .GetReadWriteAgentWithResponseValidator(testDirectory, inputFilePath, expectedOutputFilePath, inputContent)
                .WithPrompt(Const.AiAgents.Prompts.GetReadWriteAgentPrompt(inputFilePath, outputFolderPath))
                .WithMaxAttempts(3))
            {
                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();
                await act.Should().NotThrowAsync();
            }

            GlobalIoUtils.DeleteDirectory(testDirectory);
        }

        [Test]
        public async Task MemorizeNumberTest()
        {
            var testDirectory = PrepareTempTestWorkingDirectory(nameof(MemorizeNumberTest));

            await using (var aiAgent = GetGitHubCopilotAiAgentInteractor()
                .WithDefaultContentHandlers()
                .WithSessionConfig(config => config.WithModel("claude-haiku-4.5"))
                .WithWorkingDirectory(testDirectory))
            {
                var randomNumber = new Random().Next(1000, 9999);

                await aiAgent
                    .WithPrompt($"Memorize number {randomNumber}.")
                    .InvokeAiAgentWithStreamingAsync();

                await aiAgent
                    .WithPrompt($"Subtract 1 from previously memorized number.")
                    .InvokeAiAgentWithStreamingAsync();
            }
            GlobalIoUtils.DeleteDirectory(testDirectory);
        }

        //[Test]
        //public async Task ReadWriteAgentWithInstructionsTest()
        //{
        //    var testDirectory = Path.Combine(Path.GetTempPath(), nameof(ReadWriteAgentWithInstructionsTest));
        //    GlobalIoUtils.DeleteDirectory(testDirectory);

        //    if (Directory.Exists(testDirectory))
        //    {
        //        Directory.Delete(testDirectory, recursive: true);
        //    }

        //    Directory.CreateDirectory(testDirectory);

        //    var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd_hh-mm-ss");

        //    var inputFileName = $"file_to_read_{timestamp}.txt";
        //    var inputFilePath = Path.Combine(testDirectory, inputFileName);
        //    var inputContent = $"Random content for {timestamp} - {Guid.NewGuid()}";
        //    await File.WriteAllTextAsync(inputFilePath, inputContent);

        //    var outputFolderName = $"output_folder_to_write_{timestamp}";
        //    var outputFolderPath = Path.Combine(testDirectory, outputFolderName);
        //    Directory.CreateDirectory(outputFolderPath);

        //    var expectedOutputFileName = $"file_to_read_{timestamp}_copilot.txt";
        //    var expectedOutputFilePath = Path.Combine(outputFolderPath, expectedOutputFileName);

        //    var responseValidator = new ReadWriteAgentResponseValidator(inputFilePath, expectedOutputFilePath, inputContent);

        //    await using (var agent = AiAgentsInteractorsFactory
        //        .GetGitHubCopilotAiAgentInteractor()
        //        .WithAgent(Const.AiAgents.Names.READ_WRITE_AGENT)
        //        .WithIsolation()
        //        //.WithSessionConfig(config=> config.WithIn)
        //        .WithWorkingDirectory(testDirectory))
        //    {
        //        var request = new AiInteractionRequestModel
        //        {
        //            Prompt = Const.AiAgents.Prompts.GetReadWriteAgentPrompt(inputFilePath, outputFolderPath)
        //        };

        //        await agent.InvokeAiAgentWithStreamingAsync(
        //            request,
        //            responseValidationFunc: responseValidator.Validate,
        //            maxAttempts: 3);
        //    }

        //    var finalValidation = responseValidator.Validate();
        //    finalValidation.IsSuccessful.Should().BeTrue(finalValidation.Error);

        //    GlobalIoUtils.DeleteDirectory(testDirectory);
        //}

        [Test]
        public void Test1()
        {
            Log.Info("Test1 executed.");
            Console.WriteLine("Test1 executed.");
            true.Should().BeTrue();
        }

        [Test]
        public void Test2()
        {
            Log.Info("Test2 executed.");
            Console.WriteLine("Test2 executed.");
            true.Should().BeFalse();
        }
    }
}
