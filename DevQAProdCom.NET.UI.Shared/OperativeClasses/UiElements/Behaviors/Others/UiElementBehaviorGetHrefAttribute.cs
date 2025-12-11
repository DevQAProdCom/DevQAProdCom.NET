using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Others;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Others
{
    public class UiElementBehaviorGetHrefAttribute(IBehaviorParameters parameters) : UiElementBehavior<IUiElement>(parameters), IUiElementBehaviorGetHrefAttribute
    {
        public string? GetHrefAttribute()
        {
            return UiElement.GetNonBooleanAttribute(SharedUiConstants.HtmlElementAttributes.Href);
        }
    }
}
