using System.Drawing;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements
{
    public abstract class BaseUiElement : IUiElement
    {
        public IUiPage UiPage { get; }
        public IUiElementInfo Info { get; internal set; }

        protected INativeElementsSearcher NativeElementsSearcher { get; set; }

        protected IUiElementsInstantiator UiElementsInstantiator;

        private IExecuteJavaScript _javaScriptExecutor;

        private IUiElementBehaviorFactory _uiElementBehaviorFactory;

        public Dictionary<string, object> NativeObjects { get; set; } = new();

        protected ILogger _log;

        internal BaseUiElement(ILogger log, IUiPage uiPage, IUiElementInfo info,
            INativeElementsSearcher nativeElementsSearcher,
            IExecuteJavaScript javaScriptExecutor,
            IUiElementBehaviorFactory uiElementBehaviorFactory,
            IUiElementsInstantiator uiElementsInstantiator,
            params KeyValuePair<string, object>[] nativeObjects)
        {
            _log = log;
            UiPage = uiPage;
            Info = info;
            NativeElementsSearcher = nativeElementsSearcher;
            _javaScriptExecutor = javaScriptExecutor;
            UiElementsInstantiator = uiElementsInstantiator;
            _uiElementBehaviorFactory = uiElementBehaviorFactory;
            NativeObjects.Upsert(nativeObjects);
        }

        #region General Properties

        public abstract string GetTagName();
        public abstract PointF GetLocation();
        public abstract SizeF GetSize();


        #endregion region General Properties

        #region Specific Properties

        public abstract string? GetAttribute(string attributeName, bool isBooleanAttributeType);
        public abstract string? GetCssValue(string propertyName);
        public abstract string GetTextContent();

        #endregion Specific Properties

        #region States

        public abstract bool Exists();
        public abstract bool IsDisabled();
        public abstract bool IsDisplayed();
        public bool IsElementInViewportJs()
        {
            var fileInfo = new FileInfo(SharedUiConstants.Files.IsElementInViewportJavaScriptFilePath);
            return ExecuteJavaScript<bool>(fileInfo);
        }
        public abstract bool IsEnabled();

        #endregion States

        #region Actions

        public T AddBehavior<T>(params KeyValuePair<string, object>[]? auxiliaryParams) where T : IBehavior
        {
            return _uiElementBehaviorFactory.Create<T>(this, _javaScriptExecutor, auxiliaryParams);
        }

        public void FocusJs()
        {
            var fileInfo = new FileInfo(SharedUiConstants.Files.FocusJavaScriptFilePath);
            ExecuteJavaScript(fileInfo);
        }

        public abstract void MouseClick();
        public abstract void ScrollToElement();

        #endregion Actions

        #region Execute JavaScript
        public void ExecuteJavaScript(string script, params KeyValuePair<string, object>[] arguments)
        {
            var supplementedArguments = SupplementJavaScriptExecutorArguments(arguments);
            _javaScriptExecutor.ExecuteJavaScript(script, supplementedArguments);
        }

        public void ExecuteJavaScript(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            var supplementedArguments = SupplementJavaScriptExecutorArguments(arguments);
            _javaScriptExecutor.ExecuteJavaScript(file, supplementedArguments);
        }

        public T ExecuteJavaScript<T>(string script, params KeyValuePair<string, object>[] arguments)
        {
            var supplementedArguments = SupplementJavaScriptExecutorArguments(arguments);
            return _javaScriptExecutor.ExecuteJavaScript<T>(script, supplementedArguments);
        }

        public T ExecuteJavaScript<T>(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            var supplementedArguments = SupplementJavaScriptExecutorArguments(arguments);
            return _javaScriptExecutor.ExecuteJavaScript<T>(file, supplementedArguments);
        }

        protected abstract KeyValuePair<string, object>[] SupplementJavaScriptExecutorArguments(params KeyValuePair<string, object>[] arguments);

        #endregion Execute JavaScript

        #region Find Child/Descendant UI Elements

        public TUiElement FindUiElement<TUiElement>(string name, Use method, string criteria) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElement<TUiElement>(name, method, criteria);
        }

        public TUiElement FindUiElement<TUiElement>(string name, List<IUiElementsFindInfo> findOptions) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElement<TUiElement>(name, findOptions);
        }

        public IUiElementsList<TUiElement> FindUiElements<TUiElement>(string name, Use method, string criteria) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(name, method, criteria);
        }

        public IUiElementsList<TUiElement> FindUiElements<TUiElement>(string name, List<IUiElementsFindInfo> findOptions) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(name, findOptions);
        }

        #endregion Find Child/Descendant UI Elements

        #region Waiters

        public IUiElement WaitToExist(TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            CheckTimeoutAndPollingInterval(ref timeout, ref pollingInterval);

            Wait.WithTimeout(timeout.Value)
                .PollingEvery(pollingInterval.Value)
                .WithErrorMessage($"Element '{Info.InstantiationStage.FullName}' doesn't exist in the DOM.")
                .Until(() => Exists());

            return this;
        }

        public IUiElement WaitToExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => WaitToExist(TimeSpan.FromSeconds(timeoutSec), TimeSpan.FromSeconds(pollingIntervalSec));

        public IUiElement WaitToNotExist(TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            CheckTimeoutAndPollingInterval(ref timeout, ref pollingInterval);

            Wait.WithTimeout(timeout.Value)
                .PollingEvery(pollingInterval.Value)
                .WithErrorMessage($"Element '{Info.InstantiationStage.FullName}' still exists in the DOM.")
                .Until(() => !Exists());

            return this;
        }

        public IUiElement WaitToNotExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => WaitToNotExist(TimeSpan.FromSeconds(timeoutSec), TimeSpan.FromSeconds(pollingIntervalSec));

        public IUiElement WaitToBeVisible(TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            CheckTimeoutAndPollingInterval(ref timeout, ref pollingInterval);

            Wait.WithTimeout(timeout.Value)
                .PollingEvery(pollingInterval.Value)
                .WithErrorMessage($"Element '{Info.InstantiationStage.FullName}' has not become visible'.")
                .Until(() => IsDisplayed());

            return this;
        }

        public IUiElement WaitToBeVisible(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => WaitToBeVisible(TimeSpan.FromSeconds(timeoutSec), TimeSpan.FromSeconds(pollingIntervalSec));

        public IUiElement WaitToNotBeVisible(TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            CheckTimeoutAndPollingInterval(ref timeout, ref pollingInterval);

            Wait.WithTimeout(timeout.Value)
                .PollingEvery(pollingInterval.Value)
                .WithErrorMessage($"Element '{Info.InstantiationStage.FullName}' has not become visible'.")
                .Until(() => !IsDisplayed());

            return this;
        }

        public IUiElement WaitToNotBeVisible(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => WaitToNotBeVisible(TimeSpan.FromSeconds(timeoutSec), TimeSpan.FromSeconds(pollingIntervalSec));

        private void CheckTimeoutAndPollingInterval(ref TimeSpan? timeout, ref TimeSpan? pollingInterval)
        {
            timeout ??= TimeSpan.FromSeconds(SharedUiConstants.UiElementWaitTimeoutSec);
            pollingInterval ??= TimeSpan.FromSeconds(SharedUiConstants.UiElementWaitPollingIntervalSec);
        }

        #endregion
    }
}
