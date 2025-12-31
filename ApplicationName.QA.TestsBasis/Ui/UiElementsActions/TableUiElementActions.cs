using ApplicationName.QA.TestsBasis.Ui.UiElements;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;

namespace ApplicationName.QA.TestsBasis.Ui.UiElementsActions
{
    public class TableUiElementActions : UiElementActions<Table>
    {
        public override Table UiElement => UiInteractor.Find<Table>(Use.XPath, "//table[@id='Table2']");
        public TableUiElementActions(IUiInteractorsManagersProvider uiInteractorsManagersProvider) : base(uiInteractorsManagersProvider) { }
        public TableUiElementActions(IUiInteractor uiInteractor, string tabName = SharedUiConstants.DefaultUiInteractorTab) : base(uiInteractor, tabName) { }
    }
}
