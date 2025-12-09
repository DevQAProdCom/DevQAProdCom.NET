using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Text
{
    public class UiElementBehaviorSetTextContentJs(IBehaviorParameters parameters) : BaseUiElementBehaviorTextJs(parameters), IUiElementBehaviorSetTextContentJs
    {
        public void SetTextContentJs(string text)
        {
            ExecuteScript(SharedUiConstants.Files.SetTextContentJavaScriptFilePath, text);
        }
    }
}
