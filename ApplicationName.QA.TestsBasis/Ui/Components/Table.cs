using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components
{
    public class Table : UiElement
    {
        [Find(Use.XPath, "//table[@id='NOT EXISTING XPATH']")]
        [Find(Use.XPath, ".//tr")]
        public IUiElementsList<Row> Rows;

        [Find(Use.XPath, "//table[@id='NOT EXISTING XPATH']")]
        [Find(Use.XPath, ".//tr[2]")]
        public Row Row;

        private IUiElement? _dynamic_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator;
        public IUiElement Dynamic_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator
        {
            get
            {
                if (_dynamic_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator == null)
                    _dynamic_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator = Find<IUiElement>(
                                Use.XPath,
                                ".//tr[2]//th[2]",
                                name: "Dynamic_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator");

                return _dynamic_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator;
            }
        }

        private Cell? _dynamic_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator;
        public Cell Dynamic_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator
        {
            get
            {
                if (_dynamic_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator == null)
                    _dynamic_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator = Find<Cell>(
                                Use.XPath,
                                ".//tr[2]//th[2]",
                                name: "Dynamic_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator");

                return _dynamic_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator;
            }
        }

        private IUiElementsList<IUiElement>? _dynamic_UiElementsList_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator;
        public IUiElementsList<IUiElement> Dynamic_UiElementsList_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator
        {
            get
            {
                if (_dynamic_UiElementsList_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator == null)
                    _dynamic_UiElementsList_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator = FindAll<IUiElement>(
                            Use.XPath,
                            ".//tr[2]//th",
                            "Dynamic_UiElementsList_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator");

                return _dynamic_UiElementsList_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator;
            }
        }

        private IUiElementsList<Cell>? _dynamic_UiElementsList_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator;
        public IUiElementsList<Cell> Dynamic_UiElementsList_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator
        {
            get
            {
                if (_dynamic_UiElementsList_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator == null)
                    _dynamic_UiElementsList_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator = FindAll<Cell>(
                            Use.XPath,
                            ".//tr[2]//th",
                             "Dynamic_UiElementsList_IUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator");

                return _dynamic_UiElementsList_TUiElement_Without_Find_Attribute_With_Parent_Using_UiElementInstantiator;
            }
        }
    }
}
