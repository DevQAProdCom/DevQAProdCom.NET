using System.Text.Json.Serialization;

namespace DevQAProdCom.NET.UI.Shared.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FindOrderType
    {
        NotSet,
        FrameInsideShadowRoot,
        ShadowRootInsideFrame
    }
}
