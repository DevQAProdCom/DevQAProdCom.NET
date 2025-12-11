using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search
{
    public class FindParametersWithSearchResult : FindParameters, IFindParametersWithSearchResult
    {
        public FindState FindState { get => GetFindState(); }
        public Uri? UriBeforeSearch { get; set; }
        public Uri? UriAfterSearch { get; set; }
        public List<IFindParametersWithSearchResult>? FindChain { get; set; }
        public int? TotalAmountOfElementsFound { get; set; }
        public Exception? Exception { get; set; }

        public FindParametersWithSearchResult(string name, IUiElementsFindInfo findOption, uint nestingLevel, IUiElement? parent = null, bool isList = false, int? uiIndex = null,
            object? nativeElement = null, object? nativeFrameElement = null, object? nativeShadowRootHostElement = null,
            List<IFindParametersWithSearchResult>? findChain = null, int? totalAmountOfElementsFound = null, Exception? exception = null, Uri? uriBeforeSearch = null)
            : base(name: name, findOption: findOption, nestingLevel: nestingLevel, parent: parent, isList: isList, uiIndex: uiIndex, nativeElement: nativeElement, nativeFrameElement: nativeFrameElement, nativeShadowRootHostElement: nativeShadowRootHostElement)
        {
            FindChain = findChain;
            TotalAmountOfElementsFound = totalAmountOfElementsFound;
            Exception = exception;
            UriBeforeSearch = uriBeforeSearch;
        }

        public FindParametersWithSearchResult(IFindParameters findParameters, List<IFindParametersWithSearchResult>? findChain = null, int? totalAmountOfElementsFound = null, Exception? exception = null, Uri? uriBeforeSearch = null)
            : this(findParameters.Name, findParameters.FindInfo, findParameters.NestingLevel, findParameters.Parent, findParameters.IsList, findParameters.UiIndex, findParameters.NativeElement, findParameters.NativeFrameElement, findParameters.NativeShadowRootHostElement,
                 findChain: findChain, totalAmountOfElementsFound: totalAmountOfElementsFound, exception: exception, uriBeforeSearch: uriBeforeSearch)
        {

        }

        public FindParametersWithSearchResult(IFindParametersWithSearchResult findParametersWithSearchResult)
            : this(findParametersWithSearchResult.Name, findParametersWithSearchResult.FindInfo, findParametersWithSearchResult.NestingLevel, findParametersWithSearchResult.Parent, findParametersWithSearchResult.IsList,
                  findParametersWithSearchResult.UiIndex, findParametersWithSearchResult.NativeElement, findParametersWithSearchResult.NativeFrameElement, findParametersWithSearchResult.NativeShadowRootHostElement,
                  findChain: findParametersWithSearchResult.FindChain, totalAmountOfElementsFound: findParametersWithSearchResult.TotalAmountOfElementsFound, exception: findParametersWithSearchResult.Exception, uriBeforeSearch: findParametersWithSearchResult.UriBeforeSearch)
        {

        }

        private FindState GetFindState()
        {
            if (Exception != null)
                return FindState.Failed;

            if (TotalAmountOfElementsFound == 0)
                return FindState.NotFound;

            if (TotalAmountOfElementsFound > 0)
                return FindState.Found;

            return FindState.NotSearched;
        }
    }
}
