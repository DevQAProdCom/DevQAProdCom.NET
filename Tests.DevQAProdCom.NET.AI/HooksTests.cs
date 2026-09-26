using System.Globalization;
using System.Text.RegularExpressions;
using DevQAProdCom.NET.Global.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class HooksTests : BaseTest
    {
        [Test]
        public async Task Should_FileHook_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier()
        {
            var testStartTime = DateTime.UtcNow;
            var workingDirectory = PrepareTempTestWorkingDirectory(nameof(Should_FileHook_Be_Used_Using_Agent_Custom_Metadata_Hooks_Field_By_Identifier));
            var expectedFilePath = Path.Combine(workingDirectory, "session-start-hook-1.txt");

            await using (var agent = GetGitHubCopilotAiAgentInteractor()
                .WithDefaultContentHandlers()
                .WithPrimaryAgent("hooks-check-agent")
                .WithWorkingDirectory(workingDirectory)
                .WithPrompt("Hello"))
            {
                var act = async () => await agent.InvokeAiAgentWithStreamingAsync();
                await act.Should().NotThrowAsync();
            }

            IoUtils.CheckFileMustExist(expectedFilePath);

            var content = (await File.ReadAllTextAsync(expectedFilePath)).Trim();
            var match = Regex.Match(content, @"^session-start-hook-1 (\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{6})$");
            match.Success.Should().BeTrue();

            var parsedDateTime = DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
            parsedDateTime.Should().BeAfter(testStartTime);

            IoUtils.DeleteDirectory(workingDirectory);
        }
    }
}
