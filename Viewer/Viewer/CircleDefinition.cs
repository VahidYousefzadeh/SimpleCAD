namespace Viewer
{
    public class CircleDefinition : ShapeDefinition
    {
        public override string Type => "circle";

        public string Center { get; set; }

        public double Radius { get; set; }

        public bool Filled { get; set; }

        public override Shape Convert()
        {
            return new Circle(Filled ? RandomBrush() : null, Pen(), Point(Center), Radius);
        }
    }
}