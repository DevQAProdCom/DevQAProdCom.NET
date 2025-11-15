using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.Logging.Models
{
    public class UiElementsSearchResultLoggingModelBase
    {
        protected IUiElementInfo UiElementInfo;

        protected Uri? _uriBeforeSearch
        {
            get
            {
                //As far as there is different combination of search, but it is the same UiElement seached, there should be the same Page and as a result the same Uri within all find chain.
                var union = FoundFindChains.Union(NotFoundFindChains).Union(FailedFindChains);
                if (union.Any())
                {
                    var uris = union.Select(x => x.Chain).SelectMany(x => x.Where(x => x.UriBeforeSearch != null).Select(x => x.UriBeforeSearch)).Distinct().ToList();

                    if (uris.Count() == 0)
                        return null;

                    else if (uris.Count() == 1)
                        return uris.Single();

                    else //if (uris.Count() > 1)
                        throw new InvalidOperationException($"Unable to determine the Uri before search for the UiElement as there are multiple different Uris within the find chains: {string.Join(",", uris)}");
                }

                return null;
            }
        }

        protected Uri? _uriAfterSearch
        {
            get
            {
                //As far as there is different combination of search, but it is the same UiElement seached, there should be the same Page and as a result the same Uri within all find chain.
                var union = FoundFindChains.Union(NotFoundFindChains).Union(FailedFindChains);
                if (union.Any())
                {
                    var uris = union.Select(x => x.Chain).SelectMany(x => x.Where(x => x.UriAfterSearch != null).Select(x => x.UriAfterSearch)).Distinct().ToList();

                    if (uris.Count() == 0)
                        return null;

                    else if (uris.Count() == 1)
                        return uris.Single();

                    else //if (uris.Count() > 1)
                        throw new InvalidOperationException($"Unable to determine the Uri after search for the UiElement as there are multiple different Uris within the find chains: {string.Join(",", uris)}");
                }

                return null;
            }
        }

        protected List<(int ChainExecutionOrder, List<IFindParametersWithSearchResult> Chain)> FoundFindChains { get; set; } = new();
        protected List<(int ChainExecutionOrder, List<IFindParametersWithSearchResult> Chain)> NotFoundFindChains { get; set; } = new();
        protected List<(int ChainExecutionOrder, List<IFindParametersWithSearchResult> Chain)> FailedFindChains { get; set; } = new();

        public string UiElementFullName
        {
            get
            {
                return UiElementInfo.InstantiationStage.FullName;
            }
        }

        public string UiElementName
        {
            get
            {
                return UiElementInfo.InstantiationStage.Name;
            }
        }

        private FindState? _generalFindState;
        public FindState GeneralFindState
        {
            get
            {
                if (_generalFindState == null)
                {
                    if (GeneralException != null)
                        return FindState.Failed;

                    if (FoundFindChains.Count > 0)
                        _generalFindState = FindState.Found;
                    else if (FoundFindChains.Count == 0 && NotFoundFindChains.Count > 0)
                        _generalFindState = FindState.NotFound;
                    else if (FoundFindChains.Count == 0 && NotFoundFindChains.Count == 0 && FailedFindChains.Count > 0)
                        _generalFindState = FindState.Failed;
                    else if (FoundFindChains.Count == 0 && NotFoundFindChains.Count == 0 && FailedFindChains.Count == 0)
                        _generalFindState = FindState.NotSearched;
                    else throw new InvalidOperationException("Unable to determine the general find state.");
                }

                return _generalFindState.Value;
            }
        }

        internal Exception? GeneralException { get; set; }
        public string? GeneralExceptionMessage { get { return GeneralException?.Message; } }

        public int TotalAmountOfFindChainsProcessed
        {
            get
            {
                return FoundFindChains.Count + NotFoundFindChains.Count + FailedFindChains.Count;
            }
        }

        public Dictionary<string, Dictionary<string, List<FindParameterResultLoggingModel>>> FindChains
        {
            get
            {
                Dictionary<string, Dictionary<string, List<FindParameterResultLoggingModel>>> dict = new();

                dict.Add(FindState.Found.GetDescriptionAttributeValue(), ToLoggingFormat(FoundFindChains));
                dict.Add(FindState.NotFound.GetDescriptionAttributeValue(), ToLoggingFormat(NotFoundFindChains));
                dict.Add(FindState.Failed.GetDescriptionAttributeValue(), ToLoggingFormat(FailedFindChains));

                return dict;
            }
        }

        public UiElementsSearchResultLoggingModelBase(IUiElementInfo uiElementInfo)
        {
            UiElementInfo = uiElementInfo;
        }

        public UiElementsSearchResultLoggingModelBase(UiElementsSearchResultLoggingModelBase baseModel)
        {
            UiElementInfo = baseModel.GetUiElementInfo();
            FoundFindChains = baseModel.FoundFindChains;
            NotFoundFindChains = baseModel.NotFoundFindChains;
            FailedFindChains = baseModel.FailedFindChains;
            GeneralException = baseModel.GeneralException;
        }

        public void AddFoundFindChain(int chainExecutionOrder, List<IFindParametersWithSearchResult> findChain)
        {
            FoundFindChains.Add((chainExecutionOrder, findChain));
        }

        //public void SetFoundFindChains(List<(int ChainExecutionOrder, List<IFindParametersWithSearchResult> Chain)> chains)
        //{
        //    _foundFindChains = chains;
        //}

        public void AddNotFoundFindChain(int chainExecutionOrder, List<IFindParametersWithSearchResult> findChain)
        {
            NotFoundFindChains.Add((chainExecutionOrder, findChain));
        }

        //public void SetNotFoundFindChains(List<(int ChainExecutionOrder, List<IFindParametersWithSearchResult> Chain)> chains)
        //{
        //    _notFoundFindChains = chains;
        //}

        public void AddFailedFindChain(int chainExecutionOrder, List<IFindParametersWithSearchResult> findChain)
        {
            FailedFindChains.Add((chainExecutionOrder, findChain));
        }

        //public void SetFailedFindChains(List<(int ChainExecutionOrder, List<IFindParametersWithSearchResult> Chain)> chains)
        //{
        //    _failedFindChains = chains;
        //}

        protected FindParameterResultLoggingModel ToFindParameterResultLoggingModel(IFindParametersWithSearchResult result)
        {
            FindParameterResultLoggingModel model = new();

            model.NestingLevel = result.NestingLevel;
            model.Name = result.Name;
            model.FindState = result.FindState;
            model.IsElementOfList = result.IsElementOfList;
            model.UiIndex = result.UiIndex;
            model.FindInfo = new UiElementsFindInfoLoggingModel(result.FindInfo);
            model.TotalAmountOfElementsFound = result.TotalAmountOfElementsFound;
            model.ExceptionMessage = result.Exception?.Message;
            model.StackTrace = result.Exception?.StackTrace;

            return model;
        }

        protected Dictionary<string, List<FindParameterResultLoggingModel>> ToLoggingFormat(List<(int ChainExecutionOrder, List<IFindParametersWithSearchResult> Chain)> chains)
        {
            Dictionary<string, List<FindParameterResultLoggingModel>> dict = new();

            foreach (var chain in chains)
            {
                var list = new List<FindParameterResultLoggingModel>();

                foreach (var findParameter in chain.Chain)
                {
                    FindParameterResultLoggingModel logModel = ToFindParameterResultLoggingModel(findParameter);
                    list.Add(logModel);
                }

                dict.Add($"Chain {chain.ChainExecutionOrder}", list);
            }

            return dict;
        }

        private const string UriIsNotSetCheckLoggingLevel = "Uri string is not set. Check logging level parameters.";

        public string? GetUriBeforeSearchString()
        {
            if (_uriBeforeSearch != null)
                return _uriBeforeSearch?.ToString();

            return UriIsNotSetCheckLoggingLevel;
        }

        public string? GetUriAfterSearchString()
        {
            if (_uriAfterSearch != null)
                return _uriAfterSearch?.ToString();

            return UriIsNotSetCheckLoggingLevel;
        }

        public IUiElementInfo GetUiElementInfo()
        {
            return UiElementInfo;
        }
    }
}
