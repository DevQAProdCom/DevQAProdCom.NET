using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.Interfaces
{
    public interface ISeleniumFindOptionSearchMethod : IFindOptionSearchMethod
    {
        #region Find List of Elements

        public List<IWebElement> FindElementsFromBeginningOfDom(IWebDriver driver, IFindOption findOption);
        public List<IWebElement> FindElementsRelativeToPreviousElementInFindChain(IWebElement previousElement, IFindOption findOption, IWebDriver driver);
        public List<IWebElement> FindElementsRelativeToPreviousFrameElementInFindChain(IWebElement previousFrameElement, IFindOption findOption, IWebDriver driver);
        public List<IWebElement> FindElementsRelativeToPreviousShadowRootHostElementInFindChain(IWebElement previousShadowRootHostElement, IFindOption findOption, IWebDriver driver);

        #endregion Find List of Elements

        #region Find List of Frame Elements

        public List<IWebElement> FindFrameElementsFromBeginningOfDom(IWebDriver driver, IFindOption findOption);
        public List<IWebElement> FindFrameElementsRelativeToPreviousElementInFindChain(IWebElement previousElement, IFindOption findOption, IWebDriver driver);
        public List<IWebElement> FindFrameElementsRelativeToPreviousFrameElementInFindChain(IWebElement previousFrameElement, IFindOption findOption, IWebDriver driver);
        public List<IWebElement> FindFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(IWebElement previousShadowRootHostElement, IFindOption findOption, IWebDriver driver);

        #endregion Find List of Frame Elements

        #region Find List of ShadowRootHost Elements

        public List<IWebElement> FindShadowRootHostElementsFromBeginningOfDom(IWebDriver driver, IFindOption findOption);
        public List<IWebElement> FindShadowRootHostElementsRelativeToPreviousElementInFindChain(IWebElement previousElement, IFindOption findOption, IWebDriver driver);
        public List<IWebElement> FindShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(IWebElement previousFrameElement, IFindOption findOption, IWebDriver driver);
        public List<IWebElement> FindShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(IWebElement previousShadowRootHostElement, IFindOption findOption, IWebDriver driver);

        #endregion Find List of ShadowRootHost Elements
    }
}
