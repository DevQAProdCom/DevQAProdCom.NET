using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.Logging.Models
{
    public class UiElementsFindInfoLoggingModel
    {
        public UiElementType? UiElementType { get; }
        public SearchKind? SearchKind { get; }
        public FindOrderType? FindOrderType { get; }
        public FindOptionLoggingModel? ElementsFindOption { get; }
        public FindOptionLoggingModel? FramesFindOption { get; }
        public FindOptionLoggingModel? ShadowRootHostsFindOption { get; }

        public UiElementsFindInfoLoggingModel(IUiElementsFindInfo findInfo)
        {
            UiElementType = findInfo.UiElementType;
            SearchKind = findInfo.SearchKind;
            FindOrderType = findInfo.FindOrderType;
            ElementsFindOption = findInfo.ElementsFindOption != null ? new FindOptionLoggingModel(findInfo.ElementsFindOption) : null;
            FramesFindOption = findInfo.FramesFindOption != null ? new FindOptionLoggingModel(findInfo.FramesFindOption) : null;
            ShadowRootHostsFindOption = findInfo.ShadowRootHostsFindOption != null ? new FindOptionLoggingModel(findInfo.ShadowRootHostsFindOption) : null;
        }
    }
}
