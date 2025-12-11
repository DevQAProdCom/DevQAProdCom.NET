using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Others;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Others
{
    public class UiElementBehaviorFocusJs(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorFocusJs
    {
        public void FocusJs()
        {
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.FocusJavaScriptFilePath));
        }
    }
}
