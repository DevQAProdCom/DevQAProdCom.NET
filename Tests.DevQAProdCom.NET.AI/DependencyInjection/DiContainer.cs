using DevQAProdCom.NET.AI.MicrosoftAgentFramework.DependencyInjection;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.Interfaces;
using DevQAProdCom.NET.AI.MicrosoftAgentFramework.OperativeClasses;
using DevQAProdCom.NET.DependencyInjection.Microsoft.OperativeClasses;
using DevQAProdCom.NET.Logging.Providers.Serilog.DependencyInjection;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tests.DevQAProdCom.NET.AI.InfrastructureForTests.Factories;
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
            _serviceCollection.AddSingleton<AiAgentsLibrary>();
        }

        protected override void InitializeRequiredServices()
        {
            base.InitializeRequiredServices();
            Log = GetRequiredService<ILogger>();
        }

        private IMicrosoftAgentFrameworkAiAgentInteractorsFactory? _aiAgentsInteractorsFactory;
        public IMicrosoftAgentFrameworkAiAgentInteractorsFactory AiAgentsInteractorsFactory => _aiAgentsInteractorsFactory ??= GetRequiredService<IMicrosoftAgentFrameworkAiAgentInteractorsFactory>();

        private AiAgentsLibrary? _aiAgentsLibrary;
        public AiAgentsLibrary AiAgentsLibrary => _aiAgentsLibrary ??= GetRequiredService<AiAgentsLibrary>();
    }
}
