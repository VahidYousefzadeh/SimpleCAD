using Newtonsoft.Json;

namespace Viewer
{
    public sealed class TriangleDefinition : ShapeDefinition
    {
        public override string Type => "triangle";
        public override Shape Convert()
        {
            return Polygon.Triangle(Filled ? RandomBrush() : null, Pen(), Point(A), Point(B), Point(C));
        }

        [JsonProperty(PropertyName = "a")]
        public string A { get; set; }

        [JsonProperty(PropertyName = "b")]
        public string B { get; set; }

        [JsonProperty(PropertyName = "c")]
        public string C { get; set; }

        [JsonProperty(PropertyName = "filled")]
        public bool Filled { get; set; }
    }
}