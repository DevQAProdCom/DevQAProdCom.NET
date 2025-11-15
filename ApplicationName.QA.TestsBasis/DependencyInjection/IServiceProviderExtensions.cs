namespace ApplicationName.QA.TestsBasis.DependencyInjection
{
    public static class IServiceProviderExtensions
    {
        public static void ConfigureApplicationNameQATestsBasisDiContainer(this IServiceProvider serviceProvider)
        {
            DiContainer.Instance.InitializeInstance(serviceProvider);
        }
    }
}
