using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses
{
    public class PlaywrightJavaScriptExecutor : BaseJavaScriptExecutor
    {
        private readonly IPage _page;

        public PlaywrightJavaScriptExecutor(IPage page)
        {
            _page = page;
        }

        public override void ExecuteJavaScript(string script, params KeyValuePair<string, object>[] arguments)
        {
            EvaluateJavaScript(script, arguments);
        }

        public override T ExecuteJavaScript<T>(string script, params KeyValuePair<string, object>[] arguments)
        {
            return EvaluateJavaScript<T>(script, arguments);
        }

        private void EvaluateJavaScript(string script, params KeyValuePair<string, object>[] arguments)
        {
            object[] processedArguments = ProcessArguments(arguments);
            string playwrightFormattedScript = GetScriptString(script, arguments);

            try
            {
                _ = _page.EvaluateAsync(playwrightFormattedScript, processedArguments).Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to execute JavaScript: \n{script}\n", ex);
            }
        }

        private T EvaluateJavaScript<T>(string script, params KeyValuePair<string, object>[] arguments)
        {
            object[] processedArguments = ProcessArguments(arguments);
            string playwrightFormattedScript = GetScriptString(script, arguments);
            T entity = _page.EvaluateAsync<T>(playwrightFormattedScript, processedArguments).Result;
            return entity;
        }

        private string GetScriptString(string script, params KeyValuePair<string, object>[] arguments)
        {
            return $"([ {string.Join(",", arguments.Select(x => x.Key))} ]) => {{ {script} }}";
        }

        private object[] ProcessArguments(KeyValuePair<string, object>[] arguments)
        {
            List<object> processedArguments = new();

            foreach (var argument in arguments)
            {
                PlaywrightUiElement? playwrightUiElement = null;
                UiElement parentUiElement = argument.Value as UiElement;

                if (parentUiElement != null)
                    playwrightUiElement = parentUiElement.InternalUiElement as PlaywrightUiElement;
                playwrightUiElement = argument.Value as PlaywrightUiElement;

                if (playwrightUiElement != null)
                {
                    ILocator locator = playwrightUiElement.GetLocator();
                    IElementHandle elementHandle = locator.ElementHandleAsync().Result;
                    processedArguments.Add(elementHandle);
                    continue;
                }

                processedArguments.Add(argument.Value);
            }

            return processedArguments.ToArray();
        }
    }
}
