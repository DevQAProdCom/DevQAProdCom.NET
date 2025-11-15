using DevQAProdCom.NET.Logging.Shared.Constans;

namespace DevQAProdCom.NET.UI.Shared.Logging.Models
{
    public class UiElementsSearchLoggingModelWithSearchResultDebug : UiElementsSearchLoggingModel
    {
        public UiElementsSearchResultLoggingModelInfo SearchResult { get; }

        public UiElementsSearchLoggingModelWithSearchResultDebug(UiElementsSearchResultLoggingModelVerbose uiElementsSearchResultLoggingModelVerbose)
            : base(uiElementsSearchResultLoggingModelVerbose.GetUiElementInfo(), SharedLoggingConstants.SearchResult)
        {
            SearchResult = new UiElementsSearchResultLoggingModelInfo(uiElementsSearchResultLoggingModelVerbose);
        }
    }
}
