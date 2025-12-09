using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Keyboard
{
    public class UiElementBehaviorPressKeysSequentially(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorPressKeysSequentially
    {
        public void PressKeysSequentially(params string[] keys)
        {
            UiElement.FocusJs();
            UiPage.PressKeysSequentially(keys);
        }

        public void PressKeysSequentially(params Key[] keys)
        {
            PressKeysSequentially(keys.Select(x => x.ToString()).ToArray());
        }
    }
}
