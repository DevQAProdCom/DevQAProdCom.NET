using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Text;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.UiElements.Interfaces;

namespace DevQAProdCom.NET.UI.UiElements.OperativeClasses
{
    public class InputText : UiElement, IInputText
    {
        public void SetText(string text)
        {
            AddBehavior<IFulfillTextBehavior>().SetText(text);
        }

        public void AppendText(string text)
        {
            var currentText = AddBehavior<IGetTextBehavior>().GetText();
            var fulfilledText = currentText + text;
            SetText(fulfilledText);
        }

        public string GetText()
        {
            return AddBehavior<IGetTextBehavior>().GetText();
        }

        public void ClearText()
        {
            AddBehavior<IClearTextBehavior>().ClearText();
        }
    }
}
