using DevQAProdCom.NET.DependencyInjection.Shared.Interfaces;

namespace DevQAProdCom.NET.DependencyInjection.Shared.OperativeClasses
{
    public abstract class BaseDiContainer : IDiContainer
    {
        protected readonly object _lock = new object();
        private bool _isConfigured = false;
        protected virtual string AssemblyName => this.GetType().Assembly.GetName().Name ?? "UnknownAssembly";

        public BaseDiContainer()
        {
            if (!_isConfigured)
                lock (_lock)
                {
                    if (!_isConfigured)
                    {
                        BaseConfigurationInsideConstructorSetUp();
                        _isConfigured = true;
                    }
                }
        }

        protected virtual void BaseConfigurationInsideConstructorSetUp()
        {
            ConfigureDefaultServices();
            ConfigureServices();
            ConfigureExternalDiContainers();
            InitializeRequiredServices();
        }

        protected virtual void ConfigureDefaultServices() { }
        protected virtual void ConfigureServices() { }
        protected virtual void InitializeRequiredServices() { }
        protected virtual void ConfigureExternalDiContainers() { }

        public abstract T GetRequiredService<T>() where T : notnull;
        public abstract T? TryGetService<T>();
    }
}
