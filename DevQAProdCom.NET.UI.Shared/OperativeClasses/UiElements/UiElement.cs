using System.Drawing;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements
{
    public abstract class UiElement : IParentUiElement
    {
        public IUiPage UiPage { get; internal set; }

        internal IUiElement InternalUiElement { get; set; }
        internal INativeElementsSearcher NativeElementsSearcher { get; set; }
        internal IUiElementsInstantiator UiElementsInstantiator { get; set; }
        internal IExecuteJavaScript JavaScriptExecutor { get; set; }

        public IUiElementInfo Info => InternalUiElement.Info;
        public Dictionary<string, object> NativeObjects => InternalUiElement.NativeObjects;

        #region General Properties

        public PointF GetLocation() => InternalUiElement.GetLocation();
        public SizeF GetSize() => InternalUiElement.GetSize();
        public string GetTagName() => InternalUiElement.GetTagName();

        #endregion General Properties

        #region Specific Properties

        public string? GetAttribute(string attributeName, bool isBooleanAttributeType) => InternalUiElement.GetAttribute(attributeName, isBooleanAttributeType);
        public string? GetCssValue(string propertyName) => InternalUiElement.GetCssValue(propertyName);
        public string GetTextContent() => InternalUiElement.GetTextContent();

        #endregion Specific Properties

        #region States

        public bool Exists() => InternalUiElement.Exists();
        public bool IsDisabled() => InternalUiElement.IsDisabled();
        public bool IsDisplayed() => InternalUiElement.IsDisplayed();
        public bool IsElementInViewportJs() => InternalUiElement.IsElementInViewportJs();
        public bool IsEnabled() => InternalUiElement.IsEnabled();

        #endregion States

        #region Actions

        public T AddBehavior<T>(params KeyValuePair<string, object>[]? auxiliaryParams) where T : IBehavior => InternalUiElement.AddBehavior<T>(auxiliaryParams);
        public void FocusJs() => InternalUiElement.FocusJs();
        public void MouseClick() => InternalUiElement.MouseClick();
        public void ScrollToElement() => InternalUiElement.ScrollToElement();


        #endregion Actions

        #region Execute JavaScript

        public void ExecuteJavaScript(string script, params KeyValuePair<string, object>[] arguments)
        {
            InternalUiElement.ExecuteJavaScript(script, arguments);
        }

        public void ExecuteJavaScript(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            InternalUiElement.ExecuteJavaScript(file, arguments);
        }

        public T ExecuteJavaScript<T>(string script, params KeyValuePair<string, object>[] arguments)
        {
            return InternalUiElement.ExecuteJavaScript<T>(script, arguments);
        }

        public T ExecuteJavaScript<T>(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            return InternalUiElement.ExecuteJavaScript<T>(file, arguments);
        }

        #endregion Execute JavaScript

        #region Find Child/Descendant UI Elements

        public TUiElement FindUiElement<TUiElement>(string name, Use method, string criteria) where TUiElement : IUiElement
            => InternalUiElement.FindUiElement<TUiElement>(name, method, criteria);

        public TUiElement FindUiElement<TUiElement>(string name, List<IUiElementsFindInfo> findOptions) where TUiElement : IUiElement
            => InternalUiElement.FindUiElement<TUiElement>(name, findOptions);

        public IUiElementsList<TUiElement> FindUiElements<TUiElement>(string name, Use method, string criteria) where TUiElement : IUiElement
            => InternalUiElement.FindUiElements<TUiElement>(name, method, criteria);

        public IUiElementsList<TUiElement> FindUiElements<TUiElement>(string name, List<IUiElementsFindInfo> findOptions) where TUiElement : IUiElement
            => InternalUiElement.FindUiElements<TUiElement>(name, findOptions);

        #endregion Find Child/Descendant UI Elements

        #region Instantiate UI Elements

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

        #endregion Instantiate UI Elements

        #region Waiters

        public IUiElement WaitToExist(TimeSpan? timeout = null, TimeSpan? pollingInterval = null) => InternalUiElement.WaitToExist(timeout, pollingInterval);
        public IUiElement WaitToExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => InternalUiElement.WaitToExist(timeoutSec, pollingIntervalSec);
        public IUiElement WaitToNotExist(TimeSpan? timeout = null, TimeSpan? pollingInterval = null) => InternalUiElement.WaitToNotExist(timeout, pollingInterval);
        public IUiElement WaitToNotExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => InternalUiElement.WaitToNotExist(timeoutSec, pollingIntervalSec);
        public IUiElement WaitToBeVisible(TimeSpan? timeout = null, TimeSpan? pollingInterval = null) => InternalUiElement.WaitToBeVisible(timeout, pollingInterval);
        public IUiElement WaitToBeVisible(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => InternalUiElement.WaitToBeVisible(timeoutSec, pollingIntervalSec);
        public IUiElement WaitToNotBeVisible(TimeSpan? timeout = null, TimeSpan? pollingInterval = null) => InternalUiElement.WaitToNotBeVisible(timeout, pollingInterval);
        public IUiElement WaitToNotBeVisible(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => InternalUiElement.WaitToNotBeVisible(timeoutSec, pollingIntervalSec);

        #endregion
    }
}
