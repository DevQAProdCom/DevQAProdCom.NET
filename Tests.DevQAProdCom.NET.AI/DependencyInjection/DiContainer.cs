using DevQAProdCom.NET.DependencyInjection.Microsoft.OperativeClasses;
using DevQAProdCom.NET.Logging.Providers.Serilog.DependencyInjection;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace Tests.DevQAProdCom.NET.AI.DependencyInjection
{
    internal class DiContainer : MicrosoftDiContainerWithDefaultServices
    {
        private static readonly Lazy<DiContainer> _instance = new Lazy<DiContainer>(() => new DiContainer());
        public static DiContainer Instance => _instance.Value;

        public ILogger Log { get; set; }

        protected override void ConfigureServices()
        {
            AddLogger();
        }

        protected override void InitializeRequiredServices()
        {
            base.InitializeRequiredServices();
            Log = GetRequiredService<ILogger>();
        }

        private void AddLogger()
        {
            _serviceCollection.AddSerilogLogger();
        }
    }
}
