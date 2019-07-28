using System.Windows;

namespace Viewer
{
    internal sealed class LineGeometry : Geometry
    {
        internal Point StartPoint { get; }
        internal Point EndPoint { get; }

        internal LineGeometry(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        internal Vector Direction()
        {
            Vector vector = EndPoint - StartPoint;
            vector.Normalize();

            return vector;
        }

        internal override Point[] Intersect(Geometry other)
        {
            switch (other)
            {
                case LineGeometry line:
                    return MathHelper.LineLineIntersection(this, line);
                case CircleGeometry circle:
                    return MathHelper.LineCircleIntersection(this, circle);
                case PolygonGeometry polygon:
                    return MathHelper.LinePolygonIntersection(this, polygon);
                default:
                    return new Point[0];
            }
        }

        internal override Rect Bounds()
        {
            return new Rect(StartPoint, EndPoint);
        }
    }
}