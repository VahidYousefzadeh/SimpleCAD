using Newtonsoft.Json;

namespace Viewer
{
    public abstract class ShapeDefinition
    {
        [JsonProperty(PropertyName = "type")]
        public abstract string Type { get; }

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "lineType")]
        public string LineType { get; set; }
    }
}