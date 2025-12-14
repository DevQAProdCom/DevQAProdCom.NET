using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements
{
    public class SeleniumUiElementsList<TUiElement> : BaseUiElementsList<TUiElement> where TUiElement : IUiElement //was internal
    {
        public SeleniumUiElementsList(ILogger log, IUiPage uiPage, IUiElementInfo info, INativeElementsSearcher nativeElementsSearcher, IExecuteJavaScript javaScriptExecutor,
            IUiElementsInstantiator uiElementsInstantiator, params KeyValuePair<string, object>[] nativeObjects)
            : base(log, uiPage, info, nativeElementsSearcher, javaScriptExecutor, uiElementsInstantiator, nativeObjects)
        {

        }

        public SeleniumUiElementsList(ILogger log, IUiPage uiPage, List<TUiElement> uiElements, IUiElementInfo info, INativeElementsSearcher nativeElementsSearcher,
            IExecuteJavaScript javaScriptExecutor, IUiElementsInstantiator uiElementsInstantiator, params KeyValuePair<string, object>[] nativeObjects)
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
                    var tagName = uiElement.GetTagName();
                }
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }

            return false;
        }

        public override List<TUiElement> GetUiElementItems(bool reFindItems = true)
        {
            UiPage.UiTab.SwitchTo();
            if (reFindItems || UiElementItems == null || HasStaleElements(UiElementItems) == true)
            {
                LogUiElementSearchOperation();
                var nativeItemsList = NativeElementsSearcher.FindElements<IWebElement, IWebElement, IWebElement>(Info)
                .Where(x => x != null)
                .ToList();
                UiElementItems = UiElementsInstantiator.InitializeUiElementsList<TUiElement, IWebElement, IWebElement, IWebElement>(Info, nativeItemsList);
            }
            if (UiElementItems == null)
                throw new ArgumentNullException();

            return UiElementItems;
        }

        public override IEnumerable<TResult> Select<TResult>(Func<TUiElement, TResult> selector, bool reFindItems = true)
        {
            try
            {
                var items = GetUiElementItems(reFindItems);
                return items.Select(selector).ToList(); //should be ToList() - to be able to catch exception, otherwise Select(selector) will executed outside of this code due to IEnumerable lazy loading
            }
            catch (Exception ex) when (ex is StaleElementReferenceException || ex is UiElementStaleReferenceException)
            {
                var items = GetUiElementItems(reFindItems: true);
                return items.Select(selector).ToList();
            }
        }

        public override IEnumerable<TResult> SelectMany<TResult>(Func<TUiElement, IEnumerable<TResult>> selector, bool reFindItems = true)
        {
            try
            {
                var items = GetUiElementItems(reFindItems);
                return items.SelectMany(selector).ToList(); //should be ToList() - to be able to catch exception, otherwise Select(selector) will executed outside of this code due to IEnumerable lazy loading
            }
            catch (Exception ex) when (ex is StaleElementReferenceException || ex is UiElementStaleReferenceException)
            {
                var items = GetUiElementItems(reFindItems);
                return items.SelectMany(selector).ToList();
            }
        }

        private void LogUiElementSearchOperation()
        {
            var currentPageUri = new Uri(UiPage.UiTab.GetTabUriAsString());
            _log.Verbose("Operation: {Operation}. Page Absolute Uri: {PageAbsoluteUri}. Page Host: {PageHost}. Page Absolute Path {PageAbsolutePath}. UiElement Name: {UiElementName}. UiElement FullName: {UiElementFullName}.",
                "UiElementSearch",
                currentPageUri.AbsoluteUri,
                currentPageUri.Host,
                currentPageUri.AbsolutePath,
                Info.InstantiationStage.Name,
                Info.InstantiationStage.FullName);
        }
    }
}
