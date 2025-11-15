namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search
{
    public interface INativeElementsSearcher
    {
        IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement> FindElement<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>(IUiElementInfo uiElementInfo)
            where TNativeElement : class
            where TNativeFrameElement : class
            where TNativeShadowRootHostElement : class;

        IList<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> FindElements<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>(IUiElementInfo uiElementsInfo)
            where TNativeElement : class
            where TNativeFrameElement : class
            where TNativeShadowRootHostElement : class;
    }
}
