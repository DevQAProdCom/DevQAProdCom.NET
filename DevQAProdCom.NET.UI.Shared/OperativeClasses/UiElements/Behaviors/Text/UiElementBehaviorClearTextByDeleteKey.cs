using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Text
{
    public class UiElementBehaviorClearTextByDeleteKey(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorClearTextByDeleteKey
    {
        public void ClearTextByDeleteKey()
        {
            UiElement.FocusJs();
            UiElement.PressKeysCombination($"{Key.Control}+a");
            UiElement.PressKeysSimultaneously(Key.Delete);
        }
    }
}
