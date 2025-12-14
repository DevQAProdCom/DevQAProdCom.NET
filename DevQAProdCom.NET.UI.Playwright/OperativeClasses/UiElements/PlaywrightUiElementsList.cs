using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements
{
    public class PlaywrightUiElementsList<TUiElement> : BaseUiElementsList<TUiElement> where TUiElement : IUiElement
    {
        public PlaywrightUiElementsList(ILogger log, IUiPage uiPage, IUiElementInfo info, INativeElementsSearcher nativeElementsSearcher, IExecuteJavaScript javaScriptExecutor,
            IUiElementsInstantiator uiElementsInstantiator, params KeyValuePair<string, object>[] nativeObjects)
            : base(log, uiPage, info, nativeElementsSearcher, javaScriptExecutor, uiElementsInstantiator, nativeObjects)
        {

        }

        public PlaywrightUiElementsList(ILogger log, IUiPage uiPage, List<TUiElement> uiElements, IUiElementInfo info, INativeElementsSearcher nativeElementsSearcher, IExecuteJavaScript javaScriptExecutor,
            IUiElementsInstantiator uiElementsInstantiator, params KeyValuePair<string, object>[] nativeObjects)
            : base(log, uiPage, uiElements, info, nativeElementsSearcher, javaScriptExecutor, uiElementsInstantiator, nativeObjects)
        {

        }

        private bool? HasStaleElements(List<TUiElement>? uiElements)
        {
            if (uiElements == null)
                return null;

            try
            {
                foreach (var uiElement in uiElements)
                {
                    string tagName = uiElement.GetTagName();
                }
            }
            catch (PlaywrightException ex)
            {
                // Check if the exception indicates a stale element
                if (ex.Message.Contains("ElementHandle is either not visible or not an HTMLElement"))
                {
                    _log.Info("Element is stale.");
                    return true;
                }
                else
                {
                    // Handle other exceptions if needed
                    _log.Info("An error occurred while interacting with the element.");
                }
            }

            return false;
        }

        public override IEnumerable<TResult> Select<TResult>(Func<TUiElement, TResult> selector, bool reFindItems = true)
        {
            try
            {
                var items = GetUiElementItems(reFindItems);
                return items.Select(selector);
            }
            catch (Exception ex) when ((ex is PlaywrightException && IsStaleElementException(ex as PlaywrightException)) || ex is UiElementStaleReferenceException)
            {
                var items = GetUiElementItems(reFindItems: true);
                return items.Select(selector);
            }
        }

        public override IEnumerable<TResult> SelectMany<TResult>(Func<TUiElement, IEnumerable<TResult>> selector, bool reFindItems = true)
        {
            try
            {
                var items = GetUiElementItems(reFindItems);
                return items.SelectMany(selector);
            }
            catch (Exception ex) when ((ex is PlaywrightException && IsStaleElementException(ex as PlaywrightException)) || ex is UiElementStaleReferenceException)
            {
                var items = GetUiElementItems(reFindItems: true);
                return items.SelectMany(selector);
            }
        }

        public override List<TUiElement> GetUiElementItems(bool reFindItems = true)
        {
            UiPage.UiTab.SwitchTo();
            if (reFindItems || UiElementItems == null || HasStaleElements(UiElementItems) == true)
            {
                List<IFindResult<ILocator, IFrameLocator, ILocator>> nativeItemsList = NativeElementsSearcher.FindElements<ILocator, IFrameLocator, ILocator>(Info).ToList();
                UiElementItems = UiElementsInstantiator.InitializeUiElementsList<TUiElement, ILocator, IFrameLocator, ILocator>(Info, nativeItemsList);
            }

            if (UiElementItems == null)
                throw new ArgumentNullException();

            return UiElementItems;
        }

        private bool IsStaleElementException(PlaywrightException ex)
        {
            // Check if the exception indicates a stale element
            if (ex.Message.Contains(ProjectConst.PlaywrightStaleElementExceptionMessageContains))
            {
                _log.Info("Element is stale.");
                return true;
            }
            return false;
        }
    }
}
