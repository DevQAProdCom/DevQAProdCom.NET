using DevQAProdCom.NET.Global.Utils;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class Program
    {
        // Method name is replaced during build when AsApp=true
        // In test mode: Main_ForApp_With_This_Long_Name_Is_Changed_To_Just_Main_During_Build_AsApp (not an entry point)
        // In app mode: Main (becomes entry point)
        static async Task Main_ForApp_With_This_Long_Name_Is_Changed_To_Just_Main_During_Build_AsApp(string[] args)
        {
            Console.WriteLine(args.Length > 0 ? $"Application Started with arguments: {string.Join(", ", args)}" : "Application Started with no arguments");

            // Expecting at least: class_name method_name
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: dotnet run -- <ClassName> <MethodName> [Parameters...]");
                return;
            }

            string className = args[0];
            string methodName = args[1];

            // Separate method arguments from the class and method names
            List<string> methodArgs = args.Skip(2).ToList();

            // Pass everything to the utility class
            await ReflectionUtils.InvokeMethodWithArgsAsync(className, methodName, args: methodArgs, logger: DiContainer.Instance.Log);
        }
    }
}
