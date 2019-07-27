using System.Windows;

namespace Viewer
{
    internal sealed class CircleGeometry : Geometry
    {
        public Point Center { get; }

        public double Radius { get; }

        public CircleGeometry(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        internal override Point[] Intersect(Geometry other)
        {
            switch (other)
            {
                case LineGeometry line:
                    return MathHelper.LineCircleIntersection(line, this);
                case CircleGeometry circle:
                    return MathHelper.CircleCircleIntersecton(this, circle);
                case PolygonGeometry polygon:
                    return MathHelper.CirclePolygonIntersection(this, polygon);
                default:
                    return new Point[0];
            }
        }
    }
}