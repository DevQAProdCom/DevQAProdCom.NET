using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.TestData;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    internal class Tests_UiElement_Search_RelativeTo_UiElementsListOfShadowRootHostComplexUiElementsAsClass : PerFeatureBaseTest
    {
        private UiElementSearchRelativeToUiElementsListOfShadowRootHostComplexUiElementsAsClassTestPageService _pageService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageService = UiInteractor.Interact<UiElementSearchRelativeToUiElementsListOfShadowRootHostComplexUiElementsAsClassTestPageService>();
        }

        [Test]
        [Description("Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ShadowRootHostComplexUiElementsAsClass_SimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_bbe0_ShadowRootHostComplexUiElementAsClass_bbe0_Indx1_SimpleUiElementAsInterface_bbe0_TextContent,
                ExpectedValues.Page_UiElementsList_bbe0_ShadowRootHostComplexUiElementAsClass_bbe0_Indx2_SimpleUiElementAsInterface_bbe0_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfShadowRootHostComplexUiElementsAsClass_bbe0.Select(x => x.SimpleUiElementAsInterface_bbe0.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Complex_UiElement_AsClass")]
        public void Should_Support_Search_Page_UiElementsList_Of_ShadowRootHostComplexUiElementsAsClass_ComplexUiElementAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_46e3_ShadowRootHostComplexUiElementAsClass_46e3_Indx1_ComplexUiElementAsClass_a41c_TextContent,
                ExpectedValues.Page_UiElementsList_46e3_ShadowRootHostComplexUiElementAsClass_46e3_Indx2_ComplexUiElementAsClass_a41c_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfShadowRootHostComplexUiElementsAsClass_46e3.Select(x => x.ComplexUiElementAsClass_a41c.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ShadowRootHostComplexUiElementsAsClass_UiElementsList_Of_SimpleUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_a368_ShadowRootHostComplexUiElementAsClass_a368_Indx1_UiElementsList_a1a9_SimpleUiElementAsInterface_a1a9_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_a368_ShadowRootHostComplexUiElementAsClass_a368_Indx1_UiElementsList_a1a9_SimpleUiElementAsInterface_a1a9_Indx2_TextContent,
                ExpectedValues.Page_UiElementsList_a368_ShadowRootHostComplexUiElementAsClass_a368_Indx2_UiElementsList_a1a9_SimpleUiElementAsInterface_a1a9_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_a368_ShadowRootHostComplexUiElementAsClass_a368_Indx2_UiElementsList_a1a9_SimpleUiElementAsInterface_a1a9_Indx2_TextContent
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfShadowRootHostComplexUiElementsAsClass_a368
                .SelectMany(x => x.UiElementsListOfSimpleUiElementsAsInterface_a1a9.Select(x => x.GetTextContent())).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass")]
        public void Should_Support_Search_Page_UiElementsList_Of_ShadowRootHostComplexUiElementsAsClass_UiElementsList_Of_ShadowRootHostComplexUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_c417_ShadowRootHostComplexUiElementAsClass_c417_Indx1_UiElementsList_2b8c_ComplexUiElementAsClass_2b8c_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_c417_ShadowRootHostComplexUiElementAsClass_c417_Indx1_UiElementsList_2b8c_ComplexUiElementAsClass_2b8c_Indx2_TextContent,
                ExpectedValues.Page_UiElementsList_c417_ShadowRootHostComplexUiElementAsClass_c417_Indx2_UiElementsList_2b8c_ComplexUiElementAsClass_2b8c_Indx1_TextContent,
                ExpectedValues.Page_UiElementsList_c417_ShadowRootHostComplexUiElementAsClass_c417_Indx2_UiElementsList_2b8c_ComplexUiElementAsClass_2b8c_Indx2_TextContent
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfShadowRootHostComplexUiElementsAsClass_c417
                .SelectMany(x => x.UiElementsListOfComplexUiElementsAsClass_2b8c.Select(x => x.GetTextContent())).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ShadowRootHostComplexUiElementsAsClass_FrameSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_1e08_ShadowRootHostComplexUiElementAsClass_1e08_Indx1_FrameSimpleUiElementAsInterface_1e08_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_1e08_ShadowRootHostComplexUiElementAsClass_1e08_Indx2_FrameSimpleUiElementAsInterface_1e08_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfShadowRootHostComplexUiElementsAsClass_1e08.Select(x => x.FrameSimpleUiElementAsInterface_1e08.GetAttribute(Const.dataText, isBooleanAttributeType: false)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ShadowRootHostComplexUiElementsAsClass_UiElementsList_Of_Frame_SimpleUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_a939_ShadowRootHostComplexUiElementAsClass_a939_Indx1_UiElementsList_4bf9_FrameSimpleUiElementAsInterface_4bf9_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_a939_ShadowRootHostComplexUiElementAsClass_a939_Indx1_UiElementsList_4bf9_FrameSimpleUiElementAsInterface_4bf9_Indx2_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_a939_ShadowRootHostComplexUiElementAsClass_a939_Indx2_UiElementsList_4bf9_FrameSimpleUiElementAsInterface_4bf9_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_a939_ShadowRootHostComplexUiElementAsClass_a939_Indx2_UiElementsList_4bf9_FrameSimpleUiElementAsInterface_4bf9_Indx2_DataTextAttribute
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfShadowRootHostComplexUiElementsAsClass_a939
                .SelectMany(x => x.UiElementsListOfFrameSimpleUiElementsAsInterface_4bf9.Select(x => x.GetAttribute(Const.dataText, isBooleanAttributeType: false))).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ShadowRootHostComplexUiElementsAsClass_FrameComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_a6e8_ShadowRootHostComplexUiElementAsClass_a6e8_Indx1_FrameComplexUiElementAsClass_53dd_FrameSimpleUiElementAsInterface_53dd_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_a6e8_ShadowRootHostComplexUiElementAsClass_a6e8_Indx2_FrameComplexUiElementAsClass_53dd_FrameSimpleUiElementAsInterface_53dd_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfShadowRootHostComplexUiElementsAsClass_a6e8
                .Select(x => x.FrameComplexUiElementAsClass_53dd.FrameSimpleUiElementAsInterface_53dd.GetAttribute(Const.dataText, isBooleanAttributeType: false)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ShadowRootHostComplexUiElementsAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_9efe_ShadowRootHostComplexUiElementAsClass_9efe_Indx1_ShadowRootHostSimpleUiElementAsInterface_9efe_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_9efe_ShadowRootHostComplexUiElementAsClass_9efe_Indx2_ShadowRootHostSimpleUiElementAsInterface_9efe_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfShadowRootHostComplexUiElementsAsClass_9efe.Select(x => x.ShadowRootHostSimpleUiElementAsInterface_9efe.GetAttribute(Const.dataText, isBooleanAttributeType: false)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ShadowRootHostComplexUiElementsAsClass_UiElementsList_Of_ShadowRootHost_SimpleUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_2377_ShadowRootHostComplexUiElementAsClass_2377_Indx1_UiElementsList_cc40_ShadowRootHostSimpleUiElementAsInterface_cc40_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_2377_ShadowRootHostComplexUiElementAsClass_2377_Indx1_UiElementsList_cc40_ShadowRootHostSimpleUiElementAsInterface_cc40_Indx2_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_2377_ShadowRootHostComplexUiElementAsClass_2377_Indx2_UiElementsList_cc40_ShadowRootHostSimpleUiElementAsInterface_cc40_Indx1_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_2377_ShadowRootHostComplexUiElementAsClass_2377_Indx2_UiElementsList_cc40_ShadowRootHostSimpleUiElementAsInterface_cc40_Indx2_DataTextAttribute
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfShadowRootHostComplexUiElementsAsClass_2377
                .SelectMany(x => x.UiElementsListOfShadowRootHostSimpleUiElementsAsInterface_cc40.Select(x => x.GetAttribute(Const.dataText, isBooleanAttributeType: false))).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_UiElementsList_Of_ShadowRootHostComplexUiElementsAsClass_ShadowRootHostComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_UiElementsList_378f_ShadowRootHostComplexUiElementAsClass_378f_Indx1_ShadowRootHostComplexUiElementAsClass_eea5_ShadowRootHostSimpleUiElementAsInterface_eea5_DataTextAttribute,
                ExpectedValues.Page_UiElementsList_378f_ShadowRootHostComplexUiElementAsClass_378f_Indx2_ShadowRootHostComplexUiElementAsClass_eea5_ShadowRootHostSimpleUiElementAsInterface_eea5_DataTextAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_UiElementsListOfShadowRootHostComplexUiElementsAsClass_378f
                .Select(x => x.ShadowRootHostComplexUiElementAsClass_eea5.ShadowRootHostSimpleUiElementAsInterface_eea5.GetAttribute(Const.dataText, isBooleanAttributeType: false)).ToList();
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
