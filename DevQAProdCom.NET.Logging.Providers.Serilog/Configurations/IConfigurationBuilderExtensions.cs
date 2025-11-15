using DevQAProdCom.NET.Logging.Providers.Serilog.Constants;
using Microsoft.Extensions.Configuration;

namespace DevQAProdCom.NET.Logging.Providers.Serilog.Configurations
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddSerilogLoggingProviderConfiguration(this IConfigurationBuilder builder)
        {
            builder.AddJsonFile(Path.Combine(Const.Configurations.DefaultConfigurationsFolder, "appsettings.Default.SerilogLoggingProvider.json"));
            builder.AddJsonFile(Path.Combine(Const.Configurations.DefaultConfigurationsFolder, Const.Configurations.AppsettingsSerilogLoggingProviderJsonFile), optional: true);
            builder.AddJsonFile(Path.Combine(Const.Configurations.DefaultConfigurationsSharedFolder, Const.Configurations.AppsettingsSerilogLoggingProviderJsonFile), optional: true);

            return builder;
        }
    }
}
