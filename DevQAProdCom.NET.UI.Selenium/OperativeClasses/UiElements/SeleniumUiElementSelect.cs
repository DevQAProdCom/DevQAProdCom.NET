using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.UiElements.Interfaces;
using OpenQA.Selenium.Support.UI;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements
{
    public class SeleniumUiElementSelect : UiElement, ISelect
    {
        private SelectElement _seleniumSelectElement
        {
            get
            {
                var seleniumUiElement = (SeleniumUiElement)InternalUiElement;
                var iWebElement = seleniumUiElement.GetWebElement();
                return new SelectElement(iWebElement);
            }
        }

        public void Select(string value)
        {
            _seleniumSelectElement.SelectByText(value);

        }

        public string GetSelectedValue()
        {
            return _seleniumSelectElement.SelectedOption.Text;
        }
    }
}
