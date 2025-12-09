using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.TestData;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    internal class Tests_UiElement_Search_RelativeTo_ComplexUiElementAsClass : PerFeatureBaseTest
    {
        private UiElementSearchRelativeToComplexUiElementAsClassTestPageService _pageService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageService = UiInteractor.Interact<UiElementSearchRelativeToComplexUiElementAsClassTestPageService>();
        }

        [Test]
        [Description("Page -> Complex_UiElement_AsClass -> Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ComplexUiElementAsClass_SimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ComplexUiElementAsClass_86ea.SimpleUiElementAsInterface_86ea.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ComplexUiElementAsClass_86ea_SimpleUiElementAsInterface_86ea);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Complex_UiElement_AsClass -> Complex_UiElement_AsClass")]
        public void Should_Support_Search_Page_ComplexUiElementAsClass_ComplexUiElementAsClass()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ComplexUiElementAsClass_0d51.ComplexUiElementAsClass_fd65.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ComplexUiElementAsClass_0d51_ComplexUiElementAsClass_fd65);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_ComplexUiElementAsClass_UiElementsList_Of_SimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ComplexUiElementAsClass_576e_UiElementsList_e141_SimpleUiElementAsInterface_e141_Indx1_TextContent,
                ExpectedValues.Page_ComplexUiElementAsClass_576e_UiElementsList_e141_SimpleUiElementAsInterface_e141_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ComplexUiElementAsClass_576e.UiElementsListOfSimpleUiElementsAsInterface_e141.Select(x => x.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass")]
        public void Should_Support_SearchPage_ComplexUiElementAsClass_UiElementsList_Of_ComplexUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ComplexUiElementAsClass_44a5_UiElementsList_b0d2_ComplexUiElementAsClass_b0d2_Indx1_TextContent,
                ExpectedValues.Page_ComplexUiElementAsClass_44a5_UiElementsList_b0d2_ComplexUiElementAsClass_b0d2_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ComplexUiElementAsClass_44a5.UiElementsListOfComplexUiElementsAsClass_b0d2.Select(x => x.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ComplexUiElementAsClass_cd75.FrameSimpleUiElementAsInterface_cd75.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ComplexUiElementAsClass_cd75_FrameSimpleUiElementAsInterface_cd75_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_ComplexUiElementAsClass_UiElementsList_Of_FrameSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ComplexUiElementAsClass_030a_UiElementsList_030a_FrameSimpleUiElementAsInterface_030a_Indx1_IdAttribute,
                ExpectedValues.Page_ComplexUiElementAsClass_030a_UiElementsList_030a_FrameSimpleUiElementAsInterface_030a_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ComplexUiElementAsClass_030a.UiElementsListOfFrameSimpleUiElementsAsInterface_030a.Select(x => x.GetIdAttribute()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ComplexUiElementAsClass_FrameComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ComplexUiElementAsClass_5905.FrameComplexUiElementAsClass_5905.FrameSimpleUiElementAsInterface_5905.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ComplexUiElementAsClass_5905_FrameComplexUiElementAsClass_5905_FrameSimpleUiElementAsInterface_5905_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ComplexUiElementAsClass_7275.ShadowRootHostSimpleUiElementAsInterface_7275.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ComplexUiElementAsClass_7275_ShadowRootHostSimpleUiElementAsInterface_7275_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_ComplexUiElementAsClass_UiElementsList_Of_ShadowRootHostSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ComplexUiElementAsClass_8939_UiElementsList_8939_ShadowRootHostSimpleUiElementAsInterface_8939_Indx1_IdAttribute,
                ExpectedValues.Page_ComplexUiElementAsClass_8939_UiElementsList_8939_ShadowRootHostSimpleUiElementAsInterface_8939_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ComplexUiElementAsClass_8939.UiElementsListOfShadowRootHostSimpleUiElementsAsInterface_8939.Select(x => x.GetIdAttribute()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ComplexUiElementAsClass_4f49.ShadowRootHostComplexUiElementAsClass_4f49.ShadowRootHostSimpleUiElementAsInterface_4f49.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ComplexUiElementAsClass_4f49_ShadowRootHostComplexUiElementAsClass_4f49_ShadowRootHostSimpleUiElementAsInterface_4f49_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }
    }
}
