using System.Text.Json;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses
{
    public class SeleniumJavaScriptExecutor : BaseJavaScriptExecutor
    {
        private readonly IWebDriver _driver;
        private IJavaScriptExecutor _javaScriptExecutor;

        public SeleniumJavaScriptExecutor(IWebDriver driver)
        {
            _driver = driver;
            _javaScriptExecutor = (IJavaScriptExecutor)_driver;
        }

        /// <summary>
        /// Executes the given JavaScript code.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="arguments">The arguments to pass to the script.</param>
        public override void ExecuteJavaScript(string script, params KeyValuePair<string, object>[] arguments)
        {
            ExecuteJavaScriptPrivate(script, arguments);
        }

        /// <summary>
        /// Executes the given JavaScript code and returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="arguments">The arguments to pass to the script.</param>
        /// <returns>The result of the script execution.</returns>
        public override T ExecuteJavaScript<T>(string script, params KeyValuePair<string, object>[] arguments)
        {
            object result = ExecuteJavaScriptPrivate(script, arguments);
            return Deserialize<T>(result);
        }

        /// <summary>
        /// Executes the given JavaScript code privately.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="arguments">The arguments to pass to the script.</param>
        /// <returns>The result of the script execution.</returns>
        private object ExecuteJavaScriptPrivate(string script, params KeyValuePair<string, object>[] arguments)
        {
            object[] processedArguments = ProcessArguments(arguments);
            string seleniumFormattedScript = GetScriptString(script, arguments);
            object result = null;

            try
            {
                result = _javaScriptExecutor.ExecuteScript(seleniumFormattedScript, processedArguments);
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new InvalidOperationException("Error executing JavaScript", ex);
            }

            return result;
        }

        /// <summary>
        /// Formats the script string by replacing parameter keys with argument placeholders.
        /// </summary>
        /// <param name="script">The script string to format.</param>
        /// <param name="arguments">The arguments to replace in the script.</param>
        /// <returns>The formatted script string.</returns>
        private string GetScriptString(string script, params KeyValuePair<string, object>[] arguments)
        {
            int i = 0;

            foreach (var parameter in arguments)
            {
                script = script.Replace(parameter.Key, $"arguments[{i}]");
                i++;
            }

            return script;
        }

        /// <summary>
        /// Deserializes the given object to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="object">The object to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        private T Deserialize<T>(object @object)
        {
            string serializedResultObject = JsonSerializer.Serialize(@object);
            T deserializedResultObject = JsonSerializer.Deserialize<T>(serializedResultObject);
            return deserializedResultObject;
        }

        /// <summary>
        /// Processes the arguments to be passed to the JavaScript code.
        /// </summary>
        /// <param name="arguments">The arguments to process.</param>
        /// <returns>The processed arguments.</returns>
        private object[] ProcessArguments(KeyValuePair<string, object>[] arguments)
        {
            List<object> processedArguments = new();

            foreach (var argument in arguments)
            {
                SeleniumUiElement? seleniumUiElement = null;
                UiElement parentUiElement = argument.Value as UiElement;

                if (parentUiElement != null)
                    seleniumUiElement = parentUiElement.InternalUiElement as SeleniumUiElement;
                seleniumUiElement = argument.Value as SeleniumUiElement;

                if (seleniumUiElement != null)
                {
                    IWebElement webElement = seleniumUiElement.GetWebElement();
                    processedArguments.Add(webElement);
                    continue;
                }

                processedArguments.Add(argument.Value);
            }

            return processedArguments.ToArray();
        }
    }
}
