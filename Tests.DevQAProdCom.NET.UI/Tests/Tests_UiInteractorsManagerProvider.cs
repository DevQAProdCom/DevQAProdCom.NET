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

            /* May abort the whole test run instead of just failing the test, if no try/catch is used inside the threads.
            The issue is that if an unhandled exception occurs inside either thread1 or thread2, that exception is thrown on the thread pool thread, not on the main test thread. 
            By default, in .NET, an unhandled exception on a background thread will terminate the process (or at least abort the test run), not just fail the test. This is why your test run is aborted rather than just reporting a failed test.
            Why does this happen?
            •	Unhandled exceptions in threads: If a thread throws an exception and it is not caught, the exception is unhandled. 
            In .NET, unhandled exceptions on threads (other than the main thread) can terminate the process, especially in .NET Core and later (.NET 5/6/7/8).
            •	Test frameworks: Most test frameworks (like NUnit, xUnit, MSTest) expect exceptions to be thrown on the main test thread. If an exception is thrown on a background thread, the framework may not catch it, leading to process termination.
            */
            var thread1 = new Thread(() =>
            {
                try
                {
                    thread1UiInteractorsManagerId = UiInteractorsManagersProvider.GetUiInteractorsManager(uiInteractorsManagerScope: UiInteractorsManagerScope.Test, threadId: Thread.CurrentThread.ManagedThreadId).Id;
                }
                catch (Exception ex)
                {

                }
            });

            var thread2 = new Thread(() =>
            {
                try
                {
                    thread2UiInteractorsManagerId = UiInteractorsManagersProvider.GetUiInteractorsManager(uiInteractorsManagerScope: UiInteractorsManagerScope.Test, threadId: Thread.CurrentThread.ManagedThreadId).Id;
                }
                catch (Exception ex)
                {

                }
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
