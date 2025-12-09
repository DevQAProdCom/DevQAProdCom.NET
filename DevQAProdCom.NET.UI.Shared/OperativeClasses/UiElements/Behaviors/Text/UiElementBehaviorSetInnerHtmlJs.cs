using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Text
{
    public class UiElementBehaviorSetInnerHtmlJs(IBehaviorParameters parameters) : BaseUiElementBehaviorTextJs(parameters), IUiElementBehaviorSetInnerHtmlJs
    {
        public void SetInnerHtmlJs(string text)
        {
            ExecuteScript(SharedUiConstants.Files.SetInnerHtmlJavaScriptFilePath, text);
        }
    }
}
