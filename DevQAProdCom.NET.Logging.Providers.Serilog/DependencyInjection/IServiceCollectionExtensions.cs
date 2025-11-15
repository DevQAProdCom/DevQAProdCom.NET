using DevQAProdCom.NET.Logging.Providers.Serilog.Configurations;
using DevQAProdCom.NET.Logging.Providers.Serilog.Interfaces;
using DevQAProdCom.NET.Logging.Providers.Serilog.Mappers;
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
    }
}
