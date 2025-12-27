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

        private readonly ConcurrentDictionary<(UiInteractorsManagerScope Scope, string Name, int ThreadId), IUiInteractorsManager> _uiInteractorsManagers = new();

        public IUiInteractorsManager GetUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, int? threadId = null)
        {
            uiInteractorsManagerName ??= GetUiInteractorsManagerName(uiInteractorsManagerScope);
            threadId = Thread.CurrentThread.ManagedThreadId;

            var key = (uiInteractorsManagerScope, uiInteractorsManagerName, Thread.CurrentThread.ManagedThreadId);

            // Use GetOrAdd to ensure atomicity and avoid race conditions
            var uiInteractorsManager = _uiInteractorsManagers.GetOrAdd(key, _ =>
            {
                var manager = getUiInteractorManagerFunc();
                if (manager == null)
                    throw new Exception($"Unable to create UiInteractorsManager instance for {uiInteractorsManagerName}.");
                return manager;
            });

            return uiInteractorsManager;
        }

        public IUiInteractorsManager GetUiInteractorsManagerOfCurrentThread()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var uiInteractorsManagers = _uiInteractorsManagers.Where(x => x.Key.ThreadId == threadId).ToList();

            if (uiInteractorsManagers.Count == 1)
                return uiInteractorsManagers.Single().Value;

            else if (uiInteractorsManagers.Count > 1)
                throw new Exception($"Expected single UiInteractorsManager for thread '{threadId}'" +
                   $" Actual found: '{uiInteractorsManagers.Count()}'. {string.Concat(uiInteractorsManagers.Select((x, i) => $"\n[{i}] {KeyToString(x.Key)}"))}");

            throw new Exception($"No UiInteractorsManager found for current thread (ID: {threadId}).");
        }

        public void DisposeAllUiInteractorsManagers()
        {
            foreach (var key in _uiInteractorsManagers.Keys.ToList())
            {
                if (_uiInteractorsManagers.TryRemove(key, out var manager))
                {
                    try
                    {
                        manager.DisposeAllUiInteractors();
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Error disposing UiInteractorsManager for key {key}: {ex.Message}", ex);
                    }
                }
            }
        }

        public void DisposeUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, int? threadId = null)
        {
            uiInteractorsManagerName ??= GetUiInteractorsManagerName(uiInteractorsManagerScope);
            threadId ??= Thread.CurrentThread.ManagedThreadId;

            if (_uiInteractorsManagers.TryRemove((uiInteractorsManagerScope, uiInteractorsManagerName, threadId.Value), out var manager))
            {
                try
                {
                    manager.DisposeAllUiInteractors();
                }
                catch (Exception ex)
                {
                    log.Error($"Error disposing UiInteractorsManager for key {(uiInteractorsManagerScope, uiInteractorsManagerName, threadId)}: {ex.Message}", ex);
                }
            }
        }

        public void DisposeUiInteractorsManagers(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null)
        {
            uiInteractorsManagerName ??= GetUiInteractorsManagerName(uiInteractorsManagerScope);
            var keys = _uiInteractorsManagers.Keys.Where(k => k.Scope == uiInteractorsManagerScope && k.Name == uiInteractorsManagerName).ToList();

            foreach (var key in keys)
            {
                if (_uiInteractorsManagers.TryRemove(key, out var manager))
                {
                    try
                    {
                        manager.DisposeAllUiInteractors();
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Error disposing UiInteractorsManager for key {key}: {ex.Message}", ex);
                    }
                }
            }
        }

        #endregion UiInteractorsManagers

        #region UiInteractors

        public IUiInteractor GetUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance, int? threadId = null)
        {
            return GetUiInteractorsManager(uiInteractorsManagerScope: uiInteractorsManagerScope, uiInteractorsManagerName: uiInteractorsManagerName, threadId: threadId).GetUiInteractor(uiInteractorName);
        }

        public IUiInteractor GetUiInteractorOfCurrentThread(string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance)
        {
            return GetUiInteractorsManagerOfCurrentThread().GetUiInteractor(uiInteractorName);
        }

        public void DisposeUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance, int? threadId = null)
        {
            uiInteractorsManagerName ??= GetUiInteractorsManagerName(uiInteractorsManagerScope);
            var uiInteractorManagerEntry = GetUiInteractorManagerEntryOrDefault(uiInteractorsManagerScope, uiInteractorsManagerName, threadId);

            if (uiInteractorManagerEntry != null)
                try
                {
                    uiInteractorManagerEntry.Value.Value.DisposeUiInteractor(uiInteractorName);
                }
                catch (Exception ex)
                {
                    log.Error($"Error disposing UiInteractor '{uiInteractorName}' in manager '{uiInteractorsManagerName}': {ex.Message}", ex);
                }
        }

        public void DisposeAllUiInteractors(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null)
        {
            var manager = GetUiInteractorsManager(uiInteractorsManagerScope, uiInteractorsManagerName);
            try
            {
                manager.DisposeAllUiInteractors();
            }
            catch (Exception ex)
            {
                log.Error($"Error disposing all UiInteractors in manager '{uiInteractorsManagerName}': {ex.Message}", ex);
            }
        }

        #endregion UiInteractors

        #region AuxiliaryMethods

        private string GetUiInteractorsManagerName(UiInteractorsManagerScope uiInteractorsManagerScope)
        {
            string? uiInteractorsManagerName = null;

            try
            {
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
            }
            catch (Exception ex)
            {
                log.Error($"Error getting UiInteractorsManagerName for scope '{uiInteractorsManagerScope}': {ex.Message}", ex);
                throw;
            }

            return uiInteractorsManagerName;
        }

        private KeyValuePair<(UiInteractorsManagerScope Scope, string Name, int ThreadId), IUiInteractorsManager>? GetUiInteractorManagerEntryOrDefault(UiInteractorsManagerScope uiInteractorsManagerScope, string uiInteractorsManagerName, int? threadId = null)
        {
            uiInteractorsManagerName ??= GetUiInteractorsManagerName(uiInteractorsManagerScope);

            IEnumerable<KeyValuePair<(UiInteractorsManagerScope Scope, string Name, int ThreadId), IUiInteractorsManager>>? uiInteractorsManagers = null;

            if (threadId == null)
                uiInteractorsManagers = _uiInteractorsManagers.Where(x => x.Key.Scope == uiInteractorsManagerScope && x.Key.Name == uiInteractorsManagerName).ToList();
            else
                uiInteractorsManagers = _uiInteractorsManagers.Where(x => x.Key.Scope == uiInteractorsManagerScope && x.Key.Name == uiInteractorsManagerName && x.Key.ThreadId == threadId.Value).ToList();

            if (uiInteractorsManagers.Count() == 1)
                return uiInteractorsManagers.Single();
            else if (uiInteractorsManagers.Count() > 1)
                throw new Exception($"Expected single UiInteractorsManager Entry for '{uiInteractorsManagerScope}' scope and '{uiInteractorsManagerName}' name." +
                    $" Actual found: '{uiInteractorsManagers.Count()}'. {string.Concat(uiInteractorsManagers.Select((x, i) => $"\n[{i}] {KeyToString(x.Key)}"))}");

            return null;
        }

        private string KeyToString((UiInteractorsManagerScope Scope, string Name, int ThreadId) key)
        {
            return $"UiInteractorsManager Entry: '{key.Scope}' scope, '{key.Name}' name, '{key.ThreadId}' threadId.";
        }

        #endregion AuxiliaryMethods
    }
}


