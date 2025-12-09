using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.UiElements.Interfaces;

namespace DevQAProdCom.NET.UI.UiElements.OperativeClasses
{
    public class InputText : UiElement, IInputText
    {
        public virtual void AppendText(string text)
        {
            var currentText = AddBehavior<IUiElementBehaviorGetInputText>().GetInputText();
            var fulfilledText = currentText + text;
            SetText(fulfilledText);
        }

        public virtual void ClearText()
        {
            AddBehavior<IUiElementBehaviorClearText>().ClearText();
        }

        public virtual void ClearTextByDeleteKey()
        {
            AddBehavior<IUiElementBehaviorClearTextByDeleteKey>().ClearTextByDeleteKey();
        }

        public virtual void ClearTextByBackspaceKey(int? numberOfSymbols = null, TimeSpan? delay = null)
        {
            AddBehavior<IUiElementBehaviorClearTextByBackspaceKey>().ClearTextByBackspaceKey(numberOfSymbols: numberOfSymbols, delay: delay);
        }

        public virtual void CopyPasteTextJs(string text)
        {
            AddBehavior<IUiElementBehaviorCopyPasteTextJs>().CopyPasteTextJs(text);
        }

        public virtual void SetTextCopyPasteJs(string text)
        {
            AddBehavior<IUiElementBehaviorSetTextCopyPasteJs>().SetTextCopyPasteJs(text);
        }

        public virtual string GetInputText()
        {
            return AddBehavior<IUiElementBehaviorGetInputText>().GetInputText();
        }

        public virtual bool IsInputTextEmpty()
        {
            return AddBehavior<IUiElementBehaviorIsInputTextEmpty>().IsInputTextEmpty();
        }

        public virtual string GetTextFromValueAttributeOrEmpty()
        {
            return AddBehavior<IUiElementBehaviorGetTextFromValueAttribute>().GetTextFromValueAttributeOrEmpty();
        }

        public virtual string? GetTextFromValueAttributeOrNull()
        {
            return AddBehavior<IUiElementBehaviorGetTextFromValueAttribute>().GetTextFromValueAttributeOrNull();
        }

        public virtual void SetInnerHtmlJs(string text)
        {
            AddBehavior<IUiElementBehaviorSetInnerHtmlJs>().SetInnerHtmlJs(text);
        }

        public virtual void SetText(string text)
        {
            AddBehavior<IUiElementBehaviorSetText>().SetText(text);
        }

        public virtual void SetTextContentJs(string text)
        {
            AddBehavior<IUiElementBehaviorSetTextContentJs>().SetTextContentJs(text);
        }

        public virtual void TypeText(string text)
        {
            AddBehavior<IUiElementBehaviorTypeText>().TypeText(text);
        }
    }
}
