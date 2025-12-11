using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Files;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Behaviors.Files
{
    public class SeleniumUiElementBehaviorUploadFiles : SeleniumUiElementBehavior, IUiElementBehaviorUploadFiles
    {
        public SeleniumUiElementBehaviorUploadFiles(IBehaviorParameters parameters) : base(parameters) { }

        public void UploadFiles(params string[] filePaths)
        {
            foreach (var filePath in filePaths)
                WebElement.SendKeys(filePath);
        }
    }
}