/*

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

        private readonly ConcurrentDictionary<(UiInteractorsManagerScope Scope, string Name, int ThreadId), IUiInteractorsManager> _uiInteractorsManagers = new();

        public IUiInteractorsManager GetUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null)
        {
            uiInteractorsManagerName ??= GetUiInteractorsManagerName(uiInteractorsManagerScope);
            var key = (uiInteractorsManagerScope, uiInteractorsManagerName, Thread.CurrentThread.ManagedThreadId);

            // Use GetOrAdd to ensure atomicity and avoid race conditions
            var uiInteractorsManager = _uiInteractorsManagers.GetOrAdd(key, _ =>
            {
                var manager = getUiInteractorManagerFunc();
                if (manager == null)
                    throw new Exception($"Unable to create UiInteractorsManager instance for {uiInteractorsManagerName}.");
                return manager;
            });

            return uiInteractorsManager;
        }

        public IUiInteractorsManager GetUiInteractorsManagerOfCurrentThread()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            foreach (var uiInteractorManagerEntry in _uiInteractorsManagers)
            {
                if (uiInteractorManagerEntry.Key.ThreadId == threadId)
                    return uiInteractorManagerEntry.Value;
            }

            throw new Exception($"No UiInteractorsManager found for current thread (ID: {threadId}).");
        }

        public IUiInteractorsManager GetUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, int threadId, string? uiInteractorsManagerName = null)
        {
            uiInteractorsManagerName ??= GetUiInteractorsManagerName(uiInteractorsManagerScope);
            var key = (uiInteractorsManagerScope, uiInteractorsManagerName, threadId);
            if (_uiInteractorsManagers.TryGetValue(key, out var manager))
                return manager;
            return GetUiInteractorsManager(uiInteractorsManagerScope, uiInteractorsManagerName);
        }

        public void DisposeAllUiInteractorsManagers()
        {
            foreach (var key in _uiInteractorsManagers.Keys.ToList())
            {
                if (_uiInteractorsManagers.TryRemove(key, out var manager))
                {
                    try
                    {
                        manager.DisposeAllUiInteractors();
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Error disposing UiInteractorsManager for key {key}: {ex.Message}", ex);
                    }
                }
            }
        }

        public void DisposeUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null)
        {
            uiInteractorsManagerName ??= GetUiInteractorsManagerName(uiInteractorsManagerScope);
            var keys = _uiInteractorsManagers.Keys
                .Where(k => k.Scope == uiInteractorsManagerScope && k.Name == uiInteractorsManagerName)
                .ToList();

            foreach (var key in keys)
            {
                if (_uiInteractorsManagers.TryRemove(key, out var manager))
                {
                    try
                    {
                        manager.DisposeAllUiInteractors();
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Error disposing UiInteractorsManager for key {key}: {ex.Message}", ex);
                    }
                }
            }
        }

        #endregion UiInteractorsManagers

        #region UiInteractors

        public IUiInteractor GetUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance)
        {
            return GetUiInteractorsManager(uiInteractorsManagerScope, uiInteractorsManagerName).GetUiInteractor(uiInteractorName);
        }

        public IUiInteractor GetUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, int threadId, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance)
        {
            return GetUiInteractorsManager(uiInteractorsManagerScope, threadId, uiInteractorsManagerName).GetUiInteractor(uiInteractorName);
        }

        public IUiInteractor GetUiInteractorOfCurrentThread(string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance)
        {
            return GetUiInteractorsManagerOfCurrentThread().GetUiInteractor(uiInteractorName);
        }

        public void DisposeUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance)
        {
            var manager = GetUiInteractorsManager(uiInteractorsManagerScope, uiInteractorsManagerName);
            try
            {
                manager.DisposeUiInteractor(uiInteractorName);
            }
            catch (Exception ex)
            {
                log.Error($"Error disposing UiInteractor '{uiInteractorName}' in manager '{uiInteractorsManagerName}': {ex.Message}", ex);
            }
        }

        public void DisposeAllUiInteractors(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null)
        {
            var manager = GetUiInteractorsManager(uiInteractorsManagerScope, uiInteractorsManagerName);
            try
            {
                manager.DisposeAllUiInteractors();
            }
            catch (Exception ex)
            {
                log.Error($"Error disposing all UiInteractors in manager '{uiInteractorsManagerName}': {ex.Message}", ex);
            }
        }

        #endregion UiInteractors

        #region AuxiliaryMethods

        private string GetUiInteractorsManagerName(UiInteractorsManagerScope uiInteractorsManagerScope)
        {
            string? uiInteractorsManagerName = null;

            try
            {
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
            }
            catch (Exception ex)
            {
                log.Error($"Error getting UiInteractorsManagerName for scope '{uiInteractorsManagerScope}': {ex.Message}", ex);
                throw;
            }

            return uiInteractorsManagerName;
        }

        private KeyValuePair<(UiInteractorsManagerScope Scope, string Name, int ThreadId), IUiInteractorsManager>? GetUiInteractorManagerEntry(UiInteractorsManagerScope uiInteractorsManagerScope, string uiInteractorsManagerName)
        {
            foreach (var uiInteractorManagerEntry in _uiInteractorsManagers)
                if (uiInteractorManagerEntry.Key.Scope == uiInteractorsManagerScope && uiInteractorManagerEntry.Key.Name == uiInteractorsManagerName)
                    return uiInteractorManagerEntry;

            return null;
        }

        #endregion AuxiliaryMethods
    }
}


*/


