using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Others;
using DevQAProdCom.NET.UI.Shared.Constants;

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
