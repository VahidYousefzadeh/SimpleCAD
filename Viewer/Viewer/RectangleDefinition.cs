using Newtonsoft.Json;

namespace Viewer
{
    public sealed class RectangleDefinition : ShapeDefinition
    {
        public override string Type => "rectangle";

        [JsonProperty(PropertyName = "origin")]
        public string Origin { get; set; }

        [JsonProperty(PropertyName = "width")]
        public double Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public double Height { get; set; }

        [JsonProperty(PropertyName = "rotation")]
        public double Rotation { get; set; }

        [JsonProperty(PropertyName = "filled")]
        public bool Filled { get; set; }

        public override Shape Convert()
        {
            return Polygon.Rectangle(Filled ? RandomBrush() : null, Pen(), Point(Origin), Width, Height, Rotation);
        }
    }
}