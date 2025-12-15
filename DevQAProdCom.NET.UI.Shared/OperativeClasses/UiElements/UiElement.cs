using System.Drawing;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements
{
    public abstract class UiElement : IUiElement
    {
        public IUiPage UiPage { get; internal set; }

        internal IUiElement InternalUiElement { get; set; }
        internal INativeElementsSearcher NativeElementsSearcher { get; set; }
        internal IUiElementsInstantiator UiElementsInstantiator { get; set; }
        internal IExecuteJavaScript JavaScriptExecutor { get; set; }

        public IUiElementInfo Info => InternalUiElement.Info;
        public Dictionary<string, object> NativeObjects => InternalUiElement.NativeObjects;

        #region General Properties

        public virtual PointF GetLocation() => InternalUiElement.GetLocation();
        public virtual SizeF GetSize() => InternalUiElement.GetSize();
        public virtual string GetTagName() => InternalUiElement.GetTagName();
        public int? UiIndex => InternalUiElement.UiIndex;

        #endregion General Properties

        #region Specific Properties

        public virtual string? GetAttribute(string attributeName, bool isBooleanAttributeType) => InternalUiElement.GetAttribute(attributeName, isBooleanAttributeType);
        public virtual bool GetBooleanAttribute(string attributeName) => InternalUiElement.GetBooleanAttribute(attributeName);
        public virtual string? GetNonBooleanAttribute(string attributeName) => InternalUiElement.GetNonBooleanAttribute(attributeName);
        public virtual string? GetIdAttribute() => InternalUiElement.GetIdAttribute();
        public virtual string? GetNameAttribute() => InternalUiElement.GetNameAttribute();
        public virtual string? GetStyleAttribute() => InternalUiElement.GetStyleAttribute();
        public virtual void SetAttributeJs(string attributeName, string attributeValue) => InternalUiElement.SetAttributeJs(attributeName, attributeValue);
        public virtual string? GetClassAttribute() => InternalUiElement.GetClassAttribute();
        public virtual bool ClassAttributeContains(string value) => InternalUiElement.ClassAttributeContains(value);
        public virtual bool ClassAttributeEquals(string value) => InternalUiElement.ClassAttributeEquals(value);
        public virtual void RemoveAttributeJs(string attributeName) => InternalUiElement.RemoveAttributeJs(attributeName);
        public virtual void RemoveClassJs(string className) => InternalUiElement.RemoveClassJs(className);
        public virtual string? GetCssValue(string propertyName) => InternalUiElement.GetCssValue(propertyName);
        public virtual string GetTextContent() => InternalUiElement.GetTextContent();

        #endregion Specific Properties

        #region States

        public virtual bool Exists() => InternalUiElement.Exists();
        public virtual bool IsDisabled() => InternalUiElement.IsDisabled();
        public virtual bool IsDisplayed() => InternalUiElement.IsDisplayed();
        public virtual bool IsElementInViewportJs() => InternalUiElement.IsElementInViewportJs();
        public virtual bool IsEnabled() => InternalUiElement.IsEnabled();

        #endregion States

        #region Actions

        public T AddBehavior<T>(params KeyValuePair<string, object>[]? auxiliaryParams) where T : IBehavior => InternalUiElement.AddBehavior<T>(auxiliaryParams);

        #region Keyboard Actions
        public virtual void KeysDown(params string[] keys) => InternalUiElement.KeysDown(keys);
        public virtual void KeysDown(params Key[] keys) => InternalUiElement.KeysDown(keys);

        public virtual void KeysUp(params string[] keys) => InternalUiElement.KeysUp(keys);
        public virtual void KeysUp(params Key[] keys) => InternalUiElement.KeysUp(keys);

        public virtual void PressKey(string key) => InternalUiElement.PressKey(key);
        public virtual void PressKey(Key key) => InternalUiElement.PressKey(key);

        public virtual void PressKeysSequentially(params string[] keys) => InternalUiElement.PressKeysSequentially(keys);
        public virtual void PressKeysSequentially(params Key[] keys) => InternalUiElement.PressKeysSequentially(keys);

        public virtual void PressKeysCombination(string keysCombination, string separator = SharedUiConstants.DefaultKeysCombinationSeparator) => InternalUiElement.PressKeysCombination(keysCombination, separator);
        public virtual void PressKeysSimultaneously(params string[] keys) => InternalUiElement.PressKeysSimultaneously(keys);
        public virtual void PressKeysSimultaneously(params Key[] keys) => InternalUiElement.PressKeysSimultaneously(keys);

        public virtual void SendText(string textKeys) => InternalUiElement.SendText(textKeys);

        #endregion Keyboard Actions

        #region Mouse Actions

        public virtual void Click() => InternalUiElement.Click();
        public virtual void ClickJs() => InternalUiElement.ClickJs();
        public virtual void ContextClick() => InternalUiElement.ContextClick();
        public virtual void ContextClickJs() => InternalUiElement.ContextClickJs();
        public virtual void DoubleClick() => InternalUiElement.DoubleClick();
        public virtual void DragAndDrop(IUiElement uiElementToDrop) => InternalUiElement.DragAndDrop(uiElementToDrop);
        public virtual void DragAndDropByOffset(float offsetX, float offsetY) => InternalUiElement.DragAndDropByOffset(offsetX, offsetY);
        public virtual void MouseDown() => InternalUiElement.MouseDown();
        public virtual void MouseDownJs() => InternalUiElement.MouseDownJs();
        public virtual void MouseHover() => InternalUiElement.MouseHover();
        public virtual void MouseUp() => InternalUiElement.MouseUp();
        public virtual void MouseUpJs() => InternalUiElement.MouseUpJs();
        public virtual void ScrollIntoView() => InternalUiElement.ScrollIntoView();
        public virtual void ScrollIntoViewInstantlyJs() => InternalUiElement.ScrollIntoViewInstantlyJs();
        public virtual void ScrollIntoViewSmoothlyJs() => InternalUiElement.ScrollIntoViewSmoothlyJs();

        #endregion Mouse Actions

        #region Other Actions

        public virtual void FocusJs() => InternalUiElement.FocusJs();
        public virtual void RemoveJs() => InternalUiElement.RemoveJs();
        public virtual void UnfocusJs() => InternalUiElement.UnfocusJs();

        #endregion Other Actions

        #endregion Actions

        #region Execute JavaScript

        public virtual void ExecuteJavaScript(string script, params KeyValuePair<string, object>[] arguments)
        {
            InternalUiElement.ExecuteJavaScript(script, arguments);
        }

        public virtual void ExecuteJavaScript(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            InternalUiElement.ExecuteJavaScript(file, arguments);
        }

        public virtual T ExecuteJavaScript<T>(string script, params KeyValuePair<string, object>[] arguments)
        {
            return InternalUiElement.ExecuteJavaScript<T>(script, arguments);
        }

        public virtual T ExecuteJavaScript<T>(FileInfo file, params KeyValuePair<string, object>[] arguments)
        {
            return InternalUiElement.ExecuteJavaScript<T>(file, arguments);
        }

        #endregion Execute JavaScript

        #region Find Child/Descendant UI Elements

        public virtual TUiElement Find<TUiElement>(string method, string criteria, string? name = null) where TUiElement : IUiElement
            => UiElementsInstantiator.InstantiateUiElement<TUiElement>(method, criteria, parentUiElement: this, name: name);

        public virtual TUiElement Find<TUiElement>(Use method, string criteria, string? name = null) where TUiElement : IUiElement
            => UiElementsInstantiator.InstantiateUiElement<TUiElement>(method, criteria, parentUiElement: this, name: name);

        public virtual TUiElement Find<TUiElement>(List<IUiElementsFindInfo> findOptions, string? name = null) where TUiElement : IUiElement
            => UiElementsInstantiator.InstantiateUiElement<TUiElement>(findOptions, parentUiElement: this, name: name);

        public virtual IUiElementsList<TUiElement> FindAll<TUiElement>(string method, string criteria, string? name = null) where TUiElement : IUiElement
            => UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(method, criteria, parentUiElement: this, name: name);

        public virtual IUiElementsList<TUiElement> FindAll<TUiElement>(Use method, string criteria, string? name = null) where TUiElement : IUiElement
            => UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(method, criteria, parentUiElement: this, name: name);

        public virtual IUiElementsList<TUiElement> FindAll<TUiElement>(List<IUiElementsFindInfo> findOptions, string? name = null) where TUiElement : IUiElement
            => UiElementsInstantiator.InstantiateUiElementsList<TUiElement>(findOptions, parentUiElement: this, name: name);

        #endregion Find Child/Descendant UI Elements

        #region Waiters

        public virtual IUiElement WaitToExist(TimeSpan? timeout, TimeSpan? pollingInterval = null) => InternalUiElement.WaitToExist(timeout, pollingInterval);
        public virtual IUiElement WaitToExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => InternalUiElement.WaitToExist(timeoutSec, pollingIntervalSec);

        public virtual IUiElement WaitToNotExist(TimeSpan? timeout, TimeSpan? pollingInterval = null) => InternalUiElement.WaitToNotExist(timeout, pollingInterval);
        public virtual IUiElement WaitToNotExist(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => InternalUiElement.WaitToNotExist(timeoutSec, pollingIntervalSec);

        public virtual IUiElement WaitToBeDisplayed(TimeSpan? timeout, TimeSpan? pollingInterval = null) => InternalUiElement.WaitToBeDisplayed(timeout, pollingInterval);
        public virtual IUiElement WaitToBeDisplayed(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => InternalUiElement.WaitToBeDisplayed(timeoutSec, pollingIntervalSec);

        public virtual IUiElement WaitToBeHidden(TimeSpan? timeout, TimeSpan? pollingInterval = null) => InternalUiElement.WaitToBeHidden(timeout, pollingInterval);
        public virtual IUiElement WaitToBeHidden(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => InternalUiElement.WaitToBeHidden(timeoutSec, pollingIntervalSec);

        public virtual IUiElement WaitToBeEnabled(TimeSpan? timeout, TimeSpan? pollingInterval = null) => InternalUiElement.WaitToBeEnabled(timeout, pollingInterval);
        public virtual IUiElement WaitToBeEnabled(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => InternalUiElement.WaitToBeEnabled(timeoutSec, pollingIntervalSec);

        public virtual IUiElement WaitToBeDisabled(TimeSpan? timeout, TimeSpan? pollingInterval = null) => InternalUiElement.WaitToBeDisabled(timeout, pollingInterval);
        public virtual IUiElement WaitToBeDisabled(double timeoutSec = SharedUiConstants.UiElementWaitTimeoutSec, double pollingIntervalSec = SharedUiConstants.UiElementWaitPollingIntervalSec)
            => InternalUiElement.WaitToBeDisabled(timeoutSec, pollingIntervalSec);

        #endregion

        public IUiElement Refind() => InternalUiElement.Refind();
    }
}
