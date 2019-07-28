using Newtonsoft.Json;

namespace Viewer
{
    public class LineDefinition : ShapeDefinition
    {
        public override string Type => "line";
        public override Shape Convert()
        {
            return new Line(Pen(), Point(A), Point(B));
        }

        [JsonProperty(PropertyName = "a")]
        public string A { get; set; }

        [JsonProperty(PropertyName = "b")]
        public string B { get; set; }
    }
}