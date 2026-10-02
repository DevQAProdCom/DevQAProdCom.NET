using System.Collections;
using DevQAProdCom.NET.AI.Shared.Interfaces;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class BaseLocationsProvider : ILocationsProvider
    {
        protected List<string> Locations { get; set; } = new List<string>();
        public virtual List<string> GetLocations() => Locations;

        public IEnumerator<string> GetEnumerator()
        {
            return Locations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
