using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public class MultipleUiPagesService : UiPageService, IMultipleUiPagesService
    {
        public MultipleUiPagesService(IUiInteractor interactor, string tabName = SharedUiConstants.DefaultTab) : base(interactor, tabName) { }
    }
}
