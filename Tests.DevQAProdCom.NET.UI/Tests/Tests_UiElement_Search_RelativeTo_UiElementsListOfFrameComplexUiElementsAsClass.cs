using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.TestData;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    internal class Tests_UiElement_Search_RelativeTo_UiElementsListOfFrameComplexUiElementsAsClass : PerFeatureBaseTest
    {
        private UiElementSearchRelativeToUiElementsListOfFrameComplexUiElementsAsClassTestPageService _pageService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageService = UiInteractor.Interact<UiElementSearchRelativeToUiElementsListOfFrameComplexUiElementsAsClassTestPageService>();
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_FrameComplexUiElementsAsClass_SimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_5d7b_FrameComplexUiElementAsClass_5d7b_Indx1_SimpleUiElementAsInterface_5d7b_TextContent,
                ExpectedValues.Page_UiElementsList_5d7b_FrameComplexUiElementAsClass_5d7b_Indx2_SimpleUiElementAsInterface_5d7b_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfFrameComplexUiElementsAsClass_5d7b.Select(x => x.SimpleUiElementAsInterface_5d7b.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Complex_UiElement_AsClass")]
        public void Should_Support_Search_Page_UiElementsList_Of_FrameComplexUiElementsAsClass_ComplexUiElementAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_5694_FrameComplexUiElementAsClass_5694_Indx1_ComplexUiElementAsClass_246f_TextContent,
                ExpectedValues.Page_UiElementsList_5694_FrameComplexUiElementAsClass_5694_Indx2_ComplexUiElementAsClass_246f_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfFrameComplexUiElementsAsClass_5694.Select(x => x.ComplexUiElementAsClass_246f.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_FrameComplexUiElementsAsClass_UiElementsList_Of_SimpleUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_473a_FrameComplexUiElementAsClass_473a_Indx1_UiElementsList_b48f_SimpleUiElementAsInterface_b48f_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_473a_FrameComplexUiElementAsClass_473a_Indx1_UiElementsList_b48f_SimpleUiElementAsInterface_b48f_Indx2_TextContent,
                ExpectedValues.Page_UiElementsList_473a_FrameComplexUiElementAsClass_473a_Indx2_UiElementsList_b48f_SimpleUiElementAsInterface_b48f_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_473a_FrameComplexUiElementAsClass_473a_Indx2_UiElementsList_b48f_SimpleUiElementAsInterface_b48f_Indx2_TextContent
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfFrameComplexUiElementsAsClass_473a
                .SelectMany(x => x.UiElementsListOfSimpleUiElementsAsInterface_b48f.Select(x => x.GetTextContent())).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass")]
        public void Should_Support_Search_Page_UiElementsList_Of_FrameComplexUiElementsAsClass_UiElementsList_Of_FrameComplexUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_5c57_FrameComplexUiElementAsClass_5c57_Indx1_UiElementsList_0f36_ComplexUiElementAsClass_0f36_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_5c57_FrameComplexUiElementAsClass_5c57_Indx1_UiElementsList_0f36_ComplexUiElementAsClass_0f36_Indx2_TextContent,
                ExpectedValues.Page_UiElementsList_5c57_FrameComplexUiElementAsClass_5c57_Indx2_UiElementsList_0f36_ComplexUiElementAsClass_0f36_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_5c57_FrameComplexUiElementAsClass_5c57_Indx2_UiElementsList_0f36_ComplexUiElementAsClass_0f36_Indx2_TextContent
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfFrameComplexUiElementsAsClass_5c57
                .SelectMany(x => x.UiElementsListOfComplexUiElementsAsClass_0f36.Select(x => x.GetTextContent())).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_FrameComplexUiElementsAsClass_FrameSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_5d5a_FrameComplexUiElementAsClass_5d5a_Indx1_FrameSimpleUiElementAsInterface_5d5a_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_5d5a_FrameComplexUiElementAsClass_5d5a_Indx2_FrameSimpleUiElementAsInterface_5d5a_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfFrameComplexUiElementsAsClass_5d5a.Select(x => x.FrameSimpleUiElementAsInterface_5d5a.GetNonBooleanAttribute(Const.dataText)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_FrameComplexUiElementsAsClass_UiElementsList_Of_Frame_SimpleUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_dd66_FrameComplexUiElementAsClass_dd66_Indx1_UiElementsList_ac68_FrameSimpleUiElementAsInterface_ac68_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_dd66_FrameComplexUiElementAsClass_dd66_Indx1_UiElementsList_ac68_FrameSimpleUiElementAsInterface_ac68_Indx2_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_dd66_FrameComplexUiElementAsClass_dd66_Indx2_UiElementsList_ac68_FrameSimpleUiElementAsInterface_ac68_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_dd66_FrameComplexUiElementAsClass_dd66_Indx2_UiElementsList_ac68_FrameSimpleUiElementAsInterface_ac68_Indx2_DataTextAttribute
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfFrameComplexUiElementsAsClass_dd66
                .SelectMany(x => x.UiElementsListOfFrameSimpleUiElementsAsInterface_ac68.Select(x => x.GetNonBooleanAttribute(Const.dataText))).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_FrameComplexUiElementsAsClass_FrameComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_558a_FrameComplexUiElementAsClass_558a_Indx1_FrameComplexUiElementAsClass_a401_FrameSimpleUiElementAsInterface_a401_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_558a_FrameComplexUiElementAsClass_558a_Indx2_FrameComplexUiElementAsClass_a401_FrameSimpleUiElementAsInterface_a401_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfFrameComplexUiElementsAsClass_558a
                .Select(x => x.FrameComplexUiElementAsClass_a401.FrameSimpleUiElementAsInterface_a401.GetNonBooleanAttribute(Const.dataText)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_FrameComplexUiElementsAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_4ffa_FrameComplexUiElementAsClass_4ffa_Indx1_ShadowRootHostSimpleUiElementAsInterface_4ffa_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_4ffa_FrameComplexUiElementAsClass_4ffa_Indx2_ShadowRootHostSimpleUiElementAsInterface_4ffa_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfFrameComplexUiElementsAsClass_4ffa.Select(x => x.ShadowRootHostSimpleUiElementAsInterface_4ffa.GetNonBooleanAttribute(Const.dataText)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_FrameComplexUiElementsAsClass_UiElementsList_Of_ShadowRootHost_SimpleUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_93c7_FrameComplexUiElementAsClass_93c7_Indx1_UiElementsList_7580_ShadowRootHostSimpleUiElementAsInterface_7580_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_93c7_FrameComplexUiElementAsClass_93c7_Indx1_UiElementsList_7580_ShadowRootHostSimpleUiElementAsInterface_7580_Indx2_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_93c7_FrameComplexUiElementAsClass_93c7_Indx2_UiElementsList_7580_ShadowRootHostSimpleUiElementAsInterface_7580_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_93c7_FrameComplexUiElementAsClass_93c7_Indx2_UiElementsList_7580_ShadowRootHostSimpleUiElementAsInterface_7580_Indx2_DataTextAttribute
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfFrameComplexUiElementsAsClass_93c7
                .SelectMany(x => x.UiElementsListOfShadowRootHostSimpleUiElementsAsInterface_7580.Select(x => x.GetNonBooleanAttribute(Const.dataText))).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_FrameComplexUiElementsAsClass_ShadowRootHostComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_76f1_FrameComplexUiElementAsClass_76f1_Indx1_ShadowRootHostComplexUiElementAsClass_76f1_ShadowRootHostSimpleUiElementAsInterface_76f1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_76f1_FrameComplexUiElementAsClass_76f1_Indx2_ShadowRootHostComplexUiElementAsClass_76f1_ShadowRootHostSimpleUiElementAsInterface_76f1_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfFrameComplexUiElementsAsClass_76f1
                .Select(x => x.ShadowRootHostComplexUiElementAsClass_76f1.ShadowRootHostSimpleUiElementAsInterface_76f1.GetNonBooleanAttribute(Const.dataText)).ToList();
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
