using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Keyboard;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors.Keyboard
{
    public class UiPageBehaviorPressKeysSimultaneously(IBehaviorParameters parameters) : UiPageBehavior(parameters), IUiPageBehaviorPressKeysSimultaneously
    {
        public void PressKeysSimultaneously(params string[] keys)
        {
            if (keys?.Length > 0)
            {
                //Key Down
                UiPage.KeysDown(keys);

                //Keys Up
                var keysReversed = keys.Reverse().ToArray();
                UiPage.KeysUp(keysReversed.ToArray());
            }
        }

        public void PressKeysSimultaneously(params Key[] keys)
        {
            PressKeysSimultaneously(keys.Select(x => x.ToString()).ToArray());
        }
    }
}
