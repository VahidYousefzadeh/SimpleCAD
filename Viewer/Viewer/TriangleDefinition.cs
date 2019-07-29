namespace Viewer
{
    public sealed class TriangleDefinition : ShapeDefinition
    {
        public override string Type => "triangle";

        public string A { get; set; }

        public string B { get; set; }

        public string C { get; set; }

        public bool Filled { get; set; }

        public override Shape Convert()
        {
            return Polygon.Triangle(Filled ? RandomBrush() : null, Pen(), Point(A), Point(B), Point(C));
        }
    }
}