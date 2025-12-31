using System.Collections.Concurrent;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorsManager
{
    /// <summary>
    /// Provides single/singleton entry point of orchestrating UiInteractorsManagers and theirs UiInteractors for different tests/features.
    /// </summary>
    /// <param name="getUiInteractorManagerFunc"></param>
    /// <param name="log"></param>
    /// <param name="getCurrentFeatureIdentifierFunc">Can be set to null. However is not preferable.
    /// As far as, at the same time with ThreadId, it is better to use it as Name of UiInteractorsManager in composite key in Pool of UiInteractorManagers to strictly identify which of them belongs to particular test.  
    /// And is required parameter to avoid situations when UiInteractor of Feature scope is used. In such case on is created in OneTimeSetup and is disposed in OneTimeTearDown.
    /// However, as far as OneTimeTearDown may be executed in different thread, than OneTimeSetup, in parallel runs conflict situation may appear when another feature tries to access Pool and gets UiInteractorsManager by DefaultName and ThreadId 
    /// and then OneTimeTearDown of previous feature is executed by other thread, disposes UiInteractor and removes the record from the pool.</param>
    public class UiInteractorsManagersProvider(Func<IUiInteractorsManager> getUiInteractorManagerFunc, ILogger log, Func<string>? getCurrentFeatureIdentifierFunc) : IUiInteractorsManagersProvider
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

        private readonly ConcurrentDictionary<(string Name, int ThreadId), IUiInteractorsManager> _uiInteractorsManagers = new();

        public IUiInteractorsManager GetUiInteractorsManager(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance, int? threadId = null)
        {
            threadId = Thread.CurrentThread.ManagedThreadId;
            uiInteractorsManagerName = GetUiInteractorsManagerName(uiInteractorsManagerName);

            var key = (uiInteractorsManagerName, Thread.CurrentThread.ManagedThreadId);

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

            return GetUiInteractorsManager(threadId: threadId);
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

        public void DisposeUiInteractorsManagerOfCurrentThread()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var uiInteractorsManagers = _uiInteractorsManagers.Where(x => x.Key.ThreadId == threadId).ToList();

            foreach (var uiInteractorsManager in uiInteractorsManagers)
                DisposeUiInteractorsManager(uiInteractorsManager.Key.Name, uiInteractorsManager.Key.ThreadId);
        }

        public void DisposeUiInteractorsManager(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance, int? threadId = null)
        {
            threadId ??= Thread.CurrentThread.ManagedThreadId;
            uiInteractorsManagerName = GetUiInteractorsManagerName(uiInteractorsManagerName);

            if (_uiInteractorsManagers.TryRemove((uiInteractorsManagerName, threadId.Value), out var manager))
            {
                try
                {
                    manager.DisposeAllUiInteractors();
                }
                catch (Exception ex)
                {
                    log.Error($"Error disposing UiInteractorsManager for key {(uiInteractorsManagerName, threadId)}: {ex.Message}", ex);
                }
            }
        }

        public void DisposeUiInteractorsManagers(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance)
        {
            uiInteractorsManagerName = GetUiInteractorsManagerName(uiInteractorsManagerName);
            var keys = _uiInteractorsManagers.Keys.Where(k => k.Name == uiInteractorsManagerName).ToList();

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

        public IUiInteractor GetUiInteractor(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance, int? threadId = null)
        {
            return GetUiInteractorsManager(uiInteractorsManagerName: uiInteractorsManagerName, threadId: threadId).GetUiInteractor(uiInteractorName);
        }

        public IUiInteractor GetUiInteractorOfCurrentThread(string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance)
        {
            return GetUiInteractorsManagerOfCurrentThread().GetUiInteractor(uiInteractorName);
        }

        public void DisposeUiInteractor(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance, int? threadId = null)
        {
            var uiInteractorManagerEntry = GetUiInteractorManagerEntryOrDefault(uiInteractorsManagerName, threadId);

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

        public void DisposeAllUiInteractors(string? uiInteractorsManagerName = null)
        {
            var manager = GetUiInteractorsManager(uiInteractorsManagerName);
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

        private KeyValuePair<(string Name, int ThreadId), IUiInteractorsManager>? GetUiInteractorManagerEntryOrDefault(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance, int? threadId = null)
        {
            uiInteractorsManagerName = GetUiInteractorsManagerName(uiInteractorsManagerName);
            IEnumerable<KeyValuePair<(string Name, int ThreadId), IUiInteractorsManager>>? uiInteractorsManagers = null;

            if (threadId == null)
                uiInteractorsManagers = _uiInteractorsManagers.Where(x => x.Key.Name == uiInteractorsManagerName).ToList();
            else
                uiInteractorsManagers = _uiInteractorsManagers.Where(x => x.Key.Name == uiInteractorsManagerName && x.Key.ThreadId == threadId.Value).ToList();

            if (uiInteractorsManagers.Count() == 1)
                return uiInteractorsManagers.Single();
            else if (uiInteractorsManagers.Count() > 1)
                throw new Exception($"Expected single UiInteractorsManager Entry for '{uiInteractorsManagerName}' name." +
                    $" Actual found: '{uiInteractorsManagers.Count()}'. {string.Concat(uiInteractorsManagers.Select((x, i) => $"\n[{i}] {KeyToString(x.Key)}"))}");

            return null;
        }

        private string GetUiInteractorsManagerName(string uiInteractorsManagerName)
        {
            uiInteractorsManagerName ??= SharedUiConstants.DefaultUiInteractorsManagerInstance;

            if (uiInteractorsManagerName == SharedUiConstants.DefaultUiInteractorsManagerInstance && getCurrentFeatureIdentifierFunc != null)
                uiInteractorsManagerName = getCurrentFeatureIdentifierFunc();

            return uiInteractorsManagerName;
        }

        private string KeyToString((string Name, int ThreadId) key)
        {
            return $"UiInteractorsManager Entry: '{key.Name}' name, '{key.ThreadId}' threadId.";
        }

        #endregion AuxiliaryMethods
    }
}
