using DevQAProdCom.NET.Global.Utils;
using Tests.DevQAProdCom.NET.AI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.AI
{
#if AS_APP
    internal class Program
    {
        static void Main(string[] args)
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
            ReflectionUtils.InvokeMethodWithArgs(className, methodName, args: methodArgs, logger: DiContainer.Instance.Log);
        }
    }
#else
    // Dummy class to keep IntelliSense happy in test mode
    internal class Program
    {
        // This is never compiled as executable - only for IDE support
        static void Main_NotUsed(string[] args)
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

            ReflectionUtils.InvokeMethodWithArgs(className, methodName, args: methodArgs, logger: DiContainer.Instance.Log);
        }
    }
#endif
}
