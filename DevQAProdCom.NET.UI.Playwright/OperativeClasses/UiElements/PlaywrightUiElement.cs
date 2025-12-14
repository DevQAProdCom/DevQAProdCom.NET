using System.Drawing;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Constants;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements
{
    public class PlaywrightUiElement : BaseUiElement
    {
        public PlaywrightUiElement(ILogger log,
            IUiPage uiPage,
            IUiElementInfo info,
            INativeElementsSearcher nativeElementsSearcher,
            IExecuteJavaScript javaScriptExecutor,
            IUiElementBehaviorFactory uiElementBehaviorFactory,
            IUiElementsInstantiator uiElementsInstantiator,
            params KeyValuePair<string, object>[] nativeObjects)
            : base(log, uiPage, info, nativeElementsSearcher, javaScriptExecutor, uiElementBehaviorFactory, uiElementsInstantiator, nativeObjects)
        {

        }

        public ILocator GetLocator()
        {
            UiPage.UiTab.SwitchTo();
            ILocator? locator = GetNativeElementInternal();

            if (locator == null || IsElementStale(locator) == true)
            {
                IUiElementsFindInfo findInfo = Info.InstantiationStage.FindOptions.Last();

                if (findInfo.UiElementType == Shared.Enumerations.UiElementType.Standard)
                    locator = NativeElementsSearcher.FindElement<ILocator, IFrameLocator, ILocator>(Info).NativeElement;
                else if (findInfo.UiElementType == Shared.Enumerations.UiElementType.Frame)
                    locator = NativeElementsSearcher.FindElement<ILocator, IFrameLocator, ILocator>(Info).NativeFrameElement.Owner;
                else if (findInfo.UiElementType == Shared.Enumerations.UiElementType.ShadowRootHost)
                    locator = NativeElementsSearcher.FindElement<ILocator, IFrameLocator, ILocator>(Info).NativeShadowRootHostElement;
                else
                    throw new NotSupportedException($"Search of element of 'UiElementType.{findInfo.UiElementType}' is not supported.");

                NativeObjects.Upsert(SharedUiConstants.NativeElement, locator);
            }

            return locator;
        }

        private ILocator? GetNativeElementInternal()
        {
            NativeObjects.TryGetValue(SharedUiConstants.NativeElement, out object? nativeElement);

            if (nativeElement == null)
                return null;
            else if (nativeElement is ILocator)
                return nativeElement as ILocator;
            else if (nativeElement is IFrameLocator)
                return (nativeElement as IFrameLocator).Owner;
            else throw new Exception(); //TODO
        }

        public IPage? GetPage()
        {
            NativeObjects.TryGetValue(ProjectConst.IPage, out object? IUiPage);
            return IUiPage as IPage;
        }

        public T? GetBrowser<T>() where T : IBrowser
        {
            NativeObjects.TryGetValue(SharedUiConstants.IBrowser, out object? iBrowser);

            if (iBrowser != null)
                return (T)iBrowser;

            return default;
        }

        public IBrowser GetIBrowser()
        {
            return GetBrowser<IBrowser>();
        }

        public T? GetBrowserContext<T>() where T : IBrowserContext
        {
            NativeObjects.TryGetValue(ProjectConst.IBrowserContext, out object? iBrowserContext);

            if (iBrowserContext != null)
                return (T)iBrowserContext;

            return default;
        }

        public IBrowserContext GetIBrowserContext()
        {
            return GetBrowserContext<IBrowserContext>();
        }

        private bool? IsElementStale(ILocator? locator)
        {
            if (locator == null)
                return null;

            try
            {
                string innerHtml = locator.InnerHTMLAsync().Result;
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

        //Calls forwarded to Proxy Element
        #region General Properties

        public override PointF GetLocation()
        {
            LocatorBoundingBoxResult? boundingBox = GetLocator().BoundingBoxAsync().Result;

            if (boundingBox == null)
                throw new ArgumentNullException();

            return new PointF(boundingBox.X, boundingBox.Y);
        }

        public override SizeF GetSize()
        {
            LocatorBoundingBoxResult? boundingBox = GetLocator().BoundingBoxAsync().Result;

            if (boundingBox == null)
                throw new ArgumentNullException();

            return new SizeF(boundingBox.Width, boundingBox.Height);
        }

        public override string GetTagName()
        {
            IElementHandle elementHandle = GetLocator().ElementHandleAsync().Result;
            string tagName = GetLocator().EvaluateAsync<string>("(element) => element.tagName", elementHandle).Result;

            return tagName;
        }

        #endregion General Properties

        #region Specific Properties

        public override string? GetAttribute(string attributeName, bool isBooleanAttributeType)
        {
            string? attribute = GetLocator().GetAttributeAsync(attributeName).Result;

            if (!string.IsNullOrEmpty(attribute))
                return attribute;

            if (isBooleanAttributeType && attribute == string.Empty)
                return "true";

            return null;
        }

        public override string? GetCssValue(string propertyName)
        {
            IElementHandle elementHandle = GetLocator().ElementHandleAsync().Result;
            string? cssValue = GetLocator().EvaluateAsync<string?>($"(element) => window.getComputedStyle(element).getPropertyValue('{propertyName}') || null", elementHandle).Result;

            if (string.IsNullOrEmpty(cssValue))
                return null;

            return cssValue;
        }

        public override string GetTextContent() => GetLocator().TextContentAsync().Result.ToStringEmptyIfNull();

        #endregion Specific Properties

        #region States

        public override bool Exists()
        {
            try
            {
                // Find the element using the specified locator
                ILocator? locator = GetLocator();

                if (locator == null)
                    return false;

                // If the element is found, return true
                return true;
            }
            catch (Exception)
            {
                // If the element is not found, return false
                return false;
            }
        }
        public override bool IsDisabled() => Exists() && GetLocator().IsDisabledAsync().Result;
        public override bool IsDisplayed() => Exists() && GetLocator().IsVisibleAsync().Result;
        public override bool IsEnabled() => Exists() && GetLocator().IsEnabledAsync().Result;

        #endregion States

        #region Execute JavaScript

        protected override KeyValuePair<string, object>[] SupplementJavaScriptExecutorArguments(params KeyValuePair<string, object>[] arguments)
        {
            ILocator locator = GetLocator();
            IElementHandle elementHandle = locator.ElementHandleAsync().Result;
            var uiElementArgument = new KeyValuePair<string, object>(SharedUiConstants.JavaScriptArguments.UiElementArgument, elementHandle);
            return arguments.Insert(0, uiElementArgument);
        }

        #endregion Execute JavaScript

        protected override bool TryReturnResultOrStale<T>(Func<T> func, out T result)
        {
            result = default;

            try
            {
                result = func();
                return true;
            }
            catch (PlaywrightException ex)
            {
                // Check if the exception indicates a stale element
                if (ex.Message.Contains(ProjectConst.PlaywrightStaleElementExceptionMessageContains))
                {
                    _log.Info("Element is stale.");
                    return false;
                }

                throw;
            }
        }
    }
}
