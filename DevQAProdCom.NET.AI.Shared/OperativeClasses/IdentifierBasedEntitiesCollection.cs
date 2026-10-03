using System.Collections;
using DevQAProdCom.NET.AI.Shared.Interfaces;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class IdentifierBasedEntitiesCollection<T> : IIdentifierBasedEntitiesCollection<T> where T : IHaveStringIdentifier
    {
        public string CollectionIdentifier { get; }

        protected List<T> Entities { get; set; } = new List<T>();

        protected ILogger Logger;

        public IdentifierBasedEntitiesCollection(ILogger logger, string? collectionIdentifier = null)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            CollectionIdentifier = collectionIdentifier ?? Guid.NewGuid().ToString();
        }

        public virtual T GetByIdentifier(string identifier)
        {
            if (TryGetByIdentifier(identifier, out var entity))
            {
                return entity!;
            }

            throw new KeyNotFoundException($"Entity with identifier '{identifier}' is not found in the collection.");
        }

        public virtual bool TryGetByIdentifier(string identifier, out T? entity)
        {
            entity = Entities.SingleOrDefault(x => x.Identifier == identifier);
            return entity != null;
        }

        public virtual T Add(T entity)
        {
            var existingEntity = Entities.SingleOrDefault(x => x.Identifier == entity.Identifier);

            if (existingEntity != null)
            {
                throw new InvalidOperationException($"Entity with identifier '{entity.Identifier}' already exists in the collection.");
            }

            Entities.Add(entity);
            return entity;
        }

        public virtual List<T> Add(params T[] entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }

            return entities.ToList();
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
