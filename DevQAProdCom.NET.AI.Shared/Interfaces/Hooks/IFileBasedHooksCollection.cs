namespace DevQAProdCom.NET.AI.Shared.Interfaces.Hooks
{
    public interface IFileBasedHooksCollection : IIdentifierBasedEntitiesCollection<IFileBasedHook>, IAddFromLocationsWithMultipleEntriesPerFile<IFileBasedHook>
    {
        List<IFileBasedHook> GetByFileLocator(string fileLocator);
        bool TryGetByFileLocator(string fileLocator, out List<IFileBasedHook>? hooks);

        List<IFileBasedHook> GetByEventTriggerName(string eventTriggerName);
        bool TryGetByEventTriggerName(string eventTriggerName, out List<IFileBasedHook>? hooks);

        List<IFileBasedHook> GetByFileLocatorAndEventTriggerName(string fileLocator, string eventTriggerName);
        bool TryGetByFileLocatorAndEventTriggerName(string fileLocator, string eventTriggerName, out List<IFileBasedHook>? hooks);
    }
}
