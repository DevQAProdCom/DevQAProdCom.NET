using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.UiElements.Interfaces;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements
{
    public class PlaywrightUiElementSelect : UiElement, ISelect
    {
        private IElementHandle _elementHandle
        {
            get
            {
                var playwrightUiElement = (PlaywrightUiElement)InternalUiElement;
                return playwrightUiElement.GetLocator().ElementHandleAsync().Result;
            }
        }

        public void Select(string value)
        {
            _elementHandle.SelectOptionAsync(new[] { new SelectOptionValue { Label = value } }).Wait();
        }

        public string GetSelectedValue()
        {
            //var selectedOptions = _elementHandle.EvaluateAsync<string[]>("el => Array.from(el.selectedOptions).map(o => o.text)").Result;
            var selectedOptions = ExecuteJavaScript<string[]>("return Array.from(uiElementArgument.selectedOptions).map(o => o.text)");
            return selectedOptions.FirstOrDefault() ?? string.Empty;
        }
    }
}
