using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DevQAProdCom.NET.UI.Shared.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FindState
    {
        [Description("Not Searched")]
        NotSearched,

        [Description("Found")]
        Found,

        [Description("Not Found")]
        NotFound,

        [Description("Failed")]
        Failed
    }
}
