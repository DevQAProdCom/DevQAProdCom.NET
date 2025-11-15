using DevQAProdCom.NET.UI.Selenium.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using OpenQA.Selenium;

namespace DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements.Search.FindOptionSearchers
{
    public abstract class BaseSeleniumFindOptionSearchMethod : ISeleniumFindOptionSearchMethod
    {
        public abstract string Method { get; }

        #region Find List of Elements

        public List<IWebElement> FindElementsFromBeginningOfDom(IWebDriver driver, IFindOption findOption)
        {
            By by = GetBy(findOption);
            return driver.FindElements(by).ToList();
        }

        public List<IWebElement> FindElementsRelativeToPreviousElementInFindChain(IWebElement previousElement, IFindOption findOption, IWebDriver driver)
        {
            By by = GetBy(findOption);
            return previousElement.FindElements(by).ToList();
        }

        public List<IWebElement> FindElementsRelativeToPreviousFrameElementInFindChain(IWebElement previousFrameElement, IFindOption findOption, IWebDriver driver)
        {
            By by = GetBy(findOption);
            return driver.FindElements(by).ToList();
        }

        public List<IWebElement> FindElementsRelativeToPreviousShadowRootHostElementInFindChain(IWebElement previousShadowRootHostElement, IFindOption findOption, IWebDriver driver)
        {
            By by = GetBy(findOption);
            ISearchContext shadowRoot = previousShadowRootHostElement.GetShadowRoot();
            return shadowRoot.FindElements(by).ToList();
        }

        #endregion Find List of Elements

        #region Find List of Frame Elements

        public List<IWebElement> FindFrameElementsFromBeginningOfDom(IWebDriver driver, IFindOption findOption)
        {
            return FindElementsFromBeginningOfDom(driver, findOption);
        }

        public List<IWebElement> FindFrameElementsRelativeToPreviousElementInFindChain(IWebElement previousElement, IFindOption findOption, IWebDriver driver)
        {
            return FindElementsRelativeToPreviousElementInFindChain(previousElement, findOption, driver);
        }

        public List<IWebElement> FindFrameElementsRelativeToPreviousFrameElementInFindChain(IWebElement previousFrameElement, IFindOption findOption, IWebDriver driver)
        {
            return FindElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findOption, driver);
        }

        public List<IWebElement> FindFrameElementsRelativeToPreviousShadowRootHostElementInFindChain(IWebElement previousShadowRootHostElement, IFindOption findOption, IWebDriver driver)
        {
            return FindElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findOption, driver);
        }

        #endregion Find List of Frame Elements

        #region Find List of ShadowRootHost Elements

        public List<IWebElement> FindShadowRootHostElementsFromBeginningOfDom(IWebDriver driver, IFindOption findOption)
        {
            return FindElementsFromBeginningOfDom(driver, findOption);
        }

        public List<IWebElement> FindShadowRootHostElementsRelativeToPreviousElementInFindChain(IWebElement previousElement, IFindOption findOption, IWebDriver driver)
        {
            return FindElementsRelativeToPreviousElementInFindChain(previousElement, findOption, driver);
        }

        public List<IWebElement> FindShadowRootHostElementsRelativeToPreviousFrameElementInFindChain(IWebElement previousFrameElement, IFindOption findOption, IWebDriver driver)
        {
            return FindElementsRelativeToPreviousFrameElementInFindChain(previousFrameElement, findOption, driver);
        }

        public List<IWebElement> FindShadowRootHostElementsRelativeToPreviousShadowRootHostElementInFindChain(IWebElement previousShadowRootHostElement, IFindOption findOption, IWebDriver driver)
        {
            return FindElementsRelativeToPreviousShadowRootHostElementInFindChain(previousShadowRootHostElement, findOption, driver);
        }

        #endregion Find List of ShadowRootHost Elements

        protected abstract By GetBy(IFindOption findOption);
    }
}
