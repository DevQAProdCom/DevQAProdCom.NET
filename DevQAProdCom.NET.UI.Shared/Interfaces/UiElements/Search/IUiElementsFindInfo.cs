using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search
{
    public interface IUiElementsFindInfo
    {
        public UiElementType UiElementType { get; }
        public SearchKind SearchKind { get; }
        public FindOrderType FindOrderType { get; }
        public IFindOption? ElementsFindOption { get; }
        public IFindOption? FramesFindOption { get; }
        public IFindOption? ShadowRootHostsFindOption { get; }
    }
}
