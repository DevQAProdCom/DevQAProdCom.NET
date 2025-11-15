using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Browsers.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorsManager;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiInteractorsManager
{
    public class PlaywrightUiInteractorsManager : BaseUiInteractorsManager
    {
        private readonly IPlaywrightBrowserFactory _browserFactory;
        private readonly IUiPageFactoryProvider _pageFactoryProvider;
        private readonly IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> _findOptionSearchMethodsProvider;
        private readonly IPlaywrightCookieMappers _cookieMappers;

        public PlaywrightUiInteractorsManager(ILogger log,
            IPlaywrightBrowserFactory browserFactory,
            IUiPageFactoryProvider pageFactoryProvider,
            IFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod> findOptionSearchMethodsProvider,
            IPlaywrightCookieMappers cookieMappers,
            string? name = null) : base(log, name)
        {
            _browserFactory = browserFactory;
            _pageFactoryProvider = pageFactoryProvider;
            _findOptionSearchMethodsProvider = findOptionSearchMethodsProvider;
            _cookieMappers = cookieMappers;
        }

        protected override IUiInteractor CreateTechnologySpecificInteractor()
        {
            IUiInteractor instance = new PlaywrightUiInteractor(_log, this, _browserFactory, _pageFactoryProvider, _findOptionSearchMethodsProvider, _cookieMappers);
            return instance;
        }
    }
}

