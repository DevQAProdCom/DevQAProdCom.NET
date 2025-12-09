using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Keyboard
{
    public class UiElementBehaviorPressKey(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorPressKey
    {
        public void PressKey(string key)
        {
            UiElement.AddBehavior<IUiElementBehaviorPressKeysSequentially>().PressKeysSequentially(key);
        }

        public void PressKey(Key key)
        {
            PressKey(key.ToString());
        }
    }
}
