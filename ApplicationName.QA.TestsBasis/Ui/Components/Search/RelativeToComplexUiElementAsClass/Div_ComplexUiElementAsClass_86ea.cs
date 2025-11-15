using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace ApplicationName.QA.TestsBasis.Ui.Components.Search.RelativeToComplexUiElementAsClass
{
    public class Div_ComplexUiElementAsClass_86ea : UiElement
    {
        [Find(Use.IdEquals, "page->complexUiElementAsClass(86ea)->simpleUiElementAsInterface(86ea)")]
        public IUiElement SimpleUiElementAsInterface_86ea;
    }
}
