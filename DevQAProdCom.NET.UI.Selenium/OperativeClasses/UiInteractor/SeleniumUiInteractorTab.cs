using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Selenium.Constants;
using DevQAProdCom.NET.UI.Selenium.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractor
{
    public class SeleniumUiInteractorTab : BaseUiInteractorTab<string>
    {
        private ISeleniumUiInteractor _uiInteractor;
        private IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> _findOptionSearchMethodsProvider;
        private readonly IUiPageFactoryProvider _uiPageFactoryProvider;

        public SeleniumUiInteractorTab(ILogger log, ISeleniumUiInteractor uiInteractor, string aliasIdentifier, string nativeTab,
            IUiPageFactoryProvider uiPageFactoryProvider, IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> findOptionSearchMethodsProvider)
            : base(log, uiInteractor, aliasIdentifier, nativeTab)
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

        public override string GetTabUrl()
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

        public override void RefreshTab()
        {
            SwitchTo();
            _uiInteractor.Driver.Navigate().Refresh();
        }

        protected override TUiPage CreatePage<TUiPage>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null)
        {
            KeyValuePair<string, object> iWebDriverNativeObject = new KeyValuePair<string, object>(ProjectConst.IWebDriver, _uiInteractor.Driver);

            INativeElementsSearcher nativeElementsSearcher = new SeleniumNativeElementsSearcher(_log, _uiInteractor.Driver, _findOptionSearchMethodsProvider);
            IExecuteJavaScript javaScriptExecutor = new SeleniumJavaScriptExecutor(_uiInteractor.Driver);

            IUiPage page = _uiPageFactoryProvider.ConstructNewPageFactory(uiInteractorTab: this, nativeElementsSearcher: nativeElementsSearcher, javaScriptExecutor: javaScriptExecutor,
                applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri,
                nativeObjects: new Dictionary<string, object> { { iWebDriverNativeObject.Key, iWebDriverNativeObject.Value } }).CreatePage<TUiPage>();

            return (TUiPage)page;
        }

        #region Screenshots

        public override void MakeScreenshot(string? directoryPath = null, string? fileNamePrefix = null)
        {
            SwitchTo();

            try
            {
                var screenshotDriver = (ITakesScreenshot)_uiInteractor.Driver;
                var screenshot = screenshotDriver.GetScreenshot();
                var filePath = GetScreenshotFilePath(directoryPath, fileNamePrefix);
                screenshot.SaveAsFile(filePath);

                Console.WriteLine($"Screenshot saved to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to take screenshot: {ex.Message}");
            }
        }

        #endregion Screenshots
    }
}
