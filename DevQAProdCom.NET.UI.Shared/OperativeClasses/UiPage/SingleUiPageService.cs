using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public abstract class SingleUiPageService<T> : UiPageService, ISingleUiPageService where T : IUiPage
    {
        protected readonly internal T _page;

        public SingleUiPageService(IUiInteractor uiInteractor, string tabName) : base(uiInteractor, tabName)
        {
            _page = _uiTab.GetPage<T>();
        }

        public SingleUiPageService(IUiInteractor uiInteractor) : this(uiInteractor, SharedUiConstants.DefaultUiInteractorTab)
        {
        }

        public SingleUiPageService(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab, string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) : base(uiInteractor, tabName)
        {
            _page = _uiTab.GetPage<T>(applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri);
        }

        public virtual void GoToPage(params KeyValuePair<string, string>[] placeholderValues)
        {
            _page.GoTo(placeholderValues);
        }

        public virtual void WaitForLoad()
        {
            _page.WaitForLoaded();
        }

        public virtual Uri GetPageUrl(params KeyValuePair<string, string>[] placeholderValues)
        {
            return _page.GetDefinedUri(placeholderValues);
        }
    }
}
