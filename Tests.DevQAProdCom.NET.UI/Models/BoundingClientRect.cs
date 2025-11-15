using System.Text.Json.Serialization;

namespace Tests.DevQAProdCom.NET.UI.Models
{
    public class BoundingClientRect
    {
        [JsonPropertyName("width")]
        public double Width { get; set; }

        [JsonPropertyName("height")]
        public double Height { get; set; }

        [JsonPropertyName("top")]
        public double Top { get; set; }

        [JsonPropertyName("bottom")]
        public double Bottom { get; set; }

        [JsonPropertyName("left")]
        public double Left { get; set; }

        [JsonPropertyName("right")]
        public double Right { get; set; }
    }

}
