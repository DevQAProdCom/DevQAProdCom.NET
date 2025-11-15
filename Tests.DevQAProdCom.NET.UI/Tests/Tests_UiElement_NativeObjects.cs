using ApplicationName.QA.TestsBasis.Ui.PageServices;
using DevQAProdCom.NET.UI.Playwright.Browsers.Interfaces;
using DevQAProdCom.NET.UI.Playwright.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Selenium.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Playwright;
using OpenQA.Selenium;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Configurations;
using Tests.DevQAProdCom.NET.UI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    [Category("TestClassCategory")]
    internal class Tests_UiElement_NativeObjects : PerScenarioBaseTest
    {
        [ThreadStatic] private static TestPage2Service _testPage2Service;

        private const string ExpectedUrl = "TestPage2";
        private const string ExpectedThTagName = "th";
        private const string ExpectedTrTagName = "tr";

        [SetUp]
        public void SetUp()
        {
            _testPage2Service = UiInteractor.Interact<TestPage2Service>();
        }

        [Test, Category("Selenium")]
        public void Should_Selenium_UiElement_Contain_Native_Objects()
        {

            if (DiContainer.CurrentTechnology == UiInteractorTechnology.Selenium)
            {
                //GIVEN
                var seleniumUiElement = (SeleniumUiElement)_testPage2Service._page.Table2.Row.InternalUiElement;

                //THEN
                AssertSeleniumUiElementNativeObjects(seleniumUiElement, ExpectedUrl, ExpectedTrTagName);
            }
        }

        [Test, Category("Selenium")]
        public void Should_Selenium_UiElement_From_List_Contain_Native_Objects()
        {
            if (DiContainer.CurrentTechnology == UiInteractorTechnology.Selenium)
            {
                //GIVEN
                var cells = _testPage2Service._page.Table2.Rows.ElementAt(1).Cells;

                //WHEN
                using (new AssertionScope())
                    foreach (var cell in cells)
                        AssertSeleniumUiElementNativeObjects(cell, ExpectedUrl, ExpectedThTagName);
            }
        }

        [Test, Category("Playwright")]
        public void Should_Playwright_UiElement_Contain_Native_Objects()
        {
            if (DiContainer.CurrentTechnology == UiInteractorTechnology.Playwright)
            {
                //GIVEN
                var playwrightUiElement = (PlaywrightUiElement)_testPage2Service._page.Table2.Row.InternalUiElement;

                //WHEN
                var actualNativeObjects = GetPlaywrightUiElementNativeObjects(playwrightUiElement);

                //THEN
                AssertPlaywrightUiElementNativeObjects(playwrightUiElement, ExpectedUrl, ExpectedTrTagName);
            }
        }

        [Test, Category("Playwright")]
        public void Should_Playwright_UiElement_From_List_Contain_Native_Objects()
        {
            if (DiContainer.CurrentTechnology == UiInteractorTechnology.Playwright)
            {
                //GIVEN
                var cells = _testPage2Service._page.Table2.Rows.ElementAt(1).Cells;

                //THEN
                using (new AssertionScope())
                    foreach (var cell in cells)
                        AssertPlaywrightUiElementNativeObjects(cell, ExpectedUrl, ExpectedThTagName);
            }
        }

        private (IBrowser Browser, IBrowserContext BrowserContext, IPage Page, ILocator Locator) GetPlaywrightUiElementNativeObjects(IUiElement uiElement)
        {
            PlaywrightUiElement playwrightUiElement = (PlaywrightUiElement)uiElement;
            IBrowser iBrowser = playwrightUiElement.GetIBrowser();
            IBrowserContext iBrowserContext = playwrightUiElement.GetBrowserContext<IBrowserContext>();
            IPage iPage = playwrightUiElement.GetPage();
            ILocator iLocator = playwrightUiElement.GetLocator();

            return (iBrowser, iBrowserContext, iPage, iLocator);
        }

        private void AssertPlaywrightUiElementNativeObjects(IUiElement uiElement, string expectedUrl, string expectedTagName)
        {
            var actualNativeObjects = GetPlaywrightUiElementNativeObjects(uiElement);
            var elementHandle = actualNativeObjects.Locator.ElementHandleAsync().Result;
            var actualTagName = actualNativeObjects.Locator.EvaluateAsync<string>("(element) => element.tagName", elementHandle).Result;
            var playwrightBrowserFactory = DiContainer.Instance.GetRequiredService<IPlaywrightBrowserFactory>() as PlaywrightBrowserFactory;
            var shouldCreateNewBrowserInstanceEachTime = playwrightBrowserFactory.ShouldCreateNewBrowserInstanceEachTime;

            using (new AssertionScope())
            {
                actualNativeObjects.Browser.Contexts.Should().Contain(actualNativeObjects.BrowserContext);

                if (shouldCreateNewBrowserInstanceEachTime)
                    actualNativeObjects.Browser.Contexts.Count.Should().Be(1);
                else
                    actualNativeObjects.Browser.Contexts.Should().Contain(actualNativeObjects.BrowserContext);

                actualNativeObjects.Page.Url.Should().Contain(expectedUrl);
                actualTagName.ToLower().Should().Contain(expectedTagName);
            }
        }

        private (IWebDriver WebDriver, IWebElement WebElement) GetSeleniumUiElementNativeObjects(IUiElement uiElement)
        {
            SeleniumUiElement selenimUiElement = (SeleniumUiElement)uiElement;
            IWebDriver iWebDriver = selenimUiElement.GetIWebDriver();
            IWebElement iWebElement = selenimUiElement.GetWebElement();

            return (iWebDriver, iWebElement);
        }

        private void AssertSeleniumUiElementNativeObjects(IUiElement uiElement, string expectedUrl, string expectedTagName)
        {
            var actualNativeObjects = GetSeleniumUiElementNativeObjects(uiElement);

            using (new AssertionScope())
            {
                actualNativeObjects.WebDriver.Url.Should().Contain(ExpectedUrl);
                actualNativeObjects.WebElement.TagName.Should().Contain(expectedTagName);
            }
        }
    }
}
