using Newtonsoft.Json;

namespace Viewer
{
    public class CircleDefinition : ShapeDefinition
    {
        public override string Type => "circle";

        [JsonProperty(PropertyName = "center")]
        public string Center { get; set; }

        [JsonProperty(PropertyName = "radius")]
        public double Radius { get; set; }

        [JsonProperty(PropertyName = "filled")]
        public bool Filled { get; set; }

        public override Shape Convert()
        {
            return new Circle(Filled ? RandomBrush() : null, Pen(), Point(Center), Radius);
        }
    }
}