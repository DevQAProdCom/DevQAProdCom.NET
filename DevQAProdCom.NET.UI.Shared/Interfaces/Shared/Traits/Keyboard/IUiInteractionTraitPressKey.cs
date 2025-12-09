using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Traits.Keyboard
{
    public interface IUiInteractionTraitPressKey
    {
        public void PressKey(string key);
        public void PressKey(Key key);
    }
}
