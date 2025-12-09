using ApplicationName.QA.TestsBasis.Ui.PageServices;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    internal class Tests_UiInteractor_Cookies : PerScenarioBaseTest
    {
        [ThreadStatic] private static IUiInteractorTab _tab1;
        [ThreadStatic] private static IUiInteractorTab _tab2;
        [ThreadStatic] private static TestPageTab1Service _service1;
        [ThreadStatic] private static TestPageTab2Service _service2;
        [ThreadStatic] private static IUiElement _tab1Button;
        [ThreadStatic] private static IUiElement _tab2Button;

        private List<IUiInteractorCookie> _expectedCookiesAfterSet = new List<IUiInteractorCookie>()
        {
            Const.CookieAWithUriConstructor,
            Const.CookieXWithDomainPathConstructor
        };

        [SetUp]
        public void SetUp()
        {
            _service1 = UiInteractor.Interact<TestPageTab1Service>(Const.Tab1);
            _service2 = UiInteractor.Interact<TestPageTab2Service>(Const.Tab2);

            _tab1 = UiInteractor.GetTab(Const.Tab1);
            _tab2 = UiInteractor.GetTab(Const.Tab2);

            _tab1Button = _service1._page.Tab1ButtonCheckCookie;
            _tab2Button = _service2._page.Tab2ButtonCheckCookie;
        }

        [Test]
        public void Should_UiInteractor_Set_CookieWithDomainPathConstructor_Using_Name_Value_Domain_Path()
        {
            var action = new Action(()
                => UiInteractor.SetCookie(Const.CookieAWithDomainPathConstructor.Name, Const.CookieAWithDomainPathConstructor.Value, Const.CookieAWithDomainPathConstructor.Domain, Const.CookieAWithDomainPathConstructor.Path));
            CheckSetCookie(action);
        }

        [Test]
        public void Should_UiInteractor_Set_CookieWithUriConstructor_Using_Name_Value_Domain_Path()
        {
            var action = new Action(()
             => UiInteractor.SetCookie(Const.CookieAWithUriConstructor.Name, Const.CookieAWithUriConstructor.Value, Const.CookieAWithUriConstructor.Domain, Const.CookieAWithUriConstructor.Path));
            CheckSetCookie(action);
        }

        [Test]
        public void Should_UiInteractor_Set_CookieWithDomainPathConstructor_Using_IUiInteractorCookie_With_All_Properties()
        {
            var action = new Action(() => UiInteractor.SetCookie(Const.CookieAWithDomainPathConstructor));
            CheckSetCookie(action);
        }

        [Test]
        public void Should_UiInteractor_Set_CookieWithUriConstructor_Using_IUiInteractorCookie_With_All_Properties()
        {
            var action = new Action(() => UiInteractor.SetCookie(Const.CookieAWithUriConstructor));
            CheckSetCookie(action);
        }

        private void CheckSetCookie(Action action)
        {
            //GIVEN
            var actualTab1ButtonText_OnPageLoad = _tab1Button.GetTextContent();
            var actualTab2ButtonText_OnPageLoad = _tab2Button.GetTextContent();

            _tab1Button.Click();
            var actualTab1ButtonText_BeforeCookieSet = _tab1Button.GetTextContent();
            _tab2Button.Click();
            var actualTab2ButtonText_BeforeCookieSet = _tab2Button.GetTextContent();

            //WHEN
            action.Invoke();

            _tab1Button.Click();
            var actualTab1ButtonText_AfterCookieSet = _tab1Button.GetTextContent();
            _tab2Button.Click();
            var actualTab2ButtonText_AfterCookieSet = _tab2Button.GetTextContent();

            //THEN
            AssertSetCookies(actualTab1ButtonText_OnPageLoad, actualTab2ButtonText_OnPageLoad, actualTab1ButtonText_BeforeCookieSet, actualTab2ButtonText_BeforeCookieSet, actualTab1ButtonText_AfterCookieSet, actualTab2ButtonText_AfterCookieSet);
        }

        private void AssertSetCookies(string actualTab1ButtonText_OnPageLoad,
            string actualTab2ButtonText_OnPageLoad,
            string actualTab1ButtonText_BeforeCookieSet,
            string actualTab2ButtonText_BeforeCookieSet,
            string actualTab1ButtonText_AfterCookieSet,
            string actualTab2ButtonText_AfterCookieSet)
        {
            using (new AssertionScope())
            {
                actualTab1ButtonText_OnPageLoad.Should().BeEquivalentTo(Const.Tab1ClickToCheckCookie);
                actualTab2ButtonText_OnPageLoad.Should().BeEquivalentTo(Const.Tab2ClickToCheckCookie);

                actualTab1ButtonText_BeforeCookieSet.Should().BeEquivalentTo(Const.Tab1CookieANotFound);
                actualTab2ButtonText_BeforeCookieSet.Should().BeEquivalentTo(Const.Tab2CookieANotFound);

                actualTab1ButtonText_AfterCookieSet.Should().BeEquivalentTo(Const.Tab1CookieAFound);
                actualTab2ButtonText_AfterCookieSet.Should().BeEquivalentTo(Const.Tab2CookieAFound);
            }
        }

        [Test]
        public void Should_UiInteractor_Set_Get_Clear_Several_Cookies()
        {
            //WHEN
            UiInteractor.SetCookies(Const.CookieAWithUriConstructor, Const.CookieXWithDomainPathConstructor);
            var actualCookiesAfterSet = UiInteractor.GetAllCookies();
            UiInteractor.ClearCookies(Const.CookieAWithUriConstructor.Name, Const.CookieXWithDomainPathConstructor.Name);
            var actualCookiesAfterClear = UiInteractor.GetAllCookies();

            //THEN
            using (new AssertionScope())
            {
                actualCookiesAfterSet.Should().BeEquivalentTo(_expectedCookiesAfterSet);
                actualCookiesAfterClear.Should().BeEmpty();
            }
        }

        [Test]
        public void Should_UiInteractor_Get_Cookie_By_Name()
        {
            //GIVEN
            UiInteractor.SetCookies(Const.CookieAWithUriConstructor);

            //WHEN
            var actualCookie = UiInteractor.GetCookie(Const.CookieAWithUriConstructor.Name);

            //THEN
            actualCookie.Should().BeEquivalentTo(Const.CookieAWithUriConstructor);
        }

        [Test]
        public void Should_UiInteractor_Get_And_Clear_Cookie_Previously_Set_Before_New_Tab_Is_Created()
        {
            //WHEN
            UiInteractor.SetCookies(Const.CookieBWithDomainPathConstructor);

            var tab3 = UiInteractor.GetTab(Const.Tab3);
            var service3 = UiInteractor.Interact<TestPageTab3Service>(Const.Tab3);

            var actualCookie = UiInteractor.GetCookie(Const.CookieBWithDomainPathConstructor.Name);
            var tab3Button = service3._page.Tab3ButtonCheckCookie;

            tab3Button.Click();
            var actualButtonTextAfterCookieSet = tab3Button.GetTextContent();

            UiInteractor.ClearCookies(Const.CookieBWithDomainPathConstructor.Name);
            tab3Button.Click();
            var actualButtonTextAfterCookieCleared = tab3Button.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualCookie.Should().BeEquivalentTo(Const.CookieBWithDomainPathConstructor);
                actualButtonTextAfterCookieSet.Should().BeEquivalentTo(Const.Tab3CookieBFound);
                actualButtonTextAfterCookieCleared.Should().BeEquivalentTo(Const.Tab3CookieBNotFound);
            }
        }

        [Test]
        public void Should_UiInteractor_Clear_All_Cookies()
        {
            //WHEN
            UiInteractor.SetCookies(Const.CookieAWithUriConstructor, Const.CookieXWithDomainPathConstructor);
            var actualCookiesAfterSet = UiInteractor.GetAllCookies();
            UiInteractor.ClearAllCookies();
            var actualCookiesAfterClear = UiInteractor.GetAllCookies();

            //THEN
            using (new AssertionScope())
            {
                actualCookiesAfterSet.Should().BeEquivalentTo(_expectedCookiesAfterSet);
                actualCookiesAfterClear.Should().BeEmpty();
            }
        }

        [Test]
        public void Should_UiInteractor_Clear_Single_Cookie_By_Name()
        {
            //GIVEN
            var _expectedCookiesAfterSet = new List<IUiInteractorCookie>()
            {
                Const.CookieAWithUriConstructor,
                Const.CookieXWithDomainPathConstructor
            };

            //WHEN
            UiInteractor.SetCookies(Const.CookieAWithUriConstructor, Const.CookieXWithDomainPathConstructor);
            var actualCookiesAfterSet = UiInteractor.GetAllCookies();
            UiInteractor.ClearCookies(Const.CookieAWithUriConstructor.Name);
            var actualCookiesAfterClear = UiInteractor.GetAllCookies();

            //THEN
            using (new AssertionScope())
            {
                actualCookiesAfterSet.Should().BeEquivalentTo(_expectedCookiesAfterSet);
                actualCookiesAfterClear.Should().BeEquivalentTo(new List<IUiInteractorCookie>() { Const.CookieXWithDomainPathConstructor });
            }
        }
    }
}
