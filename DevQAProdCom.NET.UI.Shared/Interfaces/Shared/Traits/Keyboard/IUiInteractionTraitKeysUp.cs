using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Traits.Keyboard
{
    public interface IUiInteractionTraitKeysUp
    {
        public void KeysUp(params string[] keys);
        public void KeysUp(params Key[] keys);
    }
}
