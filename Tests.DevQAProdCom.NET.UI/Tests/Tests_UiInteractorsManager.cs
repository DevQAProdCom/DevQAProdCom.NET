using ApplicationName.QA.TestsBasis.Ui.PageServices;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.Constants;
using Tests.DevQAProdCom.NET.UI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    internal class Tests_UiInteractorsManager
    {

        [Test]
        public void Should_New_UiInteractorsManager_Be_Returned_By_Dependency_Injection_After_Each_Call_Of_Get_Required_Service()
        {
            //WHEN
            var manager1 = DiContainer.Instance.GetRequiredService<IUiInteractorsManager>();
            var manager2 = DiContainer.Instance.GetRequiredService<IUiInteractorsManager>();

            //THEN
            manager1.Id.Should().NotBe(manager2.Id);
        }

        [Test]
        public void Should_UiInteractorsManager_Operate_With_Several_Interactors()
        {
            //GIVEN
            var manager1 = DiContainer.Instance.GetRequiredService<IUiInteractorsManager>();
            var interactor1_1 = manager1.GetUiInteractor(Const.Interactor1);
            var interactor1_2 = manager1.GetUiInteractor(Const.Interactor2);

            var manager2 = DiContainer.Instance.GetRequiredService<IUiInteractorsManager>();
            var interactor2_1 = manager1.GetUiInteractor(Const.Interactor1);
            var interactor2_2 = manager1.GetUiInteractor(Const.Interactor2);

            //THEN
            using (new AssertionScope())
            {
                manager1.Id.Should().NotBe(manager2.Id);
                interactor1_1.UiInteractorsManager.Id.Should().Be(interactor1_2.UiInteractorsManager.Id);
                interactor2_1.UiInteractorsManager.Id.Should().Be(interactor2_2.UiInteractorsManager.Id);
            }
        }

        [Test]
        public void Should_UiInteractorsManager_Create_Interactor()
        {
            //GIVEN
            var manager1 = DiContainer.Instance.GetRequiredService<IUiInteractorsManager>();
            var interactor1_1 = manager1.GetUiInteractor(Const.Interactor1);
            interactor1_1.Interact<TestPage2Service>();
            interactor1_1.Dispose();
        }
    }
}
