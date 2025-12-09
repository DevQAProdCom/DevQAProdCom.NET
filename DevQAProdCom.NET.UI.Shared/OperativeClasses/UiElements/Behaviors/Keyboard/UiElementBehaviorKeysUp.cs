using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Keyboard
{
    public class UiElementBehaviorKeysUp(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorKeysUp
    {
        public void KeysUp(params string[] keys)
        {
            UiElement.FocusJs();
            UiPage.KeysUp(keys);
        }

        public void KeysUp(params Key[] keys)
        {
            KeysUp(keys.Select(x => x.ToString()).ToArray());
        }
    }
}
