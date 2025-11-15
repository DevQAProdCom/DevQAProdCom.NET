using ApplicationName.QA.TestsBasis.DependencyInjection;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class BaseAppUiPage : UiPage
    {
        public override string ApplicationName => DiContainer.Instance.AppSettings.ApplicationName;
        public override string BaseUri => DiContainer.Instance.AppSettings.BaseUrl;
    }
}
