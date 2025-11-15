using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search
{
    public abstract class BaseFindOptionSearchMethodsProvider<T> : IFindOptionSearchMethodsProvider<T> where T : IFindOptionSearchMethod
    {
        protected List<T> FindOptionSearchMethods { get; } = new();

        public IFindOptionSearchMethodsProvider<T> RegisterFindOptionSearchMethod(T searcher)
        {
            FindOptionSearchMethods.Add(searcher);
            return this;
        }

        public IFindOptionSearchMethodsProvider<T> RegisterFindOptionSearchMethod<TImplementation>() where TImplementation : class, T
        {
            var instance = Activator.CreateInstance<TImplementation>();
            FindOptionSearchMethods.Add(instance);
            return this;
        }

        public T GetFindOptionSearchMethod(string method)
        {
            return FindOptionSearchMethods.Single(x => x.Method == method);
        }

        public T GetFindOptionSearchMethod(IFindOption findOption)
        {
            return FindOptionSearchMethods.Single(x => x.Method == findOption.Method);
        }
    }
}
