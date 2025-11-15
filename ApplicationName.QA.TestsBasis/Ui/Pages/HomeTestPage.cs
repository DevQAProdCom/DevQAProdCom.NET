using ApplicationName.QA.TestsBasis.Ui.Components;
using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class HomeTestPage : BaseAppUiPage
    {
        public override string RelativeUri => @"/HomeTestPage";

        //MULTIPLE FIND ATTRIBUTES

        [Find(Use.IdEquals, "multiple-find-attributes-1")]
        [Find(Use.ClassNameEquals, "multiple-find-attributes-2")]
        public IUiElementsList<IUiElement> ButtonMultipleFindAttributes;

        [Find(Use.XPath, "//table[@id='NOT EXISTING XPATH']")]
        [Find(Use.XPath, "//table[@id='Table2']")]
        public Table Table2;

        //DYNAMIC UiElements INSTANTIATION

        private IUiElement? _dynamiclyInstantiatedUiElement;
        public IUiElement Dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator
        {
            get
            {
                if (_dynamiclyInstantiatedUiElement == null)
                    _dynamiclyInstantiatedUiElement = InstantiateUiElement<IUiElement>(
                                "Dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator",
                                Use.XPath,
                                "//table[@id='Table2']//tr[2]//th[2]");

                return _dynamiclyInstantiatedUiElement;
            }
        }

        //FRAMES UiElements support with AUTO SWITCHING

        [Find(Use.IdEquals, "frame-top-level0-button", framesFindMethod: Use.IdEquals, framesFindCriteria: "frame-top-level0-a-id")]
        public IUiElement ButtonInsideTopLevel0Frame;

        [Frame(Use.IdEquals, "frame-top-level0-a-id")]
        public TopLevelFrame TopLevelFrame;

        [Find(Use.IdEquals, "button-not-in-frame")]
        public IUiElement ButtonNotInFrame;

        //CUSTOM FIND METHODS

        [Find("CustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceTUsingCustomAttribute", "attribute-for-custom-find-option-search-method-registered-from-activator-create-instance-t-value")]
        public IUiElement CustomFindMethodElement;
    }
}
