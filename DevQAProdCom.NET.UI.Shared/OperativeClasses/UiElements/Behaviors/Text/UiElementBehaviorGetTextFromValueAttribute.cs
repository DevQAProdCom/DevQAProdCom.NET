using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Others;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Text
{
    public class UiElementBehaviorGetTextFromValueAttribute(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorGetTextFromValueAttribute
    {
        public string GetTextFromValueAttributeOrEmpty()
        {
            return GetTextFromValueAttributeOrNull() ?? string.Empty;
        }

        public string? GetTextFromValueAttributeOrNull()
        {
            return UiElement.AddBehavior<IUiElementBehaviorGetValueAttribute>().GetValueAttribute();
        }
    }
}
