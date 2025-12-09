using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.TestData;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    internal class Tests_UiElement_Search_RelativeTo_ShadowRootHostComplexUiElementAsClass : PerFeatureBaseTest
    {
        private UiElementSearchRelativeToShadowRootHostComplexUiElementAsClassTestPageService _pageService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pageService = UiInteractor.Interact<UiElementSearchRelativeToShadowRootHostComplexUiElementAsClassTestPageService>();
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_SimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_5282.SimpleUiElementAsInterface_5282.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_5282_SimpleUiElementAsInterface_5282);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> Complex_UiElement_AsClass")]
        public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_ComplexUiElementAsClass()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_8a39.ComplexUiElementAsClass_8a39.GetTextContent();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_8a39_ComplexUiElementAsClass_8a39);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_ShadowRootHostComplexUiElementAsClass_UiElementsList_Of_SimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_3134_UiElementsList_4d30_SimpleUiElementAsInterface_4d30_Indx1_TextContent,
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_3134_UiElementsList_4d30_SimpleUiElementAsInterface_4d30_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_3134.UiElementsListOfSimpleUiElementsAsInterface_4d30.Select(x => x.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass")]
        public void Should_Support_SearchPage_ShadowRootHostComplexUiElementAsClass_UiElementsList_Of_ComplexUiElementsAsClass()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_8c40_UiElementsList_43de_ComplexUiElementAsClass_43de_Indx1_TextContent,
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_8c40_UiElementsList_43de_ComplexUiElementAsClass_43de_Indx2_TextContent,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_8c40.UiElementsListOfComplexUiElementsAsClass_43de.Select(x => x.GetTextContent()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_66c4.FrameSimpleUiElementAsInterface_66c4.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_66c4_FrameSimpleUiElementAsInterface_66c4_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_ShadowRootHostComplexUiElementAsClass_UiElementsList_Of_FrameSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_f789_UiElementsList_f789_FrameSimpleUiElementAsInterface_f789_Indx1_IdAttribute,
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_f789_UiElementsList_f789_FrameSimpleUiElementAsInterface_f789_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_f789.UiElementsListOfFrameSimpleUiElementsAsInterface_f789.Select(x => x.GetIdAttribute()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_FrameSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_8d68.FrameComplexUiElementAsClass_8d68.FrameSimpleUiElementAsInterface_8d68.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_8d68_FrameComplexUiElementAsClass_8d68_FrameSimpleUiElementAsInterface_8d68_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_7fbe.ShadowRootHostSimpleUiElementAsInterface_7fbe.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_7fbe_ShadowRootHostSimpleUiElementAsInterface_7fbe_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface")]
        public void Should_Support_SearchPage_ShadowRootHostComplexUiElementAsClass_UiElementsList_Of_ShadowRootHostSimpleUiElementsAsInterface()
        {
            //GIVEN
            var expectedValues = new List<string>
            {
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_ba6b_UiElementsList_ba6b_ShadowRootHostSimpleUiElementAsInterface_ba6b_Indx1_IdAttribute,
                ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_ba6b_UiElementsList_ba6b_ShadowRootHostSimpleUiElementAsInterface_ba6b_Indx2_IdAttribute,
            };

            //WHEN
            var actualValues = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_ba6b.UiElementsListOfShadowRootHostSimpleUiElementsAsInterface_ba6b.Select(x => x.GetIdAttribute()).ToList();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValues.Should().BeEquivalentTo(expectedValues);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }

        [Test]
        [Description("Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface")]
        public void Should_Support_Search_Page_ShadowRootHostComplexUiElementAsClass_ShadowRootHostComplexUiElementAsClass_ShadowRootHostSimpleUiElementAsInterface()
        {
            //WHEN
            var actualValue = _pageService._page.Page_ShadowRootHostComplexUiElementAsClass_4e79.ShadowRootHostComplexUiElementAsClass_bb3d.ShadowRootHostSimpleUiElementAsInterface_bb3d.GetIdAttribute();
            var actualTopLevelUiElementTextContent = _pageService._page.Page_TopLevelSimpleUiElementAsInterface.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualValue.Should().Be(ExpectedValues.Page_ShadowRootHostComplexUiElementAsClass_4e79_ShadowRootHostComplexUiElementAsClass_bb3d_ShadowRootHostSimpleUiElementAsInterface_bb3d_IdAttribute);
                actualTopLevelUiElementTextContent.Should().Be(ExpectedValues.Page_TopLevelSimpleUiElementAsInterface);
            }
        }
    }
}
