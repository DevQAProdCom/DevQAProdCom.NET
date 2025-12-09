using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Constants;
using DevQAProdCom.NET.UI.Playwright.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorTab;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiInteractor
{
    public class PlaywrightUiInteractorTab : BaseUiInteractorTab<IPage>
    {
        private readonly IPlaywrightUiInteractor _uiInteractor;
        private readonly IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> _findOptionSearchMethodsProvider;
        private readonly IUiPageFactoryProvider _uiPageFactoryProvider;

        private INativeElementsSearcher _nativeElementsSearcher;
        protected override INativeElementsSearcher NativeElementsSearcher
        {
            get
            {
                if (_nativeElementsSearcher == null)
                    _nativeElementsSearcher = new PlaywrightNativeElementsSearcher(_log, _nativeTab, _findOptionSearchMethodsProvider);

                return _nativeElementsSearcher;
            }
        }

        private IExecuteJavaScript _javaScriptExecutor;
        protected override IExecuteJavaScript JavaScriptExecutor
        {
            get
            {
                if (_javaScriptExecutor == null)
                    _javaScriptExecutor = new PlaywrightJavaScriptExecutor(_nativeTab);

                return _javaScriptExecutor;
            }
        }

        public PlaywrightUiInteractorTab(ILogger log, IPlaywrightUiInteractor uiInteractor, string aliasIdentifier, IPage nativeTab,
            IUiPageFactoryProvider uiPageFactoryProvider, IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> findOptionSearchMethodsProvider,
            IUiInteractorTabBehaviorFactory uiInteractorTabBehaviorFactory, Dictionary<string, object> nativeObjects)
            : base(log, uiInteractor, aliasIdentifier, nativeTab, uiInteractorTabBehaviorFactory, nativeObjects)
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

        public override string GetTabUriAsString()
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

        public override void Refresh()
        {
            SwitchTo();
            _ = _nativeTab.ReloadAsync().Result;
        }

        protected override TUiPage CreatePage<TUiPage>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null)
        {
            var nativeObjects = new Dictionary<string, object>();
            nativeObjects.Upsert(NativeObjects);
            nativeObjects.Add(ProjectConst.IPage, _nativeTab);

            IUiPage page = _uiPageFactoryProvider.ConstructNewPageFactory(uiInteractorTab: this, nativeElementsSearcher: NativeElementsSearcher, javaScriptExecutor: JavaScriptExecutor,
                applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri,
                nativeObjects: nativeObjects).CreatePage<TUiPage>();

            return (TUiPage)page;
        }

        #region Screenshots

        public override IUiInteractorTabScreenshotModel MakeScreenshot(string? directoryPath = null, string? fileNamePrefix = null)
        {
            SwitchTo();
            var screenshotModel = new UiInteractorTabScreenshotModel();
            screenshotModel.TabName = Name;

            try
            {
                screenshotModel.FilePath = GetScreenshotFilePath(directoryPath, fileNamePrefix);
                var options = new PageScreenshotOptions()
                {
                    Path = screenshotModel.FilePath,
                    FullPage = true,
                };
                screenshotModel.ScreenshotByteArray = _nativeTab.ScreenshotAsync(options).Result;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to take screenshot: {ex.Message}";
                screenshotModel.FilePath = errorMessage;
                Console.WriteLine(errorMessage);
                //TODO Add Log.Warning
            }

            return screenshotModel;
        }

        #endregion Screenshots
    }
}
