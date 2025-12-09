using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Others;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Others
{
    public class UiElementBehaviorSetAttributeJs(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorSetAttributeJs
    {
        public void SetAttributeJs(string attributeName, string attributeValue)
        {
            var attributeNameArgument = new KeyValuePair<string, object>(SharedUiConstants.JavaScriptArguments.AttributeNameArgument, attributeName);
            var attributeValueArgument = new KeyValuePair<string, object>(SharedUiConstants.JavaScriptArguments.AttributeValueArgument, attributeValue);
            UiElement.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.SetAttributeJavaScriptFilePath), attributeNameArgument, attributeValueArgument);
        }
    }
}
