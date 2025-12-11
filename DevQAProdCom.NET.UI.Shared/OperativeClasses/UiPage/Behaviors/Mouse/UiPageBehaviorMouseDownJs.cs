using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class UiPageBehaviorMouseDownJs(IBehaviorParameters parameters) : UiPageBehavior(parameters), IUiPageBehaviorMouseDownJs
    {
        public void MouseDownJs()
        {
            JavaScriptExecutor.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.UiPageMouseDownJavaScriptFilePath));
        }
    }
}
