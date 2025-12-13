using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Scroll;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors.Scroll
{
    public class UiPageBehaviorScrollToTopSmoothlyJs(IBehaviorParameters parameters) : UiPageBehavior(parameters), IUiPageBehaviorScrollToTopSmoothlyJs
    {
        public void ScrollToTopSmoothlyJs()
        {
            JavaScriptExecutor.ExecuteJavaScript(new FileInfo(SharedUiConstants.Files.UiPageScrollToTopSmoothlyJavaScriptFilePath));
        }
    }
}
