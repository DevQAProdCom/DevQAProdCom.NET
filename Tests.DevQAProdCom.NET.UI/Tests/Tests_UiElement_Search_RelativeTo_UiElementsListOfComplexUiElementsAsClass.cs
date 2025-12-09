using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.TestData;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    internal class Tests_UiElement_Search_RelativeTo_UiElementsListOfComplexUiElementsAsClass : PerFeatureBaseTest
    {
        private UiElementSearchRelativeToUiElementsListOfComplexUiElementsAsClassTestPageService _pageService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageService = UiInteractor.Interact<UiElementSearchRelativeToUiElementsListOfComplexUiElementsAsClassTestPageService>();
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ComplexUiElementsAsClass_SimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_8921_ComplexUiElementAsClass_8921_Indx1_SimpleUiElementAsInterface_8921_TextContent,
                ExpectedValues.Page_UiElementsList_8921_ComplexUiElementAsClass_8921_Indx2_SimpleUiElementAsInterface_8921_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfComplexUiElementsAsClass_8921.Select(x => x.SimpleUiElementAsInterface_8921.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Complex_UiElement_AsClass")]
        public void Should_Support_Search_Page_UiElementsList_Of_ComplexUiElementsAsClass_ComplexUiElementAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_8e8c_ComplexUiElementAsClass_8e8c_Indx1_ComplexUiElementAsClass_d543_TextContent,
                ExpectedValues.Page_UiElementsList_8e8c_ComplexUiElementAsClass_8e8c_Indx2_ComplexUiElementAsClass_d543_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfComplexUiElementsAsClass_8e8c.Select(x => x.ComplexUiElementAsClass_d543.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ComplexUiElementsAsClass_UiElementsList_Of_SimpleUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_79c4_ComplexUiElementAsClass_79c4_Indx1_UiElementsList_5bea_SimpleUiElementAsInterface_5bea_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_79c4_ComplexUiElementAsClass_79c4_Indx1_UiElementsList_5bea_SimpleUiElementAsInterface_5bea_Indx2_TextContent,
                ExpectedValues.Page_UiElementsList_79c4_ComplexUiElementAsClass_79c4_Indx2_UiElementsList_5bea_SimpleUiElementAsInterface_5bea_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_79c4_ComplexUiElementAsClass_79c4_Indx2_UiElementsList_5bea_SimpleUiElementAsInterface_5bea_Indx2_TextContent
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfComplexUiElementsAsClass_79c4
                .SelectMany(x => x.UiElementsListOfSimpleUiElementsAsInterface_5bea.Select(x => x.GetTextContent())).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass")]
        public void Should_Support_Search_Page_UiElementsList_Of_ComplexUiElementsAsClass_UiElementsList_Of_ComplexUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_c0d6_ComplexUiElementAsClass_c0d6_Indx1_UiElementsList_9400_ComplexUiElementAsClass_9400_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_c0d6_ComplexUiElementAsClass_c0d6_Indx1_UiElementsList_9400_ComplexUiElementAsClass_9400_Indx2_TextContent,
                ExpectedValues.Page_UiElementsList_c0d6_ComplexUiElementAsClass_c0d6_Indx2_UiElementsList_9400_ComplexUiElementAsClass_9400_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_c0d6_ComplexUiElementAsClass_c0d6_Indx2_UiElementsList_9400_ComplexUiElementAsClass_9400_Indx2_TextContent
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfComplexUiElementsAsClass_c0d6
                .SelectMany(x => x.UiElementsListOfComplexUiElementsAsClass_9400.Select(x => x.GetTextContent())).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ComplexUiElementsAsClass_FrameSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_4596_ComplexUiElementAsClass_4596_Indx1_FrameSimpleUiElementAsInterface_4596_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_4596_ComplexUiElementAsClass_4596_Indx2_FrameSimpleUiElementAsInterface_4596_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfComplexUiElementsAsClass_4596.Select(x => x.FrameSimpleUiElementAsInterface_4596.GetNonBooleanAttribute(Const.dataText)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ComplexUiElementsAsClass_UiElementsList_Of_Frame_SimpleUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_9240_ComplexUiElementAsClass_9240_Indx1_UiElementsList_07b9_FrameSimpleUiElementAsInterface_07b9_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_9240_ComplexUiElementAsClass_9240_Indx1_UiElementsList_07b9_FrameSimpleUiElementAsInterface_07b9_Indx2_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_9240_ComplexUiElementAsClass_9240_Indx2_UiElementsList_07b9_FrameSimpleUiElementAsInterface_07b9_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_9240_ComplexUiElementAsClass_9240_Indx2_UiElementsList_07b9_FrameSimpleUiElementAsInterface_07b9_Indx2_DataTextAttribute
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfComplexUiElementsAsClass_9240
                .SelectMany(x => x.UiElementsListOfFrameSimpleUiElementsAsInterface_07b9.Select(x => x.GetNonBooleanAttribute(Const.dataText))).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ComplexUiElementsAsClass_FrameComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_68b1_ComplexUiElementAsClass_68b1_Indx1_FrameComplexUiElementAsClass_68b1_FrameSimpleUiElementAsInterface_68b1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_68b1_ComplexUiElementAsClass_68b1_Indx2_FrameComplexUiElementAsClass_68b1_FrameSimpleUiElementAsInterface_68b1_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfComplexUiElementsAsClass_68b1
                .Select(x => x.FrameComplexUiElementAsClass_68b1.FrameSimpleUiElementAsInterface_68b1.GetNonBooleanAttribute(Const.dataText)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Complex_UiElements_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ComplexUiElementsAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_52fa_ComplexUiElementAsClass_52fa_Indx1_ShadowRootHostSimpleUiElementAsInterface_52fa_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_52fa_ComplexUiElementAsClass_52fa_Indx2_ShadowRootHostSimpleUiElementAsInterface_52fa_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfComplexUiElementsAsClass_52fa.Select(x => x.ShadowRootHostSimpleUiElementAsInterface_52fa.GetNonBooleanAttribute(Const.dataText)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ComplexUiElementsAsClass_UiElementsList_Of_ShadowRootHost_SimpleUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_d413_ComplexUiElementAsClass_d413_Indx1_UiElementsList_a018_ShadowRootHostSimpleUiElementAsInterface_a018_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_d413_ComplexUiElementAsClass_d413_Indx1_UiElementsList_a018_ShadowRootHostSimpleUiElementAsInterface_a018_Indx2_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_d413_ComplexUiElementAsClass_d413_Indx2_UiElementsList_a018_ShadowRootHostSimpleUiElementAsInterface_a018_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_d413_ComplexUiElementAsClass_d413_Indx2_UiElementsList_a018_ShadowRootHostSimpleUiElementAsInterface_a018_Indx2_DataTextAttribute
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfComplexUiElementsAsClass_d413
                .SelectMany(x => x.UiElementsListOfShadowRootHostSimpleUiElementsAsInterface_a018.Select(x => x.GetNonBooleanAttribute(Const.dataText))).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Complex_UiElements_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ComplexUiElementsAsClass_ShadowRootHostComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_49e5_ComplexUiElementAsClass_49e5_Indx1_ShadowRootHostComplexUiElementAsClass_49e5_ShadowRootHostSimpleUiElementAsInterface_49e5_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_49e5_ComplexUiElementAsClass_49e5_Indx2_ShadowRootHostComplexUiElementAsClass_49e5_ShadowRootHostSimpleUiElementAsInterface_49e5_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfComplexUiElementsAsClass_49e5
                .Select(x => x.ShadowRootHostComplexUiElementAsClass_49e5.ShadowRootHostSimpleUiElementAsInterface_49e5.GetNonBooleanAttribute(Const.dataText)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }
    }
}
