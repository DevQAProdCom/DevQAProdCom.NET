using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Text;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors.Text
{
    public class SeleniumGetTextBehavior : SeleniumUiElementBehavior, IGetTextBehavior
    {
        public SeleniumGetTextBehavior(IBehaviorParameters parameters) : base(parameters) { }

        public string GetText()
        {
            return UiElement.GetAttribute("value", false).ToStringEmptyIfNull();
        }
    }
}
