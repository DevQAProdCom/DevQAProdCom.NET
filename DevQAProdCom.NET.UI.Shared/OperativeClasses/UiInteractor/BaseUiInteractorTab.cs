using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor
{
    public abstract class BaseUiInteractorTab<TNativeTab> : IUiInteractorTab
        where TNativeTab : class
    {
        public IUiInteractor UiInteractor { get; }
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }

        protected ILogger _log;
        private IUiInteractorTabBehaviorFactory _uiInteractorTabBehaviorFactory { get; set; }
        protected abstract INativeElementsSearcher NativeElementsSearcher { get; }
        protected abstract IExecuteJavaScript JavaScriptExecutor { get; }
        internal TNativeTab NativeTab { get; set; }

        public BaseUiInteractorTab(ILogger log, IUiInteractor uiInteractor, string aliasIdentifier, TNativeTab nativeTab,
            IUiInteractorTabBehaviorFactory uiInteractorTabBehaviorFactory, Dictionary<string, object> nativeObjects)
        {
            _log = log;
            UiInteractor = uiInteractor;
            Name = aliasIdentifier;
            _uiInteractorTabBehaviorFactory = uiInteractorTabBehaviorFactory;
            NativeTab = nativeTab;
            NativeObjects.Upsert(nativeObjects);
        }

        public abstract void SwitchTo();
        public abstract void GoTo(string url);

        public void GoTo(Uri uri)
        {
            GoTo(uri.AbsoluteUri);
        }

        public abstract string GetTabUriAsString();
        public Uri GetTabUri() => new Uri(GetTabUriAsString());

        public abstract string GetTabTitle();

        public abstract void NavigateBack();
        public abstract void NavigateForward();
        public abstract void Refresh();

        public TNativeTab GetNativeTab<TNativeTab>() where TNativeTab : class
        {
            TNativeTab nativeTab = NativeTab as TNativeTab;
            return nativeTab;
        }

        public TUiPage GetPage<TUiPage>(string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null) where TUiPage : IUiPage
        {
            TUiPage page = CreatePage<TUiPage>(applicationName: applicationName, pageName: pageName, baseUri: baseUri, relativeUri: relativeUri);
            return page;
        }

        public TUiElement Find<TUiElement>(string? name = null) where TUiElement : IUiElement
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            return page.Find<TUiElement>(name: name);
        }

        public TUiElement Find<TUiElement>(string method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            return page.Find<TUiElement>(method, criteria, parentUiElement, name: name);
        }

        public TUiElement Find<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            return Find<TUiElement>(method.ToString(), criteria, parentUiElement, name: name);
        }

        public TUiElement Find<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            return page.Find<TUiElement>(findOptions, parentUiElement, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(string? name = null) where TUiElement : IUiElement
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            return page.FindAll<TUiElement>(name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(string method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            return page.FindAll<TUiElement>(method, criteria, parentUiElement, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            return FindAll<TUiElement>(method.ToString(), criteria, parentUiElement, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            return page.FindAll<TUiElement>(findOptions, parentUiElement, name: name);
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

        public abstract IUiInteractorTabScreenshotModel MakeScreenshot(string? directoryPath = null, string? fileNamePrefix = null);

        public string GetScreenshotFilePath(string? directoryPath = null, string? fileNamePrefix = null)
        {
            if (string.IsNullOrEmpty(directoryPath))
                directoryPath = Environment.CurrentDirectory;

            string? fileName = fileNamePrefix;
            var fileNameSuffix = $"{SharedUiConstants.UiInteractorTab}_{Name}_{DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds()}";
            if (string.IsNullOrEmpty(fileNamePrefix))
                fileName = fileNameSuffix;
            else
                fileName = $"{fileNamePrefix}_{fileNameSuffix}";

            string filePath = Path.Combine(directoryPath, $"{fileName}.png");
            return filePath;
        }

        #endregion Screenshots

        #region Actions

        #region Keyboard Actions

        public void KeysDown(params string[] keys)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.KeysDown(keys);
        }

        public void KeysDown(params Key[] keys)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.KeysDown(keys);
        }

        public void KeysUp(params string[] keys)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.KeysUp(keys);
        }

        public void KeysUp(params Key[] keys)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.KeysUp(keys);
        }

        public void PressKey(string key)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.PressKey(key);
        }

        public void PressKey(Key key)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.PressKey(key);
        }

        public void PressKeysSequentially(params string[] keys)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.PressKeysSequentially(keys);
        }

        public void PressKeysSequentially(params Key[] keys)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.PressKeysSequentially(keys);
        }

        public void PressKeysCombination(string keysCombination, string separator = SharedUiConstants.DefaultKeysCombinationSeparator)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.PressKeysCombination(keysCombination, separator);
        }

        public void PressKeysSimultaneously(params string[] keys)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.PressKeysSimultaneously(keys);
        }

        public void PressKeysSimultaneously(params Key[] keys)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.PressKeysSimultaneously(keys);
        }

        #endregion Keyboard Actions

        #region Mouse Actions

        public void MouseMove(float x, float y)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.MouseMove(x, y);
        }

        public void MouseMoveJs(float x, float y)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.MouseMoveJs(x, y);
        }

        public void MouseScroll(float deltaX, float deltaY)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.MouseScroll(deltaX, deltaY);
        }

        public void MouseScroll(float deltaX, float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.MouseScroll(deltaX, deltaY, untilCondition, timeoutSec, pollingIntervalSec);
        }

        public void MouseScrollVertically(float deltaY)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.MouseScrollVertically(deltaY);
        }

        public void MouseScrollVertically(float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.MouseScrollVertically(deltaY, untilCondition, timeoutSec, pollingIntervalSec);
        }

        public void MouseScrollHorizontally(float deltaX)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.MouseScrollHorizontally(deltaX);
        }

        public void MouseScrollHorizontally(float deltaX, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.MouseScrollHorizontally(deltaX, untilCondition, timeoutSec, pollingIntervalSec);
        }

        #endregion Mouse Actions

        #region Scroll Actions

        public void ScrollToTopSmoothlyJs()
        {
            IUiPage page = GetPage<UiPage.UiPage>();
            page.ScrollToTopSmoothlyJs();
        }

        #endregion Scroll Actions

        #endregion Actions

        #region Execute JavaScript

        public void ExecuteJavaScript(string script, params KeyValuePair<string, object>[] arguments)
        {
            JavaScriptExecutor.ExecuteJavaScript(script, arguments);
        }

        public void ExecuteJavaScript(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            JavaScriptExecutor.ExecuteJavaScript(file, arguments);
        }

        public TResult ExecuteJavaScript<TResult>(string script, params KeyValuePair<string, object>[] arguments)
        {
            return JavaScriptExecutor.ExecuteJavaScript<TResult>(script, arguments);
        }

        public TResult ExecuteJavaScript<TResult>(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            return JavaScriptExecutor.ExecuteJavaScript<TResult>(file, arguments);
        }

        #endregion Execute JavaScript

        public Dictionary<string, object> NativeObjects { get; internal set; } = new();

        public TResult AddBehavior<TResult>(params KeyValuePair<string, object>[]? auxiliaryParams) where TResult : IBehavior
        {
            return _uiInteractorTabBehaviorFactory.Create<TResult>(this, JavaScriptExecutor, auxiliaryParams.Union(NativeObjects).ToArray());
        }

        public Dictionary<string, string> GetLocalStorageData()
        {
            var result = ExecuteJavaScript<Dictionary<string, string>>(SharedUiConstants.Files.GetLocalStorageDataJavaScriptFilePath);
            return result.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.ToString() ?? string.Empty);
        }
    }
}
