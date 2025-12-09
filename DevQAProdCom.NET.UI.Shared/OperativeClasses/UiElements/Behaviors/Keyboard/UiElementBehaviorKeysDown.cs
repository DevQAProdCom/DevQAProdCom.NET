using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Keyboard
{
    public class UiElementBehaviorKeysDown(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorKeysDown
    {
        public void KeysDown(params string[] keys)
        {
            UiElement.FocusJs();
            UiPage.KeysDown(keys);
        }

        public void KeysDown(params Key[] keys)
        {
            KeysDown(keys.Select(x => x.ToString()).ToArray());
        }
    }
}
