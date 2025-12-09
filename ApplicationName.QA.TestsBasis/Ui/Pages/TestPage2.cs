using ApplicationName.QA.TestsBasis.Ui.Components;
using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.UiElements.Interfaces;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class TestPage2 : BaseAppUiPage
    {
        public override string RelativeUri => @"/TestPage2";

        [Find(Use.XPath, "//table[@id='Table2']")]
        public Table Table2;

        [Find(Use.XPath, "//table[@id='Table2']//tr")]
        public IUiElementsList<Row> Rows;

        [Find(Use.XPath, "//button[@id='RefreshButton']")]
        public IUiElement RefreshButton;

        [Find(Use.XPath, "//button[@id='AddTableButton']")]
        public IUiElement AddTableButton;

        [Find(Use.XPath, "//button[@id='DeleteTableButton']")]
        public IUiElement DeleteTableButton;

        [Find(Use.XPath, "//button[@id='AddCellButton']")]
        public IUiElement AddCellButton;

        [Find(Use.XPath, "//button[@id='DeleteCellButton']")]
        public IUiElement DeleteCellButton;

        [Find(Use.XPath, "//th")]
        public IUiElementsList<IUiElement> Cells;

        [Find(Use.XPath, "//app-test-table//table")]
        public Table DeletableTable;

        //

        [Find(Use.XPath, ".//table[@id='Table3']//tr")]
        public IUiElementsList<Row3>? Rows3;

        [Find(Use.IdEquals, "Invisible Button Hidden HTML Attribute")]
        public IUiElement InvisibleButtonHiddenHtmlAttribute;

        [Find(Use.IdEquals, "Invisible Button CSS Display None")]
        public IUiElement InvisibleButtonCssDisplayNone;

        [Find(Use.IdEquals, "Invisible Button CSS Visibility Hidden")]
        public IUiElement InvisibleButtonCssVisibilityHidden;

        [Find(Use.IdEquals, "Visible Button")]
        public IUiElement VisibleButton;

        [Find(Use.IdEquals, "Disabled Button")]
        public IUiElement DisabledButton;

        [Find(Use.IdEquals, "Mouse Click/DoubleClick Button")]
        public IUiElement MouseClickDoubleClickButton;

        [Find(Use.IdEquals, "Mouse Context/Right Click Button")]
        public IUiElement MouseContextRightClickButton;

        [Find(Use.IdEquals, "Mouse Hover Button")]
        public IUiElement MouseHoverButton;

        [Find(Use.IdEquals, "Mouse Down Button")]
        public IUiElement MouseDownButton;

        [Find(Use.IdEquals, "Mouse Up Button")]
        public IUiElement MouseUpButton;

        [Find(Use.IdEquals, "Mouse Move By Offset Outer Div")]
        public IUiElement MouseMoveByOffsetOuterDiv;

        [Find(Use.IdEquals, "Mouse Move By Offset Inner Div")]
        public IUiElement MouseMoveByOffsetInnerDiv;

        [Find(Use.IdEquals, "Button for Scroll")]
        public IUiElement ButtonForScroll;

        [Find(Use.IdEquals, "Text Area")]
        public IInputText TextArea;

        [Find(Use.IdEquals, "InputTextBox")]
        public IInputText InputTextBox;

        [Find(Use.IdEquals, "Form")]
        public Form Form;

        [Find(Use.IdEquals, "Element for Page Mouse Move")]
        public IUiElement ElementForPageMouseMove;

        [Find(Use.IdEquals, "Not Existing in the DOM List")]
        public IUiElementsList<IUiElement> NotExistingInTheDomList;

        [Find(Use.IdEquals, "Download File Button")]
        public IUiElement DownloadFileButton;


        [Find("IdEquals", "use-id-equals")]
        public IUiElement UseIdEquals;

        [Find("IdContains", "use-id-contains")]
        public IUiElement UseIdContains;


        [Find("NameEquals", "use-name-equals")]
        public IUiElement UseNameEquals;

        [Find("NameContains", "use-name-contains")]
        public IUiElement UseNameContains;


        [Find("ClassNameEquals", "use-class-name-equals")]
        public IUiElement UseClassNameEquals;

        [Find("ClassNameContains", "use-class-name-contains")]
        public IUiElement UseClassNameContains;


        [Find("CssSelector", ".use-css-selector")]
        public IUiElement UseCssSelector;


        [Find("XPath", "//button[@xpath='use-xpath']")]
        public IUiElement UseXPath;

        [Find("TagName", "h6")]
        public IUiElement UseTagName;

        [Find("LinkTextEquals", "Use.LinkTextEquals")]
        public IUiElement UseLinkTextEquals;

        [Find("LinkTextContains", "LinkTextContains")]
        public IUiElement UseLinkTextContains;

        [Find("TextEquals", "Use.TextEquals")]
        public IUiElement UseTextEquals;

        [Find("TextContains", "Use.TextContains")]
        public IUiElement UseTextContains;

        [Find("LabelEquals", "Use.LabelEquals")]
        public IUiElement UseLabelEquals;

        [Find("LabelContains", "Use.LabelContains")]
        public IUiElement UseLabelContains;

        [Find("PlaceholderEquals", "use-placeholder-equals")]
        public IUiElement UsePlaceholderEquals;

        [Find("PlaceholderContains", "use-placeholder-contains")]
        public IUiElement UsePlaceholderContains;

        [Find("AltTextEquals", "use-alt-text-equals")]
        public IUiElement UseAltTextEquals;

        [Find("AltTextContains", "use-alt-text-contains")]
        public IUiElement UseAltTextContains;

        [Find("TitleEquals", "use-title-equals")]
        public IUiElement UseTitleEquals;

        [Find("TitleContains", "use-title-contains")]
        public IUiElement UseTitleContains;

        [Find("DataTestIdEquals", "use-data-testid-equals")]
        public IUiElement UseDataTestIdEquals;

        [Find("DataTestIdContains", "use-data-testid-contains")]
        public IUiElement UseDataTestIdContains;

        [Find("CustomFindOptionSearchMethodRegisteredFromDiUsingCustomAttribute", "attribute-for-custom-find-option-search-method-registered-from-di-value")]
        public IUiElement UseCustomFindOptionSearchMethodRegisteredFromDi;

        [Find("CustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceTUsingCustomAttribute", "attribute-for-custom-find-option-search-method-registered-from-activator-create-instance-t-value")]
        public IUiElement UseCustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceT;

        [Find(Use.NameEquals, "test-list-item-1")]
        public IUiElementsList<TestListItemA> TestListItems1;


        private IUiElement? _dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator;
        public IUiElement Dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator
        {
            get
            {
                if (_dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator == null)
                    _dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator = Find<IUiElement>(
                                Use.XPath,
                                "//table[@id='Table2']//tr[2]//th[2]",
                                name: "Dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator");

                return _dynamic_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator;
            }
        }

        private Cell? _dynamic_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator;
        public Cell Dynamic_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator
        {
            get
            {
                if (_dynamic_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator == null)
                    _dynamic_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator = Find<Cell>(
                                Use.XPath,
                                "//table[@id='Table2']//tr[2]//th[2]",
                                name: "Dynamic_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator");

                return _dynamic_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator;
            }
        }

        private IUiElementsList<IUiElement>? _dynamic_UiElementsList_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator;
        public IUiElementsList<IUiElement> Dynamic_UiElementsList_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator
        {
            get
            {
                if (_dynamic_UiElementsList_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator == null)
                    _dynamic_UiElementsList_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator = FindAll<IUiElement>(
                                Use.XPath,
                                "//table[@id='Table2']//tr[2]//th",
                                name: "Dynamic_UiElementsList_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator");

                return _dynamic_UiElementsList_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator;
            }
        }

        private IUiElementsList<Cell>? _dynamic_UiElementsList_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator;
        public IUiElementsList<Cell> Dynamic_UiElementsList_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator
        {
            get
            {
                if (_dynamic_UiElementsList_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator == null)
                    _dynamic_UiElementsList_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator = FindAll<Cell>(
                            Use.XPath,
                            "//table[@id='Table2']//tr[2]//th",
                            name: "Dynamic_UiElementsList_IUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator");

                return _dynamic_UiElementsList_TUiElement_Without_Find_Attribute_Without_Parent_Using_UiElementInstantiator;
            }
        }
    }
}
