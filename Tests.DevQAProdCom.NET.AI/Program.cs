using DevQAProdCom.NET.Global.Utils;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            ReflectionUtils.InvokeMethodWithArgs(className, methodName, args: methodArgs);
        }
    }
}
