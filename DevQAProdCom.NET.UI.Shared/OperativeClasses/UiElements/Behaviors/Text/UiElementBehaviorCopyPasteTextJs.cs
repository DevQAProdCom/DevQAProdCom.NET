using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Text
{
    public class UiElementBehaviorCopyPasteTextJs(IBehaviorParameters parameters) : BaseUiElementBehaviorTextJs(parameters), IUiElementBehaviorCopyPasteTextJs
    {
        public void CopyPasteTextJs(string text)
        {
            ExecuteScript(SharedUiConstants.Files.CopyPasteTextJavaScriptFilePath, text);
            UiElement.PressKeysCombination($"{Key.Control}+v");
        }
    }
}
