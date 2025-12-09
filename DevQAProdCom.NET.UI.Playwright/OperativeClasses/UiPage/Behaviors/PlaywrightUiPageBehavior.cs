using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Playwright.Constants;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiPage.Behaviors
{
    public class PlaywrightUiPageBehavior(IBehaviorParameters parameters) : UiPageBehavior(parameters)
    {
        private IPage? _page;
        protected IPage Page => _page ??= Parameters.Get<IPage>(ProjectConst.IPage);
    }
}
