using DevQAProdCom.NET.Global.Utils;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(args.Length > 0 ? $"Application Started with arguments: {string.Join(", ", args)}" : "Application Started with no arguments");

            if (args.Length < 2)
            {
                Console.WriteLine("Usage: dotnet run -- <ClassName> <MethodName> [Parameters...]");
                return;
            }

            string className = args[0];
            string methodName = args[1];

            List<string> methodArgs = args.Skip(2).ToList();

            await ReflectionUtils.InvokeMethodWithArgsAsync(className, methodName, args: methodArgs, logger: DiContainer.Instance.Log);
        }
    }
}
