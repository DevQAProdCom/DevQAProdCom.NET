using ApplicationName.QA.Global.Models;
using DevQAProdCom.NET.Configurations.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationName.QA.Global.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAppSettings(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddConfiguration<AppSettings>("appSettings");
        }
    }
}
