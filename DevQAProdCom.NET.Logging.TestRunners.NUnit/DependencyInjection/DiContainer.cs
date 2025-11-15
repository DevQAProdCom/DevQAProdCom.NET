using DevQAProdCom.NET.DependencyInjection.Microsoft.OperativeClasses;

namespace DevQAProdCom.NET.Logging.TestRunners.NUnit.DependencyInjection
{
    internal class DiContainer : MicrosoftDiContainer
    {
        private static readonly Lazy<DiContainer> _instance = new Lazy<DiContainer>(() => new DiContainer());
        public static DiContainer Instance => _instance.Value;
    }
}
