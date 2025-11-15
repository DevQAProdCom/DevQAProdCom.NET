using DevQAProdCom.NET.UI.Selenium.Interfaces;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class SeleniumFindOptionSearchMethodsProvider : BaseFindOptionSearchMethodsProvider<ISeleniumFindOptionSearchMethod>
    {
        public SeleniumFindOptionSearchMethodsProvider()
        {
            RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingIdEquals>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingIdContains>()

                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingNameEquals>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingNameContains>()

                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingClassNameEquals>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingClassNameContains>()

                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingLinkTextEquals>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingLinkTextContains>()

                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingTagName>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingCssSelector>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingXPath>()

                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingTextEquals>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingTextContains>()

                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingLabelEquals>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingLabelContains>()

                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingPlaceholderEquals>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingPlaceholderContains>()

                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingAltTextEquals>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingAltTextContains>()

                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingTitleEquals>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingTitleContains>()

                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingDataTestIdEquals>()
                .RegisterFindOptionSearchMethod<SeleniumFindOptionSearchMethodUsingDataTestIdContains>();
        }
    }
}
