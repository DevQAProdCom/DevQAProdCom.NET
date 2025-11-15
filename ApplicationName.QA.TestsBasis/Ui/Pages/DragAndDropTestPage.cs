using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class DragAndDropTestPage : BaseAppUiPage
    {
        public override string RelativeUri => @"/DragAndDropTestPage";

        [Find(Use.XPath, "//div[@id='toDoList']//div[contains(@class,'cdk-drag')]")]
        public IUiElementsList<IUiElement> ToDoList;

        [Find(Use.XPath, "//div[@id='doneList']//div[contains(@class,'cdk-drag')]")]
        public IUiElementsList<IUiElement>? DoneList;
    }
}
