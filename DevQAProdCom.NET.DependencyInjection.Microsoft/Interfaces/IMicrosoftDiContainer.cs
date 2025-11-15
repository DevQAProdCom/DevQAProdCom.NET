using DevQAProdCom.NET.DependencyInjection.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.DependencyInjection.Microsoft.Interfaces
{
    public interface IMicrosoftDiContainer : IDiContainer
    {
        public void ConfigureService(Action<IServiceCollection> action);
        public void InitializeInstance(IServiceCollection serviceCollection);
        public void InitializeInstance(IServiceProvider serviceProvider);
    }
}
