using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search
{
    public interface IUiElementsInstantiator
    {
        public void InstantiateUiElements(object asset);
        public List<TUiElement> InitializeUiElementsList<TUiElement, TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>
            (IUiElementInfo uiElementInfo, IList<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> nativeElements)
            where TUiElement : IUiElement
            where TNativeElement : class
            where TNativeFrameElement : class
            where TNativeShadowRootHostElement : class;

        public TUiElement InstantiateUiElement<TUiElement>(IUiElementInfo uiElementInfo, params KeyValuePair<string, object>[] nativeObjects) where TUiElement : IUiElement;
        public TUiElement InstantiateUiElement<TUiElement>(string method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;
        public TUiElement InstantiateUiElement<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;
        public TUiElement InstantiateUiElement<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;

        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(IUiElementInfo uiElementInfo) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(string method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement;
    }
}
