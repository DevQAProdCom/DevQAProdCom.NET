using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    internal class Tests_UiInteractor_Tabs : PerScenarioBaseTest
    {

        [Test]
        public void Should_UiInteractor_Operate_With_Several_Tabs()
        {
            //GIVEN
            var tab1 = UiInteractor.GetTab(Const.Tab1);
            var tab2 = UiInteractor.GetTab(Const.Tab2);

            var service1 = UiInteractor.Interact<TestPageTab1Service>(tabName: Const.Tab1);
            var service2 = UiInteractor.Interact<TestPageTab2Service>(tabName: Const.Tab2);

            //WHEN
            var actualTab1Button1Text = service1._page.Tab1Button1.GetTextContent();
            var actualTab2Button1Text = service2._page.Tab2Button1.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualTab1Button1Text.Should().BeEquivalentTo(Const.Tab1Button1);
                actualTab2Button1Text.Should().BeEquivalentTo(Const.Tab2Button1);
            }
        }

        [Test]
        public void Should_UiInteractor_Close_And_ReOpen_Several_Tabs()
        {
            //GIVEN
            var tab1 = UiInteractor.GetTab(Const.Tab1);
            var tab2 = UiInteractor.GetTab(Const.Tab2);

            var service1 = UiInteractor.Interact<TestPageTab1Service>(tabName: Const.Tab1);
            var service2 = UiInteractor.Interact<TestPageTab2Service>(tabName: Const.Tab2);

            //WHEN
            UiInteractor.CloseTab(Const.Tab1);
            UiInteractor.CloseTab(Const.Tab2);

            service1 = UiInteractor.Interact<TestPageTab1Service>(tabName: Const.Tab1);
            service2 = UiInteractor.Interact<TestPageTab2Service>(tabName: Const.Tab2);

            var actualTab1Button1Text = service1._page.Tab1Button1.GetTextContent();
            var actualTab2Button1Text = service2._page.Tab2Button1.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualTab1Button1Text.Should().BeEquivalentTo(Const.Tab1Button1);
                actualTab2Button1Text.Should().BeEquivalentTo(Const.Tab2Button1);
            }
        }
    }
}
