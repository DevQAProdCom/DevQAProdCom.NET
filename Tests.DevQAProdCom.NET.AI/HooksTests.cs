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
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Constants;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Hooks;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class HooksTests : BaseTest
    {
        private const string TimestampFormat = "yyyy-MM-dd HH:mm:ss.ffffff";
        private const string TimestampRegexPattern = @"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{6}";

        private const string FileBasedHookIdentifier = "on-session-start-hook-1";
        private const string FileBasedHookOutputFileName = "on-session-start-hook-1.txt";

        [Test]
        public async Task Should_FileBasedHook_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier()
        {
            //SETUP
            CheckGitHubCopilotAuthenticationSetup();

            //GIVEN
            var testStartTime = DateTime.UtcNow;
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_FileBasedHook_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier));
            var expectedFilePath = Path.Combine(tempWorkingDirectory, FileBasedHookOutputFileName);

            //WHEN
            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    hooksDefaultLocationsProvider: CreateFileBasedHooksDefaultLocationsProvider())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(Const.AiAgents.Names.CHECK_CUSTOM_FILE_BASED_HOOKS_FIELD_WITH_FILE_BASED_HOOK_AGENT)
                .WithWorkingDirectory(tempWorkingDirectory)
                .WithPrompt("Invoke agent"))
            {
                await InvokeAgentWithoutExceptionAsync(agent);
            }

            //THEN
            await AssertHookOutputFileAsync(expectedFilePath, FileBasedHookIdentifier, testStartTime);

            //TEAR DOWN
            CleanUpTempWorkingDirectory(tempWorkingDirectory);
        }

        [Test]
        public async Task Should_SdkBasedHook_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier()
        {
            //SETUP
            CheckGitHubCopilotAuthenticationSetup();

            //GIVEN
            var testStartTime = DateTime.UtcNow;
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_SdkBasedHook_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier));
            var expectedFilePath = Path.Combine(tempWorkingDirectory, $"{nameof(GitHubCopilotSdkBasedOnSessionStartHook1)}.txt");

            //WHEN
            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    allSdkBasedSessionHooksCollection: CreateSdkBasedSessionHooksCollection())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(Const.AiAgents.Names.CHECK_CUSTOM_HOOKS_FIELD_WITH_SDK_BASED_HOOK_AGENT)
                .WithWorkingDirectory(tempWorkingDirectory)
                .WithPrompt("Invoke agent"))
            {
                await InvokeAgentWithoutExceptionAsync(agent);
            }

            //THEN
            await AssertHookOutputFileAsync(expectedFilePath, nameof(GitHubCopilotSdkBasedOnSessionStartHook1), testStartTime);

            //TEAR DOWN
            CleanUpTempWorkingDirectory(tempWorkingDirectory);
        }

        [Test]
        public async Task Should_FileBased_And_SdkBased_Hooks_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifiers()
        {
            //SETUP
            CheckGitHubCopilotAuthenticationSetup();

            //GIVEN
            var testStartTime = DateTime.UtcNow;
            var tempWorkingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_FileBased_And_SdkBased_Hooks_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifiers));
            var expectedFileBasedHookFilePath = Path.Combine(tempWorkingDirectory, FileBasedHookOutputFileName);
            var expectedSdkBasedHookFilePath = Path.Combine(tempWorkingDirectory, $"{nameof(GitHubCopilotSdkBasedOnSessionStartHook1)}.txt");


            //WHEN
            await using (var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor(
                    hooksDefaultLocationsProvider: CreateFileBasedHooksDefaultLocationsProvider(),
                    allSdkBasedSessionHooksCollection: CreateSdkBasedSessionHooksCollection())
                .WithDefaultContentHandlers()
                .WithPrimaryAgent(Const.AiAgents.Names.CHECK_CUSTOM_HOOKS_FIELD_WITH_FILE_AND_SDK_BASED_HOOKS_AGENT)
                .WithWorkingDirectory(tempWorkingDirectory)
                .WithPrompt("Invoke agent"))
            {
                await InvokeAgentWithoutExceptionAsync(agent);
            }

            //THEN
            await AssertHookOutputFileAsync(expectedFileBasedHookFilePath, FileBasedHookIdentifier, testStartTime);
            await AssertHookOutputFileAsync(expectedSdkBasedHookFilePath, nameof(GitHubCopilotSdkBasedOnSessionStartHook1), testStartTime);

            //TEAR DOWN
            CleanUpTempWorkingDirectory(tempWorkingDirectory);
        }

        private string GetHookContentRegex(string prefix) => $"^{Regex.Escape(prefix)} ({TimestampRegexPattern})$";

        private DateTime ParseTimestamp(string timestamp) =>
            DateTime.ParseExact(timestamp, TimestampFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

        private IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook> CreateSdkBasedSessionHooksCollection()
        {
            var collection = new IdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>(Log);
            collection.Add(new GitHubCopilotSdkBasedOnSessionStartHook1());
            return collection;
        }

        private ILocationsProvider CreateFileBasedHooksDefaultLocationsProvider() =>
            new GitHubCopilotFileBasedHooksDefaultLocationsProvider(useExtendedSearch: false);

        private async Task InvokeAgentWithoutExceptionAsync(GitHubCopilotAiAgentInteractor agent)
        {
            var act = async () => await agent.InvokeAiAgentWithStreamingAsync();
            await act.Should().NotThrowAsync();
        }

        private async Task AssertHookOutputFileAsync(string filePath, string expectedPrefix, DateTime testStartTime)
        {
            IoUtils.CheckFileMustExist(filePath);

            var content = (await File.ReadAllTextAsync(filePath)).Trim();
            var match = Regex.Match(content, GetHookContentRegex(expectedPrefix));
            match.Success.Should().BeTrue();

            var parsedDateTime = ParseTimestamp(match.Groups[1].Value);
            parsedDateTime.Should().BeAfter(testStartTime);
        }
    }
}
