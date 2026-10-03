namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface ISearchEntitiesInLocations<T>
    {
        public List<T> Search(string path, bool userExtendedSearch = true);
    }
}
