using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public abstract class UiPageActions : IUiPageActions
    {
        private string _tabName;
        protected virtual string TabName
        {
            get
            {
                if (_tabName == null)
                    _tabName = SharedUiConstants.DefaultUiInteractorTab;

                return _tabName;
            }
        }

        protected internal readonly IUiInteractorsManagersProvider UiInteractorsManagersProvider;

        private IUiInteractor _uiInteractor;
        protected internal IUiInteractor UiInteractor
        {
            get
            {
                if (_uiInteractor == null && UiInteractorsManagersProvider != null)
                    _uiInteractor = UiInteractorsManagersProvider.GetUiInteractorOfCurrentThread();

                return _uiInteractor ?? throw new Exception("UiInteractor is not set in UiPageActions.");
            }
        }

        private IUiInteractorTab _uiTab;
        protected virtual IUiInteractorTab UiTab
        {
            get
            {
                if (_uiTab == null)
                    _uiTab = UiInteractor.GetTab(TabName);

                return _uiTab;
            }
        }

        public UiPageActions(IUiInteractorsManagersProvider uiInteractorsManagersProvider)
        {
            UiInteractorsManagersProvider = uiInteractorsManagersProvider;
        }

        public UiPageActions(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            _uiInteractor = uiInteractor;
            _tabName = tabName;
        }
    }
}
