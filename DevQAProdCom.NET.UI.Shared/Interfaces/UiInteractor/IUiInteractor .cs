using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor
{
    public interface IUiInteractor : IHaveIdentifiers, IOperateWithCookies, IMakeScreenshot
    {
        public IUiInteractorsManager UiInteractorsManager { get; }

        public void LaunchInteractor();
        public bool IsInteractorInteractable();
        public void DisposeInteractor();

        #region Tabs

        public IUiInteractorTab GetTab(string name = SharedUiConstants.DefaultTab);
        public void CloseTab(string name = SharedUiConstants.DefaultTab);

        #endregion Tabs

        public TUiPageService GetSingleUiPageService<TUiPageService>(string tabName = SharedUiConstants.DefaultTab, string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) where TUiPageService : ISingleUiPageService;
        public TUiPageService GetMultipleUiPagesService<TUiPageService>(string tabName = SharedUiConstants.DefaultTab) where TUiPageService : IMultipleUiPagesService;
        public TUiPageService Interact<TUiPageService>(string tabName = SharedUiConstants.DefaultTab, string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null, params KeyValuePair<string, string>[] urlPlaceholderValues) where TUiPageService : ISingleUiPageService;

        public string? DownloadsDefaultDirectory { get; set; }
    }
}
