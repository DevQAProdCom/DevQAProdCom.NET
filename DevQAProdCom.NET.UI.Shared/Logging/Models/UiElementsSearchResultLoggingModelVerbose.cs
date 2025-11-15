using DevQAProdCom.NET.Logging.Shared.Models;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace DevQAProdCom.NET.UI.Shared.Logging.Models
{
    public class UiElementsSearchResultLoggingModelVerbose : UiElementsSearchResultLoggingModelInfo
    {
        public new UriLoggingModel? UriBeforeSearch
        {
            get
            {
                if (_uriBeforeSearch != null)
                    return new UriLoggingModel(_uriBeforeSearch);

                return null;
            }
        }

        public new UriLoggingModel? UriAfterSearch
        {
            get
            {
                if (_uriAfterSearch != null)
                    return new UriLoggingModel(_uriAfterSearch);

                return null;
            }
        }

        public double? SearchTimeMilliseconds { get; set; }

        public UiElementsSearchResultLoggingModelVerbose(IUiElementInfo info) : base(info)
        {
        }
    }
}
