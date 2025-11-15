using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorsManager
{
    public abstract class BaseUiInteractorsManager : IUiInteractorsManager
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }

        private readonly Dictionary<string, IUiInteractor> _instances = new();

        protected ILogger _log;

        public BaseUiInteractorsManager(ILogger log, string? name = null)
        {
            _log = log;
            Name = name ?? Id.ToString();
        }

        public IUiInteractor GetUiInteractor(string name = SharedUiConstants.DefaultUiInteractorInstance)
        {
            if (_instances.ContainsKey(name))
                return _instances[name];
            else
                return CreateInteractor(name);
        }

        public void DisposeUiInteractor(string identifier = SharedUiConstants.DefaultUiInteractorInstance)
        {
            if (_instances.ContainsKey(identifier))
            {
                _instances[identifier].DisposeInteractor();
                _instances.Remove(identifier);
            }
            else
                throw new KeyNotFoundException($"Interactor with identifier '{identifier}' was not found.");
        }

        public void DisposeUiInteractors()
        {
            foreach (var instance in _instances.Values)
            {
                try
                {
                    instance.DisposeInteractor();
                }
                catch (Exception ex)
                {
                }

                _instances.Clear();
            }
        }

        protected abstract IUiInteractor CreateTechnologySpecificInteractor();

        private IUiInteractor CreateInteractor(string? name = null)
        {
            if (string.IsNullOrEmpty(name))
                name = Guid.NewGuid().ToString();

            if (_instances.Keys.Contains(name))
                throw new Exception($"UI Interactor with identifier '{name}' already exists within UI Interactors Manager.");

            var instance = CreateTechnologySpecificInteractor();
            instance.Name = name;

            if (!_instances.TryAdd(name, instance))
                throw new Exception("Identifier exists");


            return instance;
        }
    }
}
