using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public abstract class BasePlaywrightFindOptionSearchMethodWithCustomSelector : BasePlaywrightFindOptionSearchMethod
    {
        protected abstract string GetSelector(IFindOption findOption);

        #region Find List of Elements

        public override ILocator FindElementsFromBeginningOfDom(IPage page, IFindOption findOption)
        {
            var selector = GetSelector(findOption);
            return page.Locator(selector);
        }

        public override ILocator FindElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindOption findOption, IPage page)
        {
            var selector = GetSelector(findOption);
            return previousElement.Locator(selector);
        }

        public override ILocator FindElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindOption findOption, IPage page)
        {
            var selector = GetSelector(findOption);
            return previousFrameElement.Locator(selector);
        }

        #endregion Find List of Elements

        #region Find List of Frame Elements

        public override IFrameLocator FindFrameElementsFromBeginningOfDom(IPage page, IFindOption findOption)
        {
            var selector = GetSelector(findOption);
            return page.FrameLocator(selector);
        }

        public override IFrameLocator FindFrameElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindOption findOption, IPage page)
        {
            var selector = GetSelector(findOption);
            return previousElement.FrameLocator(selector);
        }

        public override IFrameLocator FindFrameElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindOption findOption, IPage page)
        {
            var selector = GetSelector(findOption);
            return previousFrameElement.FrameLocator(selector);
        }

        #endregion Find List of Frame Elements
    }
}
