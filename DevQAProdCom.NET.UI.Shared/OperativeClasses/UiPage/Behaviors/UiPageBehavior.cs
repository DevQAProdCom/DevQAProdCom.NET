using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.OperativeClasses;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors
{
    public class UiPageBehavior(IBehaviorParameters parameters) : BaseBehavior(parameters)
    {
        private IUiPage? _uiPage;
        protected IUiPage UiPage => _uiPage ??= Parameters.Get<IUiPage>(SharedUiConstants.IUiPage);

        private IExecuteJavaScript? _javaScriptExecutor;
        protected IExecuteJavaScript JavaScriptExecutor => _javaScriptExecutor ??= Parameters.Get<IExecuteJavaScript>(SharedUiConstants.IExecuteJavaScript);
    }
}
