using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]

    internal class Tests_UiElement_Search_UseMethods : PerFeatureBaseTest
    {
        private TestPage2Actions _testPage2Actions;

        [OneTimeSetUp]
        public void SetUp()
        {
            _testPage2Actions = UiInteractor.Interact<TestPage2Actions>();
        }

        [Test]
        public void Should_Find_UiElement_Using_IdEquals()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseIdEquals.GetTextContent();

            //THEN
            actualText.Should().Be("Use.IdEquals");
        }

        [Test]
        public void Should_Find_UiElement_Using_IdContains()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseIdContains.GetTextContent();
            var actualIdAttribute = _testPage2Actions._page.UseIdContains.GetIdAttribute();

            //THEN
            using (new AssertionScope())
            {
                actualText.Should().Be("Use.IdContains");
                actualIdAttribute.Should().Be("use-id-contains-value");
            }
        }

        [Test]
        public void Should_Find_UiElement_Using_NameEquals()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseNameEquals.GetTextContent();

            //THEN
            actualText.Should().Be("Use.NameEquals");
        }

        [Test]
        public void Should_Find_UiElement_Using_NameContains()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseNameContains.GetTextContent();
            var actualNameAttribute = _testPage2Actions._page.UseNameContains.GetNameAttribute();

            //THEN
            using (new AssertionScope())
            {
                actualText.Should().Be("Use.NameContains");
                actualNameAttribute.Should().Be("use-name-contains-value");
            }
        }

        [Test]
        public void Should_Find_UiElement_Using_ClassNameEquals()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseClassNameEquals.GetTextContent();

            //THEN
            actualText.Should().Be("Use.ClassNameEquals");
        }

        [Test]
        public void Should_Find_UiElement_Using_ClassNameContains()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseClassNameContains.GetTextContent();
            var actualClassNameAttribute = _testPage2Actions._page.UseClassNameContains.GetClassAttribute();

            //THEN
            using (new AssertionScope())
            {
                actualText.Should().Be("Use.ClassNameContains");
                actualClassNameAttribute.Should().Be("use-class-name-contains-value");
            }
        }

        [Test]
        public void Should_Find_UiElement_Using_LinkTextEquals()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseLinkTextEquals.GetTextContent();

            //THEN
            actualText.Should().Be("Use.LinkTextEquals");
        }

        [Test]
        public void Should_Find_UiElement_Using_LinkTextContains()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseLinkTextContains.GetTextContent();

            //THEN
            actualText.Should().Be("Use.LinkTextContainsValue");
        }

        [Test]
        public void Should_Find_UiElement_Using_TagName()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseTagName.GetTextContent();

            //THEN
            actualText.Should().Be("Use.TagName");
        }

        [Test]
        public void Should_Find_UiElement_Using_CssSelector()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseCssSelector.GetTextContent();

            //THEN
            actualText.Should().Be("Use.CssSelector");
        }

        [Test]
        public void Should_Find_UiElement_Using_XPath()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseXPath.GetTextContent();

            //THEN
            actualText.Should().Be("Use.XPath");
        }

        [Test]
        public void Should_Find_UiElement_Using_Text_Equals()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseTextEquals.GetTextContent();

            //THEN
            actualText.Should().Be("Use.TextEquals");
        }

        [Test]
        public void Should_Find_UiElement_Using_Text_Contains()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseTextContains.GetTextContent();

            //THEN
            actualText.Should().Be("Use.TextContains - Value");
        }

        [Test]
        public void Should_Find_UiElement_Using_Label_Equals()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseLabelEquals.GetTextContent();

            //THEN
            actualText.Should().Be("Use.LabelEquals");
        }

        [Test]
        public void Should_Find_UiElement_Using_Label_Contains()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseLabelContains.GetTextContent();

            //THEN
            actualText.Should().Be("Use.LabelContains - Value");
        }


        [Test]
        public void Should_Find_UiElement_Using_Placeholder_Equals()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UsePlaceholderEquals.GetTextContent();

            //THEN
            actualText.Should().Be("Use.PlaceholderEquals");
        }

        [Test]
        public void Should_Find_UiElement_Using_Placeholder_Contains()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UsePlaceholderContains.GetTextContent();

            //THEN
            actualText.Should().Be("Use.PlaceholderContains");
        }

        [Test]
        public void Should_Find_UiElement_Using_AltText_Equals()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseAltTextEquals.GetTextContent();

            //THEN
            actualText.Should().Be("Use.AltTextEquals");
        }

        [Test]
        public void Should_Find_UiElement_Using_AltText_Contains()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseAltTextContains.GetTextContent();

            //THEN
            actualText.Should().Be("Use.AltTextContains");
        }

        [Test]
        public void Should_Find_UiElement_Using_Title_Equals()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseTitleEquals.GetTextContent();

            //THEN
            actualText.Should().Be("Use.TitleEquals");
        }

        [Test]
        public void Should_Find_UiElement_Using_Title_Contains()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseTitleContains.GetTextContent();

            //THEN
            actualText.Should().Be("Use.TitleContains");
        }

        [Test]
        public void Should_Find_UiElement_Using_DataTestId_Equals()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseDataTestIdEquals.GetTextContent();

            //THEN
            actualText.Should().Be("Use.DataTestIdEquals");
        }

        [Test]
        public void Should_Find_UiElement_Using_DataTestId_Contains()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseDataTestIdContains.GetTextContent();

            //THEN
            actualText.Should().Be("Use.DataTestIdContains");
        }

        [Test]
        public void Should_Find_UiElement_Using_Custom_Find_Option_Search_Method_Registered_From_Di_Using_Custom_Attribute()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseCustomFindOptionSearchMethodRegisteredFromDi.GetTextContent();

            //THEN
            actualText.Should().Be("Use.CustomFindOptionSearchMethodRegisteredFromDi");
        }

        [Test]
        public void Should_Find_UiElement_Using_Custom_Find_Option_Search_Method_Registered_From_Activator_Create_Instance_T()
        {
            //WHEN
            var actualText = _testPage2Actions._page.UseCustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceT.GetTextContent();

            //THEN
            actualText.Should().Be("Use.CustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceT");
        }
    }
}
