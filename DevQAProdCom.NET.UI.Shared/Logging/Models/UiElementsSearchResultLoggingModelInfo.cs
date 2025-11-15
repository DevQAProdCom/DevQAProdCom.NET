using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace DevQAProdCom.NET.UI.Shared.Logging.Models
{
    public class UiElementsSearchResultLoggingModelInfo : UiElementsSearchResultLoggingModelBase
    {
        public string? UriAfterSearch
        {
            get
            {
                return GetUriAfterSearchString();
            }
        }

        public UiElementsSearchResultLoggingModelInfo(IUiElementInfo info) : base(info)
        {
        }

        public UiElementsSearchResultLoggingModelInfo(UiElementsSearchResultLoggingModelBase baseModel) : base(baseModel)
        {
        }
    }
}
