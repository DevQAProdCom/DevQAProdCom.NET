using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.TestData;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    internal class Tests_UiElement_Search_RelativeTo_2ShadowRootHostsComplexUiElementsAsClass : PerFeatureBaseTest
    {
        private UiElementSearchRelativeTo2ShadowRootHostsComplexUiElementsAsClassTestPageService _pageService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageService = UiInteractor.Interact<UiElementSearchRelativeTo2ShadowRootHostsComplexUiElementsAsClassTestPageService>();
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_SimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_d304.ShadowRootHostComplexUiElementAsClass_14e6.SimpleUiElementAsInterface_14e6.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_d304_ShadowRootHostComplexUiElementAsClass_14e6_SimpleUiElementAsInterface_14e6);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Complex_UiElement_AsClass")]
        public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_ComplexUiElementAsClass()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_a95f.ShadowRootHostComplexUiElementAsClass_4463.ComplexUiElementAsClass_4463.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_a95f_ShadowRootHostComplexUiElementAsClass_4463_ComplexUiElementAsClass_4463);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_UiElementsList_Of_SimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_9b04_ShadowRootHostComplexUiElementAsClass_c24c_UiElementsList_5ddd_SimpleUiElementAsInterface_5ddd_Indx1_TextContent,
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_9b04_ShadowRootHostComplexUiElementAsClass_c24c_UiElementsList_5ddd_SimpleUiElementAsInterface_5ddd_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_9b04.ShadowRootHostComplexUiElementAsClass_c24c.UiElementsListOfSimpleUiElementsAsInterface_5ddd.Select(x => x.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass")]
        public void Should_Support_SearchPage_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_UiElementsList_Of_ComplexUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_95c0_ShadowRootHostComplexUiElementAsClass_074a_UiElementsList_3503_ComplexUiElementAsClass_3503_Indx1_TextContent,
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_95c0_ShadowRootHostComplexUiElementAsClass_074a_UiElementsList_3503_ComplexUiElementAsClass_3503_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_95c0.ShadowRootHostComplexUiElementAsClass_074a.UiElementsListOfComplexUiElementsAsClass_3503.Select(x => x.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_5621.ShadowRootHostComplexUiElementAsClass_4853.FrameSimpleUiElementAsInterface_4853.GetAttribute(Const.id, isBooleanAttributeType: false);
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_5621_ShadowRootHostComplexUiElementAsClass_4853_FrameSimpleUiElementAsInterface_4853_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_UiElementsList_Of_FrameSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_6c2b_ShadowRootHostComplexUiElementAsClass_512f_UiElementsList_512f_FrameSimpleUiElementAsInterface_512f_Indx1_IdAttribute,
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_6c2b_ShadowRootHostComplexUiElementAsClass_512f_UiElementsList_512f_FrameSimpleUiElementAsInterface_512f_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_6c2b.ShadowRootHostComplexUiElementAsClass_512f.UiElementsListOfFrameSimpleUiElementsAsInterface_512f.Select(x => x.GetAttribute(Const.id, isBooleanAttributeType: false)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_FrameComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_8237.ShadowRootHostComplexUiElementAsClass_5a5b.FrameComplexUiElementAsClass_5a5b.FrameSimpleUiElementAsInterface_5a5b.GetAttribute(Const.id, isBooleanAttributeType: false);
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_8237_ShadowRootHostComplexUiElementAsClass_5a5b_FrameComplexUiElementAsClass_5a5b_FrameSimpleUiElementAsInterface_5a5b_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        //[Test]
        //[Ignore("Pairwise combination '2ShadowRootHosts -> ShadowRootHost' is covered in scope of other test for combination 'ShadowRootHost -> 2ShadowRootHosts'. Search by 'Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface' inside solution.")]
        //[Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        //public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        //{
        //}

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_UiElementsList_Of_ShadowRootHostSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_f929_ShadowRootHostComplexUiElementAsClass_733f_UiElementsList_733f_ShadowRootHostSimpleUiElementAsInterface_733f_Indx1_IdAttribute,
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_f929_ShadowRootHostComplexUiElementAsClass_733f_UiElementsList_733f_ShadowRootHostSimpleUiElementAsInterface_733f_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_f929.ShadowRootHostComplexUiElementAsClass_733f.UiElementsListOfShadowRootHostSimpleUiElementsAsInterface_733f.Select(x => x.GetAttribute(Const.id, isBooleanAttributeType: false)).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        //[Test]
        //[Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        //[Ignore("Pairwise combination '2ShadowRootHosts -> 2ShadowRootHosts' is covered in scope of other test for combination 'ShadowRootHost -> 2ShadowRootHosts'. Search by 'Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface' inside solution.")]
        //public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        //{
        //
        //
        //}
    }
}
