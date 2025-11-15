namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search
{
    public interface IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>
        where TNativeElement : class
        where TNativeFrameElement : class
        where TNativeShadowRootHostElement : class
    {
        TNativeElement? NativeElement { get; }
        TNativeFrameElement? NativeFrameElement { get; }
        TNativeShadowRootHostElement? NativeShadowRootHostElement { get; }

        IFindParametersWithSearchResult FindParametersWithSearchResult { get; }
        Uri Uri { get; }
    }
}
