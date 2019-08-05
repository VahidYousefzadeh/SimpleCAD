using System;
using System.Windows;

namespace Viewer.Geometry
{
    public sealed class CircleGeometry : Geometry
    {
        public Point Center { get; }

        public double Radius { get; }

        public override Rect Bounds { get; }

        public CircleGeometry(Point center, double radius)
        {
            Center = center;
            Radius = radius;
            Bounds = GetBounds();
        }

        public override Point[] Intersect(Geometry other)
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