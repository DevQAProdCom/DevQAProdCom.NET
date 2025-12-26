using System.Collections.Concurrent;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
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


        #region UiInteractorsManagers

        private ConcurrentDictionary<(UiInteractorsManagerScope, string), IUiInteractorsManager> _uiInteractorsManagers = new();

        public IUiInteractorsManager GetUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null)
        {
            uiInteractorsManagerName ??= GetUiInteractorsManagerName(uiInteractorsManagerScope);
            IUiInteractorsManager? uiInteractorsManager = null;

            if (!_uiInteractorsManagers.TryGetValue((uiInteractorsManagerScope, uiInteractorsManagerName), out uiInteractorsManager))
            {
                uiInteractorsManager = getUiInteractorManagerFunc();
                _uiInteractorsManagers.TryAdd((uiInteractorsManagerScope, uiInteractorsManagerName), uiInteractorsManager);
            }

            if (uiInteractorsManager == null)
                throw new Exception($"Unable to get UiInteractorsManager instance for {uiInteractorsManagerName}.");

            return uiInteractorsManager;
        }

        public void DisposeAllUiInteractorsManagers()
        {
            foreach (var uiInteractorsManager in _uiInteractorsManagers)
            {
                uiInteractorsManager.Value.DisposeAllUiInteractors();
                _uiInteractorsManagers.TryRemove(uiInteractorsManager.Key, out _);
            }
        }

        public void DisposeUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null)
        {
            IUiInteractorsManager uiInteractorsManager = GetUiInteractorsManager(uiInteractorsManagerScope: uiInteractorsManagerScope, uiInteractorsManagerName);
            uiInteractorsManager.DisposeAllUiInteractors();
            _uiInteractorsManagers.TryRemove((uiInteractorsManagerScope, uiInteractorsManager.Name), out _);
        }

        #endregion UiInteractorsManagers

        #region UiInteractors

        public IUiInteractor GetUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance)
        {
            return GetUiInteractorsManager(uiInteractorsManagerName: uiInteractorsManagerName, uiInteractorsManagerScope: uiInteractorsManagerScope).GetUiInteractor(uiInteractorName);
        }

        public void DisposeUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance)
        {
            IUiInteractorsManager uiInteractorsManager = GetUiInteractorsManager(uiInteractorsManagerScope: uiInteractorsManagerScope, uiInteractorsManagerName: uiInteractorsManagerName);
            uiInteractorsManager.DisposeUiInteractor(uiInteractorName);
        }

        public void DisposeAllUiInteractors(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null)
        {
            IUiInteractorsManager uiInteractorsManager = GetUiInteractorsManager(uiInteractorsManagerScope: uiInteractorsManagerScope, uiInteractorsManagerName: uiInteractorsManagerName);
            uiInteractorsManager.DisposeAllUiInteractors();
        }

        #endregion UiInteractors

        private string GetUiInteractorsManagerName(UiInteractorsManagerScope uiInteractorsManagerScope)
        {
            string? uiInteractorsManagerName = null;

            if (uiInteractorsManagerScope == UiInteractorsManagerScope.Test)
            {
                if (getCurrentTestIdentifierFunc != null)
                    uiInteractorsManagerName = getCurrentTestIdentifierFunc();
                else
                    throw new Exception("Unable to get UiInteractorsManager identifier for Test scope. Please provide a function to get current test identifier.");
            }
            else if (uiInteractorsManagerScope == UiInteractorsManagerScope.Feature)
            {
                if (getCurrentFeatureIdentifierFunc != null)
                    uiInteractorsManagerName = getCurrentFeatureIdentifierFunc();
                else
                    throw new Exception("Unable to get UiInteractorsManager identifier for Feature scope. Please provide a function to get current feature identifier.");
            }

            if (uiInteractorsManagerName == null)
                throw new Exception("Unable to get UiInteractorsManager identifier. Please provide at least one of the functions to get current test or feature identifier.");

            return uiInteractorsManagerName;
        }
    }
}
