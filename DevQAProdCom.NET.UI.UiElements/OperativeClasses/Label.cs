using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Text;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.UiElements.Interfaces;

namespace DevQAProdCom.NET.UI.UiElements.OperativeClasses
{
    internal class Label : UiElement, ILabel
    {
        public string GetText()
        {
            return AddBehavior<IGetTextBehavior>().GetText();
        }
    }
}
