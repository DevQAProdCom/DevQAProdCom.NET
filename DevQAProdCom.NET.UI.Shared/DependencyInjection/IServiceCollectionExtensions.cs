using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorsManager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DevQAProdCom.NET.UI.Shared.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddUiInteractionBehavior<TInterface, TImplementation>(this IServiceCollection serviceCollection)
            where TImplementation : TInterface
            where TInterface : IBehavior
        {
            serviceCollection.TryAddTransient<Func<IBehaviorParameters, TInterface>>((provider) =>
            {
                return new Func<IBehaviorParameters, TInterface>(behaviorParameters => ActivatorUtilities.CreateInstance<TImplementation>(provider, behaviorParameters));
            });

            return serviceCollection;
        }

        public static IServiceCollection AddUiInteractorsManagerAsyncLocalInstance(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddSingleton<IUiInteractorsManagerAsyncLocalInstance, UiInteractorsManagerAsyncLocalInstance>(provider =>
               {
                   ILogger log = provider.GetRequiredService<ILogger>();
                   Func<IUiInteractorsManager> getUiInteractorsManagerFunc = () => provider.GetRequiredService<IUiInteractorsManager>();
                   return new UiInteractorsManagerAsyncLocalInstance(getUiInteractorsManagerFunc, log);
               });

            return serviceCollection;
        }
    }
}
