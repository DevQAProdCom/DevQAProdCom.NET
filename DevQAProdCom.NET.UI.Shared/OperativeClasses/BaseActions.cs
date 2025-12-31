using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses
{
    public abstract class BaseActions
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
                if (_uiInteractor != null)
                    return _uiInteractor;

                if (UiInteractorsManagersProvider != null)
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

        public BaseActions(IUiInteractorsManagersProvider uiInteractorsManagersProvider)
        {
            UiInteractorsManagersProvider = uiInteractorsManagersProvider;
        }

        public BaseActions(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab)
        {
            _uiInteractor = uiInteractor;
            _tabName = tabName;
        }
    }
}
