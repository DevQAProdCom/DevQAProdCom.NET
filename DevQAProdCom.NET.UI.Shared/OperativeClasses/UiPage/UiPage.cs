using DevQAProdCom.NET.Global.Builders;
using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public class UiPage : IUiPage
    {
        public IUiInteractorTab UiTab { get; internal set; }

        #region UiPageInfo

        private string? _applicationName;
        public virtual string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationName))
                    return $"{nameof(ApplicationName)} of {nameof(UiPage)} is not set/overriden inside child class.";

                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        private string? _pageName;
        public virtual string PageName
        {
            get
            {
                if (string.IsNullOrEmpty(_pageName))
                    return $"UiPage name is not set.";

                return _pageName;
            }
            set
            {
                _pageName = value;
            }
        }

        private string Uri => string.Join("/", BaseUri?.Trim('/'), RelativeUri?.Trim('/'));

        private string? _baseUri;
        public virtual string BaseUri
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUri))
                    return $"{nameof(BaseUri)} of page '{PageName}' is not set/overriden inside child class.";

                return _baseUri;
            }
            set
            {
                _baseUri = value;
            }
        }

        private string? _relativeUri;
        public virtual string RelativeUri
        {

            get
            {
                if (string.IsNullOrEmpty(_relativeUri))
                    return string.Empty;

                return _relativeUri;
            }
            set
            {
                _relativeUri = value;
            }
        }

        public string GetActualUriAsString() => UiTab.GetTabUriAsString();
        public Uri GetActualUri() => UiTab.GetTabUri();

        public string GetDefinedUriAsString(params KeyValuePair<string, string>[] placeholderValues) => GetDefinedUri(placeholderValues).ToString();
        public virtual Uri GetDefinedUri(params KeyValuePair<string, string>[] placeholderValues)
        {
            return new AppUriBuilder(Uri).WithPathParameters(placeholderValues).Build();
        }

        public bool IsOpened(params KeyValuePair<string, string>[] placeholderValues)
        {
            var expectedUri = GetDefinedUriAsString(placeholderValues);
            var actualUri = GetActualUriAsString();
            return actualUri.Equals(expectedUri);
        }

        public bool IsOpenedWhenUriStartsWith(string? uriStartWith = null, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            uriStartWith ??= Uri;
            var actualUri = GetActualUriAsString();
            return actualUri.StartsWith(uriStartWith, stringComparison);
        }

        public bool IsOpenedWhenUriEndsWith(string? uriEndsWith = null, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            uriEndsWith ??= Uri;
            var actualUri = GetActualUriAsString();
            return actualUri.EndsWith(uriEndsWith, stringComparison);
        }

        public bool IsOpenedWhenUriEquals(string? uriEquals = null, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            uriEquals ??= Uri;
            var actualUri = GetActualUriAsString();
            return actualUri.Equals(uriEquals, stringComparison);
        }

        #endregion UiPageInfo

        internal INativeElementsSearcher NativeElementsSearcher { get; set; }
        internal IUiElementsInstantiator UiElementsInstantiator { get; set; }
        internal IExecuteJavaScript JavaScriptExecutor { get; set; }
        internal IUiPageBehaviorFactory PageBehaviorFactory { get; set; }

        public Dictionary<string, object> NativeObjects { get; internal set; } = new();

        public T AddBehavior<T>(params KeyValuePair<string, object>[]? auxiliaryParams) where T : IBehavior
        {
            return PageBehaviorFactory.Create<T>(this, JavaScriptExecutor, auxiliaryParams.Union(NativeObjects).ToArray());
        }

        public TUiElement Find<TUiElement>(string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElement<TUiElement>(Use.XPath, SharedUiConstants.HtmlXPath, name: name);
        }

        public TUiElement Find<TUiElement>(string method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElement<TUiElement>(method, criteria, parentUiElement, name: name);
        }

        public TUiElement Find<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElement<TUiElement>(method, criteria, parentUiElement, name: name);
        }

        public TUiElement Find<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElement<TUiElement>(findOptions, parentUiElement, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(Use.XPath, SharedUiConstants.HtmlXPath, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(string method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(method, criteria, parentUiElement, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(method, criteria, parentUiElement, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(findOptions, parentUiElement, name: name);
        }

        #region Execute JavaScript

        public void ExecuteJavaScript(string script, params KeyValuePair<string, object>[] arguments)
        {
            JavaScriptExecutor.ExecuteJavaScript(script, arguments);
        }

        public void ExecuteJavaScript(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            JavaScriptExecutor.ExecuteJavaScript(file, arguments);
        }

        public T ExecuteJavaScript<T>(string script, params KeyValuePair<string, object>[] arguments)
        {
            return JavaScriptExecutor.ExecuteJavaScript<T>(script, arguments);
        }

        public T ExecuteJavaScript<T>(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            return JavaScriptExecutor.ExecuteJavaScript<T>(file, arguments);
        }

        #endregion Execute JavaScript

        #region Actions

        #region Keyboard Actions

        public void KeysDown(params string[] keys) => AddBehavior<IUiPageBehaviorKeysDown>().KeysDown(keys);
        public void KeysDown(params Key[] keys) => AddBehavior<IUiPageBehaviorKeysDown>().KeysDown(keys);

        public void KeysUp(params string[] keys) => AddBehavior<IUiPageBehaviorKeysUp>().KeysUp(keys);
        public void KeysUp(params Key[] keys) => AddBehavior<IUiPageBehaviorKeysUp>().KeysUp(keys);

        public void PressKey(string key) => AddBehavior<IUiPageBehaviorPressKey>().PressKey(key);
        public void PressKey(Key key) => AddBehavior<IUiPageBehaviorPressKey>().PressKey(key);

        public void PressKeysSequentially(params string[] keys) => AddBehavior<IUiPageBehaviorPressKeysSequentially>().PressKeysSequentially(keys);
        public void PressKeysSequentially(params Key[] keys) => AddBehavior<IUiPageBehaviorPressKeysSequentially>().PressKeysSequentially(keys);

        public void PressKeysCombination(string keysCombination, string separator = SharedUiConstants.DefaultKeysCombinationSeparator) => AddBehavior<IUiPageBehaviorPressKeysSimultaneously>().PressKeysCombination(keysCombination, separator);
        public void PressKeysSimultaneously(params string[] keys) => AddBehavior<IUiPageBehaviorPressKeysSimultaneously>().PressKeysSimultaneously(keys);
        public void PressKeysSimultaneously(params Key[] keys) => AddBehavior<IUiPageBehaviorPressKeysSimultaneously>().PressKeysSimultaneously(keys);

        #endregion Keyboard Actions

        #region Mouse Actions

        public void MouseMove(float x, float y)
        {
            AddBehavior<IUiPageBehaviorMouseMove>().MouseMove(x, y);
        }

        public void MouseMoveJs(float x, float y)
        {
            AddBehavior<IUiPageBehaviorMouseMoveJs>().MouseMoveJs(x, y);
        }

        public void MouseScroll(float deltaX, float deltaY)
        {
            AddBehavior<IUiPageBehaviorMouseScroll>().MouseScroll(deltaX, deltaY);
        }

        public void MouseScroll(float deltaX, float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {
            AddBehavior<IUiPageBehaviorMouseScroll>().MouseScroll(deltaX, deltaY, untilCondition, timeoutSec, pollingIntervalSec);
        }

        public void MouseScrollVertically(float deltaY)
        {
            AddBehavior<IUiPageBehaviorMouseScrollVertically>().MouseScrollVertically(deltaY);
        }

        public void MouseScrollVertically(float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {
            AddBehavior<IUiPageBehaviorMouseScrollVertically>().MouseScrollVertically(deltaY, untilCondition, timeoutSec, pollingIntervalSec);
        }

        public void MouseScrollHorizontally(float deltaX)
        {
            AddBehavior<IUiPageBehaviorMouseScrollHorizontally>().MouseScrollHorizontally(deltaX);
        }

        public void MouseScrollHorizontally(float deltaX, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {
            AddBehavior<IUiPageBehaviorMouseScrollHorizontally>().MouseScrollHorizontally(deltaX, untilCondition, timeoutSec, pollingIntervalSec);
        }

        #endregion


        #endregion Actions


        public void GoTo(params KeyValuePair<string, string>[] placeholderValues)
        {
            var url = GetDefinedUri(placeholderValues);
            UiTab.GoTo(url);
        }

        public virtual void WaitForLoaded(Func<bool>? waitTillFunc = null, TimeSpan? timeout = null)
        {
            timeout ??= TimeSpan.FromSeconds(30);

            Wait.WithTimeout(timeout.Value)
                .WithErrorMessage($"Page '{GetType().Name}' with Address '{GetDefinedUriAsString()}' is not opened after timeout")
                .IgnoreAllExceptions()
                .Until(() => waitTillFunc == null ? IsOpened() : waitTillFunc());
        }

        public void Refresh() => UiTab.Refresh();
    }
}
