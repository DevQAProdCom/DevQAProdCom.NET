using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;
using OpenQA.Selenium.Interactions;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Keyboard
{
    public class SeleniumUiElementBehaviorSendText(IBehaviorParameters parameters) : SeleniumUiElementBehavior(parameters), IUiElementBehaviorSendText
    {
        public void SendText(string textKeys)
        {
            UiElement.FocusJs();
            new Actions(WebDriver).SendKeys(textKeys).Perform();
        }
    }
}
