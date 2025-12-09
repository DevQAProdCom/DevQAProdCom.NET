using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Text
{
    public class UiElementBehaviorClearTextByBackspaceKey(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorClearTextByBackspaceKey
    {
        public void ClearTextByBackspaceKey(int? numberOfSymbols = null, TimeSpan? delay = null)
        {
            UiElement.FocusJs();

            numberOfSymbols ??= UiElement.AddBehavior<IUiElementBehaviorGetInputText>().GetInputText().Length;
            delay ??= TimeSpan.Zero;

            for (int i = 0; i < numberOfSymbols; i++)
            {
                UiElement.PressKeysSimultaneously(Key.Backspace);
                if (delay > TimeSpan.Zero)
                    Thread.Sleep(delay.Value);
            }
        }
    }
}
