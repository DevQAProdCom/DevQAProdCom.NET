using ApplicationName.QA.TestsBasis.Ui.PagesActions;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    internal class Tests_UiElement_Behaviors_General : PerScenarioBaseTest
    {
        [ThreadStatic] private static Actions_TestPage_UiElementBehaviors_General _uiElementBehaviorsGeneralTestPageActions;

        [SetUp]
        public void SetUp()
        {
            _uiElementBehaviorsGeneralTestPageActions = UiInteractor.Interact<Actions_TestPage_UiElementBehaviors_General>();
        }

        [Test]
        public void Should_UiElementBehavior_RemoveAttributeJs()
        {
            //GIVEN            
            var expectedStyleAttributeValueBeforeRemoval = "border: 2px solid rgb(182, 255, 0);";

            //WHEN
            var actualStyleAttributeValueBeforeRemoval = _uiElementBehaviorsGeneralTestPageActions.Page.UiElementBehaviorRemoveAttributeJsStyleInput.GetAttribute(Const.style, isBooleanAttributeType: false);
            _uiElementBehaviorsGeneralTestPageActions.Page.UiElementBehaviorRemoveAttributeJsStyleInput.RemoveAttributeJs(Const.style);
            var actualStyleAttributeValueAfterRemoval = _uiElementBehaviorsGeneralTestPageActions.Page.UiElementBehaviorRemoveAttributeJsStyleInput.GetAttribute(Const.style, isBooleanAttributeType: false);

            //THEN
            using (new AssertionScope())
            {
                actualStyleAttributeValueBeforeRemoval.Should().Be(expectedStyleAttributeValueBeforeRemoval);
                actualStyleAttributeValueAfterRemoval.Should().BeNull();
            }
        }
    }
}
