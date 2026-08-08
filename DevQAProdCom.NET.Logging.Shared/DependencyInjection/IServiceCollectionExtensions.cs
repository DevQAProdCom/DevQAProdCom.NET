using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.Logging.Shared.OperativeClasses;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.Logging.Shared.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseLogger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<BaseLogger>()
                .AddSingleton<ILogger>(provider => provider.GetRequiredService<BaseLogger>());

            return serviceCollection;
        }
    }
}
