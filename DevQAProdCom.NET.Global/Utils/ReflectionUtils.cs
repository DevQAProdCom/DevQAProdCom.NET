using System.Reflection;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.Global.Utils
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Dynamically finds a class, creates an instance, parses string arguments, and invokes the specified method.
        /// </summary>
        /// <param name="className">The full or relative name of the class.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="args">An array of string arguments from the command line.</param>
        /// <param name="logger">An optional logger instance for logging messages.</param>
        public static void InvokeMethodWithArgs(string className, string methodName, List<string>? args = null, ILogger? logger = null)
        {
            try
            {
                // 1. Find the type in the current assembly (first by full name, then by short name)
                Type? type = Type.GetType(className) ??
                             Assembly.GetEntryAssembly()?.GetType(className) ??
                             Assembly.GetExecutingAssembly().GetType(className);

                // If not found by full name, search by short class name in the entry and executing assemblies
                type ??= Assembly.GetEntryAssembly()?.GetTypes().FirstOrDefault(t => t.Name == className);
                type ??= Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == className);

                if (type == null)
                {
                    var message = $"Error: Class '{className}' not found in the current assembly.";
                    if (logger != null)
                    {
                        logger.Error(message);
                    }
                    else
                    {
                        Console.WriteLine(message);
                    }
                    return;
                }

                // 2. Find the method in the class
                MethodInfo? method = type.GetMethod(methodName);
                if (method == null)
                {
                    var message = $"Error: Method '{methodName}' not found in class '{className}'.";
                    if (logger != null)
                    {
                        logger.Error(message);
                    }
                    else
                    {
                        Console.WriteLine(message);
                    }
                    return;
                }

                // 3. Create an instance of the class (requires a parameterless constructor)
                object? instance = Activator.CreateInstance(type);

                // 4. Convert string CLI arguments to the formal parameter types of the method
                ParameterInfo[] formalParams = method.GetParameters();
                object?[] convertedArgs = new object?[formalParams.Length];

                for (int i = 0; i < formalParams.Length; i++)
                {
                    if (args != null && i < args.Count)
                    {
                        // Convert string to the required type (int, bool, double, etc.)
                        convertedArgs[i] = Convert.ChangeType(args[i], formalParams[i].ParameterType);
                    }
                    else
                    {
                        // If fewer CLI arguments are provided, use the default value if available
                        convertedArgs[i] = formalParams[i].HasDefaultValue ? formalParams[i].DefaultValue : null;
                    }
                }

                // 5. Invoke the method
                var invokeMessage = $" Invoking method: {type.Name}.{method.Name}...";
                if (logger != null)
                {
                    logger.Info(invokeMessage);
                }
                else
                {
                    Console.WriteLine(invokeMessage);
                }
                method.Invoke(instance, convertedArgs);
            }
            catch (TargetInvocationException ex)
            {
                // Handles an error that occurred inside the invoked method itself
                var message = $" Method execution error: {ex.InnerException?.Message ?? ex.Message}";
                if (logger != null)
                {
                    logger.Error(message);
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                // Handles reflection errors or type parsing errors
                var message = $" Reflection error: {ex.Message}";
                if (logger != null)
                    logger.Error(message);
                else
                    Console.WriteLine(message);
            }
        }
    }
}
