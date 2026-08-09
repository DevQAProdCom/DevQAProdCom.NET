using FluentAssertions;
using NUnit.Framework;
using Tests.DevQAProdCom.NET.AI.Constants;

namespace Tests.DevQAProdCom.NET.AI
{
    internal class AiAgentsTests: BaseTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public Task ReadWriteAgentTest()
        {
            await var agent = AiAgentsInteractorsFactory.GetGitHubCopilotAiAgentInteractor()
                .WithAgent(Const.AiAgents.Names.READ_WRITE_AGENT)
                .WithIsolation();

            

            Console.WriteLine("Test1 executed.");
            true.Should().BeTrue();
        }



        [Test]
        public void Test1()
        {
            DependencyInjection.DiContainer.Instance.Log.Info("Test1 executed.");
            Console.WriteLine("Test1 executed.");
            true.Should().BeTrue();
        }

        [Test]
        public void Test2()
        {
            Console.WriteLine("Test2 executed.");
            true.Should().BeFalse();
        }
    }
}
