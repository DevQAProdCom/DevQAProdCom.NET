using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.TestData;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    internal class Tests_UiElement_Search_RelativeTo_2FramesComplexUiElementsAsClass : PerFeatureBaseTest
    {
        private UiElementSearchRelativeTo2FramesComplexUiElementsAsClassTestPageService _pageService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageService = UiInteractor.Interact<UiElementSearchRelativeTo2FramesComplexUiElementsAsClassTestPageService>();
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_SimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_FrameComplexUiElementAsClass_e720.FrameComplexUiElementAsClass_b7bf.SimpleUiElementAsInterface_b7bf.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameComplexUiElementAsClass_e720_FrameComplexUiElementAsClass_b7bf_SimpleUiElementAsInterface_b7bf);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Complex_UiElement_AsClass")]
        public void Should_Support_Search_Page_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_ComplexUiElementAsClass()
        {
            //WHEN
            var actualValue = _pageService._page.Page_FrameComplexUiElementAsClass_ae97.FrameComplexUiElementAsClass_85b8.ComplexUiElementAsClass_85b8.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameComplexUiElementAsClass_ae97_FrameComplexUiElementAsClass_85b8_ComplexUiElementAsClass_85b8);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_UiElementsList_Of_SimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_FrameComplexUiElementAsClass_d299_FrameComplexUiElementAsClass_b6ad_UiElementsList_56d9_SimpleUiElementAsInterface_56d9_Indx1_TextContent,
                ExpectedValues.Page_FrameComplexUiElementAsClass_d299_FrameComplexUiElementAsClass_b6ad_UiElementsList_56d9_SimpleUiElementAsInterface_56d9_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_FrameComplexUiElementAsClass_d299.FrameComplexUiElementAsClass_b6ad.UiElementsListOfSimpleUiElementsAsInterface_56d9.Select(x => x.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass")]
        public void Should_Support_SearchPage_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_UiElementsList_Of_ComplexUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_FrameComplexUiElementAsClass_4f4d_FrameComplexUiElementAsClass_9984_UiElementsList_83f1_ComplexUiElementAsClass_83f1_Indx1_TextContent,
                ExpectedValues.Page_FrameComplexUiElementAsClass_4f4d_FrameComplexUiElementAsClass_9984_UiElementsList_83f1_ComplexUiElementAsClass_83f1_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_FrameComplexUiElementAsClass_4f4d.FrameComplexUiElementAsClass_9984.UiElementsListOfSimpleUiElementsAsInterface_83f1.Select(x => x.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        //[Test]
        //[Ignore("Pairwise combination '2Frames -> Frame' is covered in scope of other test for combination 'Frame -> 2Frames'. Search by 'Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface' inside solution.")]
        //[Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        //public void Should_Support_Search_Page_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        //{
        //}

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_UiElementsList_Of_FrameSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_FrameComplexUiElementAsClass_78c2_FrameComplexUiElementAsClass_4160_UiElementsList_4160_FrameSimpleUiElementAsInterface_4160_Indx1_IdAttribute,
                ExpectedValues.Page_FrameComplexUiElementAsClass_78c2_FrameComplexUiElementAsClass_4160_UiElementsList_4160_FrameSimpleUiElementAsInterface_4160_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_FrameComplexUiElementAsClass_78c2.FrameComplexUiElementAsClass_4160.UiElementsListOfFrameSimpleUiElementsAsInterface_4160.Select(x => x.GetIdAttribute()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        //[Test]
        //[Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        //[Ignore("Pairwise combination '2Frames -> 2Frames' is covered in scope of other test for combination 'Frame -> 2Frames'. Search by 'Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface' inside solution.")]
        //public void Should_Support_Search_Page_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        //{
        //
        //
        //}

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_FrameComplexUiElementAsClass_e8a7.FrameComplexUiElementAsClass_2075.ShadowRootHostSimpleUiElementAsInterface_2075.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameComplexUiElementAsClass_e8a7_FrameComplexUiElementAsClass_2075_ShadowRootHostSimpleUiElementAsInterface_2075_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_UiElementsList_Of_ShadowRootHostSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_FrameComplexUiElementAsClass_3279_FrameComplexUiElementAsClass_4ee2_UiElementsList_4ee2_ShadowRootHostSimpleUiElementAsInterface_4ee2_Indx1_IdAttribute,
                ExpectedValues.Page_FrameComplexUiElementAsClass_3279_FrameComplexUiElementAsClass_4ee2_UiElementsList_4ee2_ShadowRootHostSimpleUiElementAsInterface_4ee2_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_FrameComplexUiElementAsClass_3279.FrameComplexUiElementAsClass_4ee2.UiElementsListOfShadowRootHostSimpleUiElementsAsInterface_4ee2.Select(x => x.GetIdAttribute()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_FrameComplexUiElementAsClass_FrameComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_FrameComplexUiElementAsClass_29f4.FrameComplexUiElementAsClass_f2ab.ShadowRootHostComplexUiElementAsClass_f2ab.ShadowRootHostSimpleUiElementAsInterface_f2ab.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_FrameComplexUiElementAsClass_29f4_FrameComplexUiElementAsClass_f2ab_ShadowRootHostComplexUiElementAsClass_f2ab_ShadowRootHostSimpleUiElementAsInterface_f2ab_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }
    }
}
