namespace Viewer
{
    public class LineDefinition : ShapeDefinition
    {
        public override string Type => "line";

        public string A { get; set; }

        public string B { get; set; }

        public override Shape Convert()
        {
            return new Line(Pen(), Point(A), Point(B));
        }
    }
}