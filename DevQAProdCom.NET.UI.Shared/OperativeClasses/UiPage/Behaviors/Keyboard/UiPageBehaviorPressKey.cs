using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Keyboard;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors.Keyboard
{
    public class UiPageBehaviorPressKey(IBehaviorParameters parameters) : UiPageBehavior(parameters), IUiPageBehaviorPressKey
    {
        public void PressKey(string key)
        {
            UiPage.AddBehavior<IUiPageBehaviorPressKeysSequentially>().PressKeysSequentially(key);
        }

        public void PressKey(Key key)
        {
            PressKey(key.ToString());
        }
    }
}
