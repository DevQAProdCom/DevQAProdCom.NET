using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Traits.Text;

namespace DevQAProdCom.NET.UI.UiElements.Interfaces
{
    public interface IInputText : IUiElement,
        IUiElementTraitAppendText,
        IUiElementTraitClearText,
        IUiElementTraitCopyPasteTextJs,
        IUiElementTraitSetTextCopyPasteJs,
        IUiElementTraitGetInputText,
        IUiElementTraitIsInputTextEmpty,
        IUiElementTraitSetInnerHtmlJs,
        IUiElementTraitSetText,
        IUiElementTraitSetTextContentJs,
        IUiElementTraitTypeText,
        IUiElementTraitClearTextByDeleteKey,
        IUiElementTraitClearTextByBackspaceKey,
        IUiElementTraitGetTextFromValueAttribute
    {
    }
}
