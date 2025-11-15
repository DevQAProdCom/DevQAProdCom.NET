using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors
{
    public class BehaviorProvider : IBehaviorProvider
    {
        private IServiceProvider _serviceProvider;
        public BehaviorProvider(IServiceCollection serviceCollection)
        {
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public Func<IBehaviorParameters, T> GetBehaviorApplierService<T>()
        {
            return _serviceProvider.GetService<Func<IBehaviorParameters, T>>();
        }
    }
}
