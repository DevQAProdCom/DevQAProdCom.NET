using System.Text.Json.Serialization;

namespace DevQAProdCom.NET.UI.Shared.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UiElementType
    {
        NotSet,

        Standard,
        Frame,
        ShadowRootHost
    }
}
