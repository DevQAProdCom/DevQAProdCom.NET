using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class UiElementBehaviorMouseUpJs(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorMouseUpJs
    {
        public void MouseUpJs()
        {
            UiElement.MouseHover();
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.MouseUpJavaScriptFilePath));
        }
    }
}
