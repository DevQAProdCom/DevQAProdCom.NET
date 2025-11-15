using DevQAProdCom.NET.UI.Selenium.OperativeClasses.Behaviors;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Files;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.Behaviors.Files
{
    public class SeleniumUploadFilesBehavior : SeleniumUiElementBehavior, IUploadFilesBehavior
    {
        public SeleniumUploadFilesBehavior(IBehaviorParameters parameters) : base(parameters) { }

        public void UploadFiles(params string[] filePaths)
        {
            foreach (var filePath in filePaths)
                WebElement.SendKeys(filePath);
        }
    }
}
