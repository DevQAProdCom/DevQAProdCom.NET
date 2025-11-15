using DevQAProdCom.NET.Configurations.Interfaces;
using DevQAProdCom.NET.Configurations.OperativeClasses;
using DevQAProdCom.NET.DependencyInjection.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.DependencyInjection.Microsoft.OperativeClasses
{
    public class MicrosoftDiContainerWithDefaultServices : MicrosoftDiContainer
    {
        public IConfigContainer ConfigContainer { get; private set; }

        protected override void ConfigureDefaultServices()
        {
            _serviceCollection.AddSingleton<IConfigContainer, ConfigContainer>();

            //Configure general injection of current DI container to any required in classes
            _serviceCollection.AddSingleton<IDiContainer>(this);
        }

        protected override void InitializeRequiredServices()
        {
            ConfigContainer = GetRequiredService<IConfigContainer>();
        }
    }
}
