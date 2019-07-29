namespace Viewer
{
    public sealed class RectangleDefinition : ShapeDefinition
    {
        public override string Type => "rectangle";

        public string Origin { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Rotation { get; set; }

        public bool Filled { get; set; }

        public override Shape Convert()
        {
            return Polygon.Rectangle(Filled ? RandomBrush() : null, Pen(), Point(Origin), Width, Height, Rotation);
        }
    }
}