/*
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

        private ConcurrentDictionary<(UiInteractorsManagerScope Scope, string Name, int ThreadId), IUiInteractorsManager> _uiInteractorsManagers = new();

        public IUiInteractorsManager GetUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null)
        {
            uiInteractorsManagerName ??= GetUiInteractorsManagerName(uiInteractorsManagerScope);
            IUiInteractorsManager? uiInteractorsManager = GetUiInteractorManagerEntry(uiInteractorsManagerScope, uiInteractorsManagerName)?.Value;

            if (uiInteractorsManager == null)
            {
                uiInteractorsManager = getUiInteractorManagerFunc();
                _uiInteractorsManagers.TryAdd((uiInteractorsManagerScope, uiInteractorsManagerName, Thread.CurrentThread.ManagedThreadId), uiInteractorsManager);
            }

            if (uiInteractorsManager == null)
                throw new Exception($"Unable to get UiInteractorsManager instance for {uiInteractorsManagerName}.");

            return uiInteractorsManager;
        }

        public IUiInteractorsManager GetUiInteractorsManagerOfCurrentThread()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            foreach (var uiInteractorManagerEntry in _uiInteractorsManagers)
                if (uiInteractorManagerEntry.Key.ThreadId == threadId)
                    return uiInteractorManagerEntry.Value;

            throw new Exception($"No UiInteractorsManager found for current thread (ID: {threadId}).");
        }

        public IUiInteractorsManager GetUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, int threadId, string? uiInteractorsManagerName = null)
        {
            uiInteractorsManagerName ??= GetUiInteractorsManagerName(uiInteractorsManagerScope);

            var uiInteractorsManagerEntry = GetUiInteractorManagerEntry(uiInteractorsManagerScope, uiInteractorsManagerName, threadId);

            if (uiInteractorsManagerEntry != null)
                return uiInteractorsManagerEntry.Value.Value;
            else
                return GetUiInteractorsManager(uiInteractorsManagerScope: uiInteractorsManagerScope, uiInteractorsManagerName: uiInteractorsManagerName);
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
            KeyValuePair<(UiInteractorsManagerScope Scope, string Name, int ThreadId), IUiInteractorsManager>? uiInteractorsManagerEntry = GetUiInteractorManagerEntry(uiInteractorsManagerScope, uiInteractorsManagerName);

            if (uiInteractorsManagerEntry != null)
            {
                uiInteractorsManagerEntry.Value.Value.DisposeAllUiInteractors();
                _uiInteractorsManagers.TryRemove(uiInteractorsManagerEntry.Value.Key, out _);
            }
        }

        #endregion UiInteractorsManagers

        #region UiInteractors

        public IUiInteractor GetUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance)
        {
            return GetUiInteractorsManager(uiInteractorsManagerScope: uiInteractorsManagerScope, uiInteractorsManagerName: uiInteractorsManagerName).GetUiInteractor(uiInteractorName);
        }

        public IUiInteractor GetUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, int threadId, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance)
        {
            return GetUiInteractorsManager(uiInteractorsManagerScope: uiInteractorsManagerScope, threadId: threadId, uiInteractorsManagerName: uiInteractorsManagerName).GetUiInteractor(uiInteractorName);
        }

        public IUiInteractor GetUiInteractorOfCurrentThread(string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance)
        {
            return GetUiInteractorsManagerOfCurrentThread().GetUiInteractor(uiInteractorName);
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

        #region AuxiliaryMethods

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

        private KeyValuePair<(UiInteractorsManagerScope Scope, string Name, int ThreadId), IUiInteractorsManager>? GetUiInteractorManagerEntry
            (UiInteractorsManagerScope uiInteractorsManagerScope, string uiInteractorsManagerName)
        {
            foreach (var uiInteractorManagerEntry in _uiInteractorsManagers)
                if (uiInteractorManagerEntry.Key.Scope == uiInteractorsManagerScope && uiInteractorManagerEntry.Key.Name == uiInteractorsManagerName)
                    return uiInteractorManagerEntry;

            return null;
        }

        private KeyValuePair<(UiInteractorsManagerScope Scope, string Name, int ThreadId), IUiInteractorsManager>? GetUiInteractorManagerEntry(UiInteractorsManagerScope uiInteractorsManagerScope, string uiInteractorsManagerName, int threadId)
        {
            foreach (var uiInteractorManagerEntry in _uiInteractorsManagers)
                if (uiInteractorManagerEntry.Key.Scope == uiInteractorsManagerScope && uiInteractorManagerEntry.Key.Name == uiInteractorsManagerName && uiInteractorManagerEntry.Key.ThreadId == threadId)
                    return uiInteractorManagerEntry;

            return null;
        }

        #endregion AuxiliaryMethods
    }
}
*/
