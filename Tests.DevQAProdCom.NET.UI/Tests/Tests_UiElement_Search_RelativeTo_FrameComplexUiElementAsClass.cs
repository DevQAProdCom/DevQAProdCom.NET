using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.TestData;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    internal class Tests_UiElement_Search_RelativeTo_FrameComplexUiElementAsClass : PerFeatureBaseTest
    {
        private UiElementSearchRelativeToFrameComplexUiElementAsClassTestPageService _pageService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageService = UiInteractor.Interact<UiElementSearchRelativeToFrameComplexUiElementAsClassTestPageService>();
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_FrameComplexUiElementAsClass_SimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_FrameComplexUiElementAsClass_965d.SimpleUiElementAsInterface_965d.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameComplexUiElementAsClass_965d_SimpleUiElementAsInterface_965d);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Complex_UiElement_AsClass")]
        public void Should_Support_Search_Page_FrameComplexUiElementAsClass_ComplexUiElementAsClass()
        {
            //WHEN
            var actualValue = _pageService._page.Page_FrameComplexUiElementAsClass_c39d.ComplexUiElementAsClass_c39d.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameComplexUiElementAsClass_c39d_ComplexUiElementAsClass_c39d);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_FrameComplexUiElementAsClass_UiElementsList_Of_SimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_FrameComplexUiElementAsClass_7c15_UiElementsList_6399_SimpleUiElementAsInterface_6399_Indx1_TextContent,
                ExpectedValues.Page_FrameComplexUiElementAsClass_7c15_UiElementsList_6399_SimpleUiElementAsInterface_6399_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_FrameComplexUiElementAsClass_7c15.UiElementsListOfSimpleUiElementsAsInterface_6399.Select(x => x.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass")]
        public void Should_Support_SearchPage_FrameComplexUiElementAsClass_UiElementsList_Of_ComplexUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_FrameComplexUiElementAsClass_b0fc_UiElementsList_47fa_ComplexUiElementAsClass_47fa_Indx1_TextContent,
                ExpectedValues.Page_FrameComplexUiElementAsClass_b0fc_UiElementsList_47fa_ComplexUiElementAsClass_47fa_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_FrameComplexUiElementAsClass_b0fc.UiElementsListOfComplexUiElementsAsClass_47fa.Select(x => x.GetTextContent()).ToList();
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
            var actualValue = _pageService._page.Page_FrameComplexUiElementAsClass_b152.FrameSimpleUiElementAsInterface_b152.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameComplexUiElementAsClass_b152_FrameSimpleUiElementAsInterface_b152_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_FrameComplexUiElementAsClass_UiElementsList_Of_FrameSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_FrameComplexUiElementAsClass_50c2_UiElementsList_50c2_FrameSimpleUiElementAsInterface_50c2_Indx1_IdAttribute,
                ExpectedValues.Page_FrameComplexUiElementAsClass_50c2_UiElementsList_50c2_FrameSimpleUiElementAsInterface_50c2_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_FrameComplexUiElementAsClass_50c2.UiElementsListOfFrameSimpleUiElementsAsInterface_50c2.Select(x => x.GetIdAttribute()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_FrameComplexUiElementAsClass_e77c.FrameComplexUiElementAsClass_7116.FrameSimpleUiElementAsInterface_7116.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameComplexUiElementAsClass_e77c_FrameComplexUiElementAsClass_7116_FrameSimpleUiElementAsInterface_7116_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_FrameComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_FrameComplexUiElementAsClass_d481.ShadowRootHostSimpleUiElementAsInterface_d481.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameComplexUiElementAsClass_d481_ShadowRootHostSimpleUiElementAsInterface_d481_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_FrameComplexUiElementAsClass_UiElementsList_Of_ShadowRootHostSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_FrameComplexUiElementAsClass_b37b_UiElementsList_b37b_ShadowRootHostSimpleUiElementAsInterface_b37b_Indx1_IdAttribute,
                ExpectedValues.Page_FrameComplexUiElementAsClass_b37b_UiElementsList_b37b_ShadowRootHostSimpleUiElementAsInterface_b37b_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_FrameComplexUiElementAsClass_b37b.UiElementsListOfShadowRootHostSimpleUiElementsAsInterface_b37b.Select(x => x.GetIdAttribute()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_FrameComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_FrameComplexUiElementAsClass_8acd.ShadowRootHostComplexUiElementAsClass_8acd.ShadowRootHostSimpleUiElementAsInterface_8acd.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameComplexUiElementAsClass_8acd_ShadowRootHostComplexUiElementAsClass_8acd_ShadowRootHostSimpleUiElementAsInterface_8acd_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }
    }
}
