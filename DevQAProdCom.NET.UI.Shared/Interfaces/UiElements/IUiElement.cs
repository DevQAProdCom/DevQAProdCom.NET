using System.Drawing;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IUiElement : IHasUiElementInfo, IUiElementsSearcher, IExecuteJavaScript, IUiElementWaiters, IHasNativeObjects
    {
        public IUiPage UiPage { get; }

        #region General Properties

        PointF GetLocation();
        SizeF GetSize();
        string GetTagName();

        #endregion General Properties

        #region Specific Properties

        string? GetAttribute(string attributeName, bool isBooleanAttributeType); //explicit attributes / boolean attributes //TODO write comment why isPlaywright returns "empty"  if attrubite exists but not set to particular value
        string? GetCssValue(string propertyName);
        string GetTextContent();

        #endregion Specific Properties

        #region States

        bool Exists();
        bool IsDisabled();
        bool IsDisplayed();
        bool IsElementInViewportJs();
        bool IsEnabled();

        #endregion States

        #region Actions

        T AddBehavior<T>(params KeyValuePair<string, object>[]? auxiliaryParams) where T : IBehavior;
        void FocusJs();
        void MouseClick();
        void ScrollToElement();

        #endregion Actions
    }
}
