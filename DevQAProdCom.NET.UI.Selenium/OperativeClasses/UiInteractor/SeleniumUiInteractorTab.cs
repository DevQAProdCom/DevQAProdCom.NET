using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Selenium.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorTab;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractor
{
    public class SeleniumUiInteractorTab : BaseUiInteractorTab<string>
    {
        private ISeleniumUiInteractor _uiInteractor;
        private IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> _findOptionSearchMethodsProvider;
        private readonly IUiPageFactoryProvider _uiPageFactoryProvider;

        private INativeElementsSearcher _nativeElementsSearcher;
        protected override INativeElementsSearcher NativeElementsSearcher
        {
            get
            {
                if (_nativeElementsSearcher == null)
                    _nativeElementsSearcher = new SeleniumNativeElementsSearcher(_log, _uiInteractor.Driver, _findOptionSearchMethodsProvider);

                return _nativeElementsSearcher;
            }
        }

        private IExecuteJavaScript _javaScriptExecutor;
        protected override IExecuteJavaScript JavaScriptExecutor
        {
            get
            {
                if (_javaScriptExecutor == null)
                    _javaScriptExecutor = new SeleniumJavaScriptExecutor(_uiInteractor.Driver);

                return _javaScriptExecutor;
            }
        }

        public SeleniumUiInteractorTab(ILogger log, ISeleniumUiInteractor uiInteractor, string aliasIdentifier, string nativeTab,
            IUiPageFactoryProvider uiPageFactoryProvider, IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> findOptionSearchMethodsProvider, IUiInteractorTabBehaviorFactory uiInteractorTabBehaviorFactory,
            Dictionary<string, object> nativeObjects)
            : base(log, uiInteractor, aliasIdentifier, nativeTab, uiInteractorTabBehaviorFactory, nativeObjects)
        {
            _uiInteractor = uiInteractor;
            _uiPageFactoryProvider = uiPageFactoryProvider;
            _findOptionSearchMethodsProvider = findOptionSearchMethodsProvider;
        }

        public override void SwitchTo()
        {
            _uiInteractor.Driver.SwitchTo().Window(_nativeTab);
        }

        public override void GoTo(string url)
        {
            SwitchTo();
            _uiInteractor.Driver.Navigate().GoToUrl(url);
        }

        public override string GetTabUriAsString()
        {
            SwitchTo();
            return _uiInteractor.Driver.Url;
        }

        public override string GetTabTitle()
        {
            SwitchTo();
            return _uiInteractor.Driver.Title;
        }

        public override void NavigateBack()
        {
            SwitchTo();
            _uiInteractor.Driver.Navigate().Back();
        }

        public override void NavigateForward()
        {
            SwitchTo();
            _uiInteractor.Driver.Navigate().Forward();
        }

        public override void Refresh()
        {
            SwitchTo();
            _uiInteractor.Driver.Navigate().Refresh();
        }

        protected override TUiPage CreatePage<TUiPage>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null)
        {
            IUiPage page = _uiPageFactoryProvider.ConstructNewPageFactory(uiInteractorTab: this, nativeElementsSearcher: NativeElementsSearcher, javaScriptExecutor: JavaScriptExecutor,
                applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri,
                nativeObjects: NativeObjects).CreatePage<TUiPage>();

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
                var screenshotDriver = (ITakesScreenshot)_uiInteractor.Driver;
                var screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(screenshotModel.FilePath);
                screenshotModel.ScreenshotByteArray = screenshot.AsByteArray;

                Console.WriteLine($"Screenshot saved to {screenshotModel.FilePath}");
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
