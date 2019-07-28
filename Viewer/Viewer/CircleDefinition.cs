using Newtonsoft.Json;

namespace Viewer
{
    public class CircleDefinition : ShapeDefinition
    {
        public override string Type => "circle";

        [JsonProperty(PropertyName = "center")]
        public string C { get; set; }

        [JsonProperty(PropertyName = "radius")]
        public double R { get; set; }

        [JsonProperty(PropertyName = "filled")]
        public bool Filled { get; set; }
    }
}