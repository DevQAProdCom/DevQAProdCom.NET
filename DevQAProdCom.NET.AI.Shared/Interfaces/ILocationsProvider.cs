namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface ILocationsProvider : IEnumerable<string>
    {
        public List<string> GetLocations();
    }
}
