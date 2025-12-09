using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Traits.Keyboard
{
    public interface IUiInteractionTraitKeysDown
    {
        public void KeysDown(params string[] keys);
        public void KeysDown(params Key[] keys);
    }
}
