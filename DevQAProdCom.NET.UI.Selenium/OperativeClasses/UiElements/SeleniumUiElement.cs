using System.Drawing;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Selenium.Constants;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements
{
    public class SeleniumUiElement : BaseUiElement
    {
        public SeleniumUiElement(ILogger log, IUiPage uiPage, IUiElementInfo info,
            INativeElementsSearcher nativeElementsSearcher,
            IExecuteJavaScript javaScriptExecutor,
            IUiElementBehaviorFactory uiElementBehaviorFactory,
            IUiElementsInstantiator uiElementsInstantiator,
            params KeyValuePair<string, object>[] nativeObjects)
            : base(log, uiPage, info, nativeElementsSearcher, javaScriptExecutor, uiElementBehaviorFactory, uiElementsInstantiator, nativeObjects)
        {
        }

        public IWebElement GetWebElement()
        {
            UiPage.UiTab.SwitchTo();//this piece of code switches frames

            var webElement = GetNativeElementInternal();

            if (webElement == null || IsElementNotFoundWithinCurrentFrame(webElement) == true || IsElementStale(webElement) == true)
            {
                IUiElementsFindInfo findInfo = Info.InstantiationStage.FindOptions.Last();

                if (findInfo.UiElementType == Shared.Enumerations.UiElementType.Standard)
                    webElement = NativeElementsSearcher.FindElement<IWebElement, IWebElement, IWebElement>(Info).NativeElement;
                else if (findInfo.UiElementType == Shared.Enumerations.UiElementType.Frame)
                    webElement = NativeElementsSearcher.FindElement<IWebElement, IWebElement, IWebElement>(Info).NativeFrameElement;
                else if (findInfo.UiElementType == Shared.Enumerations.UiElementType.ShadowRootHost)
                    webElement = NativeElementsSearcher.FindElement<IWebElement, IWebElement, IWebElement>(Info).NativeShadowRootHostElement;
                else
                    throw new NotSupportedException($"Search of element of 'UiElementType.{findInfo.UiElementType}' is not supported.");

                NativeObjects.Upsert(SharedUiConstants.NativeElement, webElement);
            }

            return webElement!;
        }

        public IWebElement GetWebElement(bool find)
        {
            var webElement = GetNativeElementInternal();

            if (find)
            {
                var findResult = NativeElementsSearcher.FindElement<IWebElement, IWebElement, IWebElement>(Info);
                webElement = findResult.NativeElement;
                NativeObjects.Upsert(SharedUiConstants.NativeElement, webElement);
            }

            return webElement!;
        }

        private IWebElement? GetNativeElementInternal()
        {
            NativeObjects.TryGetValue(SharedUiConstants.NativeElement, out var nativeElement);
            return nativeElement as IWebElement;
        }

        public T GetWebDriver<T>() where T : IWebDriver
        {
            NativeObjects.TryGetValue(ProjectConst.IWebDriver, out var iWebDriver);

            if (iWebDriver != null)
                return (T)iWebDriver;

            throw new Exception($"Unable to find native web driver in Native Objects dictionary using key '{ProjectConst.IWebDriver}'.");
        }

        public IWebDriver GetIWebDriver()
        {
            return GetWebDriver<IWebDriver>();
        }

        private bool? IsElementStale(IWebElement? webElement)
        {
            if (webElement == null)
                return null;

            try
            {
                var tagName = webElement.TagName;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }

            return false;
        }

        private bool? IsElementNotFoundWithinCurrentFrame(IWebElement? webElement)
        {
            if (webElement == null)
                return null;

            //If webElement != null - that is was previously found - but there is no access to it with NotFoundException, means that currently active frame is not where the element was initially found and resides in
            try
            {
                SwitchToFrame();
                var tagName = webElement.TagName;
            }
            catch (Exception)
            {
                return true;
            }

            return false;
        }

        private void SwitchToFrame()
        {
            if (Info?.FindStage?.FindParametersWithSearchResult.FindChain != null)
            {
                GetIWebDriver().SwitchTo().DefaultContent();


                for (int i = 0; i < Info.FindStage.FindParametersWithSearchResult.FindChain.Count; i++)
                {
                    bool isLastElementInFindChainOrIsCurrentElement = i == Info.FindStage.FindParametersWithSearchResult.FindChain.Count - 1;

                    IFindParameters findChainElement = Info.FindStage.FindParametersWithSearchResult.FindChain[i];

                    if ((!isLastElementInFindChainOrIsCurrentElement && findChainElement.NativeFrameElement != null) || (isLastElementInFindChainOrIsCurrentElement && Info.FindStage.FindParametersWithSearchResult.IsElementInsideFrame))
                    {
                        var frame = findChainElement.NativeFrameElement as IWebElement;
                        GetIWebDriver().SwitchTo().Frame(frame);
                    }
                }
            }
        }

        //Calls forwarded to Proxy Element

        #region General Properties

        public override PointF GetLocation() => GetWebElement().Location;
        public override SizeF GetSize() => GetWebElement().Size;
        public override string GetTagName() => GetWebElement().TagName;

        #endregion General Properties

        #region Specific Properties

        public override string? GetAttribute(string attributeName, bool isBooleanAttributeType)
        {
            var attribute = GetWebElement().GetAttribute(attributeName);

            if (string.IsNullOrEmpty(attribute))
                return null;

            return attribute;
        }

        public override string? GetCssValue(string propertyName)
        {
            var cssValue = GetWebElement().GetCssValue(propertyName);

            if (string.IsNullOrEmpty(cssValue))
                return null;

            return cssValue;
        }

        public override string GetTextContent() => GetWebElement().Text.ToStringEmptyIfNull();

        #endregion Specific Properties

        #region States

        public override bool Exists()
        {
            try
            {
                // Find the element using the specified locator
                var element = GetWebElement();

                // If the element is found, return true
                return true;
            }
            catch (Exception)
            {
                // If the element is not found, return false
                return false;
            }
        }

        public override bool IsDisabled() => !IsEnabled();
        public override bool IsDisplayed() => Exists() && GetWebElement().Displayed;
        public override bool IsEnabled() => Exists() && GetWebElement().Enabled;

        #endregion States

        #region Execute JavaScript

        protected override KeyValuePair<string, object>[] SupplementJavaScriptExecutorArguments(params KeyValuePair<string, object>[] arguments)
        {
            var webElement = GetWebElement();
            var uiElementArgument = new KeyValuePair<string, object>(SharedUiConstants.JavaScriptArguments.UiElementArgument, webElement);
            return arguments.Insert(0, uiElementArgument);
        }

        #endregion Execute JavaScript
    }
}
