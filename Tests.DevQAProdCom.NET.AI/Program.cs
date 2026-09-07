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

            if (args.Length < 2)
            {
                Console.WriteLine("Usage: dotnet run -- <ClassName> <MethodName> [Parameters...]");
                return;
            }

            if (args[0].Contains("agent"))
            {
                //Add ifs for different AI Providers - add to DI
                var agentName = args[1];
                var prompt = args.Skip(2).ToList();

                var workingDirectory = Path.Combine(Path.GetTempPath(), "AiAgentTests", DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds());
               IoUtils.CreateDirectory(workingDirectory);

                await using (var agent = DiContainer.Instance.MicrosoftAiAgentsInteractorsFactory
                    .GetGitHubCopilotAiAgentInteractor()
                    .WithDefaultContentHandlers()
                    .WithSelectiveIsolation()
                    .WithWorkingDirectory(workingDirectory)
                    .WithPrimaryAgent(agentName)
                    .WithPrompt(string.Join(" ", prompt)))
                {
                    await agent.InvokeAiAgentWithStreamingAsync();
                }
            }
            else
            {
                string className = args[0];
                string methodName = args[1];

                List<string> methodArgs = args.Skip(2).ToList();
                await ReflectionUtils.InvokeMethodWithArgsAsync(className, methodName, args: methodArgs, logger: DiContainer.Instance.Log);
            }
        }
    }
}
