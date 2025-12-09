using System.Drawing;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Traits.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Traits.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Traits.Mouse;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Traits.Others;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Traits.Scroll;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IUiElement :
        IHasUiElementInfo,
        IFindUiElements,
        IExecuteJavaScript,
        IUiElementWaiters,
        IHaveNativeObjects,
        IAddBehavior,

        IUiInteractionTraitKeysDown,
        IUiInteractionTraitKeysUp,
        IUiInteractionTraitPressKey,
        IUiInteractionTraitPressKeysSequentially,
        IUiInteractionTraitPressKeysSimultaneously,
        IUiElementTraitSendText,

        IUiElementTraitClick,
        IUiElementTraitClickJs,
        IUiElementTraitContextClick,
        IUiElementTraitContextClickJs,
        IUiElementTraitDoubleClick,
        IUiElementTraitDragAndDrop,
        IUiElementTraitDragAndDropByOffset,
        IUiElementTraitMouseDown,
        IUiElementTraitMouseDownJs,
        IUiElementTraitMouseHover,
        IUiElementTraitMouseUp,
        IUiElementTraitMouseUpJs,
        IUiElementTraitScrollIntoView,
        IUiElementTraitScrollIntoViewInstantlyJs,
        IUiElementTraitScrollIntoViewSmoothlyJs,

        IUiElementTraitFocusJs,
        IUiElementTraitUnfocusJs,

        IUiElementTraitRemoveJs,

        IUiElementTraitGetAttribute,
        IUiElementTraitGetIdAttribute,
        IUiElementTraitGetNameAttribute,
        IUiElementTraitGetStyleAttribute,
        IUiElementTraitSetAttributeJs,
        IUiElementTraitRemoveAttributeJs,

        IUiElementTraitGetClassAttribute,
        IUiElementTraitRemoveClassJs,
        IUiElementTraitClassAttributeContains,
        IUiElementTraitClassAttributeEquals
    {
        public IUiPage UiPage { get; }

        #region General Properties

        PointF GetLocation();
        SizeF GetSize();
        string GetTagName();
        int? UiIndex { get; }
        #endregion General Properties

        #region Specific Properties

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
    }
}
