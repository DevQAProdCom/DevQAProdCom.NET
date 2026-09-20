using System.Collections;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.AI.Shared.Interfaces.Hooks;
using DevQAProdCom.NET.AI.Shared.Models;
using DevQAProdCom.NET.Global.Extensions;
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

        public GitHubCopilotHooksCollection(ILogger logger, string? collectionIdentifier = null, bool initializeFromDefaultLocations = true)
        {
            CollectionIdentifier = collectionIdentifier ?? Guid.NewGuid().ToString();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hookSearcher = new GitHubCopilotHookSearcher(new List<string>());

            if (initializeFromDefaultLocations)
            {
                InitializeCollectionFromDefaultLocations();
            }
        }

        public GitHubCopilotHooksCollection(ILogger logger, IHookSearcher hookSearcher, string? collectionIdentifier = null, bool initializeFromDefaultLocations = true)
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

            var addedIdentifier = hook.Identifier;
            var addedFilePath = NormalizeFilePath(hook.FilePath);

            if (string.IsNullOrEmpty(addedFilePath) && string.IsNullOrEmpty(addedIdentifier))
            {
                throw new InvalidOperationException($"[{CollectionIdentifier}] Hook must have either a valid identifier or a file path. The provided hook has neither.");
            }

            var existingHookWithTheSameIdentifierAndSameFilePath = _hooks.SingleOrDefault(existingHookInCollection =>
                existingHookInCollection.Identifier == addedIdentifier &&
                !string.IsNullOrEmpty(addedIdentifier) &&
                NormalizeFilePath(existingHookInCollection.FilePath) == addedFilePath);

            if (existingHookWithTheSameIdentifierAndSameFilePath != null)
            {
                _hooks.Remove(existingHookWithTheSameIdentifierAndSameFilePath);
                _hooks.Add(hook);
                _logger.Debug("[{CollectionIdentifier}] Hook with identifier '{Identifier}' and file path '{FilePath}' already exists in the collection. It has been replaced with the new one.", $"[{CollectionIdentifier}]", addedIdentifier ?? "null", addedFilePath ?? "null");
                return hook;
            }

            if (!string.IsNullOrEmpty(addedFilePath))
            {
                var existingHookWithSameFilePath = _hooks.SingleOrDefault(existingHookInCollection =>
                    NormalizeFilePath(existingHookInCollection.FilePath) == addedFilePath);

                if (existingHookWithSameFilePath != null)
                {
                    _hooks.Remove(existingHookWithSameFilePath);
                    _logger.Debug("[{CollectionIdentifier}] Hook with file path '{FilePath}' already exists in the collection. It has been replaced with the new one.", $"[{CollectionIdentifier}]", addedFilePath);
                }
            }

            if (!string.IsNullOrEmpty(addedIdentifier))
            {
                var existingHooksWithTheSameIdentifier = _hooks.Where(existingHookInCollection =>
                    existingHookInCollection.Identifier == addedIdentifier).ToList();

                if (existingHooksWithTheSameIdentifier.Count > 0)
                {
                    var existingFilePaths = existingHooksWithTheSameIdentifier
                        .Select(x => x.FilePath)
                        .Where(x => !string.IsNullOrEmpty(x))
                        .ToList();

                    if (existingFilePaths.Count > 0)
                    {
                        _logger.Warning(
                            "{CollectionIdentifier} Hook with identifier '{Identifier}' is already present in the collection under file path(s): {ExistingFilePaths}. Adding another hook with the same identifier from file path '{AddedFilePath}'.",
                            $"[{CollectionIdentifier}]", addedIdentifier, string.Join(", ", existingFilePaths), addedFilePath ?? "null");
                    }
                }
            }

            _hooks.Add(hook);
            return hook;
        }

        public List<IHook> AddHookData(params IHook[] hooks)
        {
            if (hooks == null || hooks.Length == 0)
            {
                throw new ArgumentException("At least one hook must be provided.", nameof(hooks));
            }

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
                var filePaths = matchingHooks
                    .Select(x => x.FilePath)
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToList();

                throw new InvalidOperationException($"[{CollectionIdentifier}] There are several hooks with the same identifier '{identifier}' under several file paths: {string.Join(", ", filePaths)}. " +
                    $"Please get the hook by file path instead of by identifier.");
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
                return hooks!;
            }

            throw new InvalidOperationException($"[{CollectionIdentifier}] No hooks found by file locator '{fileLocator}'.");
        }

        public bool TryGetHooksDataByFileLocator(string fileLocator, out List<IHook>? hooks)
        {
            hooks = new List<IHook>();

            hooks.AddRange(_hooks.Where(h =>
                !string.IsNullOrEmpty(h.FilePath) &&
                IoUtils.NormalizeFilePath(h.FilePath).Equals(IoUtils.NormalizeFilePath(fileLocator), StringComparison.OrdinalIgnoreCase)).ToList());

            if (hooks.Count == 0)
            {
                hooks.AddRange(_hooks.Where(h =>
                    !string.IsNullOrEmpty(h.FileName) &&
                    h.FileName.Equals(fileLocator, StringComparison.OrdinalIgnoreCase)).ToList());
            }

            if (hooks.Count == 0)
            {
                hooks.AddRange(_hooks.Where(h =>
                    !string.IsNullOrEmpty(h.NameFromFile) &&
                    h.NameFromFile.Equals(fileLocator, StringComparison.OrdinalIgnoreCase)).ToList());
            }

            if (hooks.Count == 0)
            {
                hooks = null;
                return false;
            }

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

        public bool TryGetHooksDataByEventTriggerName(string eventTriggerName, out List<IHook>? hooks)
        {
            hooks = _hooks.Where(h =>
                !string.IsNullOrEmpty(h.EventTriggerName) &&
                h.EventTriggerName.Equals(eventTriggerName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (hooks.Count == 0)
            {
                hooks = null;
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

        public bool TryGetHooksDataByFileLocatorAndEventTriggerName(string fileLocator, string eventTriggerName, out List<IHook>? hooks)
        {
            var hooksByLocator = new List<IHook>();

            hooksByLocator.AddRange(_hooks.Where(h =>
                !string.IsNullOrEmpty(h.FilePath) &&
                IoUtils.NormalizeFilePath(h.FilePath).Equals(IoUtils.NormalizeFilePath(fileLocator), StringComparison.OrdinalIgnoreCase)).ToList());

            if (hooksByLocator.Count == 0)
            {
                hooksByLocator.AddRange(_hooks.Where(h =>
                    !string.IsNullOrEmpty(h.FileName) &&
                    h.FileName.Equals(fileLocator, StringComparison.OrdinalIgnoreCase)).ToList());
            }

            if (hooksByLocator.Count == 0)
            {
                hooksByLocator.AddRange(_hooks.Where(h =>
                    !string.IsNullOrEmpty(h.NameFromFile) &&
                    h.NameFromFile.Equals(fileLocator, StringComparison.OrdinalIgnoreCase)).ToList());
            }

            hooks = hooksByLocator.Where(h =>
                !string.IsNullOrEmpty(h.EventTriggerName) &&
                h.EventTriggerName.Equals(eventTriggerName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (hooks.Count == 0)
            {
                hooks = null;
                return false;
            }

            return true;
        }

        private void InitializeCollectionFromDefaultLocations()
        {
            var hooks = _hookSearcher.SearchInDefaultLocations();
            AddHookData(hooks.ToArray());
        }

        private static string? NormalizeFilePath(string? filePath) => string.IsNullOrWhiteSpace(filePath) ? null : IoUtils.NormalizeFilePath(filePath);

        public IEnumerator<IHook> GetEnumerator()
        {
            return _hooks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
