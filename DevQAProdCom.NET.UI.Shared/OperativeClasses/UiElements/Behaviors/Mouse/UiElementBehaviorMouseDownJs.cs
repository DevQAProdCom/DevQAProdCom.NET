using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class UiElementBehaviorMouseDownJs(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorMouseDownJs
    {
        public void MouseDownJs()
        {
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.UiElementMouseDownJavaScriptFilePath));
        }
    }
}
