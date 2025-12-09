using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Keyboard
{
    public class UiElementBehaviorPressKeysSimultaneously(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorPressKeysSimultaneously
    {
        public void PressKeysSimultaneously(params string[] keys)
        {
            UiElement.FocusJs();
            UiPage.PressKeysSimultaneously(keys);
        }

        public void PressKeysSimultaneously(params Key[] keys)
        {
            PressKeysSimultaneously(keys.Select(x => x.ToString()).ToArray());
        }
    }
}
