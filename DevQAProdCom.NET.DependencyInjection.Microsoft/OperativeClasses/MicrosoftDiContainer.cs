using DevQAProdCom.NET.DependencyInjection.Microsoft.Interfaces;
using DevQAProdCom.NET.DependencyInjection.Shared.OperativeClasses;
using Microsoft.Extensions.DependencyInjection;

namespace DevQAProdCom.NET.DependencyInjection.Microsoft.OperativeClasses
{
    public abstract class MicrosoftDiContainer : BaseDiContainer, IMicrosoftDiContainer
    {
        private IServiceCollection? _sc;
        protected IServiceCollection _serviceCollection
        {
            get
            {
                if (_sc == null)
                    lock (_lock)
                        if (_sc == null)
                            _sc = new ServiceCollection();
                return _sc;
            }
        }

        private IServiceProvider? _sp;
        protected IServiceProvider _serviceProvider
        {
            get
            {
                if (_sp == null)
                    lock (_lock)
                        if (_sp == null)
                        {
                            if (_serviceCollection == null)
                                throw new InvalidOperationException("ServiceCollection is not set");
                            _sp = _serviceCollection.BuildServiceProvider();
                        }

                return _sp;
            }
        }

        public void InitializeInstance(IServiceCollection serviceCollection)
        {
            lock (_lock)
            {
                _sc = serviceCollection;
                _sp = null;
            }
        }

        public void InitializeInstance(IServiceProvider serviceProvider)
        {
            lock (_lock)
            {
                _sc = null;
                _sp = serviceProvider;
            }
        }

        public override T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        public override T TryGetService<T>()
        {
            var service = _serviceProvider.GetService<T>();

            if (service == null)
                return default!;

            return service;
        }

        public void ConfigureService(Action<IServiceCollection> action)
        {
            lock (_lock)
            {
                action(_serviceCollection);
                _sp = _serviceCollection.BuildServiceProvider();
            }
        }
    }
}
