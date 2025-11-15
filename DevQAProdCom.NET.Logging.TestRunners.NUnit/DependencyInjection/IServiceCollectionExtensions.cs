using DevQAProdCom.NET.DependencyInjection.Microsoft.Shared.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.Logging.TestRunners.NUnit.Configurations;
using DevQAProdCom.NET.Logging.TestRunners.NUnit.Constants;
using DevQAProdCom.NET.Logging.TestRunners.NUnit.Models;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.Logging.TestRunners.NUnit.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddNUnitTestLogger(this IServiceCollection serviceCollection, NUnitTestLoggerConfigParameters? nUnitTestLoggerConfigParameters = null)
        {
            if (!serviceCollection.ContainsService<NUnitTestLoggerConfigParameters>())
            {
                if (nUnitTestLoggerConfigParameters != null)
                    serviceCollection.AddSingleton<NUnitTestLoggerConfigParameters>(nUnitTestLoggerConfigParameters);
                else
                    serviceCollection.AddSingleton<NUnitTestLoggerConfigParameters>(provider =>
                        {
                            IConfigurationBuilder? configurationBuilder = new ConfigurationBuilder().AddNUnitTestLoggerConfiguration(); //For use cases when other projects does not injected and use IConfigContainer
                            IConfigurationSection section = configurationBuilder.Build().GetRequiredSection(Const.Configurations.NUnitTestLoggerConfigurationSection);
                            return section.Get<NUnitTestLoggerConfigParameters>();
                        });
            }

            serviceCollection.AddSingleton<NUnitTestLogger>()
                .AddSingleton<ITestLogger>(provider => provider.GetRequiredService<NUnitTestLogger>())
                .AddSingleton<ILogger>(provider => provider.GetRequiredService<NUnitTestLogger>());

            return serviceCollection;
        }
    }
}
