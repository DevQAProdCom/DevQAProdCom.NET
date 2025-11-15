using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor
{
    public abstract class BaseUiInteractorTab<T> : IUiInteractorTab
        where T : class
    {
        public IUiInteractor UiInteractor { get; }
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        protected T _nativeTab { get; set; }

        protected ILogger _log;

        public BaseUiInteractorTab(ILogger log, IUiInteractor uiInteractor, string aliasIdentifier, T nativeTab)
        {
            _log = log;
            UiInteractor = uiInteractor;
            Name = aliasIdentifier;
            _nativeTab = nativeTab;
        }

        public abstract void SwitchTo();
        public abstract void GoTo(string url);

        public void GoTo(Uri uri)
        {
            GoTo(uri.AbsoluteUri);
        }

        public abstract string GetTabUrl();
        public abstract string GetTabTitle();

        public abstract void NavigateBack();
        public abstract void NavigateForward();
        public abstract void RefreshTab();

        public TNativeTab GetNativeTab<TNativeTab>() where TNativeTab : class
        {
            TNativeTab nativeTab = _nativeTab as TNativeTab;
            return nativeTab;
        }

        public TUiPage GetPage<TUiPage>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) where TUiPage : IUiPage
        {
            TUiPage page = CreatePage<TUiPage>(applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri);
            return page;
        }

        public TUiPageService GetSingleUiPageService<TUiPageService>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) where TUiPageService : ISingleUiPageService
        {
            TUiPageService service = default;

            if (!string.IsNullOrEmpty(applicationName) || !string.IsNullOrEmpty(pageName) || !string.IsNullOrEmpty(baseUri) || !string.IsNullOrEmpty(relativeUri))
                service = (TUiPageService)Activator.CreateInstance(typeof(TUiPageService), UiInteractor, Name, applicationName, pageName, baseUri, relativeUri);
            else
                service = (TUiPageService)Activator.CreateInstance(typeof(TUiPageService), UiInteractor, Name);

            return service;
        }

        public TUiPageService GetMultipleUiPagesService<TUiPageService>() where TUiPageService : IMultipleUiPagesService
        {
            TUiPageService service = (TUiPageService)Activator.CreateInstance(typeof(TUiPageService), UiInteractor, Name);
            return service;
        }

        public TUiPageService Interact<TUiPageService>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null, params KeyValuePair<string, string>[] urlPlaceholderValues) where TUiPageService : ISingleUiPageService
        {
            TUiPageService service = GetSingleUiPageService<TUiPageService>(applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri);
            service.GoToPage(urlPlaceholderValues);
            return service;
        }

        protected abstract TUiPage CreatePage<TUiPage>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) where TUiPage : IUiPage;

        #region Screenshots

        public abstract void MakeScreenshot(string? directoryPath = null, string? fileNamePrefix = null);

        public string GetScreenshotFilePath(string? directoryPath = null, string? fileNamePrefix = null)
        {
            if (string.IsNullOrEmpty(directoryPath))
                directoryPath = Environment.CurrentDirectory;

            string? fileName = fileNamePrefix;
            var fileNameSuffix = $"{SharedUiConstants.Tab}_{Name}_{DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds()}";
            if (string.IsNullOrEmpty(fileNamePrefix))
                fileName = fileNameSuffix;
            else
                fileName = $"{fileNamePrefix}_{fileNameSuffix}";

            string filePath = Path.Combine(directoryPath, $"{fileName}.png");
            return filePath;
        }

        #endregion Screenshots
    }
}
