using System;
using System.Windows;
using System.Xml.Linq;

namespace Viewer
{
    public sealed class CircleGeometry : Geometry
    {
        public Point Center { get; }

        public double Radius { get; }

        internal override Rect Bounds { get; }
        public override string ToJson(IFormatProvider provider)
        {
            return $"\"center\": \"{Center.X.ToString(provider)}; {Center.Y.ToString(provider)}\",\n" +
                   $"\"radius\": \"{Radius.ToString(provider)}\"";
        }

        public override XElement ToXml(IFormatProvider provider)
        {
            return new XElement(
                "geometry",
                new XElement("center", $"{Center.X.ToString(provider)}; {Center.Y.ToString(provider)}"),
                new XElement("radius", $"{Radius.ToString(provider)}"));
        }

        public CircleGeometry(Point center, double radius)
        {
            Center = center;
            Radius = radius;
            Bounds = GetBounds();
        }

        internal override Point[] Intersect(Geometry other)
        {
            switch (other)
            {
                case LineGeometry line:
                    return IntersectionHelper.LineCircleIntersection(line, this);
                case CircleGeometry circle:
                    return IntersectionHelper.CircleCircleIntersecton(this, circle);
                case PolygonGeometry polygon:
                    return IntersectionHelper.CirclePolygonIntersection(this, polygon);
                default:
                    return new Point[0];
            }
        }

        public override string ToString()
        {
            return $"X: \t\t {(double) Math.Round((decimal) Center.X, 3)} \n" +
                   $"Y: \t\t {(double) Math.Round((decimal) Center.Y, 3)} \n" +
                   $"Radius: \t\t {(double) Math.Round((decimal) Radius, 3)}";
        }

        private Rect GetBounds()
        {
            var a = new Point(Center.X - Radius, Center.Y + Radius);
            var b = new Point(Center.X + Radius, Center.Y - Radius);

            return new Rect(a, b);
        }
    }
}