using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search
{
    public class UiElementsFindInfo : IUiElementsFindInfo
    {
        public IFindOption? ElementsFindOption { get; }
        public IFindOption? FramesFindOption { get; }
        public IFindOption? ShadowRootHostsFindOption { get; }

        public Enumerations.FindOrderType FindOrderType { get; }
        public Enumerations.SearchKind SearchKind { get; }
        public Enumerations.UiElementType UiElementType { get; }

        public UiElementsFindInfo(string? elementsFindMethod = null, string? elementsFindCriteria = null,
            string? framesFindMethod = null, string? framesFindCriteria = null,
            string? shadowRootHostsFindMethod = null, string? shadowRootHostsFindCriteria = null,
            FindOrderType findOrderType = FindOrderType.NotSet)
        {
            FindOption elementFindOption = new FindOption(elementsFindMethod!, elementsFindCriteria!);
            if (elementFindOption.IsSet)
                ElementsFindOption = elementFindOption;

            FindOption frameFindOption = new FindOption(framesFindMethod!, framesFindCriteria!);
            if (frameFindOption.IsSet)
                FramesFindOption = frameFindOption;

            FindOption shadowRootHostFindOption = new FindOption(shadowRootHostsFindMethod!, shadowRootHostsFindCriteria!);
            if (shadowRootHostFindOption.IsSet)
                ShadowRootHostsFindOption = shadowRootHostFindOption;

            FindOrderType = findOrderType;
            SearchKind = DetermineSearchKind();
            UiElementType = DetermineUiElementType();
        }

        private SearchKind DetermineSearchKind()
        {
            if (IsFindOptionSet(ElementsFindOption) && !IsFindOptionSet(FramesFindOption) && !IsFindOptionSet(ShadowRootHostsFindOption))
                return Enumerations.SearchKind.ElementsWithNoWrap;

            if (IsFindOptionSet(ElementsFindOption) && IsFindOptionSet(FramesFindOption) && !IsFindOptionSet(ShadowRootHostsFindOption))
                return Enumerations.SearchKind.ElementsInFrame;

            if (IsFindOptionSet(ElementsFindOption) && IsFindOptionSet(FramesFindOption) && IsFindOptionSet(ShadowRootHostsFindOption) && FindOrderType == Enumerations.FindOrderType.FrameInsideShadowRoot)
                return Enumerations.SearchKind.ElementsInFrameInsideShadowRoot;

            if (IsFindOptionSet(ElementsFindOption) && !IsFindOptionSet(FramesFindOption) && IsFindOptionSet(ShadowRootHostsFindOption))
                return Enumerations.SearchKind.ElementsInShadowRoot;

            if (IsFindOptionSet(ElementsFindOption) && IsFindOptionSet(FramesFindOption) && IsFindOptionSet(ShadowRootHostsFindOption) && FindOrderType == Enumerations.FindOrderType.ShadowRootInsideFrame)
                return Enumerations.SearchKind.ElementsInShadowRootInsideFrame;

            if (!IsFindOptionSet(ElementsFindOption) && IsFindOptionSet(FramesFindOption) && !IsFindOptionSet(ShadowRootHostsFindOption))
                return Enumerations.SearchKind.FrameElements;

            if (!IsFindOptionSet(ElementsFindOption) && IsFindOptionSet(FramesFindOption) && IsFindOptionSet(ShadowRootHostsFindOption) && FindOrderType == Enumerations.FindOrderType.FrameInsideShadowRoot)
                return Enumerations.SearchKind.FrameElementsInsideShadowRoot;

            if (!IsFindOptionSet(ElementsFindOption) && !IsFindOptionSet(FramesFindOption) && IsFindOptionSet(ShadowRootHostsFindOption))
                return Enumerations.SearchKind.ShadowRootHostElements;

            if (!IsFindOptionSet(ElementsFindOption) && IsFindOptionSet(FramesFindOption) && IsFindOptionSet(ShadowRootHostsFindOption) && FindOrderType == Enumerations.FindOrderType.ShadowRootInsideFrame)
                return Enumerations.SearchKind.ShadowRootHostElementsInsideFrame;

            return Enumerations.SearchKind.NotSet;
        }

        public UiElementType DetermineUiElementType()
        {
            List<SearchKind> standardElementsSearchKinds = new List<SearchKind>()
            {
                SearchKind.ElementsWithNoWrap,
                SearchKind.ElementsInFrame, SearchKind.ElementsInFrameInsideShadowRoot,
                SearchKind.ElementsInShadowRoot, SearchKind.ElementsInShadowRootInsideFrame
            };
            List<SearchKind> frameElementsSearchKinds = new List<SearchKind>() { SearchKind.FrameElements, SearchKind.FrameElementsInsideShadowRoot };
            List<SearchKind> shadowRootHostElementsSearchKinds = new List<SearchKind>() { SearchKind.ShadowRootHostElements, SearchKind.ShadowRootHostElementsInsideFrame };

            return SearchKind switch
            {
                Enumerations.SearchKind kind when standardElementsSearchKinds.Contains(kind) => UiElementType.Standard,
                Enumerations.SearchKind kind when frameElementsSearchKinds.Contains(kind) => UiElementType.Frame,
                Enumerations.SearchKind kind when shadowRootHostElementsSearchKinds.Contains(kind) => UiElementType.ShadowRootHost,
                _ => throw new Exception($"Not supported '{nameof(Enumerations.UiElementType)}' for {nameof(UiElementsFindInfo)} parameters: {ToString()}")
            };
        }

        private bool IsFindOptionSet(IFindOption? findOption)
        {
            return findOption != null && findOption.IsSet;
        }

        public override string ToString()
        {
            string elementsFindInfo = $"{SharedUiConstants.ElementsSemicolon} {ElementsFindOption}";
            string framesFindInfo = $"{SharedUiConstants.FramesSemicolon} {FramesFindOption}";
            string shadowRootHostsFindInfo = $"{SharedUiConstants.ShadowRootHostsSemicolon} {ShadowRootHostsFindOption}";

            return SearchKind switch
            {
                Enumerations.SearchKind.ElementsWithNoWrap => elementsFindInfo,
                Enumerations.SearchKind.ElementsInFrame => $"{framesFindInfo} -> {elementsFindInfo}",
                Enumerations.SearchKind.ElementsInFrameInsideShadowRoot => $"{shadowRootHostsFindInfo} -> {framesFindInfo} -> {elementsFindInfo}",
                Enumerations.SearchKind.ElementsInShadowRoot => $"{shadowRootHostsFindInfo} -> {elementsFindInfo}",
                Enumerations.SearchKind.ElementsInShadowRootInsideFrame => $"{framesFindInfo} -> {shadowRootHostsFindInfo} -> {elementsFindInfo}",
                Enumerations.SearchKind.FrameElements => framesFindInfo,
                Enumerations.SearchKind.FrameElementsInsideShadowRoot => $"{shadowRootHostsFindInfo} -> {framesFindInfo}",
                Enumerations.SearchKind.ShadowRootHostElements => shadowRootHostsFindInfo,
                Enumerations.SearchKind.ShadowRootHostElementsInsideFrame => $"{framesFindInfo} -> {shadowRootHostsFindInfo}",
                _ => $"Not supported search order. {nameof(UiElementsFindInfo)}: {elementsFindInfo}; {framesFindInfo}; {shadowRootHostsFindInfo}. {nameof(Enumerations.FindOrderType)}: '{FindOrderType}'."
            };
        }
    }
}
