using System.Diagnostics;
using System.Text.Json;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.Constans;
using DevQAProdCom.NET.Logging.Shared.Enumerations;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Logging.Models;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search
{
    public abstract class BaseNativeElementsSearcher<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement> : INativeElementsSearcher
           where TNativeElement : class
           where TNativeFrameElement : class
           where TNativeShadowRootHostElement : class
    {
        protected readonly ILogger _log;

        protected BaseNativeElementsSearcher(ILogger log)
        {
            _log = log;
        }

        #region Explicit implementation of interface

        public IFindResult<TNativeElementType, TNativeFrameElementType, TNativeShadowRootHostElement> FindElement<TNativeElementType, TNativeFrameElementType, TNativeShadowRootHostElement>(IUiElementInfo uiElementInfo)
            where TNativeElementType : class
            where TNativeFrameElementType : class
            where TNativeShadowRootHostElement : class
            => FindElementsT(uiElementInfo, shouldSingleElementBeFound: true).Single() as IFindResult<TNativeElementType, TNativeFrameElementType, TNativeShadowRootHostElement>;

        public IList<IFindResult<TNativeElementType, TNativeFrameElementType, TNativeShadowRootHostElement>> FindElements<TNativeElementType, TNativeFrameElementType, TNativeShadowRootHostElement>(IUiElementInfo uiElementsInfo)
            where TNativeElementType : class
            where TNativeFrameElementType : class
            where TNativeShadowRootHostElement : class
            => FindElementsT(uiElementsInfo, shouldSingleElementBeFound: false).Select(x => x as IFindResult<TNativeElementType, TNativeFrameElementType, TNativeShadowRootHostElement>).ToList();

        #endregion Explicit implementation of interface

        #region Base Methods to Find Elements

        public List<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> FindElementsT(IUiElementInfo uiElementInfo, bool shouldSingleElementBeFound = false)
        {
            _log.Verbose($"{{@{SharedLoggingConstants.Data}}}", new UiElementsSearchLoggingModel(uiElementInfo, SharedLoggingConstants.SearchStarted));

            //Log Data
            UiElementsSearchResultLoggingModelVerbose findResultsStatesLoggingModel = new(uiElementInfo);

            //Result Data
            List<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> nativeElementsList = new(); // should return empty list if nothing is found

            (List<List<IFindParametersWithSearchResult>> FindChainCombinations, int StartSearchFromIndex) findData = GetFindData(uiElementInfo);

            Stopwatch searchTimeStopWatch = Stopwatch.StartNew();
            for (var i = 0; i < findData.FindChainCombinations.Count; i++)
            {
                var findChain = findData.FindChainCombinations[i];

                try
                {
                    var result = FindElementsFromIndexOfFindChain(findChain, findData.StartSearchFromIndex, uiElementInfo);

                    if (result.Count > 0)
                    {
                        nativeElementsList.AddRange(result);
                        findResultsStatesLoggingModel.AddFoundFindChain(i, findChain);
                    }
                    else
                        findResultsStatesLoggingModel.AddNotFoundFindChain(i, findChain);
                }

                catch
                {
                    findResultsStatesLoggingModel.AddFailedFindChain(i, findChain);
                    //continue with next find chain combination in case exception was thrown because some element has not been found using previous find chain or any other exception
                }

                if (_log.MinimumLogLevel <= LogLevel.Debug)
                {
                    Uri? uriAfterSearch = GetCurrentUri();
                    foreach (var findParameters in findChain)
                        findParameters.UriAfterSearch = uriAfterSearch;
                }
            }
            searchTimeStopWatch.Stop();
            _log.Verbose($"{{@{SharedLoggingConstants.Data}}}", new UiElementsSearchLoggingModel(uiElementInfo, SharedLoggingConstants.SearchEnded));

            CheckAndLogResults(uiElementInfo, nativeElementsList, shouldSingleElementBeFound, findResultsStatesLoggingModel, searchTimeStopWatch);

            return nativeElementsList;
        }

        private (List<List<IFindParametersWithSearchResult>> FindChainCombinations, int Index) GetFindData(IUiElementInfo uiElementsInfo)
        {
            List<List<IFindParametersWithSearchResult>>? findChainsCombinations = new();
            var previousElementWithFindInfoSet = FindLastPreviousElementWithFindInfoSet(uiElementsInfo);
            var findOptionsForAllNestingLevelsOfCurrentElementSearched = uiElementsInfo.InstantiationStage.NestingLevelsFindParameters.Select(x => x.Parameters).ToList();
            var index = 0;

            if (previousElementWithFindInfoSet != null)
            {
                index = previousElementWithFindInfoSet.Info.InstantiationStage.NestingLevel;
                findOptionsForAllNestingLevelsOfCurrentElementSearched = uiElementsInfo.InstantiationStage.NestingLevelsFindParameters.Select(x => x.Parameters).ToList();
                var nestingLevelsFindParametersOfPreviousElementWithFindInfoSet =
                    previousElementWithFindInfoSet.Info.FindStage.FindParametersWithSearchResult.FindChain.Select(x => new List<IFindParametersWithSearchResult>() { (IFindParametersWithSearchResult)x }.AsReadOnly());
                findOptionsForAllNestingLevelsOfCurrentElementSearched.RemoveRange(0, index);
                findOptionsForAllNestingLevelsOfCurrentElementSearched.InsertRange(0, nestingLevelsFindParametersOfPreviousElementWithFindInfoSet);
            }

            Uri? uriBeforeSearch = null;
            if (_log.MinimumLogLevel <= LogLevel.Debug)
                uriBeforeSearch = GetCurrentUri();

            foreach (var findChainCombinationWithFindParameters in findOptionsForAllNestingLevelsOfCurrentElementSearched.CrossJoin())
            {
                //Extend/transform IFindParameters from InstantiationStage to IFindParametersWithSearchResult required for FindStage
                var findChainCombinationWithFindParametersWithSearchResultType = findChainCombinationWithFindParameters.Select(x =>
                {
                    if (x is IFindParametersWithSearchResult)
                        //Do not change cast (x as IFindParametersWithSearchResult), because it is required for particular constructor overload to be used
                        return new FindParametersWithSearchResult(x as IFindParametersWithSearchResult) as IFindParametersWithSearchResult;
                    else //if x is IFindParameters
                        return new FindParametersWithSearchResult(x, uriBeforeSearch: uriBeforeSearch) as IFindParametersWithSearchResult;
                }).ToList();

                foreach (var element in findChainCombinationWithFindParametersWithSearchResultType)
                    element.FindChain = findChainCombinationWithFindParametersWithSearchResultType;

                findChainsCombinations.Add(findChainCombinationWithFindParametersWithSearchResultType);
            }

            return (findChainsCombinations, index);
        }

        private void CheckAndLogResults(IUiElementInfo uiElementInfo, List<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> nativeElementsList, bool shouldSingleElementBeFound,
            UiElementsSearchResultLoggingModelVerbose findResultsStatesLoggingModel, Stopwatch searchTimeStopWatch)
        {

            findResultsStatesLoggingModel.SearchTimeMilliseconds = searchTimeStopWatch.Elapsed.TotalMilliseconds;

            string? baseExceptionMessage = string.Empty;
            //Check is done only for single element search. For UiElementsList empty list is returned, if nothing is found.
            if (shouldSingleElementBeFound)
            {
                Exception? generalException = null;

                //If Single UiElement is searched, but no found - throw exception.
                if (nativeElementsList.Count() == 0)
                {
                    baseExceptionMessage = $"No native elements were found or errors appeared while searching for UiElement '{uiElementInfo.InstantiationStage.FullName}' on page '{findResultsStatesLoggingModel.GetUriAfterSearchString()}'.";
                    generalException = new Exception(baseExceptionMessage);
                }
                //If Single UiElement is searched, but several found - throw exception.
                else if (nativeElementsList.Count() > 1)
                {
                    baseExceptionMessage = $"Single native element was expected, but '{nativeElementsList.Count}' were found while searching for UiElement '{uiElementInfo.InstantiationStage.FullName}' on page '{findResultsStatesLoggingModel.GetUriAfterSearchString()}'.";
                    generalException = new Exception(baseExceptionMessage);
                }

                findResultsStatesLoggingModel.GeneralException = generalException;
            }

            if (findResultsStatesLoggingModel.GeneralException != null)
            {
                if (_log.MinimumLogLevel == LogLevel.Verbose)
                {
                    var loggingModel = new UiElementsSearchLoggingModelWithSearchResultVerbose(findResultsStatesLoggingModel);
                    _log.Error($"{{@{SharedLoggingConstants.Data}}}", new UiElementsSearchLoggingModelWithSearchResultVerbose(findResultsStatesLoggingModel));
                    throw new UiElementNotFoundException($"{baseExceptionMessage}\n{loggingModel.ToJson(new JsonSerializerOptions { WriteIndented = true })}");
                }
                else if (_log.MinimumLogLevel >= LogLevel.Debug)
                {
                    var loggingModel = new UiElementsSearchLoggingModelWithSearchResultDebug(findResultsStatesLoggingModel);
                    _log.Error($"{{@{SharedLoggingConstants.Data}}}", loggingModel);
                    throw new UiElementNotFoundException($"{baseExceptionMessage}\n{loggingModel.ToJson(new JsonSerializerOptions { WriteIndented = true })}");
                }
            }
            else //if no exceptions
            {
                if (_log.MinimumLogLevel == LogLevel.Debug)
                    _log.Debug($"{{@{SharedLoggingConstants.Data}}}", new UiElementsSearchLoggingModelWithSearchResultDebug(findResultsStatesLoggingModel));
                else if (_log.MinimumLogLevel == LogLevel.Verbose)
                    _log.Verbose($"{{@{SharedLoggingConstants.Data}}}", new UiElementsSearchLoggingModelWithSearchResultVerbose(findResultsStatesLoggingModel));
            }
        }

        private List<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> FindElementsFromIndexOfFindChain(List<IFindParametersWithSearchResult> findChain, int startSearchFromIndex, IUiElementInfo info)
        {
            List<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> findResultList = new();

            //Intermediary variables to store found elements of find chain in case if it is complex
            IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>? nativeElement = null;
            IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>? nativeFrameElement = null;
            IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>? nativeShadowRootHostElement = null;

            if (startSearchFromIndex > 0)
            {
                var indexOfPreviousFoundElement = startSearchFromIndex - 1;
                var previousFoundElement = findChain.ElementAt(indexOfPreviousFoundElement);

                var uri = GetCurrentUri();
                IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement> findResult
                    = new FindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>(findChain.ElementAt(indexOfPreviousFoundElement));

                if (previousFoundElement.FindInfo.UiElementType == UiElementType.Standard)
                    nativeElement = findResult;

                if (previousFoundElement.FindInfo.UiElementType == UiElementType.Frame)
                    nativeFrameElement = findResult;

                if (previousFoundElement.FindInfo.UiElementType == UiElementType.ShadowRootHost)
                    nativeShadowRootHostElement = findResult;
            }

            for (var i = startSearchFromIndex; i < findChain.Count; i++)
            {
                var findParameters = findChain[i];

                if (findParameters.FindInfo.UiElementType == UiElementType.Standard)
                {
                    if (findParameters.IsList)
                    {
                        findResultList = FindNativeElements(findChain, i);
                        return findResultList;
                    }
                    else
                    {
                        nativeElement = FindNativeElements(findChain, i, shouldSingleElementBeFound: true).Single();
                    }
                }

                else if (findParameters.FindInfo.UiElementType == UiElementType.Frame)
                {
                    if (findParameters.IsList)
                    {
                        findResultList = FindNativeFrameElements(findChain, i);
                        return findResultList;
                    }
                    else
                    {
                        nativeFrameElement = FindNativeFrameElements(findChain, i, shouldSingleElementBeFound: true).Single();
                    }
                }

                else if (findParameters.FindInfo.UiElementType == UiElementType.ShadowRootHost)
                {
                    if (findParameters.IsList)
                    {
                        findResultList = FindNativeShadowRootHostElements(findChain, i);
                        return findResultList;
                    }
                    else
                    {
                        nativeShadowRootHostElement = FindNativeShadowRootHostElements(findChain, i, shouldSingleElementBeFound: true).Single();
                    }
                }
            }

            //When single element is searched, it is added to artificial list to be processed on the upper level.
            var uiElementType = findChain.Last().FindInfo.UiElementType;

            if (uiElementType == UiElementType.Standard)
                findResultList.Add(nativeElement);
            else if (uiElementType == UiElementType.Frame)
                findResultList.Add(nativeFrameElement);
            else if (uiElementType == UiElementType.ShadowRootHost)
                findResultList.Add(nativeShadowRootHostElement);

            return findResultList;
        }

        #endregion  Base Methods to Find Elements

        #region Technology Related Methods to Find Elements used by Based Methods

        protected abstract List<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> FindNativeElements(List<IFindParametersWithSearchResult> findParametersChain, int index, bool shouldSingleElementBeFound = false);
        protected abstract List<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> FindNativeFrameElements(List<IFindParametersWithSearchResult> findParametersChain, int index, bool shouldSingleElementBeFound = false);
        protected abstract List<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> FindNativeShadowRootHostElements(List<IFindParametersWithSearchResult> findParametersChain, int index, bool shouldSingleElementBeFound = false);

        #endregion Technology Related Methods to Find Elements used by Based Methods

        #region Auxiliary

        private IUiElement? FindLastPreviousElementWithFindInfoSet(IUiElementInfo info)
        {
            var parentContainer = info.InstantiationStage.ParentContainer;

            while (parentContainer != null)
            {

                if (parentContainer.Info.FindStage?.FindParametersWithSearchResult != null)
                    return parentContainer;

                parentContainer = parentContainer.Info.InstantiationStage.ParentContainer;
            }

            return parentContainer;
        }

        //protected TNativeElement SingleNativeElementOrException(List<TNativeElement> elements, IFindParametersWithSearchResult findParameters)
        //{
        //    if (elements.Count != 1)
        //        throw new Exception($"Single NativeElement was expected, but '{elements?.Count}' were found while searching for find chain UI element '{findParameters.Name}' using method and criteria: '{findParameters}'.");

        //    return elements.Single();
        //}

        protected TNativeFrameElement SingleNativeFrameElementOrException(List<TNativeFrameElement> elements, IFindParametersWithSearchResult findParameters)
        {
            if (elements.Count != 1)
                throw new Exception($"Single NativeFrameElement was expected, but '{elements?.Count}' were found while searching for find chain UI element '{findParameters.Name}' using method and criteria: '{findParameters.FindInfo.FramesFindOption}'.");

            return elements.Single();
        }

        protected TNativeShadowRootHostElement SingleNativeShadowRootElementOrException(List<TNativeShadowRootHostElement> elements, IFindParametersWithSearchResult findParameters)
        {
            if (elements.Count != 1)
                throw new Exception(@$"Single NativeShadowRootHostElement element was expected, but '{elements?.Count}' were found while searching for find chain UI element '{findParameters.Name}' using method and criteria: '{findParameters.FindInfo.ShadowRootHostsFindOption}'.");

            return elements.Single();
        }

        protected List<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> GetFindParametersResultsList<T>(List<T> elements, IFindParametersWithSearchResult findParametersWithSearchResult, bool shouldSingleElementBeFound, object? nativeFrameElement = null, object? nativeShadowRootHostElement = null)
        {
            List<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> list = new();
            findParametersWithSearchResult.TotalAmountOfElementsFound = elements.Count;

            //Check whether strictly only single element was expected
            if (shouldSingleElementBeFound && elements.Count != 1)
            {
                var exception = new Exception($"Single native element was expected, but {elements.Count} native elements were found while searching for find chain UI element '{findParametersWithSearchResult.Name}' using method and criteria: '{findParametersWithSearchResult}' .");
                findParametersWithSearchResult.Exception = exception;
                throw exception;
            }

            var indexOf = findParametersWithSearchResult.FindChain.IndexOf(findParametersWithSearchResult);

            for (var i = 0; i < elements.Count; i++)
            {
                int? uiIndex = findParametersWithSearchResult.IsList ? i + 1 : null; // If search was done of Single/Distinct UiElement, that is not UiElementsList, then uiIndex = null

                //T parameter can be of 3 types
                object? nativeElement = null;

                if (findParametersWithSearchResult.FindInfo.UiElementType == UiElementType.Standard)
                    nativeElement = elements[i];

                else if (findParametersWithSearchResult.FindInfo.UiElementType == UiElementType.Frame)
                    nativeFrameElement = elements[i];

                else if (findParametersWithSearchResult.FindInfo.UiElementType == UiElementType.ShadowRootHost)
                    nativeShadowRootHostElement = elements[i];

                IFindParametersWithSearchResult elementFindParametersWithSearchResult = new FindParametersWithSearchResult(findParametersWithSearchResult.Name, findParametersWithSearchResult.FindInfo,
                    findParametersWithSearchResult.NestingLevel,
                    findParametersWithSearchResult.Parent,
                    isList: false, uiIndex: uiIndex,
                    nativeElement: nativeElement,
                    nativeFrameElement: nativeFrameElement,
                    nativeShadowRootHostElement: nativeShadowRootHostElement,
                    findChain: findParametersWithSearchResult.FindChain,
                    totalAmountOfElementsFound: findParametersWithSearchResult.TotalAmountOfElementsFound,
                    uriBeforeSearch: findParametersWithSearchResult.UriBeforeSearch);

                //In case when single element is searched, then reference on the same find chain can be used.
                if (shouldSingleElementBeFound)
                    elementFindParametersWithSearchResult.FindChain[indexOf] = elementFindParametersWithSearchResult;

                //In case when not single element is searched, but list of elements, each such element should have its own FindParameters with its own Index.
                //As a result there is need to clone findChain for each element.
                else if (!shouldSingleElementBeFound)
                {
                    List<IFindParametersWithSearchResult>? elementFindParametersWithSearchResultChain = findParametersWithSearchResult.FindChain.Select(x => new FindParametersWithSearchResult(x) as IFindParametersWithSearchResult).ToList(); //TODO Make Clone Method or this one may be fine cause objects with native elements may have same reference

                    //As far as search by particular find parameters (particular index) inside find chain happened, substitution of find parameters in find chain happens with the results.
                    elementFindParametersWithSearchResultChain[indexOf] = elementFindParametersWithSearchResult;
                    elementFindParametersWithSearchResult.FindChain = elementFindParametersWithSearchResultChain;
                }

                var currentUri = GetCurrentUri();
                IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement> result = new FindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>(elementFindParametersWithSearchResult, currentUri);
                list.Add(result);
            }

            return list;
        }

        protected abstract Uri GetCurrentUri();

        #endregion Auxiliary
    }
}
