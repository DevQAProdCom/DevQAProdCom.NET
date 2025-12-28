using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public abstract class SingleUiPageActions<T> : UiPageActions, ISingleUiPageActions where T : IUiPage
    {
        // Properties were added to support lazy loading of "T Page" for use cases where ApplicationName, PageName, BaseUri, or RelativeUri are passed in specific constructor.
        private string? _applicationName;
        private string? ApplicationName
        {
            get
            {
                if (_page != null)
                    _applicationName = _page.ApplicationName;

                return _applicationName;
            }
        }

        private string? _pageName;
        private string? PageName
        {
            get
            {
                if (_page != null)
                    _pageName = _page.PageName;
                return _pageName;
            }
        }

        private string? _baseUri;
        private string? BaseUri
        {
            get
            {
                if (_page != null)
                    _baseUri = _page.BaseUri;
                return _baseUri;
            }
        }

        private string? _relativeUri;
        private string? RelativeUri
        {
            get
            {
                if (_page != null)
                    _relativeUri = _page.RelativeUri;
                return _relativeUri;
            }
        }

        protected T? _page;
        public virtual T Page
        {
            get
            {
                if (_page == null)
                    _page = UiTab.GetPage<T>(applicationName: ApplicationName, pageName: PageName, baseUri: BaseUri, relativeUri: RelativeUri);

                return _page;
            }
        }

        public SingleUiPageActions(IUiInteractorsManagersProvider uiInteractorsManagersProvider) : base(uiInteractorsManagersProvider)
        {
        }

        public SingleUiPageActions(IUiInteractor uiInteractor, string tabName) : base(uiInteractor, tabName)
        {
        }

        public SingleUiPageActions(IUiInteractor uiInteractor) : this(uiInteractor, SharedUiConstants.DefaultUiInteractorTab)
        {
        }

        public SingleUiPageActions(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab, string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) : base(uiInteractor, tabName)
        {
            _applicationName = applicationName;
            _pageName = pageName;
            _baseUri = baseUri;
            _relativeUri = relativeUri;
        }

        public virtual void GoToPage(params KeyValuePair<string, string>[] placeholderValues)
        {
            Page.GoTo(placeholderValues);
        }

        public virtual void WaitForLoad()
        {
            Page.WaitForLoaded();
        }

        public virtual Uri GetPageUrl(params KeyValuePair<string, string>[] placeholderValues)
        {
            return Page.GetDefinedUri(placeholderValues);
        }
    }
}
