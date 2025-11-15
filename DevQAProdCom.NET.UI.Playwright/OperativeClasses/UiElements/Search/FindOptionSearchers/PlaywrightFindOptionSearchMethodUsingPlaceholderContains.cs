using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public class PlaywrightFindOptionSearchMethodUsingPlaceholderContains : BasePlaywrightFindOptionSearchMethod
    {
        public override string Method => Use.PlaceholderContains.ToString();

        #region Find List of Elements

        public override ILocator FindElementsFromBeginningOfDom(IPage page, IFindOption findOption)
        {
            return page.GetByPlaceholder(findOption.Criteria);
        }

        public override ILocator FindElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindOption findOption, IPage page)
        {
            return previousElement.GetByPlaceholder(findOption.Criteria);
        }

        public override ILocator FindElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindOption findOption, IPage page)
        {
            return previousFrameElement.GetByPlaceholder(findOption.Criteria);
        }

        #endregion Find List of Elements

        #region Find List of Frame Elements

        public override IFrameLocator FindFrameElementsFromBeginningOfDom(IPage page, IFindOption findOption)
        {
            return page.GetByPlaceholder(findOption.Criteria).ContentFrame;
        }

        public override IFrameLocator FindFrameElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindOption findOption, IPage page)
        {
            return previousElement.GetByPlaceholder(findOption.Criteria).ContentFrame;
        }

        public override IFrameLocator FindFrameElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindOption findOption, IPage page)
        {
            return previousFrameElement.GetByPlaceholder(findOption.Criteria).ContentFrame;
        }

        #endregion Find List of Frame Elements
    }
}
