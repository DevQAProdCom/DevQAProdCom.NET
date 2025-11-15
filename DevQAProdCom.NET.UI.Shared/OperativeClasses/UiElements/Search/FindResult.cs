using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search
{
    public class FindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement> : IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>
       where TNativeElement : class
       where TNativeFrameElement : class
       where TNativeShadowRootHostElement : class
    {
        public TNativeElement? NativeElement
        {
            get
            {
                if (FindParametersWithSearchResult?.NativeElement is TNativeElement)
                    return FindParametersWithSearchResult.NativeElement as TNativeElement;

                return null;
            }
        }

        public TNativeFrameElement? NativeFrameElement
        {
            get
            {
                if (FindParametersWithSearchResult?.NativeFrameElement is TNativeFrameElement)
                    return FindParametersWithSearchResult.NativeFrameElement as TNativeFrameElement;

                return null;
            }
        }

        public TNativeShadowRootHostElement? NativeShadowRootHostElement
        {
            get
            {
                if (FindParametersWithSearchResult?.NativeShadowRootHostElement is TNativeShadowRootHostElement)
                    return FindParametersWithSearchResult.NativeShadowRootHostElement as TNativeShadowRootHostElement;

                return null;
            }
        }

        public IFindParametersWithSearchResult FindParametersWithSearchResult { get; }
        public Uri? Uri { get; internal set; }

        public FindResult(IFindParametersWithSearchResult findParametersWithSearchResult, Uri? uri = null)
        {
            FindParametersWithSearchResult = findParametersWithSearchResult;
            Uri = uri;
        }
    }
}
