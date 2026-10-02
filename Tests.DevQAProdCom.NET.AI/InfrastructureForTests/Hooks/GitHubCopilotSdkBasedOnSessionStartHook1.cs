using System.IO;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Hooks;
using DevQAProdCom.NET.Global.Utils;
using GitHub.Copilot;

namespace Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Hooks
{
    public class GitHubCopilotSdkBasedOnSessionStartHook1 : GitHubCopilotSdkBasedOnSessionStartHook
    {
        public override string? Identifier { get; set; } = $"{nameof(GitHubCopilotSdkBasedOnSessionStartHook1)}";

        public override Func<SessionStartHookInput, HookInvocation, Task<SessionStartHookOutput?>>? GetOnSessionStartHook()
        {
            return async (input, invocation) =>
            {
                var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                var content = $"{nameof(GitHubCopilotSdkBasedOnSessionStartHook1)} {timestamp}";
                var outputDirectory = input.WorkingDirectory ?? Directory.GetCurrentDirectory();
                IoUtils.WriteAllText(Path.Combine(outputDirectory, $"{nameof(GitHubCopilotSdkBasedOnSessionStartHook1)}.txt"), content);
                return await Task.FromResult<SessionStartHookOutput?>(null);
            };
        }
    }
}
