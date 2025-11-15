namespace DevQAProdCom.NET.UI.Shared.Logging.Models
{
    public class UiElementsSearchLoggingModelWithSearchResultVerbose : UiElementsSearchLoggingModelWithSearchResultDebug
    {
        public new UiElementsSearchResultLoggingModelVerbose SearchResult { get; }

        public UiElementsSearchLoggingModelWithSearchResultVerbose(UiElementsSearchResultLoggingModelVerbose searchResult) : base(searchResult)
        {
            SearchResult = searchResult;
        }
    }
}
