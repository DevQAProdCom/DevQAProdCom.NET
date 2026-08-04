using System.Text;

namespace DevQAProdCom.NET.AI.Shared.Interfaces
{
    public interface IAiInteractionDataBank
    {
        public Dictionary<string, List<StringBuilder>> Data { get; set; }
        public void Append(string dataType, string data);
        public void Add(string dataType, StringBuilder data);
    }
}
