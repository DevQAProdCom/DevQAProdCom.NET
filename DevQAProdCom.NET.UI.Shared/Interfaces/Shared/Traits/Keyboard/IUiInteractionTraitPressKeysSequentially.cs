using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Traits.Keyboard
{
    public interface IUiInteractionTraitPressKeysSequentially
    {
        public void PressKeysSequentially(params string[] keys);
        public void PressKeysSequentially(params Key[] keys);
    }
}
