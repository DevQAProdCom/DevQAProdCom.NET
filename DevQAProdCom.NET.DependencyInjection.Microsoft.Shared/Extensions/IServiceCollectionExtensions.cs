using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.DependencyInjection.Microsoft.Shared.Extensions
{
    public static class IServiceCollectionExtensions
    {
        //Initially was inside "DevQAProdCom.NET.DependencyInjection.Microsoft".
        //But eventually was moved to shared project because of circular dependency between DevQAProdCom.NET.Configurations and DevQAProdCom.NET.DependencyInjection.Microsoft projects
        public static bool ContainsService<TService>(this IServiceCollection serviceCollection)
        {
            return serviceCollection.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(TService));
        }
    }
}
