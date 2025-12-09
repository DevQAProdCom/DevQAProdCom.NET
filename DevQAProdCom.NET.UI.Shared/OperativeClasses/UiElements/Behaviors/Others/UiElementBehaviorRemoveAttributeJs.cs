using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Others;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Others
{
    public class UiElementBehaviorRemoveAttributeJs(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorRemoveAttributeJs
    {
        public void RemoveAttributeJs(string attributeName)
        {
            var attributeNameArgument = new KeyValuePair<string, object>(SharedUiConstants.JavaScriptArguments.AttributeNameArgument, attributeName);
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.RemoveAttributeJavaScriptFilePath), attributeNameArgument);
        }
    }
}
