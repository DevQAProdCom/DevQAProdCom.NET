namespace DevQAProdCom.NET.Logging.Providers.Serilog.Constants;

internal static partial class Const
{
    internal static class Configurations
    {
        internal const string DefaultConfigurationsFolder = "Configurations";
        internal static string DefaultConfigurationsSharedFolder = Path.Combine(DefaultConfigurationsFolder, "Shared");
        internal const string SerilogConfigurationSection = "Serilog";
        internal const string AppsettingsSerilogLoggingProviderJsonFile = "appsettings.SerilogLoggingProvider.json";
    }
}

