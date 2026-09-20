using System.Collections;

namespace DevQAProdCom.NET.AI.Shared.Interfaces.Hooks
{
    public interface IHooksCollection : IEnumerable<IHook>
    {
        string CollectionIdentifier { get; }

        IHook AddHookData(IHook hook);
        List<IHook> AddHookData(params IHook[] hooks);

        List<IHook> AddHooksDataFromFile(string filePath);
        List<IHook> AddHooksDataFromFiles(params string[] filesPaths);
        List<IHook> AddHooksDataFromDirectory(string directoryPath);
        List<IHook> AddHooksDataFromDirectories(params string[] directoriesPaths);

        IHook GetHookDataByIdentifier(string identifier);
        bool TryGetHookDataByIdentifier(string identifier, out IHook? hook);

        List<IHook> GetHooksDataByFileLocator(string fileLocator);
        bool TryGetHooksDataByFileLocator(string fileLocator, out List<IHook>? hooks);

        List<IHook> GetHooksDataByEventTriggerName(string eventTriggerName);
        bool TryGetHooksDataByEventTriggerName(string eventTriggerName, out List<IHook>? hooks);

        List<IHook> GetHooksDataByFileLocatorAndEventTriggerName(string fileLocator, string eventTriggerName);
        bool TryGetHooksDataByFileLocatorAndEventTriggerName(string fileLocator, string eventTriggerName, out List<IHook>? hooks);
    }
}
