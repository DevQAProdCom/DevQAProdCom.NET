using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Files;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Files
{
    public class SeleniumUiElementBehaviorDownloadFile : SeleniumUiElementBehavior, IUiElementBehaviorDownloadFile
    {
        public SeleniumUiElementBehaviorDownloadFile(IBehaviorParameters parameters) : base(parameters) { }

        public void DownloadFile()
        {
            UiElement.Click();
        }
    }
}
