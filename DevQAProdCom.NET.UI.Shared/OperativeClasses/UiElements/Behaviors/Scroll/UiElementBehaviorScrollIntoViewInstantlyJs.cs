using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Scroll;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Scroll
{
    public class UiElementBehaviorScrollIntoViewInstantlyJs(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorScrollIntoViewInstantlyJs
    {
        public void ScrollIntoViewInstantlyJs()
        {
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.ScrollIntoViewInstantlyJavaScriptFilePath));
        }
    }
}
