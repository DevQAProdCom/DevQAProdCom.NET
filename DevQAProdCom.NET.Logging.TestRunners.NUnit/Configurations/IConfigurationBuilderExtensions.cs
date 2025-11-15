using DevQAProdCom.NET.Logging.TestRunners.NUnit.Constants;
using Microsoft.Extensions.Configuration;

namespace DevQAProdCom.NET.Logging.TestRunners.NUnit.Configurations
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddNUnitTestLoggerConfiguration(this IConfigurationBuilder builder)
        {
            builder.AddJsonFile(Path.Combine(Const.Configurations.DefaultConfigurationsFolder, "appsettings.Default.NUnitTestLogger.json"));
            builder.AddJsonFile(Path.Combine(Const.Configurations.DefaultConfigurationsFolder, "appsettings.NUnitTestLogger.json"), optional: true);

            return builder;
        }
    }
}
