using System.Text;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Utils;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine(args.Length > 0 ? $"Application Started with arguments: {string.Join(", ", args)}" : "Application Started with no arguments");

            var arguments = ParseArguments(args);

            arguments.TryGetValue("className", out var className);
            arguments.TryGetValue("methodName", out var methodName);

            if (!string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(methodName))
            {
                await ReflectionUtils.InvokeMethodWithArgsAsync(className, methodName, args: new List<string>(), logger: DiContainer.Instance.Log);
                return;
            }

            arguments.TryGetValue("agentName", out var agentName);
            arguments.TryGetValue("configurationIdentifier", out var configurationIdentifier);
            arguments.TryGetValue("prompt", out var prompt);

            if (!string.IsNullOrEmpty(agentName) || !string.IsNullOrEmpty(configurationIdentifier))
            {
                var workingDirectory = Path.Combine(Path.GetTempPath(), "AiAgentTests", DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds());
                IoUtils.CreateDirectory(workingDirectory);

                var agentBuilder = DiContainer.Instance.MicrosoftAiAgentsInteractorsFactory
                    .GetGitHubCopilotAiAgentInteractor()
                    .WithDefaultContentHandlers()
                    .WithSelectiveIsolation()
                    .WithWorkingDirectory(workingDirectory);

                if (!string.IsNullOrEmpty(agentName) && !string.IsNullOrEmpty(configurationIdentifier))
                {
                    agentBuilder.WithPrimaryAgent(agentName, configurationIdentifier);
                }
                else if (!string.IsNullOrEmpty(agentName))
                {
                    agentBuilder.WithPrimaryAgent(agentName);
                }
                else
                {
                    agentBuilder.WithSessionConfig(builder => builder.WithPrimaryAgentByConfiguration(configurationIdentifier!));
                }

                if (!string.IsNullOrEmpty(prompt))
                {
                    agentBuilder.WithPrompt(prompt);
                }

                await using (var agent = agentBuilder)
                {
                    await agent.InvokeAiAgentWithStreamingAsync();
                }

                return;
            }

            Console.WriteLine("Usage: dotnet run -- --className <ClassName> --methodName <MethodName>");
            Console.WriteLine("       dotnet run -- --agentName <AgentName> [--configurationIdentifier <ConfigurationIdentifier>] [--prompt <Prompt>]");
            Console.WriteLine("       dotnet run -- --configurationIdentifier <ConfigurationIdentifier> [--prompt <Prompt>]");
        }

        private static Dictionary<string, string> ParseArguments(string[] args)
        {
            var arguments = new Dictionary<string, string>();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("--") && i + 1 < args.Length)
                {
                    arguments[args[i].Substring(2)] = args[++i];
                }
            }

            return arguments;
        }
    }
}
