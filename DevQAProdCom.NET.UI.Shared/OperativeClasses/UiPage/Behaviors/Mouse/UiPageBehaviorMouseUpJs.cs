using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class UiPageBehaviorMouseUpJs(IBehaviorParameters parameters) : UiPageBehavior(parameters), IUiPageBehaviorMouseUpJs
    {
        public void MouseUpJs()
        {
            JavaScriptExecutor.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.UiPageMouseUpJavaScriptFilePath));
        }
    }
}
