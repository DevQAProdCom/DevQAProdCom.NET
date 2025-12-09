using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Others;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Others
{
    public class UiElementBehaviorRemoveClassJs(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorRemoveClassJs
    {
        public void RemoveClassJs(string className)
        {
            var classNameArgument = new KeyValuePair<string, object>(SharedUiConstants.JavaScriptArguments.ClassNameArgument, className);
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.RemoveClassJavaScriptFilePath), classNameArgument);
        }
    }
}
