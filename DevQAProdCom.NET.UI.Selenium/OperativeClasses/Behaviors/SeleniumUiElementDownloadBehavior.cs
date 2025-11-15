using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors
{
    public class SeleniumUiElementDownloadBehavior : SeleniumUiElementBehavior, IUiElementDownloadBehavior
    {
        public SeleniumUiElementDownloadBehavior(IBehaviorParameters parameters) : base(parameters) { }

        public void DownloadFile()
        {
            UiElement.MouseClick();
        }
    }
}

