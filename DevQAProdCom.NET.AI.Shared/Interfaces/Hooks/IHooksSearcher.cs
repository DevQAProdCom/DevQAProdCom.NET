namespace DevQAProdCom.NET.AI.Shared.Interfaces.Hooks
{
    public interface IHooksSearcher //IHookSearcher IHookIdentifier Locator Detector Localizer Discoverer Collector Finder Retriever Scanner Analyzer Inspector
    {
        public List<IHook> Search(string path, bool userExtendedSearch = true);
    }
}
