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
        public TUiElement InstantiateUiElement<TUiElement>(string name, Use method, string criteria, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement;
        public TUiElement InstantiateUiElement<TUiElement>(string name, List<IUiElementsFindInfo> findOptions, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement;

        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(IUiElementInfo uiElementInfo) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(string name, Use method, string criteria, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement;
        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(string name, List<IUiElementsFindInfo> findOptions, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement;
    }
}
