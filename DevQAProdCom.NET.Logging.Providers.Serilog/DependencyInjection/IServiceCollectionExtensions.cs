using DevQAProdCom.NET.Logging.Providers.Serilog.Configurations;
using DevQAProdCom.NET.Logging.Providers.Serilog.Interfaces;
using DevQAProdCom.NET.Logging.Providers.Serilog.Mappers;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.Logging.Shared.OperativeClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.Logging.Providers.Serilog.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSerilogLoggingProviderFactory(this IServiceCollection serviceCollection, string? filePathWithJsonConfiguration = null)
        {
            serviceCollection.AddSingleton<ISerilogLoggingProviderMappers, SerilogLoggingProviderMappers>();
            serviceCollection.AddSingleton<SerilogLoggingProviderFactory>((provider) =>
            {
                IConfiguration? configuration = null;

                if (string.IsNullOrEmpty(filePathWithJsonConfiguration))
                    configuration = new ConfigurationBuilder().AddSerilogLoggingProviderConfiguration().Build();
                else
                    configuration = new ConfigurationBuilder().AddJsonFile(filePathWithJsonConfiguration).Build();

                ISerilogLoggingProviderMappers mappers = provider.GetRequiredService<ISerilogLoggingProviderMappers>();

                return new SerilogLoggingProviderFactory(configuration, mappers);
            });

            return serviceCollection;
        }

        public static IServiceCollection AddSerilogLoggingProviderFactorySet(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ILoggingProviderFactoriesSet>(provider =>
                {
                    ILoggingProviderFactoriesSet loggingProviderFactoriesSet = new LoggingProviderFactoriesSet();
                    ILoggingProviderFactory serilogLoggingProviderFactory = provider.GetRequiredService<SerilogLoggingProviderFactory>();

                    loggingProviderFactoriesSet.LoggingProviderFactories.TryAdd(typeof(SerilogLoggingProviderFactory).FullName!, serilogLoggingProviderFactory);

                    return loggingProviderFactoriesSet;
                });

            return serviceCollection;
        }

        public static IServiceCollection AddSerilogLogger(this IServiceCollection serviceCollection, string? filePathWithJsonConfiguration = null)
        {
            serviceCollection.AddSerilogLoggingProviderFactory(filePathWithJsonConfiguration);
            serviceCollection.AddSerilogLoggingProviderFactorySet();

            serviceCollection
                .AddSingleton<BaseLogger>()
                .AddSingleton<ILogger>(provider => provider.GetRequiredService<BaseLogger>());

            return serviceCollection;
        }
    }
}
