using System.Collections;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class SdkEntitiesCollection<T> : ISdkEntitiesCollection<T> where T : IHaveStringIdentifier
    {
        public string CollectionIdentifier { get; }

        protected List<T> Entities { get; set; } = new List<T>();

        protected ILogger Logger;

        public SdkEntitiesCollection(ILogger logger, string? collectionIdentifier = null)
        {
            Logger = logger;
            CollectionIdentifier = collectionIdentifier ?? Guid.NewGuid().ToString();
        }

        public virtual T GetByIdentifier(string identifier)
        {
            if (TryGetByIdentifierOrDefault(identifier, out var entity))
            {
                return entity!;
            }

            throw new KeyNotFoundException($"Entity with identifier '{identifier}' is not found in the collection.");
        }

        public virtual bool TryGetByIdentifierOrDefault(string identifier, out T? entity)
        {
            entity = Entities.SingleOrDefault(x => x.Identifier == identifier);
            return entity != null;
        }

        public virtual T Add(T entity)
        {
            Entities.Add(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
