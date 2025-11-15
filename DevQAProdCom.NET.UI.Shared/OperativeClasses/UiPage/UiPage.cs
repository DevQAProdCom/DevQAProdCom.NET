using DevQAProdCom.NET.Global.ModelsAndInterfaces.Exceptions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public abstract class UiPage : IUiPage
    {
        public IUiInteractorTab UiTab { get; internal set; }

        private string? _applicationName;
        public virtual string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationName))
                    return $"{SharedUiConstants.Hyphen}";

                return _applicationName;
            }
            internal set
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
                    return $"{SharedUiConstants.Hyphen}";

                return _pageName;
            }
            internal set
            {
                _pageName = value;
            }
        }

        private string? _baseUri;
        public virtual string BaseUri
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUri))
                    throw new NotSetException($"{nameof(BaseUri)} of page '{PageName}' is not set.");

                return _baseUri;
            }
            internal set
            {
                _baseUri = value;
            }
        }

        private string? _relativeUri;
        public virtual string? RelativeUri
        {

            get
            {
                if (string.IsNullOrEmpty(_relativeUri))
                    throw new NotSetException($"{nameof(RelativeUri)} of page '{PageName}' is not set.");

                return _relativeUri;
            }
            internal set
            {
                _relativeUri = value;
            }
        }

        internal INativeElementsSearcher NativeElementsSearcher { get; set; }
        internal IUiElementsInstantiator UiElementsInstantiator { get; set; }
        internal IExecuteJavaScript JavaScriptExecutor { get; set; }
        internal IUiPageBehaviorFactory PageBehaviorFactory { get; set; }

        private IBaseMouseBehavior _pageMouseBehaviorExecutor;
        private IBaseMouseBehavior PageMouseBehaviorExecutor
        {
            get
            {
                if (_pageMouseBehaviorExecutor == null)
                    _pageMouseBehaviorExecutor = AddBehavior<IBaseMouseBehavior>();

                return _pageMouseBehaviorExecutor;
            }
        }

        public Dictionary<string, object> NativeObjects { get; internal set; } = new();

        public T AddBehavior<T>(params KeyValuePair<string, object>[]? auxiliaryParams) where T : IBehavior
        {
            return PageBehaviorFactory.Create<T>(JavaScriptExecutor, auxiliaryParams.Union(NativeObjects).ToArray());
        }

        public TUiElement InstantiateUiElement<TUiElement>(string name, Use method, string criteria, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElement<TUiElement>(name, method, criteria, parentUiElement);
        }

        public TUiElement InstantiateUiElement<TUiElement>(string name, List<IUiElementsFindInfo> findOptions, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElement<TUiElement>(name, findOptions, parentUiElement);
        }

        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(string name, Use method, string criteria, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(name, method, criteria, parentUiElement);
        }

        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(string name, List<IUiElementsFindInfo> findOptions, IParentUiElement? parentUiElement = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(name, findOptions, parentUiElement);
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

        #region Mouse Actions

        public void MouseMove(float x, float y)
        {
            PageMouseBehaviorExecutor.MouseMove(x, y);
        }

        public void MouseScroll(float deltaX, float deltaY)
        {
            PageMouseBehaviorExecutor.MouseScroll(deltaX, deltaY);
        }

        public void MouseScroll(float deltaX, float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {
            PageMouseBehaviorExecutor.MouseScroll(deltaX, deltaY, untilCondition, timeoutSec, pollingIntervalSec);
        }

        public void MouseScrollVertically(float deltaY)
        {
            PageMouseBehaviorExecutor.MouseScrollVertically(deltaY);
        }

        public void MouseScrollVertically(float deltaY, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {
            PageMouseBehaviorExecutor.MouseScrollVertically(deltaY, untilCondition, timeoutSec, pollingIntervalSec);
        }

        public void MouseScrollHorizontally(float deltaX)
        {
            PageMouseBehaviorExecutor.MouseScrollHorizontally(deltaX);
        }

        public void MouseScrollHorizontally(float deltaX, Func<bool> untilCondition, double timeoutSec = SharedUiConstants.ScrollUntilTimeoutSec, double pollingIntervalSec = SharedUiConstants.ScrollUntilPollingIntervalSec)
        {
            PageMouseBehaviorExecutor.MouseScrollHorizontally(deltaX, untilCondition, timeoutSec, pollingIntervalSec);
        }

        #endregion

        public void GoTo(params KeyValuePair<string, string>[] placeholderValues)
        {
            var url = GetPageUrl(placeholderValues);
            UiTab.GoTo(url);
        }

        public virtual void WaitForLoad()
        {
            throw new NotImplementedException();
        }

        public virtual Uri GetPageUrl(params KeyValuePair<string, string>[] placeholderValues)
        {
            return new Uri(BaseUri + RelativeUri.TrimStart('/'));
        }
    }
}
