using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.UiElements.Interfaces;

namespace DevQAProdCom.NET.UI.UiElements.OperativeClasses
{
    internal class Label : UiElement, ILabel
    {
        public virtual string GetInputText()
        {
            return AddBehavior<IUiElementBehaviorGetInputText>().GetInputText();
        }
    }
}
