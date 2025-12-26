using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search
{
    internal class PlaywrightNativeElementsSearcher : BaseNativeElementsSearcher<ILocator, IFrameLocator, ILocator>
    {
        private readonly IPage _page;
        private readonly IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> _findOptionSearchMethodsProvider;

        public PlaywrightNativeElementsSearcher(ILogger log, IPage page, 
            IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> findOptionSearchMethodsProvider, 
            IUiElementSearchConfiguration uiElementSearchConfiguration) : base(log, uiElementSearchConfiguration)
        {
            _page = page;
            _findOptionSearchMethodsProvider = findOptionSearchMethodsProvider;
        }

        #region Find Result: List of Native Elements

        protected override List<IFindResult<ILocator, IFrameLocator, ILocator>> FindNativeElements(List<IFindParametersWithSearchResult> findParametersWithSearchResultChain, int index, bool shouldSingleElementBeFound = false)
        {
            IFindParametersWithSearchResult findParametersWithSearchResult = findParametersWithSearchResultChain[index];
            List<ILocator>? nativeElements = null;

            List<IFrameLocator>? nativeFrameElements = null;
            IFrameLocator? nativeFrameElement = null;

            List<ILocator>? nativeShadowRootHostElements = null;
            ILocator? nativeShadowRootHostElement = null;

            try
            {
                if (index == 0)
                {
                    if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsWithNoWrap)
                        nativeElements = FindNativeElementsFromBeginningOfDom(findParametersWithSearchResult, _page);

                    else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrame)
                    {
                        nativeFrameElement = FindNativeFrameElementFromBeginningOfDom(findParametersWithSearchResult, _page);
                        nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                    }

                    else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrameInsideShadowRoot)
                    {
                        nativeShadowRootHostElement = FindNativeShadowRootHostElementFromBeginningOfDom(findParametersWithSearchResult, _page);
                        nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                        nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                    }

                    else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRoot)
                    {
                        nativeShadowRootHostElement = FindNativeShadowRootHostElementFromBeginningOfDom(findParametersWithSearchResult, _page);
                        nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                    }

                    else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRootInsideFrame)
                    {
                        nativeFrameElement = FindNativeFrameElementFromBeginningOfDom(findParametersWithSearchResult, _page);
                        nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                        nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                    }
                }
                else //if (index > 0) // if search happens relative to some previous element
                {
                    var previousElementFindParameters = findParametersWithSearchResultChain[index - 1];

                    if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Standard)
                    {
                        ILocator previousElement = previousElementFindParameters.NativeElement.AsOrException<ILocator>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsWithNoWrap)
                            nativeElements = FindNativeElementsRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _page);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrameInsideShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _page);
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRootInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _page);
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                        }
                    }

                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Frame)
                    {
                        IFrameLocator previousFrameElement = previousElementFindParameters.NativeFrameElement.AsOrException<IFrameLocator>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsWithNoWrap)
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _page);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrameInsideShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _page);
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRootInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _page);
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                        }
                    }
                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.ShadowRootHost)
                    {
                        ILocator previousShadowRootHostElement = previousElementFindParameters.NativeShadowRootHostElement.AsOrException<ILocator>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsWithNoWrap)
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _page);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInFrameInsideShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _page);
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                        }

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ElementsInShadowRootInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _page);
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                            nativeElements = FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
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

        protected override List<IFindResult<ILocator, IFrameLocator, ILocator>> FindNativeFrameElements(List<IFindParametersWithSearchResult> findParametersWithSearchResultChain, int index, bool shouldSingleElementBeFound = false)
        {
            var findParametersWithSearchResult = findParametersWithSearchResultChain[index];

            List<IFrameLocator>? nativeFrameElements = null;
            ILocator? nativeShadowRootHostElement = null;

            try
            {
                if (index == 0)
                {
                    if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElements)
                        nativeFrameElements = FindNativeFrameElementsFromBeginningOfDom(findParametersWithSearchResult, _page);

                    else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElementsInsideShadowRoot)
                    {
                        nativeShadowRootHostElement = FindNativeShadowRootHostElementFromBeginningOfDom(findParametersWithSearchResult, _page);
                        nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                    }
                }
                else //if (index > 0)
                {
                    var previousElementFindParameters = findParametersWithSearchResultChain[index - 1];

                    if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Standard)
                    {
                        ILocator previousElement = previousElementFindParameters.NativeElement.AsOrException<ILocator>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElements)
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _page);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElementsInsideShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _page);
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                        }
                    }

                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Frame)
                    {
                        IFrameLocator previousFrameElement = previousElementFindParameters.NativeFrameElement.AsOrException<IFrameLocator>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElements)
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _page);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElementsInsideShadowRoot)
                        {
                            nativeShadowRootHostElement = FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _page);
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(nativeShadowRootHostElement, findParametersWithSearchResult, _page);
                        }
                    }

                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.ShadowRootHost)
                    {
                        ILocator previousShadowRootHostElement = previousElementFindParameters.NativeShadowRootHostElement.AsOrException<ILocator>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElements)
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _page);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.FrameElementsInsideShadowRoot)
                            nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _page);
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

        protected override List<IFindResult<ILocator, IFrameLocator, ILocator>> FindNativeShadowRootHostElements(List<IFindParametersWithSearchResult> findParametersWithSearchResultChain, int index, bool shouldSingleElementBeFound = false)
        {
            var findParametersWithSearchResult = findParametersWithSearchResultChain[index];
            List<ILocator>? nativeShadowRootHostElements = null;
            IFrameLocator? nativeFrameElement = null;

            try
            {
                if (index == 0)
                {
                    if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElements)
                        nativeShadowRootHostElements = FindNativeShadowRootHostElementsFromBeginningOfDom(findParametersWithSearchResult, _page);

                    if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElementsInsideFrame)
                    {
                        nativeFrameElement = FindNativeFrameElementFromBeginningOfDom(findParametersWithSearchResult, _page);
                        nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                    }
                }
                else //if (index > 0)
                {
                    var previousElementFindParameters = findParametersWithSearchResultChain[index - 1];

                    if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Standard)
                    {
                        ILocator previousElement = previousElementFindParameters.NativeElement.AsOrException<ILocator>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElements)
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _page);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElementsInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousElementInFindChain(previousElement, findParametersWithSearchResult, _page);
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                        }
                    }

                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.Frame)
                    {
                        IFrameLocator previousFrameElement = previousElementFindParameters.NativeFrameElement.AsOrException<IFrameLocator>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElements)
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _page);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElementsInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParametersWithSearchResult, _page);
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
                        }
                    }

                    else if (previousElementFindParameters.FindInfo.UiElementType == UiElementType.ShadowRootHost)
                    {
                        ILocator previousShadowRootHostElement = previousElementFindParameters.NativeShadowRootHostElement.AsOrException<ILocator>();

                        if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElements)
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _page);

                        else if (findParametersWithSearchResult.FindInfo.SearchKind == SearchKind.ShadowRootHostElementsInsideFrame)
                        {
                            nativeFrameElement = FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParametersWithSearchResult, _page);
                            nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(nativeFrameElement, findParametersWithSearchResult, _page);
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

        private List<ILocator> FindNativeElementsFromBeginningOfDom(IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod elementSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ElementsFindOption);
            ILocator result = elementSearchMethod.FindElementsFromBeginningOfDom(page, findParameters.FindInfo.ElementsFindOption);
            List<ILocator> nativeElements = GetElementsListOrEmpty(result);
            return nativeElements;
        }

        private List<ILocator> FindNativeElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod elementSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ElementsFindOption);
            ILocator result = elementSearchMethod.FindElementsRelativeToPreviousElementInFindChain(previousElement, findParameters.FindInfo.ElementsFindOption, page);
            List<ILocator> nativeElements = GetElementsListOrEmpty(result);
            return nativeElements;
        }

        private List<ILocator> FindNativeElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod elementSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ElementsFindOption);
            ILocator result = elementSearchMethod.FindElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParameters.FindInfo.ElementsFindOption, page);
            List<ILocator> nativeElements = GetElementsListOrEmpty(result);
            return nativeElements;
        }

        private List<ILocator> FindNativeElementsRelativeToPreviousShadowRootHostElementInFindChain(ILocator previousShadowRootHostElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod elementSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ElementsFindOption);
            ILocator result = elementSearchMethod.FindElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParameters.FindInfo.ElementsFindOption, page);
            List<ILocator> nativeElements = GetElementsListOrEmpty(result);
            return nativeElements;
        }

        #endregion Find Native Elements

        #region Find Native Frame Elements

        private List<IFrameLocator> FindNativeFrameElementsFromBeginningOfDom(IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod elementSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.FramesFindOption);
            IFrameLocator result = elementSearchMethod.FindFrameElementsFromBeginningOfDom(page, findParameters.FindInfo.FramesFindOption);
            List<IFrameLocator> nativeFrameElements = GetFrameElementsListOrEmpty(result);
            return nativeFrameElements;
        }

        private IFrameLocator FindNativeFrameElementFromBeginningOfDom(IFindParametersWithSearchResult findParameters, IPage page)
        {
            List<IFrameLocator> nativeFrameElements = FindNativeFrameElementsFromBeginningOfDom(findParameters, page);
            return SingleNativeFrameElementOrException(nativeFrameElements, findParameters);
        }

        private List<IFrameLocator> FindNativeFrameElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod frameSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.FramesFindOption);
            IFrameLocator result = frameSearchMethod.FindFrameElementsRelativeToPreviousElementInFindChain(previousElement, findParameters.FindInfo.FramesFindOption, page);
            List<IFrameLocator> nativeFrameElements = GetFrameElementsListOrEmpty(result);
            return nativeFrameElements;
        }

        private IFrameLocator FindNativeFrameElementRelativeToPreviousElementInFindChain(ILocator previousElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            List<IFrameLocator> nativeFrameElements = FindNativeFrameElementsRelativeToPreviousElementInFindChain(previousElement, findParameters, page);
            return SingleNativeFrameElementOrException(nativeFrameElements, findParameters);
        }

        private List<IFrameLocator> FindNativeFrameElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod frameSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.FramesFindOption);
            IFrameLocator result = frameSearchMethod.FindFrameElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParameters.FindInfo.FramesFindOption, page);
            List<IFrameLocator> nativeFrameElements = GetFrameElementsListOrEmpty(result);
            return nativeFrameElements;
        }

        private IFrameLocator FindNativeFrameElementRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            List<IFrameLocator> nativeFrameElements = FindNativeFrameElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParameters, page);
            return SingleNativeFrameElementOrException(nativeFrameElements, findParameters);
        }

        private List<IFrameLocator> FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(ILocator previousShadowRootHostElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod elementSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.FramesFindOption);
            IFrameLocator result = elementSearchMethod.FindFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParameters.FindInfo.FramesFindOption, page);
            List<IFrameLocator> nativeFrameElements = GetFrameElementsListOrEmpty(result);
            return nativeFrameElements;
        }

        private IFrameLocator FindNativeFrameElementRelativeToPreviousShadowRootHostElementInFindChain(ILocator previousShadowRootHostElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            List<IFrameLocator> nativeFrameElements = FindNativeFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParameters, page);
            IFrameLocator nativeFrameElement = SingleNativeFrameElementOrException(nativeFrameElements, findParameters);
            return nativeFrameElement;
        }

        #endregion Find Native Frame Elements

        #region Find Native ShadowRootHost Elements

        private List<ILocator> FindNativeShadowRootHostElementsFromBeginningOfDom(IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod elementSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ShadowRootHostsFindOption);
            ILocator result = elementSearchMethod.FindShadowRootHostElementsFromBeginningOfDom(page, findParameters.FindInfo.ShadowRootHostsFindOption);
            List<ILocator> nativeElements = GetElementsListOrEmpty(result);
            return nativeElements;
        }

        private ILocator FindNativeShadowRootHostElementFromBeginningOfDom(IFindParametersWithSearchResult findParameters, IPage page)
        {
            List<ILocator> nativeShadowRootHostElements = FindNativeShadowRootHostElementsFromBeginningOfDom(findParameters, page);
            return SingleNativeShadowRootElementOrException(nativeShadowRootHostElements, findParameters);
        }

        private List<ILocator> FindNativeShadowRootHostElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod shadowRootHostSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ShadowRootHostsFindOption);
            ILocator result = shadowRootHostSearchMethod.FindShadowRootHostElementsRelativeToPreviousElementInFindChain(previousElement, findParameters.FindInfo.ShadowRootHostsFindOption, page);
            List<ILocator> nativeElements = GetElementsListOrEmpty(result);
            return nativeElements;
        }

        private ILocator FindNativeShadowRootHostElementRelativeToPreviousElementInFindChain(ILocator previousElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            List<ILocator> nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousElementInFindChain(previousElement, findParameters, page);
            return SingleNativeShadowRootElementOrException(nativeShadowRootHostElements, findParameters);
        }

        private List<ILocator> FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod elementSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ShadowRootHostsFindOption);
            ILocator result = elementSearchMethod.FindShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParameters.FindInfo.ShadowRootHostsFindOption, page);
            List<ILocator> nativeElements = GetElementsListOrEmpty(result);
            return nativeElements;
        }

        private ILocator FindNativeShadowRootHostElementRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            List<ILocator> nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findParameters, page);
            ILocator nativeShadowRootHostElement = SingleNativeShadowRootElementOrException(nativeShadowRootHostElements, findParameters);
            return nativeShadowRootHostElement;
        }

        private List<ILocator> FindNativeShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(ILocator previousShadowRootHostElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            IPlaywrightFindOptionSearchMethod shadowRootHostSearchMethod = _findOptionSearchMethodsProvider.GetFindOptionSearchMethod(findParameters.FindInfo.ShadowRootHostsFindOption);
            ILocator result = shadowRootHostSearchMethod.FindShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParameters.FindInfo.ShadowRootHostsFindOption, page);
            List<ILocator> nativeElements = GetElementsListOrEmpty(result);
            return nativeElements;
        }

        private ILocator FindNativeShadowRootHostElementRelativeToPreviousShadowRootHostElementInFindChain(ILocator previousShadowRootHostElement, IFindParametersWithSearchResult findParameters, IPage page)
        {
            List<ILocator> nativeShadowRootHostElements = FindNativeShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findParameters, page);
            return SingleNativeShadowRootElementOrException(nativeShadowRootHostElements, findParameters);
        }

        #endregion Find Native ShadowRootHost Elements

        #region Auxiliary

        private List<ILocator> GetElementsListOrEmpty(ILocator locator)
        {
            List<ILocator> elements = new();

            var count = locator?.CountAsync()?.Result;
            if (count > 0)
                for (int i = 0; i < count; i++)
                    elements.Add(locator.Nth(i));

            return elements;
        }

        private List<IFrameLocator> GetFrameElementsListOrEmpty(IFrameLocator frameLocator)
        {
            List<IFrameLocator> elements = new();

            var count = frameLocator.Owner.CountAsync()?.Result;
            if (count > 0)
                for (int i = 0; i < count; i++)
                    elements.Add(frameLocator.Nth(i));

            return elements;
        }

        protected override Uri GetCurrentUri()
        {
            string uri = _page.Url;
            return new Uri(uri);
        }

        #endregion Auxiliary
    }
}
