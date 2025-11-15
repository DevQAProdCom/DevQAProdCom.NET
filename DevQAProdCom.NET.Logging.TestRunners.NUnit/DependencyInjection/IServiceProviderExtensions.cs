namespace DevQAProdCom.NET.Logging.TestRunners.NUnit.DependencyInjection
{
    public static class IServiceProviderExtensions
    {
        public static void ConfigureNUnitLoggingExtensionDiContainer(this IServiceProvider serviceProvider)
        {
            DiContainer.Instance.InitializeInstance(serviceProvider);
        }
    }
}
