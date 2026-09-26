using System.Collections;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.Global.Utils;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Collections
{
    public class GitHubCopilotHooksCollection : IHooksCollection, IEnumerable<IHook>
    {
        public string CollectionIdentifier { get; }

        private readonly List<IHook> _hooks = new();
        private readonly ILogger _logger;
        private readonly IHookSearcher _hookSearcher;

        public GitHubCopilotHooksCollection(ILogger logger, string? collectionIdentifier = null, bool initializeFromDefaultLocations = false)
        {
            CollectionIdentifier = collectionIdentifier ?? Guid.NewGuid().ToString();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hookSearcher = new GitHubCopilotHookSearcher(new List<string>(), _logger);

            if (initializeFromDefaultLocations)
            {
                InitializeCollectionFromDefaultLocations();
            }
        }

        public GitHubCopilotHooksCollection(ILogger logger, IHookSearcher hookSearcher, string? collectionIdentifier = null, bool initializeFromDefaultLocations = false)
        {
            CollectionIdentifier = collectionIdentifier ?? Guid.NewGuid().ToString();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hookSearcher = hookSearcher ?? throw new ArgumentNullException(nameof(hookSearcher));

            if (initializeFromDefaultLocations)
            {
                InitializeCollectionFromDefaultLocations();
            }
        }

        public IHook AddHookData(IHook hook)
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

            _hooks.Add(hook);
            return hook;
        }

        public List<IHook> AddHookData(params IHook[] hooks)
        {
            return hooks.Select(AddHookData).ToList();
        }

        public List<IHook> AddHooksDataFromFile(string filePath)
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

        public List<IHook> AddHooksDataFromFiles(params string[] filesPaths)
        {
            var addedHooks = new List<IHook>();

            foreach (var filePath in filesPaths)
            {
                addedHooks.AddRange(AddHooksDataFromFile(filePath));
            }

            return addedHooks;
        }

        public List<IHook> AddHooksDataFromDirectory(string directoryPath)
        {
            IoUtils.CheckDirectoryMustExist(directoryPath);
            var hooks = _hookSearcher.Search(directoryPath);
            return AddHookData(hooks.ToArray());
        }

        public List<IHook> AddHooksDataFromDirectories(params string[] directoriesPaths)
        {
            var addedHooks = new List<IHook>();

            foreach (var directoryPath in directoriesPaths)
            {
                addedHooks.AddRange(AddHooksDataFromDirectory(directoryPath));
            }

            return addedHooks;
        }

        public IHook GetHookDataByIdentifier(string identifier)
        {
            if (TryGetHookDataByIdentifier(identifier, out var hook))
            {
                return hook!;
            }

            throw new InvalidOperationException($"[{CollectionIdentifier}] Hook with identifier '{identifier}' is not found in the collection.");
        }

        public bool TryGetHookDataByIdentifier(string identifier, out IHook? hook)
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

        public List<IHook> GetHooksDataByFileLocator(string fileLocator)
        {
            if (TryGetHooksDataByFileLocator(fileLocator, out var hooks))
            {
                return hooks;
            }

            throw new InvalidOperationException($"[{CollectionIdentifier}] No hooks found by file locator '{fileLocator}'.");
        }

        public bool TryGetHooksDataByFileLocator(string fileLocator, out List<IHook> hooks)
        {
            hooks = new List<IHook>();

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

        public List<IHook> GetHooksDataByEventTriggerName(string eventTriggerName)
        {
            if (TryGetHooksDataByEventTriggerName(eventTriggerName, out var hooks))
            {
                return hooks!;
            }

            throw new InvalidOperationException($"[{CollectionIdentifier}] No hooks found by event trigger name '{eventTriggerName}'.");
        }

        public bool TryGetHooksDataByEventTriggerName(string eventTriggerName, out List<IHook> hooks)
        {
            hooks = _hooks.Where(h => !string.IsNullOrEmpty(h.EventTriggerName) && h.EventTriggerName.Equals(eventTriggerName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (hooks.Count == 0)
            {
                hooks = new List<IHook>();
                return false;
            }

            return true;
        }

        public List<IHook> GetHooksDataByFileLocatorAndEventTriggerName(string fileLocator, string eventTriggerName)
        {
            if (TryGetHooksDataByFileLocatorAndEventTriggerName(fileLocator, eventTriggerName, out var hooks))
            {
                return hooks!;
            }

            throw new InvalidOperationException($"[{CollectionIdentifier}] No hooks found by file locator '{fileLocator}' and event trigger name '{eventTriggerName}'.");
        }

        public bool TryGetHooksDataByFileLocatorAndEventTriggerName(string fileLocator, string eventTriggerName, out List<IHook> hooks)
        {
            hooks = new();

            if (TryGetHooksDataByFileLocator(fileLocator, out var hooksByLocator))
                hooks = hooksByLocator.Where(h => !string.IsNullOrEmpty(h.EventTriggerName) && h.EventTriggerName.Equals(eventTriggerName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (hooks.Count == 0)
                return false;

            return true;
        }

        private void InitializeCollectionFromDefaultLocations()
        {
            var hooks = _hookSearcher.SearchInDefaultLocations();
            AddHookData(hooks.ToArray());
        }

        public IEnumerator<IHook> GetEnumerator()
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
