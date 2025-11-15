using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using Microsoft.Playwright;

namespace DevQAProdCom.NET.UI.Playwright.Interfaces
{
    public interface IPlaywrightFindOptionSearchMethod : IFindOptionSearchMethod
    {
        #region Find List of Elements

        public ILocator FindElementsFromBeginningOfDom(IPage page, IFindOption findOption);
        public ILocator FindElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindOption findOption, IPage page);
        public ILocator FindElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindOption findOption, IPage page);
        public ILocator FindElementsRelativeToPreviousShadowRootHostElementInFindChain(ILocator previousShadowRootHostElement, IFindOption findOption, IPage page);

        #endregion Find List of Elements

        #region Find List of Frame Elements

        public IFrameLocator FindFrameElementsFromBeginningOfDom(IPage page, IFindOption findOption);
        public IFrameLocator FindFrameElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindOption findOption, IPage page);
        public IFrameLocator FindFrameElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindOption findOption, IPage page);
        public IFrameLocator FindFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(ILocator previousShadowRootHostElement, IFindOption findOption, IPage page);

        #endregion Find List of Frame Elements

        #region Find List of ShadowRootHost Elements

        public ILocator FindShadowRootHostElementsFromBeginningOfDom(IPage page, IFindOption findOption);
        public ILocator FindShadowRootHostElementsRelativeToPreviousElementInFindChain(ILocator previousElement, IFindOption findOption, IPage page);
        public ILocator FindShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(IFrameLocator previousFrameElement, IFindOption findOption, IPage page);
        public ILocator FindShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(ILocator previousShadowRootHostElement, IFindOption findOption, IPage page);

        #endregion Find List of ShadowRootHost Elements
    }
}
