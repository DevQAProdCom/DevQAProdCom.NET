using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Text
{
    public class UiElementBehaviorIsInputTextEmpty(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorIsInputTextEmpty
    {
        public bool IsInputTextEmpty()
        {
            UiElement.FocusJs();
            var currentText = UiElement.AddBehavior<IUiElementBehaviorGetInputText>().GetInputText();
            return string.IsNullOrEmpty(currentText);
        }
    }
}
