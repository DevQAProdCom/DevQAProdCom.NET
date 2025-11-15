using System.Text.Json.Serialization;

namespace DevQAProdCom.NET.UI.Shared.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SearchKind
    {
        NotSet,

        ElementsWithNoWrap,
        ElementsInFrame,
        ElementsInFrameInsideShadowRoot,
        ElementsInShadowRoot,
        ElementsInShadowRootInsideFrame,

        FrameElements,
        FrameElementsInsideShadowRoot,

        ShadowRootHostElements,
        ShadowRootHostElementsInsideFrame
    }
}
