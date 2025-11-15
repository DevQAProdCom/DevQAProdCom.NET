using DevQAProdCom.NET.UI.Playwright.Interfaces;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodsProvider : BaseFindOptionSearchMethodsProvider<IPlaywrightFindOptionSearchMethod>
    {
        public PlaywrightFindOptionSearchMethodsProvider()
        {
            RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingIdEquals>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingIdContains>()

                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingNameEquals>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingNameContains>()

                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingClassNameEquals>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingClassNameContains>()

                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingLinkTextEquals>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingLinkTextContains>()

                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingTagName>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingCssSelector>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingXPath>()

                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingTextEquals>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingTextContains>()

                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingLabelEquals>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingLabelContains>()

                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingPlaceholderEquals>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingPlaceholderContains>()

                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingAltTextEquals>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingAltTextContains>()

                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingTitleEquals>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingTitleContains>()

                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingDataTestIdEquals>()
                .RegisterFindOptionSearchMethod<PlaywrightFindOptionSearchMethodUsingDataTestIdContains>();
        }
    }
}
