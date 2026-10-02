using System.Collections;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class HooksCollection : IFileBasedHooksCollection, IEnumerable<IFileBasedHook>
    {
        public string CollectionIdentifier { get; }

        private readonly List<IFileBasedHook> _hooks = new();
        private readonly ILogger _logger;
        private readonly IFileBasedHooksSearcher _hookSearcher;

        public HooksCollection(ILogger logger, IFileBasedHooksSearcher hookSearcher)
        {
            CollectionIdentifier = Guid.NewGuid().ToString();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hookSearcher = hookSearcher ?? throw new ArgumentNullException(nameof(hookSearcher));
        }

        public HooksCollection(ILogger logger, IFileBasedHooksSearcher hookSearcher, string collectionIdentifier) : this(logger, hookSearcher)
        {
            CollectionIdentifier = collectionIdentifier;
        }

        public HooksCollection(ILogger logger, IFileBasedHooksSearcher hookSearcher, ILocationsProvider? defaultLocationsProvider) : this(logger, hookSearcher)
        {
            InitializeCollectionFromDefaultLocations(defaultLocationsProvider);
        }

        public HooksCollection(ILogger logger, IFileBasedHooksSearcher hookSearcher, ILocationsProvider? defaultLocationsProvider, string collectionIdentifier) : this(logger, hookSearcher, defaultLocationsProvider)
        {
            CollectionIdentifier = collectionIdentifier;
        }

        public IFileBasedHook AddHookData(IFileBasedHook hook)
        {
            ArgumentNullException.ThrowIfNull(hook);

            var addedHookNormalizedFilePath = IoUtils.NormalizeFilePath(hook.FilePath);

            if (!string.IsNullOrEmpty(hook.Identifier))
            {
                var existingHookWithTheSameIdentifier = _hooks.SingleOrDefault(existingHookInCollection => existingHookInCollection.Identifier == hook.Identifier);

                if (existingHookWithTheSameIdentifier != null)
                {
                    throw new Exception($"[{CollectionIdentifier}] Hook from file with the same identifier '{hook.Identifier}' already exists in collection. " +
                        $"FilePath: '{existingHookWithTheSameIdentifier.FilePath ?? "no filepath - added via code"}'. FilePath : '{addedHookNormalizedFilePath ?? "no filepath - added via code"}'.");
                }
            }

            if (string.IsNullOrEmpty(hook.EventTriggerName))
                throw new Exception($"[{CollectionIdentifier}] Hook with identifier '{hook.Identifier}' and filepath '{GetFilePathOrDefault(hook.FilePath)}' has no '{nameof(hook.EventTriggerName)}' set.");

            if (string.IsNullOrEmpty(addedHookNormalizedFilePath) && string.IsNullOrEmpty(hook.Identifier))
                throw new InvalidOperationException($"[{CollectionIdentifier}] Hook without file path (added programatically) must have '{nameof(hook.Identifier)}' set.");

            if (!string.IsNullOrEmpty(addedHookNormalizedFilePath) && !string.IsNullOrEmpty(hook.EventTriggerName))
            {
                var existingHookWithTheSameFilePathAndEventTriggerName = _hooks.SingleOrDefault(existingHookInCollection =>
                    IoUtils.NormalizeFilePath(existingHookInCollection.FilePath) == addedHookNormalizedFilePath &&
                    existingHookInCollection.EventTriggerName == hook.EventTriggerName);

                if (existingHookWithTheSameFilePathAndEventTriggerName != null)
                {
                    throw new Exception($"[{CollectionIdentifier}] Hook from file with the same file path '{addedHookNormalizedFilePath}' and event trigger name '{hook.EventTriggerName}' already exists in collection. " +
                        $"FilePath: '{GetFilePathOrDefault(existingHookWithTheSameFilePathAndEventTriggerName.FilePath)}'.");
                }
            }

            if (string.IsNullOrEmpty(hook.ContentValue))
                throw new InvalidOperationException($"[{CollectionIdentifier}] Hook with identifier '{hook.Identifier}' and filepath '{GetFilePathOrDefault(hook.FilePath)}' has no '{nameof(hook.ContentValue)}' set.");

            _hooks.Add(hook);
            return hook;
        }

        public List<IFileBasedHook> AddHookData(params IFileBasedHook[] hooks)
        {
            return hooks.Select(AddHookData).ToList();
        }

        public List<IFileBasedHook> AddHooksDataFromFile(string filePath)
        {
            IoUtils.CheckFileMustExist(filePath);

            _logger.Info("{CollectionIdentifier} Adding hooks data from file: {FilePath}", $"[{CollectionIdentifier}]", filePath);
            var hooks = _hookSearcher.Search(filePath);

            foreach (var hook in hooks)
            {
                hook.FilePath = filePath;
                AddHookData(hook);
            }

            return hooks;
        }

        public List<IFileBasedHook> AddHooksDataFromFiles(params string[] filesPaths)
        {
            var addedHooks = new List<IFileBasedHook>();

            foreach (var filePath in filesPaths)
            {
                addedHooks.AddRange(AddHooksDataFromFile(filePath));
            }

            return addedHooks;
        }

        public List<IFileBasedHook> AddHooksDataFromDirectory(string directoryPath)
        {
            IoUtils.CheckDirectoryMustExist(directoryPath);
            var hooks = _hookSearcher.Search(directoryPath);
            return AddHookData(hooks.ToArray());
        }

        public List<IFileBasedHook> AddHooksDataFromDirectories(params string[] directoriesPaths)
        {
            var addedHooks = new List<IFileBasedHook>();

            foreach (var directoryPath in directoriesPaths)
            {
                addedHooks.AddRange(AddHooksDataFromDirectory(directoryPath));
            }

            return addedHooks;
        }

        public IFileBasedHook GetHookDataByIdentifier(string identifier)
        {
            if (TryGetHookDataByIdentifier(identifier, out var hook))
            {
                return hook!;
            }

            throw new InvalidOperationException($"[{CollectionIdentifier}] Hook with identifier '{identifier}' is not found in the collection.");
        }

        public bool TryGetHookDataByIdentifier(string identifier, out IFileBasedHook? hook)
        {
            var matchingHooks = _hooks.Where(x => x.Identifier == identifier).ToList();

            if (matchingHooks.Count > 1)
            {
                throw new InvalidOperationException($"[{CollectionIdentifier}] There are several hooks with the same identifier '{identifier}'. " +
                    $"File paths: {string.Join(", ", matchingHooks.Select(x => x.FilePath).Select(GetFilePathOrDefault))}. ");
            }

            if (matchingHooks.Count == 1)
            {
                hook = matchingHooks.First();
                return true;
            }

            hook = default;
            return false;
        }

        public List<IFileBasedHook> GetHooksDataByFileLocator(string fileLocator)
        {
            if (TryGetHooksDataByFileLocator(fileLocator, out var hooks))
            {
                return hooks;
            }

            throw new InvalidOperationException($"[{CollectionIdentifier}] No hooks found by file locator '{fileLocator}'.");
        }

        public bool TryGetHooksDataByFileLocator(string fileLocator, out List<IFileBasedHook> hooks)
        {
            hooks = new List<IFileBasedHook>();

            var hooksByFilePath = _hooks.Where(h => !string.IsNullOrEmpty(h.FilePath) && IoUtils.NormalizeFilePath(h.FilePath).Equals(IoUtils.NormalizeFilePath(fileLocator), StringComparison.OrdinalIgnoreCase)).ToList();
            if (hooks.Count == 0)
                hooks.AddRange(_hooks.Where(h => !string.IsNullOrEmpty(h.FileName) && h.FileName.Equals(fileLocator, StringComparison.OrdinalIgnoreCase)).ToList());

            var hooksByNameFromFile = _hooks.Where(h => !string.IsNullOrEmpty(h.NameFromFile) && h.NameFromFile.Equals(fileLocator, StringComparison.OrdinalIgnoreCase)).ToList();
            if (hooks.Count == 0)
                hooks.AddRange(hooksByNameFromFile);

            if (hooks.Count == 0)
                return false;

            return true;
        }

        public List<IFileBasedHook> GetHooksDataByEventTriggerName(string eventTriggerName)
        {
            if (TryGetHooksDataByEventTriggerName(eventTriggerName, out var hooks))
            {
                return hooks!;
            }

            throw new InvalidOperationException($"[{CollectionIdentifier}] No hooks found by event trigger name '{eventTriggerName}'.");
        }

        public bool TryGetHooksDataByEventTriggerName(string eventTriggerName, out List<IFileBasedHook> hooks)
        {
            hooks = _hooks.Where(h => !string.IsNullOrEmpty(h.EventTriggerName) && h.EventTriggerName.Equals(eventTriggerName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (hooks.Count == 0)
            {
                hooks = new List<IFileBasedHook>();
                return false;
            }

            return true;
        }

        public List<IFileBasedHook> GetHooksDataByFileLocatorAndEventTriggerName(string fileLocator, string eventTriggerName)
        {
            if (TryGetHooksDataByFileLocatorAndEventTriggerName(fileLocator, eventTriggerName, out var hooks))
            {
                return hooks!;
            }

            throw new InvalidOperationException($"[{CollectionIdentifier}] No hooks found by file locator '{fileLocator}' and event trigger name '{eventTriggerName}'.");
        }

        public bool TryGetHooksDataByFileLocatorAndEventTriggerName(string fileLocator, string eventTriggerName, out List<IFileBasedHook> hooks)
        {
            hooks = new();

            if (TryGetHooksDataByFileLocator(fileLocator, out var hooksByLocator))
                hooks = hooksByLocator.Where(h => !string.IsNullOrEmpty(h.EventTriggerName) && h.EventTriggerName.Equals(eventTriggerName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (hooks.Count == 0)
                return false;

            return true;
        }

        private void InitializeCollectionFromDefaultLocations(ILocationsProvider? defaultLocationsProvider = null)
        {
            if (defaultLocationsProvider != null)
            {
                foreach (var location in defaultLocationsProvider)
                {
                    var hooks = _hookSearcher.Search(location);
                    AddHookData(hooks.ToArray());
                }
            }
        }

        public IEnumerator<IFileBasedHook> GetEnumerator()
        {
            return _hooks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private string GetFilePathOrDefault(string? filePath)
        {
            return string.IsNullOrEmpty(filePath) ? "No filepath  (added via code or else)" : filePath;
        }
    }
}
