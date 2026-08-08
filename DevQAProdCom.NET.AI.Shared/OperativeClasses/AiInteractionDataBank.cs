using System.Text;
using DevQAProdCom.NET.AI.Shared.Interfaces.Interactions;

namespace DevQAProdCom.NET.AI.Shared.OperativeClasses
{
    public class AiInteractionDataBank : IAiInteractionDataBank
    {
        public Dictionary<string, List<StringBuilder>> Data { get; set; } = new();

        public void Append(string dataType, string data)
        {
            if (Data.TryGetValue(dataType, out var stringBuilders) && stringBuilders.Count > 0)
            {
                var strBuilder = stringBuilders[0];
                strBuilder.Append(data);
            }
            else
            {
                var strBuilder = new StringBuilder(data);
                Data[dataType] = new List<StringBuilder> { strBuilder };
            }
        }

        public void Add(string dataType, StringBuilder data)
        {
            if (Data.TryGetValue(dataType, out var stringBuilders))
                stringBuilders.Add(data);
            else
                Data[dataType] = new List<StringBuilder> { data };
        }
    }
}
