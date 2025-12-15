using System.Drawing;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Global.Helpers;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Others;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Scroll;
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
        public int? UiIndex => Info?.FindStage?.FindParametersWithSearchResult?.UiIndex;

        #endregion region General Properties

        #region Specific Properties
        public string? GetNonBooleanAttribute(string attributeName)
        {
            return GetAttribute(attributeName, isBooleanAttributeType: false);
        }

        public bool GetBooleanAttribute(string attributeName)
        {
            var attribute = GetAttribute(attributeName, isBooleanAttributeType: true);
            return attribute == "true";
        }

        public abstract string? GetAttribute(string attributeName, bool isBooleanAttributeType);

        public string? GetIdAttribute() => GetNonBooleanAttribute(SharedUiConstants.HtmlElementAttributes.Id);
        public string? GetNameAttribute() => GetNonBooleanAttribute(SharedUiConstants.HtmlElementAttributes.Name);
        public string? GetStyleAttribute() => GetNonBooleanAttribute(SharedUiConstants.HtmlElementAttributes.Style);
        public void SetAttributeJs(string attributeName, string attributeValue) => AddBehavior<IUiElementBehaviorSetAttributeJs>().SetAttributeJs(attributeName, attributeValue);
        public string? GetClassAttribute() => GetNonBooleanAttribute(SharedUiConstants.HtmlElementAttributes.Class);
        public bool ClassAttributeContains(string value) => GetClassAttribute()?.Contains(value) == true;
        public bool ClassAttributeEquals(string value) => GetClassAttribute() == value;
        public void RemoveAttributeJs(string attributeName) => AddBehavior<IUiElementBehaviorRemoveAttributeJs>().RemoveAttributeJs(attributeName);
        public void RemoveClassJs(string className) => AddBehavior<IUiElementBehaviorRemoveClassJs>().RemoveClassJs(className);
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
            return RetryIfStaleElementReference(() => _uiElementBehaviorFactory.Create<T>(this, _javaScriptExecutor, auxiliaryParams));
        }

        #region Keyboard Actions

        public void KeysDown(params string[] keys) => AddBehavior<IUiElementBehaviorKeysDown>().KeysDown(keys);
        public void KeysDown(params Key[] keys) => AddBehavior<IUiElementBehaviorKeysDown>().KeysDown(keys);

        public void KeysUp(params string[] keys) => AddBehavior<IUiElementBehaviorKeysUp>().KeysUp(keys);
        public void KeysUp(params Key[] keys) => AddBehavior<IUiElementBehaviorKeysUp>().KeysUp(keys);

        public void PressKey(string key) => AddBehavior<IUiElementBehaviorPressKey>().PressKey(key);
        public void PressKey(Key key) => AddBehavior<IUiElementBehaviorPressKey>().PressKey(key);

        public void PressKeysSequentially(params string[] keys) => AddBehavior<IUiElementBehaviorPressKeysSequentially>().PressKeysSequentially(keys);
        public void PressKeysSequentially(params Key[] keys) => AddBehavior<IUiElementBehaviorPressKeysSequentially>().PressKeysSequentially(keys);

        public void PressKeysCombination(string keysCombination, string separator = SharedUiConstants.DefaultKeysCombinationSeparator) => AddBehavior<IUiElementBehaviorPressKeysSimultaneously>().PressKeysCombination(keysCombination, separator);
        public void PressKeysSimultaneously(params string[] keys) => AddBehavior<IUiElementBehaviorPressKeysSimultaneously>().PressKeysSimultaneously(keys);
        public void PressKeysSimultaneously(params Key[] keys) => AddBehavior<IUiElementBehaviorPressKeysSimultaneously>().PressKeysSimultaneously(keys);

        public void SendText(string textKeys) => AddBehavior<IUiElementBehaviorSendText>().SendText(textKeys);

        #endregion Keyboard Actions

        #region Mouse Actions

        public void Click() => AddBehavior<IUiElementBehaviorClick>().Click();
        public void ClickJs() => AddBehavior<IUiElementBehaviorClickJs>().ClickJs();
        public void ContextClick() => AddBehavior<IUiElementBehaviorContextClick>().ContextClick();
        public void ContextClickJs() => AddBehavior<IUiElementBehaviorContextClickJs>().ContextClickJs();
        public void DoubleClick() => AddBehavior<IUiElementBehaviorDoubleClick>().DoubleClick();
        public void DragAndDrop(IUiElement uiElementToDrop) => AddBehavior<IUiElementBehaviorDragAndDrop>().DragAndDrop(uiElementToDrop);
        public void DragAndDropByOffset(float offsetX, float offsetY) => AddBehavior<IUiElementBehaviorDragAndDropByOffset>().DragAndDropByOffset(offsetX, offsetY);
        public void MouseDown() => AddBehavior<IUiElementBehaviorMouseDown>().MouseDown();
        public void MouseDownJs() => AddBehavior<IUiElementBehaviorMouseDownJs>().MouseDownJs();
        public void MouseHover() => AddBehavior<IUiElementBehaviorMouseHover>().MouseHover();
        public void MouseUp() => AddBehavior<IUiElementBehaviorMouseUp>().MouseUp();
        public void MouseUpJs() => AddBehavior<IUiElementBehaviorMouseUpJs>().MouseUpJs();
        public void ScrollIntoView() => AddBehavior<IUiElementBehaviorScrollIntoView>().ScrollIntoView();
        public void ScrollIntoViewInstantlyJs() => AddBehavior<IUiElementBehaviorScrollIntoViewInstantlyJs>().ScrollIntoViewInstantlyJs();
        public void ScrollIntoViewSmoothlyJs() => AddBehavior<IUiElementBehaviorScrollIntoViewSmoothlyJs>().ScrollIntoViewSmoothlyJs();

        #endregion Mouse Actions

        #region Other Actions

        public void FocusJs() => AddBehavior<IUiElementBehaviorFocusJs>().FocusJs();
        public void RemoveJs() => AddBehavior<IUiElementBehaviorRemoveJs>().RemoveJs();
        public void UnfocusJs() => AddBehavior<IUiElementBehaviorUnfocusJs>().UnfocusJs();

        #endregion Other Actions

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

        public TUiElement Find<TUiElement>(string method, string criteria, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElement<TUiElement>(method, criteria, parentUiElement: this, name: name);
        }

        public TUiElement Find<TUiElement>(Use method, string criteria, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElement<TUiElement>(method, criteria, parentUiElement: this, name: name);
        }

        public TUiElement Find<TUiElement>(List<IUiElementsFindInfo> findOptions, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElement<TUiElement>(findOptions, parentUiElement: this, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(string method, string criteria, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(method, criteria, parentUiElement: this, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(Use method, string criteria, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(method, criteria, parentUiElement: this, name: name);
        }

        public IUiElementsList<TUiElement> FindAll<TUiElement>(List<IUiElementsFindInfo> findOptions, string? name = null) where TUiElement : IUiElement
        {
            return UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(findOptions, parentUiElement: this, name: name);
        }

        #endregion Find Child/Descendant UI Elements

        #region Waiters
        //TODO Add WaitNotStale
        public IUiElement WaitToExist(TimeSpan? timeout, TimeSpan? pollingInterval = null)
        {
            CheckTimeoutAndPollingInterval(ref timeout, ref pollingInterval);

            Wait.WithTimeout(timeout.Value)
                .PollingEvery(pollingInterval.Value)
                .WithErrorMessage($"UiElement '{Info.InstantiationStage.FullName}' doesn't exist in the DOM.")
                .Until(() => Exists());

            return this;
        }

        public IUiElement WaitToExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => WaitToExist(TimeSpan.FromSeconds(timeoutSec), TimeSpan.FromSeconds(pollingIntervalSec));

        public IUiElement WaitToNotExist(TimeSpan? timeout, TimeSpan? pollingInterval = null)
        {
            CheckTimeoutAndPollingInterval(ref timeout, ref pollingInterval);

            Wait.WithTimeout(timeout.Value)
                .PollingEvery(pollingInterval.Value)
                .WithErrorMessage($"UiElement '{Info.InstantiationStage.FullName}' still exists in the DOM.")
                .Until(() => !Exists());

            return this;
        }

        public IUiElement WaitToNotExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => WaitToNotExist(TimeSpan.FromSeconds(timeoutSec), TimeSpan.FromSeconds(pollingIntervalSec));

        public IUiElement WaitToBeDisplayed(TimeSpan? timeout, TimeSpan? pollingInterval = null)
        {
            CheckTimeoutAndPollingInterval(ref timeout, ref pollingInterval);

            Wait.WithTimeout(timeout.Value)
                .PollingEvery(pollingInterval.Value)
                .WithErrorMessage($"UiElement '{Info.InstantiationStage.FullName}' has not become displayed.")
                .Until(() => IsDisplayed());

            return this;
        }

        public IUiElement WaitToBeDisplayed(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => WaitToBeDisplayed(TimeSpan.FromSeconds(timeoutSec), TimeSpan.FromSeconds(pollingIntervalSec));

        public IUiElement WaitToBeHidden(TimeSpan? timeout, TimeSpan? pollingInterval = null)
        {
            CheckTimeoutAndPollingInterval(ref timeout, ref pollingInterval);

            Wait.WithTimeout(timeout.Value)
                .PollingEvery(pollingInterval.Value)
                .WithErrorMessage($"UiElement '{Info.InstantiationStage.FullName}' has not become hidden, but is still visible.")
                .Until(() => !IsDisplayed());

            return this;
        }

        public IUiElement WaitToBeHidden(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => WaitToBeHidden(TimeSpan.FromSeconds(timeoutSec), TimeSpan.FromSeconds(pollingIntervalSec));

        public IUiElement WaitToBeEnabled(TimeSpan? timeout, TimeSpan? pollingInterval = null)
        {
            CheckTimeoutAndPollingInterval(ref timeout, ref pollingInterval);

            Wait.WithTimeout(timeout.Value)
                .PollingEvery(pollingInterval.Value)
                .WithErrorMessage($"UiElement '{Info.InstantiationStage.FullName}' has not become enabled.")
                .Until(() => IsEnabled());

            return this;
        }

        public IUiElement WaitToBeEnabled(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => WaitToBeEnabled(TimeSpan.FromSeconds(timeoutSec), TimeSpan.FromSeconds(pollingIntervalSec));

        public IUiElement WaitToBeDisabled(TimeSpan? timeout, TimeSpan? pollingInterval = null)
        {
            CheckTimeoutAndPollingInterval(ref timeout, ref pollingInterval);

            Wait.WithTimeout(timeout.Value)
                .PollingEvery(pollingInterval.Value)
                .WithErrorMessage($"UiElement '{Info.InstantiationStage.FullName}' has not become disabled, but is still enabled.")
                .Until(() => !IsEnabled());

            return this;
        }

        public IUiElement WaitToBeDisabled(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => WaitToBeDisabled(TimeSpan.FromSeconds(timeoutSec), TimeSpan.FromSeconds(pollingIntervalSec));

        private void CheckTimeoutAndPollingInterval(ref TimeSpan? timeout, ref TimeSpan? pollingInterval)
        {
            timeout ??= TimeSpan.FromSeconds(SharedUiConstants.UiElementWaitTimeoutSec);
            pollingInterval ??= TimeSpan.FromSeconds(SharedUiConstants.UiElementWaitPollingIntervalSec);
        }

        #endregion

        protected T RetryIfStaleElementReference<T>(Func<T> func)
        {
            if (!TryReturnResultOrStale(func, out T result))
            {
                ThrowUiElementStaleReferenceExceptionIfIsElementOfList();
                Refind();
                if (!TryReturnResultOrStale(func, out result))
                    throw new UiElementStaleReferenceException($"UiElement '{Info.InstantiationStage.FullName}' is stale even after retry.");
            }
            _log.Info($"{result}");
            return result;
        }

        protected abstract bool TryReturnResultOrStale<T>(Func<T> func, out T result);

        protected void ThrowUiElementStaleReferenceExceptionIfIsElementOfList()
        {
            if (Info.InstantiationStage.IsElementOfList)
                throw new UiElementStaleReferenceException($"UiElement '{Info.InstantiationStage.FullName}' is stale and is part of UiElements list. Refind of all list is required.");
        }

        public abstract IUiElement Refind();
    }
}
