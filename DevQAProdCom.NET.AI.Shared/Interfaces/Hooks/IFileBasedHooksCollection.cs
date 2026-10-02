namespace DevQAProdCom.NET.AI.Shared.Interfaces.Hooks
{
    public interface IFileBasedHooksCollection : IEnumerable<IFileBasedHook>
    {
        string CollectionIdentifier { get; }

        IFileBasedHook AddHookData(IFileBasedHook hook);
        List<IFileBasedHook> AddHookData(params IFileBasedHook[] hooks);

        List<IFileBasedHook> AddHooksDataFromFile(string filePath);
        List<IFileBasedHook> AddHooksDataFromFiles(params string[] filesPaths);
        List<IFileBasedHook> AddHooksDataFromDirectory(string directoryPath);
        List<IFileBasedHook> AddHooksDataFromDirectories(params string[] directoriesPaths);

        IFileBasedHook GetHookDataByIdentifier(string identifier);
        bool TryGetHookDataByIdentifier(string identifier, out IFileBasedHook? hook);

        List<IFileBasedHook> GetHooksDataByFileLocator(string fileLocator);
        bool TryGetHooksDataByFileLocator(string fileLocator, out List<IFileBasedHook>? hooks);

        List<IFileBasedHook> GetHooksDataByEventTriggerName(string eventTriggerName);
        bool TryGetHooksDataByEventTriggerName(string eventTriggerName, out List<IFileBasedHook>? hooks);

        List<IFileBasedHook> GetHooksDataByFileLocatorAndEventTriggerName(string fileLocator, string eventTriggerName);
        bool TryGetHooksDataByFileLocatorAndEventTriggerName(string fileLocator, string eventTriggerName, out List<IFileBasedHook>? hooks);
    }
}
