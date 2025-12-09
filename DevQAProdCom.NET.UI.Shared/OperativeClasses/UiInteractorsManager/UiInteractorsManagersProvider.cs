using System.Collections.Concurrent;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorsManager
{
    public class UiInteractorsManagersProvider(Func<IUiInteractorsManager> getUiInteractorManagerFunc, ILogger log,
        Func<string>? getCurrentTestIdentifierFunc = null, Func<string>? getCurrentFeatureIdentifierFunc = null) : IUiInteractorsManagersProvider
    {
        public Guid Id { get; } = Guid.NewGuid();

        private string? _name;
        public string? Name
        {
            get
            {
                if (_name == null)
                    _name = GetType().FullName;

                return _name;
            }
            set { _name = value; }
        }

        private ConcurrentDictionary<string, IUiInteractorsManager> _uiInteractorsManagers = new();
        public IUiInteractorsManager GetUiInteractorsManager(string? uiInteractorsManagerIdentifier = null)
        {
            uiInteractorsManagerIdentifier ??= GetUiInteractorsManagerIdentifier();
            IUiInteractorsManager? uiInteractorsManager = null;

            if (!_uiInteractorsManagers.TryGetValue(uiInteractorsManagerIdentifier, out uiInteractorsManager))
            {
                uiInteractorsManager = getUiInteractorManagerFunc();
                _uiInteractorsManagers.TryAdd(uiInteractorsManagerIdentifier, uiInteractorsManager);
            }

            if (uiInteractorsManager == null)
                throw new Exception($"Unable to get UiInteractorsManager instance for {uiInteractorsManagerIdentifier}.");

            return uiInteractorsManager;
        }

        private string GetUiInteractorsManagerIdentifier()
        {
            string? uiInteractorsManagerIdentifier = null;

            if (getCurrentTestIdentifierFunc != null)
                uiInteractorsManagerIdentifier = getCurrentTestIdentifierFunc();

            if (getCurrentFeatureIdentifierFunc != null)
                uiInteractorsManagerIdentifier = getCurrentFeatureIdentifierFunc();

            if (uiInteractorsManagerIdentifier == null)
                throw new Exception("Unable to get UiInteractorsManager identifier. Please provide at least one of the functions to get current test or feature identifier.");

            return uiInteractorsManagerIdentifier;
        }

        public IUiInteractor GetUiInteractor(string? uiInteractorsManagerIdentifier = null, string uiInteractorIdentifier = SharedUiConstants.DefaultUiInteractorInstance)
        {
            return GetUiInteractorsManager().GetUiInteractor(uiInteractorIdentifier);
        }

        public void DisposeUiInteractor(string? uiInteractorsManagerIdentifier = null, string uiInteractorIdentifier = SharedUiConstants.DefaultUiInteractorInstance)
        {
            IUiInteractorsManager uiInteractorsManager = GetUiInteractorsManager(uiInteractorsManagerIdentifier);
            uiInteractorsManager.DisposeUiInteractor(uiInteractorIdentifier);
        }

        public void DisposeAllUiInteractors(string? uiInteractorsManagerIdentifier = null)
        {
            IUiInteractorsManager uiInteractorsManager = GetUiInteractorsManager(uiInteractorsManagerIdentifier);
            uiInteractorsManager.DisposeAllUiInteractors();
        }

        public void DisposeUiInteractorsManager(string? uiInteractorsManagerIdentifier = null)
        {
            uiInteractorsManagerIdentifier ??= GetUiInteractorsManagerIdentifier();
            IUiInteractorsManager uiInteractorsManager = GetUiInteractorsManager(uiInteractorsManagerIdentifier);
            uiInteractorsManager.DisposeAllUiInteractors();
            _uiInteractorsManagers.TryRemove(uiInteractorsManagerIdentifier, out _);
        }

        public void DisposeAllUiInteractorsManagers()
        {
            foreach (var uiInteractorsManager in _uiInteractorsManagers)
            {
                uiInteractorsManager.Value.DisposeAllUiInteractors();
                _uiInteractorsManagers.TryRemove(uiInteractorsManager.Key, out _);
            }
        }
    }
}
