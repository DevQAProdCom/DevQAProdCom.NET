using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.Shared.Traits.Keyboard
{
    public interface IUiInteractionTraitPressKeysSimultaneously
    {
        public void PressKeysCombination(string keysCombination, string separator = SharedUiConstants.DefaultKeysCombinationSeparator)
        {
            var keys = keysCombination.ToArray(separator: separator);
            PressKeysSimultaneously(keys);
        }

        public void PressKeysSimultaneously(params string[] keys);
        public void PressKeysSimultaneously(params Key[] keys);
    }
}
