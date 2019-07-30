using System;
using System.Windows;

namespace Viewer
{
    public sealed class LineGeometry : Geometry
    {
        internal Point StartPoint { get; }
        internal Point EndPoint { get; }
        internal override Rect Bounds { get; }

        internal LineGeometry(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Bounds = GetBounds();
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
                    return IntersectionHelper.LineLineIntersection(this, line);
                case CircleGeometry circle:
                    return IntersectionHelper.LineCircleIntersection(this, circle);
                case PolygonGeometry polygon:
                    return IntersectionHelper.LinePolygonIntersection(this, polygon);
                default:
                    return new Point[0];
            }
        }

        public override string ToString()
        {
            return $"X1: \t\t {(double) Math.Round((decimal) StartPoint.X, 3)} \n" +
                   $"Y1: \t\t {(double) Math.Round((decimal) StartPoint.Y, 3)} \n" +
                   $"X2: \t\t {(double) Math.Round((decimal) EndPoint.X, 3)} \n" +
                   $"Y2: \t\t {(double) Math.Round((decimal) EndPoint.Y, 3)} \n";
        }

        private Rect GetBounds()
        {
            return new Rect(StartPoint, EndPoint);
        }
    }
}