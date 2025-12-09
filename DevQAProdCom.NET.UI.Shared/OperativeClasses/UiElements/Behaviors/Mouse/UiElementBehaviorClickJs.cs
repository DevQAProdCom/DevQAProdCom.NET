using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Mouse
{
    public class UiElementBehaviorClickJs(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorClickJs
    {
        public void ClickJs()
        {
            UiElement.MouseHover();
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.ClickJavaScriptFilePath));
        }
    }
}
