namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search
{
    public interface IFindOptionSearchMethodsProvider<T> where T : IFindOptionSearchMethod
    {
        public IFindOptionSearchMethodsProvider<T> RegisterFindOptionSearchMethod(T searcher);
        public IFindOptionSearchMethodsProvider<T> RegisterFindOptionSearchMethod<TImplementation>() where TImplementation : class, T;
        public T GetFindOptionSearchMethod(string method);
        public T GetFindOptionSearchMethod(IFindOption findOption);
    }
}
