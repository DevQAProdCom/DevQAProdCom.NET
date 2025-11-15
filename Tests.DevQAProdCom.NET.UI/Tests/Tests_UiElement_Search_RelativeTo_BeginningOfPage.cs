using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.TestData;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    internal class Tests_UiElement_Search_RelativeTo_BeginningOfPage : PerFeatureBaseTest
    {
        private UiElementSearchRelativeToBeginningOfPageTestPageService _pageService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageService = UiInteractor.Interact<UiElementSearchRelativeToBeginningOfPageTestPageService>();
        }

        [Test]
        [Description("Page -> Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_SimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_SimpleUiElementAsInterface_e424.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_SimpleUiElementAsInterface_e424_TextContent);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Complex_UiElement_AsClass")]
        public void Should_Support_Search_Page_ComplexUiElementAsClass()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ComplexUiElementAsClass_4fe1.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ComplexUiElementAsClass_4fe1_TextContent);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Simple_UiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_SimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_36d9_SimpleUiElementAsInterface_36d9_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_36d9_SimpleUiElementAsInterface_36d9_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfSimpleUiElementsAsInterface_36d9.Select(x => x.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }


        [Test]
        [Description("Page -> UiElementsList_Of_Complex_UiElements_AsClass")]
        public void Should_Support_Search_Page_UiElementsList_Of_ComplexUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_e125_ComplexUiElementsAsClass_e125_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_e125_ComplexUiElementsAsClass_e125_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfComplexUiElementsAsClass_e125.Select(x => x.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_FrameSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_FrameSimpleUiElementAsInterface_c7bd.GetAttribute(Const.id, isBooleanAttributeType: false);
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameSimpleUiElementAsInterface_c7bd_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Frame_SimpleUiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_FrameSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_1fc6_FrameSimpleUiElementAsInterface_1fc6_Indx1_IdAttribute,
                ExpectedValues.Page_UiElementsList_1fc6_FrameSimpleUiElementAsInterface_1fc6_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfFrameSimpleUiElementsAsInterface_1fc6.Select(x => x.GetAttribute(Const.id, isBooleanAttributeType: false)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_FrameComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_FrameComplexUiElementAsClass_07e0.FrameSimpleUiElementAsInterface_07e0.GetAttribute(Const.id, isBooleanAttributeType: false);
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameComplexUiElementAsClass_07e0_FrameSimpleUiElementAsInterface_07e0_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ShadowRootHostSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostSimpleUiElementAsInterface_4d55.GetAttribute(Const.id, isBooleanAttributeType: false);
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostSimpleUiElementAsInterface_4d55_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_ShadowRootHost_SimpleUiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ShadowRootHostSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_bbd6_ShadowRootHostSimpleUiElementAsInterface_bbd6_Indx1_IdAttribute,
                ExpectedValues.Page_UiElementsList_bbd6_ShadowRootHostSimpleUiElementAsInterface_bbd6_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfShadowRootHostSimpleUiElementsAsInterface_bbd6.Select(x => x.GetAttribute(Const.id, isBooleanAttributeType: false)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_ShadowRootHost_Search_Page_ShadowRootHostComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_740c.ShadowRootHostSimpleUiElementAsInterface_740c.GetAttribute(Const.id, isBooleanAttributeType: false);
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_740c_ShadowRootHostSimpleUiElementAsInterface_740c_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }
    }
}
