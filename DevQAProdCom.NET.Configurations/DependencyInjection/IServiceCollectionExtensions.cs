using DevQAProdCom.NET.Configurations.Interfaces;
using DevQAProdCom.NET.Configurations.OperativeClasses;
using DevQAProdCom.NET.DependencyInjection.Microsoft.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.Configurations.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        //No optional is used, cause if class is registered, it is supposed that one is going to use it.
        //Or it should not be registered at all. Or if it is registered, but not exists, but not used eventually - then no resolve is going to happen and no errors.
        public static IServiceCollection AddConfiguration<TImplementation>(this IServiceCollection serviceCollection, string keyPath, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where TImplementation : class, new()
        {

            return AddConfiguration<TImplementation>(serviceCollection,
                () => new ServiceDescriptor(typeof(TImplementation), provider =>
                {
                    IConfigContainer configContainer = provider.GetRequiredService<IConfigContainer>();
                    TImplementation? configuration = configContainer.Get<TImplementation>(keyPath);
                    return configuration;
                },
                serviceLifetime));
        }

        public static IServiceCollection AddConfiguration<TInterface, TImplementation>(this IServiceCollection serviceCollection, string keyPath, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TInterface : class
            where TImplementation : class, TInterface, new()
        {
            return AddConfiguration<TInterface>(serviceCollection,
                () => new ServiceDescriptor(typeof(TInterface), provider => provider.GetRequiredService<IConfigContainer>().Get<TImplementation>(keyPath),
                serviceLifetime));
        }

        private static IServiceCollection AddConfiguration<TServiceType>(IServiceCollection serviceCollection, Func<ServiceDescriptor> func)
        {
            if (serviceCollection.ContainsService<TServiceType>())
                throw new Exception($"Configuration with type '{typeof(TServiceType).FullName}' has already been registered inside service collection.");

            if (!serviceCollection.ContainsService<IConfigContainer>())
                serviceCollection.AddSingleton<IConfigContainer, ConfigContainer>();

            ServiceDescriptor serviceDescriptor = func();
            serviceCollection.Add(serviceDescriptor);

            return serviceCollection;
        }
    }
}
