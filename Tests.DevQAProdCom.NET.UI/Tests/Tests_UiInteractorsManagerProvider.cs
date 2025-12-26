using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.DependencyInjection;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]

    internal class Tests_UiInteractorsManagersProvider
    {
        [Test]
        public void Should_Single_Instance_Of_UiInteractorsManagersProvider_Be_Returned_By_Dependency_Injection_After_Each_Call_Of_Get_Required_Service()
        {
            //WHEN
            var actualAgent1 = DiContainer.Instance.GetRequiredService<IUiInteractorsManagersProvider>();
            var actualAgent2 = DiContainer.Instance.GetRequiredService<IUiInteractorsManagersProvider>();

            //THEN
            actualAgent1.Id.Should().Be(actualAgent2.Id);
        }

        [Test]
        public void Should_UiInteractorsManagersProvider_Return_Different_IUiInteractorsManagers_For_Different_Threads()
        {
            //GIVEN
            IUiInteractorsManagersProvider UiInteractorsManagersProvider = DiContainer.Instance.GetRequiredService<IUiInteractorsManagersProvider>();

            //WHEN
            Guid? thread1UiInteractorsManagerId = null;
            Guid? thread2UiInteractorsManagerId = null;

            var thread1 = new Thread(() =>
            {
                thread1UiInteractorsManagerId = UiInteractorsManagersProvider.GetUiInteractorsManager(UiInteractorsManagerScope.Test).Id;
            });

            var thread2 = new Thread(() =>
            {
                thread2UiInteractorsManagerId = UiInteractorsManagersProvider.GetUiInteractorsManager(UiInteractorsManagerScope.Test).Id;
            });

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            //THEN
            using (new AssertionScope())
            {
                thread1.ManagedThreadId.Should().NotBe(thread2.ManagedThreadId);

                thread1UiInteractorsManagerId.Should().NotBeNull();
                thread2UiInteractorsManagerId.Should().NotBeNull();

                thread1UiInteractorsManagerId.Value.Should().NotBeEmpty();
                thread2UiInteractorsManagerId.Value.Should().NotBeEmpty();

                thread1UiInteractorsManagerId.Value.Should().NotBe(thread2UiInteractorsManagerId.Value);
            }
        }
    }
}
