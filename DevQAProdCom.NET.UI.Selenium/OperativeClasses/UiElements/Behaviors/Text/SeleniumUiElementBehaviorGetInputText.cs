using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Text
{
    public class SeleniumUiElementBehaviorGetInputText : SeleniumUiElementBehavior, IUiElementBehaviorGetInputText
    {
        public SeleniumUiElementBehaviorGetInputText(IBehaviorParameters parameters) : base(parameters) { }

        public string GetInputText()
        {
            return UiElement.GetNonBooleanAttribute("value").ToStringEmptyIfNull();
        }
    }
}
