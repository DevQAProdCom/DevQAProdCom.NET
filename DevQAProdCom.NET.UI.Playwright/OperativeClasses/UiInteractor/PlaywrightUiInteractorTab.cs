using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Constants;
using DevQAProdCom.NET.UI.Playwright.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiInteractor
{
    public class PlaywrightUiInteractorTab : BaseUiInteractorTab<IPage>
    {
        private readonly IPlaywrightUiInteractor _uiInteractor;
        private readonly IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> _findOptionSearchMethodsProvider;
        private readonly IUiPageFactoryProvider _uiPageFactoryProvider;

        public PlaywrightUiInteractorTab(ILogger log, IPlaywrightUiInteractor uiInteractor, string aliasIdentifier, IPage nativeTab,
            IUiPageFactoryProvider uiPageFactoryProvider, IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> findOptionSearchMethodsProvider)
            : base(log, uiInteractor, aliasIdentifier, nativeTab)
        {
            _uiInteractor = uiInteractor;
            _uiPageFactoryProvider = uiPageFactoryProvider;
            _findOptionSearchMethodsProvider = findOptionSearchMethodsProvider;
        }

        public override void SwitchTo()
        {
            _nativeTab.BringToFrontAsync().Wait();
        }

        public override void GoTo(string url)
        {
            SwitchTo();
            _ = _nativeTab.GotoAsync(url).Result;
        }

        public override string GetTabUrl()
        {
            SwitchTo();
            return _nativeTab.Url;
        }

        public override string GetTabTitle()
        {
            SwitchTo();
            return _nativeTab.TitleAsync().Result;
        }

        public override void NavigateBack()
        {
            SwitchTo();
            _ = _nativeTab.GoBackAsync().Result;
        }

        public override void NavigateForward()
        {
            SwitchTo();
            _ = _nativeTab.GoForwardAsync().Result;
        }

        public override void RefreshTab()
        {
            SwitchTo();
            _ = _nativeTab.ReloadAsync().Result;
        }

        protected override TUiPage CreatePage<TUiPage>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null)
        {
            var iBrowserNativeObject = new KeyValuePair<string, object>(ProjectConst.IBrowser, _uiInteractor.Browser);
            var iBrowserContextNativeObject = new KeyValuePair<string, object>(ProjectConst.IBrowserContext, _uiInteractor.BrowserContext);
            var iUiPageNativeObject = new KeyValuePair<string, object>(ProjectConst.IUiPage, _nativeTab);

            INativeElementsSearcher nativeElementsSearcher = new PlaywrightNativeElementsSearcher(_log, _nativeTab, _findOptionSearchMethodsProvider);
            IExecuteJavaScript javaScriptExecutor = new PlaywrightJavaScriptExecutor(_nativeTab);

            IUiPage page = _uiPageFactoryProvider.ConstructNewPageFactory(uiInteractorTab: this, nativeElementsSearcher: nativeElementsSearcher, javaScriptExecutor: javaScriptExecutor,
                applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri,
                nativeObjects: new Dictionary<string, object> { 
                    { iBrowserNativeObject.Key, iBrowserNativeObject.Value },
                    { iBrowserContextNativeObject.Key, iBrowserContextNativeObject.Value },
                    { iUiPageNativeObject.Key, iUiPageNativeObject.Value } }).CreatePage<TUiPage>();

            return (TUiPage)page;
        }

        #region Screenshots

        public override void MakeScreenshot(string? directoryPath = null, string? fileNamePrefix = null)
        {
            SwitchTo();
            var filePath = GetScreenshotFilePath(directoryPath, fileNamePrefix);
            var options = new PageScreenshotOptions()
            {
                Path = filePath,
                FullPage = true,
            };
            _nativeTab.ScreenshotAsync(options).Wait();
        }

        #endregion Screenshots
    }
}
