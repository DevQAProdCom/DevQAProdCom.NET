using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Text
{
    public class UiElementBehaviorAppendText(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorAppendText
    {
        public void AppendText(string text)
        {
            UiElement.FocusJs();
            var currentText = UiElement.AddBehavior<IUiElementBehaviorGetInputText>().GetInputText();
            var textToAppend = currentText + text;
            UiElement.AddBehavior<IUiElementBehaviorTypeText>().TypeText(textToAppend);
        }
    }
}
