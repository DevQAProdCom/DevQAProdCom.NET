using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors.Mouse
{
    public class UiPageBehaviorMouseMoveJs(IBehaviorParameters parameters) : UiPageBehavior(parameters), IUiPageBehaviorMouseMoveJs
    {
        public void MouseMoveJs(float x, float y)
        {
            JavaScriptExecutor.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.MouseMoveJavaScriptFilePath),
                new KeyValuePair<string, object>(SharedUiConstants.JavaScriptArguments.XArgument, x), new KeyValuePair<string, object>(SharedUiConstants.JavaScriptArguments.YArgument, y));
        }
    }
}
