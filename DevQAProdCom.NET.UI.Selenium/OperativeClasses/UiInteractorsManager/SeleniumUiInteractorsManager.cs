using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Selenium.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractor;
using DevQAProdCom.NET.UI.Selenium.WebDrivers.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorsManager;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiInteractorsManager
{
    public class SeleniumUiInteractorsManager : BaseUiInteractorsManager
    {
        private readonly ISeleniumWebDriverFactory _webDriverFactory;
        private readonly IUiPageFactoryProvider _pageFactoryProvider;
        private readonly IUiElementsInstantiator _uiElementsInstantiator;
        private readonly IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> _findOptionSearchMethodsProvider;
        private readonly ISeleniumCookieMappers _cookieMappers;

        public SeleniumUiInteractorsManager(ILogger log,
            ISeleniumWebDriverFactory webDriverFactory,
            IUiPageFactoryProvider pageFactoryProvider,
            IFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod> findOptionSearchMethodsProvider,
            ISeleniumCookieMappers cookieMappers,
            string? name = null) : base(log, name)
        {
            _webDriverFactory = webDriverFactory;
            _pageFactoryProvider = pageFactoryProvider;
            _findOptionSearchMethodsProvider = findOptionSearchMethodsProvider;
            _cookieMappers = cookieMappers;
        }

        protected override IUiInteractor CreateTechnologySpecificInteractor()
        {
            IUiInteractor instance = new SeleniumUiInteractor(_log, this, _webDriverFactory, _pageFactoryProvider, _findOptionSearchMethodsProvider, _cookieMappers);
            return instance;
        }
    }
}

