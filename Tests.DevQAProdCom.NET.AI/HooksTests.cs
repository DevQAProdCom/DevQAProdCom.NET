using System.Globalization;
using System.Text.RegularExpressions;
using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Global.Utils;
using FluentAssertions;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Hooks;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class HooksTests : BaseTest
    {
        private const string TimestampFormat = "yyyy-MM-dd HH:mm:ss.ffffff";
        private const string TimestampRegexPattern = @"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{6}";

        private const string FileBasedHookAgentName = "check-custom-hooks-field-with-file-based-hook-agent";
        private const string SdkBasedHookAgentName = "check-custom-hooks-field-with-sdk-based-hook-agent";
        private const string FileAndSdkBasedHooksAgentName = "check-custom-hooks-field-with-file-and-sdk-based-hooks-agent";

        private const string FileBasedHookIdentifier = "on-session-start-hook-1";
        private const string FileBasedHookOutputFileName = "on-session-start-hook-1.txt";

        private static string GetHookContentRegex(string prefix) => $"^{Regex.Escape(prefix)} ({TimestampRegexPattern})$";

        private static DateTime ParseTimestamp(string timestamp) =>
            DateTime.ParseExact(timestamp, TimestampFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

        private ISdkEntitiesCollection<IGitHubCopilotSdkBasedSessionHook> CreateSdkBasedSessionHooksCollection()
        {
            var collection = new SdkEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>(Log);
            collection.Add(new GitHubCopilotSdkBasedOnSessionStartHook1());
            return collection;
        }

        private static ILocationsProvider CreateFileBasedHooksDefaultLocationsProvider() =>
            new GitHubCopilotFileBasedHooksDefaultLocationsProvider(useExtendedSearch: false);

        private static async Task InvokeAgentWithoutExceptionAsync(GitHubCopilotAiAgentInteractor agent)
        {
            var act = async () => await agent.InvokeAiAgentWithStreamingAsync();
            await act.Should().NotThrowAsync();
        }

        private static async Task AssertHookOutputFileAsync(string filePath, string expectedPrefix, DateTime testStartTime)
        {
            IoUtils.CheckFileMustExist(filePath);

            var content = (await File.ReadAllTextAsync(filePath)).Trim();
            var match = Regex.Match(content, GetHookContentRegex(expectedPrefix));
            match.Success.Should().BeTrue();

            var parsedDateTime = ParseTimestamp(match.Groups[1].Value);
            parsedDateTime.Should().BeAfter(testStartTime);
        }

        [Test]
        public async Task Should_FileBasedHook_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier()
        {
            var testStartTime = DateTime.UtcNow;
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_FileBasedHook_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier));
            var expectedFilePath = Path.Combine(workingDirectory, FileBasedHookOutputFileName);

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    hooksDefaultLocationsProvider: CreateFileBasedHooksDefaultLocationsProvider())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(FileBasedHookAgentName)
                .WithWorkingDirectory(workingDirectory)
                .WithPrompt("Invoke agent"))
            {
                await InvokeAgentWithoutExceptionAsync(agent);
            }

            await AssertHookOutputFileAsync(expectedFilePath, FileBasedHookIdentifier, testStartTime);

            IoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task Should_SdkBasedHook_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier()
        {
            var testStartTime = DateTime.UtcNow;
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_SdkBasedHook_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier));
            var expectedFilePath = Path.Combine(workingDirectory, $"{nameof(GitHubCopilotSdkBasedOnSessionStartHook1)}.txt");

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    allSdkBasedSessionHooksCollection: CreateSdkBasedSessionHooksCollection())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(SdkBasedHookAgentName)
                .WithWorkingDirectory(workingDirectory)
                .WithPrompt("Invoke agent"))
            {
                await InvokeAgentWithoutExceptionAsync(agent);
            }

            await AssertHookOutputFileAsync(expectedFilePath, nameof(GitHubCopilotSdkBasedOnSessionStartHook1), testStartTime);

            IoUtils.DeleteDirectory(workingDirectory);
        }

        [Test]
        public async Task Should_FileBased_And_SdkBased_Hooks_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifiers()
        {
            var testStartTime = DateTime.UtcNow;
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_FileBased_And_SdkBased_Hooks_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifiers));
            var expectedFileBasedHookFilePath = Path.Combine(workingDirectory, FileBasedHookOutputFileName);
            var expectedSdkBasedHookFilePath = Path.Combine(workingDirectory, $"{nameof(GitHubCopilotSdkBasedOnSessionStartHook1)}.txt");

            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    hooksDefaultLocationsProvider: CreateFileBasedHooksDefaultLocationsProvider(),
                    allSdkBasedSessionHooksCollection: CreateSdkBasedSessionHooksCollection())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(FileAndSdkBasedHooksAgentName)
                .WithWorkingDirectory(workingDirectory)
                .WithPrompt("Invoke agent"))
            {
                await InvokeAgentWithoutExceptionAsync(agent);
            }

            await AssertHookOutputFileAsync(expectedFileBasedHookFilePath, FileBasedHookIdentifier, testStartTime);
            await AssertHookOutputFileAsync(expectedSdkBasedHookFilePath, nameof(GitHubCopilotSdkBasedOnSessionStartHook1), testStartTime);

            IoUtils.DeleteDirectory(workingDirectory);
        }
    }
}
