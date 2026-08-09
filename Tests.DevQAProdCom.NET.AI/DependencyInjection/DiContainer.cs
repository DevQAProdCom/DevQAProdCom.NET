using DevQAProdCom.NET.AI.MicrosoftAgentFramework.DependencyInjection;
using DevQAProdCom.NET.DependencyInjection.Microsoft.OperativeClasses;
using DevQAProdCom.NET.Logging.Providers.Serilog.DependencyInjection;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Interfaces;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Services;

namespace Tests.DevQAProdCom.NET.AI.DependencyInjection
{
    internal class DiContainer : MicrosoftDiContainerWithDefaultServices
    {
        private static readonly Lazy<DiContainer> _instance = new Lazy<DiContainer>(() => new DiContainer());
        public static DiContainer Instance => _instance.Value;

        public ILogger Log { get; set; }

        protected override void ConfigureServices()
        {
            _serviceCollection.AddSerilogLogger();
            _serviceCollection.AddGitHubCopilotAiAgentInteractor();
            _serviceCollection.AddMicrosoftAgentFrameworkAiAgentInteractorsFactory();
            _serviceCollection.AddSingleton<IAiAgentsService, AiAgentsService>();
        }

        protected override void InitializeRequiredServices()
        {
            base.InitializeRequiredServices();
            Log = GetRequiredService<ILogger>();
        }
    }
}
