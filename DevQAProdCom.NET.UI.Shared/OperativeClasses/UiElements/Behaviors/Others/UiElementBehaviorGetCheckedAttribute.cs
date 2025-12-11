using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Others;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Others
{
    public class UiElementBehaviorGetCheckedAttribute(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorGetCheckedAttribute
    {
        public bool GetCheckedAttribute()
        {
            return UiElement.GetBooleanAttribute(SharedUiConstants.HtmlElementAttributes.Checked);
        }

        public bool? GetCheckedAttributeOrNull()
        {
            return UiElement.GetNonBooleanAttribute(SharedUiConstants.HtmlElementAttributes.Checked).ToBooleanOrNull();
        }
    }
}
