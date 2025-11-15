using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search
{
    public class FindParameters : IFindParameters
    {
        public Guid Id { get; } = Guid.NewGuid();
        public IUiElementsFindInfo FindInfo { get; }
        public uint NestingLevel { get; }
        public string Name { get; }
        public IUiElement? Parent { get; }

        public int? _uiIndex;

        public int? UiIndex
        {
            get { return _uiIndex; }
            internal set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException($"Index of UI element should be more than or equal 1.");

                _uiIndex = value;
            }
        }

        private bool _isList = false;

        public bool IsList
        {
            get
            {
                if (UiIndex >= 1)
                    _isList = false;

                return _isList;
            }
            private set { _isList = value; }
        }

        public bool IsElementOfList => UiIndex >= 1;

        public object? NativeElement { get; internal set; }
        public object? NativeFrameElement { get; internal set; }
        public object? NativeShadowRootHostElement { get; internal set; }

        public bool IsElementInsideFrame => ElementInFrameSearchKindList.Contains(FindInfo.SearchKind);

        private List<SearchKind> ElementInFrameSearchKindList = new() { SearchKind.ElementsInFrame, SearchKind.ElementsInFrameInsideShadowRoot, SearchKind.ElementsInShadowRootInsideFrame, SearchKind.ShadowRootHostElementsInsideFrame };

        public FindParameters(string name, IUiElementsFindInfo findOption, uint nestingLevel, IUiElement? parent = null, bool isList = false, int? uiIndex = null,
            object? nativeElement = null, object? nativeFrameElement = null, object? nativeShadowRootHostElement = null)
        {
            Name = name;
            Parent = parent;
            UiIndex = uiIndex;
            IsList = isList;
            FindInfo = findOption;
            NestingLevel = nestingLevel;

            NativeElement = nativeElement;
            NativeFrameElement = nativeFrameElement;
            NativeShadowRootHostElement = nativeShadowRootHostElement;
        }

        public FindParameters(IFindParameters findParameters)
            : this(findParameters.Name, findParameters.FindInfo, findParameters.NestingLevel, findParameters.Parent, findParameters.IsList, findParameters.UiIndex, findParameters.NativeElement, findParameters.NativeFrameElement, findParameters.NativeShadowRootHostElement) { }

        public override string ToString()
        {
            string GetUiIndex() => UiIndex != null ? $"[UiIndex: {UiIndex}]" : string.Empty;
            string elementsFindInfo = $"{SharedUiConstants.ElementsSemicolon} {FindInfo.ElementsFindOption}{GetUiIndex()}";
            string framesFindInfo = $"{SharedUiConstants.FramesSemicolon} {FindInfo.FramesFindOption}{GetUiIndex()}";
            string shadowRootHostsFindInfo = $"{SharedUiConstants.ShadowRootHostsSemicolon} {FindInfo.ShadowRootHostsFindOption}{GetUiIndex()}";

            string findInfoString = FindInfo.SearchKind switch
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
                _ => $"Not supported search order. {nameof(UiElementsFindInfo)}: {elementsFindInfo}; {framesFindInfo}; {shadowRootHostsFindInfo}. {nameof(Enumerations.FindOrderType)}: '{FindInfo.FindOrderType}'."
            };

            return $"{SharedUiConstants.NameSemicolon} \"{Name}\". {SharedUiConstants.FindParametersSemicolon} <{findInfoString}>.";
        }
    }
}
