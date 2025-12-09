using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.Global.OperativeClasses
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
