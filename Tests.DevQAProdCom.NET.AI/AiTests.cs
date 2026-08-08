using NUnit.Framework;

namespace Tests.DevQAProdCom.NET.AI
{
    public class AiTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            DependencyInjection.DiContainer.Instance.Log.Info("Test1 executed.");
            Console.WriteLine("Test1 executed.");
            //Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            Console.WriteLine("Test2 executed.");
            Assert.Fail();
        }
    }
}
