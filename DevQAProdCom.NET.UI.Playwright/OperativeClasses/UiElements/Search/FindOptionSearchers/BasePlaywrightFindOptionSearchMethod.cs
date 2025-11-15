using DevQAProdCom.NET.UI.Playwright.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public abstract class BasePlaywrightFindOptionSearchMethod : IPlaywrightFindOptionSearchMethod
    {
        public abstract string Method { get; }

        #region Find List of Elements

        public abstract ILocator FindElementsFromBeginningOfDom(IPage page, IFindOption findOption);
        public abstract ILocator FindElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindOption findOption, IPage page);
        public abstract ILocator FindElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindOption findOption, IPage page);
        public ILocator FindElementsRelativeToPreviousShadowRootHostElementInFindChain(ILocator previousShadowRootHostElement, IFindOption findOption, IPage page)
        {
            return FindElementsRelativeToPreviousElementInFindChain(previousShadowRootHostElement, findOption, page);
        }

        #endregion Find List of Elements

        #region Find List of Frame Elements

        public abstract IFrameLocator FindFrameElementsFromBeginningOfDom(IPage page, IFindOption findOption);
        public abstract IFrameLocator FindFrameElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindOption findOption, IPage page);
        public abstract IFrameLocator FindFrameElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindOption findOption, IPage page);
        public IFrameLocator FindFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(ILocator previousShadowRootHostElement, IFindOption findOption, IPage page)
        {
            return FindFrameElementsRelativeToPreviousElementInFindChain(previousShadowRootHostElement, findOption, page);
        }

        #endregion Find List of Frame Elements

        #region Find List of ShadowRootHost Elements

        public ILocator FindShadowRootHostElementsFromBeginningOfDom(IPage page, IFindOption findOption)
        {
            return FindElementsFromBeginningOfDom(page, findOption);
        }

        public ILocator FindShadowRootHostElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindOption findOption, IPage page)
        {
            return FindElementsRelativeToPreviousElementInFindChain(previousElement, findOption, page);
        }

        public ILocator FindShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindOption findOption, IPage page)
        {
            return FindElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findOption, page);
        }

        public ILocator FindShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(ILocator previousShadowRootHostElement, IFindOption findOption, IPage page)
        {
            return FindElementsRelativeToPreviousElementInFindChain(previousShadowRootHostElement, findOption, page);
        }

        #endregion Find List of ShadowRootHost Elements
    }
}
