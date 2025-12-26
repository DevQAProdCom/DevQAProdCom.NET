using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Selenium.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search
{
    internal class SeleniumNativeElementsSearcher(ILogger log, Func<IWebDriver> getWebDriver,
        IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> findOptionSearchMethodsProvider,
        IUiElementSearchConfiguration uiElementSearchConfiguration)
        : BaseNativeElementsSearcher<IWebElement, IWebElement, IWebElement>(log, uiElementSearchConfiguration)
    {
        private IWebDriver _webDriver => getWebDriver();

        #region Find Result: List of Native Elements

        protected override List<IFindResult<IWebElement, IWebElement, IWebElement>> FindNativeElements(List<IFindParametersWithSearchResult> findParametersWithSearchResultChain, int index, bool shouldSingleElementBeFound = false)
        {
            IFindParametersWithSearchResult findParametersWithSearchResult = findParametersWithSearchResultChain[index];
            List<IWebElement>? nativeElements = null;

            List<IWebElement>? nativeFrameElements = null;
            IWebElement? nativeFrameElement = null;

            List<IWebElement>? nativeShadowRootHostElements = null;
            IWebElement? nativeShadowRootHostElement = null;

            try
            {
                SwitchToFrame(findParametersWithSearchResultChain, findParametersWithSearchResult);

                if (index == 0)
                {
                    if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsWithNoWrap)
                        nativeElements = FindNativeElementsFromBeginningOfDom(findParametersWithSearchResult, _webDriver);

                    else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrame)
                    {
                        nativeFrameElement = FindNativeFrameElementFromBeginningOfDom(findParametersWithSearchResult, _webDriver);
                        _webDriver.SwitchTo().Frame(nativeFrameElement);
                        nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                    }

                    else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrameInsideShadowRoot)
                    {
                        nativeShadowRootHostElement = FindNativeShadowRootHostElementFromBeginningOfDom(findParametersWithSearchResult, _webDriver);
                        nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                        _webDriver.SwitchTo().Frame(nativeFrameElement);
                        nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                    }

                    else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRoot)
                    {
                        nativeShadowRootHostElement = FindNativeShadowRootHostElementFromBeginningOfDom(findParametersWithSearchResult, _webDriver);
                        nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                    }

                    else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRootInsideFrame)
                    {
                        nativeFrameElement = FindNativeFrameElementFromBeginningOfDom(findParametersWithSearchResult, _webDriver);
                        _webDriver.SwitchTo().Frame(nativeFrameElement);
                        nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                        nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                    }
                }
                else //if (index > 0) // if search happens relative to some previous element
                {
                    var previousElementFindParameters = findParametersWithSearchResultChain[index - 1];

                    if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Standard)
                    {
                        IWebElement previousElement = previousElementFindParameters.NativeElement.AsOrException<IWebElement>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsWithNoWrap)
                            nativeElements = FindNativeElementsRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _webDriver);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrameInsideShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _webDriver);
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _webDriver);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRootInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                        }
                    }

                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Frame)
                    {
                        IWebElement previousFrameElement = previousElementFindParameters.NativeFrameElement.AsOrException<IWebElement>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsWithNoWrap)
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _webDriver);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrameInsideShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _webDriver);
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _webDriver);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRootInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                        }
                    }
                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.ShadowRootHost)
                    {
                        IWebElement previousShadowRootHostElement = previousElementFindParameters.NativeShadowRootHostElement.AsOrException<IWebElement>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsWithNoWrap)
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _webDriver);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrameInsideShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRootInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                findParametersWithSearchResult.Exception = ex;
                throw;
            }

            return GetFindParametersResultsList(nativeElements, findParametersWithSearchResult, shouldSingleElementBeFound, nativeFrameElement: nativeFrameElement, nativeShadowRootHostElement: nativeShadowRootHostElement);
        }

        #endregion Find Result: List of Native Elements

        #region Find Result: List of Native Frame Elements

        protected override List<IFindResult<IWebElement, IWebElement, IWebElement>> FindNativeFrameElements(List<IFindParametersWithSearchResult> findParametersWithSearchResultChain, int index, bool shouldSingleElementBeFound = false)
        {
            var findParametersWithSearchResult = findParametersWithSearchResultChain[index];

            List<IWebElement>? nativeFrameElements = null;
            IWebElement? nativeShadowRootHostElement = null;

            try
            {
                SwitchToFrame(findParametersWithSearchResultChain, findParametersWithSearchResult);

                if (index == 0)
                {
                    if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElements)
                        nativeFrameElements = FindNativeFrameElementsFromBeginningOfDom(findParametersWithSearchResult, _webDriver);

                    else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElementsInsideShadowRoot)
                    {
                        nativeShadowRootHostElement = FindNativeShadowRootHostElementFromBeginningOfDom(findParametersWithSearchResult, _webDriver);
                        nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                    }
                }
                else //if (index > 0)
                {
                    var previousElementFindParameters = findParametersWithSearchResultChain[index - 1];

                    if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Standard)
                    {
                        IWebElement previousElement = previousElementFindParameters.NativeElement.AsOrException<IWebElement>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElements)
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _webDriver);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElementsInsideShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _webDriver);
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                        }
                    }

                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Frame)
                    {
                        IWebElement previousFrameElement = previousElementFindParameters.NativeFrameElement.AsOrException<IWebElement>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElements)
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _webDriver);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElementsInsideShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _webDriver);
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                        }
                    }

                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.ShadowRootHost)
                    {
                        IWebElement previousShadowRootHostElement = previousElementFindParameters.NativeShadowRootHostElement.AsOrException<IWebElement>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElements)
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _webDriver);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElementsInsideShadowRoot)
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                    }
                }
            }
            catch (Exception ex)
            {
                findParametersWithSearchResult.Exception = ex;
                throw;
            }

            return GetFindParametersResultsList(nativeFrameElements, findParametersWithSearchResult, shouldSingleElementBeFound, nativeShadowRootHostElement: nativeShadowRootHostElement);
        }

        #endregion Find Result: List of Native Frame Elements

        #region Find Result: List of Native ShadowRootHost Elements

        protected override List<IFindResult<IWebElement, IWebElement, IWebElement>> FindNativeShadowRootHostElements(List<IFindParametersWithSearchResult> findParametersWithSearchResultChain, int index, bool shouldSingleElementBeFound = false)
        {
            var findParametersWithSearchResult = findParametersWithSearchResultChain[index];
            List<IWebElement>? nativeShadowRootHostElements = null;
            IWebElement? nativeFrameElement = null;

            try
            {
                SwitchToFrame(findParametersWithSearchResultChain, findParametersWithSearchResult);

                if (index == 0)
                {
                    if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElements)
                        nativeShadowRootHostElements = FindNativeShadowRootHostElementsFromBeginningOfDom(findParametersWithSearchResult, _webDriver);

                    if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElementsInsideFrame)
                    {
                        nativeFrameElement = FindNativeFrameElementFromBeginningOfDom(findParametersWithSearchResult, _webDriver);
                        _webDriver.SwitchTo().Frame(nativeFrameElement);
                        nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                    }
                }
                else //if (index > 0)
                {
                    var previousElementFindParameters = findParametersWithSearchResultChain[index - 1];

                    if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Standard)
                    {
                        IWebElement previousElement = previousElementFindParameters.NativeElement.AsOrException<IWebElement>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElements)
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _webDriver);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElementsInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                        }
                    }

                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Frame)
                    {
                        IWebElement previousFrameElement = previousElementFindParameters.NativeFrameElement.AsOrException<IWebElement>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElements)
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _webDriver);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElementsInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                        }
                    }

                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.ShadowRootHost)
                    {
                        IWebElement previousShadowRootHostElement = previousElementFindParameters.NativeShadowRootHostElement.AsOrException<IWebElement>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElements)
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _webDriver);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElementsInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _webDriver);
                            _webDriver.SwitchTo().Frame(nativeFrameElement);
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _webDriver);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                findParametersWithSearchResult.Exception = ex;
                throw;
            }

            return GetFindParametersResultsList(nativeShadowRootHostElements, findParametersWithSearchResult, shouldSingleElementBeFound, nativeFrameElement: nativeFrameElement);
        }

        #endregion Find Result: List of Native ShadowRootHost Elements

        #region Find Native Elements

        private List<IWebElement> FindNativeElementsFromBeginningOfDom(IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod elementSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ElementsFindOption);
            return elementSearchMethod.FindElementsFromBeginningOfDom(driver, findParameters.FindInfo.ElementsFindOption);
        }

        private List<IWebElement> FindNativeElementsRelativeToPreviousElementInFindChain(IWebElement previousElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod elementSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ElementsFindOption);
            return elementSearchMethod.FindElementsRelativeToPreviousElementInFindChain(previousElement, findParameters.FindInfo.ElementsFindOption, driver);
        }

        private List<IWebElement> FindNativeElementsRelativeToPreviousFrameElementInFindChain(IWebElement previousFrameElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod elementSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ElementsFindOption);
            return elementSearchMethod.FindElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParameters.FindInfo.ElementsFindOption, driver);
        }

        private List<IWebElement> FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(IWebElement previousShadowRootHostElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod elementSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ElementsFindOption);
            return elementSearchMethod.FindElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParameters.FindInfo.ElementsFindOption, driver);
        }

        #endregion Find Native Elements

        #region Find Native Frame Elements

        private List<IWebElement> FindNativeFrameElementsFromBeginningOfDom(IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod elementSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.FramesFindOption);
            return elementSearchMethod.FindFrameElementsFromBeginningOfDom(driver, findParameters.FindInfo.FramesFindOption);
        }

        private IWebElement FindNativeFrameElementFromBeginningOfDom(IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            List<IWebElement> nativeFrameElements = FindNativeFrameElementsFromBeginningOfDom(findParameters, driver);
            return SingleNativeFrameElementOrException(nativeFrameElements, findParameters);
        }

        private List<IWebElement> FindNativeFrameElementsRelativeToPreviousElementInFindChain(IWebElement previousElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod frameSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.FramesFindOption);
            return frameSearchMethod.FindFrameElementsRelativeToPreviousElementInFindChain(previousElement, findParameters.FindInfo.FramesFindOption, driver);
        }

        private IWebElement FindNativeFrameElementRelativeToPreviousElementInFindChain(IWebElement previousElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            List<IWebElement> nativeFrameElements = FindNativeFrameElementsRelativeToPreviousElementInFindChain(previousElement, findParameters, driver);
            return SingleNativeFrameElementOrException(nativeFrameElements, findParameters);
        }

        private List<IWebElement> FindNativeFrameElementsRelativeToPreviousFrameElementInFindChain(IWebElement previousFrameElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod frameSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.FramesFindOption);
            return frameSearchMethod.FindFrameElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParameters.FindInfo.FramesFindOption, driver);
        }

        private IWebElement FindNativeFrameElementRelativeToPreviousFrameElementInFindChain(IWebElement previousFrameElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            List<IWebElement> nativeFrameElements = FindNativeFrameElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParameters, driver);
            return SingleNativeFrameElementOrException(nativeFrameElements, findParameters);
        }

        private List<IWebElement> FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(IWebElement previousShadowRootHostElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod elementSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.FramesFindOption);
            return elementSearchMethod.FindFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParameters.FindInfo.FramesFindOption, driver);
        }

        private IWebElement FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(IWebElement previousShadowRootHostElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            List<IWebElement> nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParameters, driver);
            IWebElement nativeFrameElement = SingleNativeFrameElementOrException(nativeFrameElements, findParameters);
            return nativeFrameElement;
        }

        #endregion Find Native Frame Elements

        #region Find Native ShadowRootHost Elements

        private List<IWebElement> FindNativeShadowRootHostElementsFromBeginningOfDom(IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod elementSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ShadowRootHostsFindOption);
            return elementSearchMethod.FindShadowRootHostElementsFromBeginningOfDom(driver, findParameters.FindInfo.ShadowRootHostsFindOption);
        }

        private IWebElement FindNativeShadowRootHostElementFromBeginningOfDom(IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            List<IWebElement> nativeShadowRootHostElements = FindNativeShadowRootHostElementsFromBeginningOfDom(findParameters, driver);
            return SingleNativeShadowRootElementOrException(nativeShadowRootHostElements, findParameters);
        }

        private List<IWebElement> FindNativeShadowRootHostElementsRelativeToPreviousElementInFindChain(IWebElement previousElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod shadowRootHostSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ShadowRootHostsFindOption);
            return shadowRootHostSearchMethod.FindShadowRootHostElementsRelativeToPreviousElementInFindChain(previousElement, findParameters.FindInfo.ShadowRootHostsFindOption, driver);
        }

        private IWebElement FindNativeShadowRootHostElementRelativeToPreviousElementInFindChain(IWebElement previousElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            List<IWebElement> nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousElementInFindChain(previousElement, findParameters, driver);
            return SingleNativeShadowRootElementOrException(nativeShadowRootHostElements, findParameters);
        }

        private List<IWebElement> FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(IWebElement previousFrameElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod elementSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ShadowRootHostsFindOption);
            return elementSearchMethod.FindShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParameters.FindInfo.ShadowRootHostsFindOption, driver);
        }

        private IWebElement FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(IWebElement previousFrameElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            List<IWebElement> nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParameters, driver);
            IWebElement nativeShadowRootHostElement = SingleNativeShadowRootElementOrException(nativeShadowRootHostElements, findParameters);
            return nativeShadowRootHostElement;
        }

        private List<IWebElement> FindNativeShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(IWebElement previousShadowRootHostElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            ISeleniumFindOptionSearchMethod shadowRootHostSearchMethod = findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ShadowRootHostsFindOption);
            return shadowRootHostSearchMethod.FindShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParameters.FindInfo.ShadowRootHostsFindOption, driver);
        }

        private IWebElement FindNativeShadowRootHostElementRelativeToPreviousShadowRootHostElementInFindChain(IWebElement previousShadowRootHostElement, IFindParametersWithSearchResult findParameters, IWebDriver driver)
        {
            List<IWebElement> nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParameters, driver);
            return SingleNativeShadowRootElementOrException(nativeShadowRootHostElements, findParameters);
        }

        #endregion Find Native ShadowRootHost Elements

        #region Auxiliary

        private void SwitchToFrame(List<IFindParametersWithSearchResult> findParametersChain, IFindParametersWithSearchResult findParameters)
        {
            _webDriver.SwitchTo().DefaultContent();
            var indexOfCurrentlySearchedElement = findParametersChain.IndexOf(findParameters);

            for (var i = 0; i <= indexOfCurrentlySearchedElement; i++)
            {
                if (findParametersChain[i].NativeFrameElement != null)
                {
                    var frame = findParametersChain[i].NativeFrameElement as IWebElement;
                    _webDriver.SwitchTo().Frame(frame);
                }
            }
        }

        protected override Uri GetCurrentUri()
        {
            string uri = _webDriver.Url;
            return new Uri(uri);
        }

        #endregion Auxiliary
    }
}
