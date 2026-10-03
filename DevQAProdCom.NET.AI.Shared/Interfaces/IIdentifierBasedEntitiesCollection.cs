using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IIdentifierBasedEntitiesCollection<T> : IEnumerable<T> where T : IHaveStringIdentifier
    {
        public string CollectionIdentifier { get; }
        public T GetByIdentifier(string identifier);
        public bool TryGetByIdentifier(string identifier, out T? entity);
        public T Add(T entity);
        public List<T> Add(params T[] entities);
    }
}
