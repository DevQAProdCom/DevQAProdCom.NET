namespace DevQAProdCom.NET.AI.Shared.Interfaces.Hooks
{
    public interface IFileBasedHooksSearcher //IHookSearcher IHookIdentifier Locator Detector Localizer Discoverer Collector Finder Retriever Scanner Analyzer Inspector
    {
        public List<IFileBasedHook> Search(string path, bool userExtendedSearch = true);
    }
}
