using System;
using System.Linq;
using System.Windows;

namespace Viewer
{
    internal static class MathHelper
    {
        /// <summary>
        /// Zero tolerance constant.
        /// </summary>
        private const double Epsilon = 1.0e-9;

        /// <summary>
        /// Compare two doubles for equality within a tolerance.
        /// </summary>
        public static bool AlmostEquals(this double a, double b, double epsilon = Epsilon)
        {
            return Math.Abs(a - b) < epsilon;
        }

        public static double DistanceTo(this Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }

        /// <summary>
        /// Intersection of two line segments
        /// </summary>
        public static Point[] LineLineIntersection(LineGeometry a, LineGeometry b)
        {
            double a1 = a.EndPoint.Y - a.StartPoint.Y;
            double a2 = b.EndPoint.Y - b.StartPoint.Y;

            double b1 = a.StartPoint.X - a.EndPoint.X;
            double b2 = b.StartPoint.X - b.EndPoint.X;

            double c1 = a1 * a.StartPoint.X + b1 * a.StartPoint.Y;
            double c2 = a2 * b.StartPoint.X + b2 * b.StartPoint.Y;

            double delta = a1 * b2 - a2 * b1;

            if (delta.AlmostEquals(0))
                return new Point[0];

            double x = (b2 * c1 - b1 * c2) / delta;
            double y = (a1 * c2 - a2 * c1) / delta;

            if (Math.Min(a.StartPoint.X, a.EndPoint.X) <= x && x <= Math.Max(a.StartPoint.X, a.EndPoint.X) &&
                Math.Min(b.StartPoint.X, b.EndPoint.X) <= x && x <= Math.Max(b.StartPoint.X, b.EndPoint.X))
            {
                return new[] { new Point(x, y) };
            }

            return new Point[0];
        }

        public static Point[] CircleCircleIntersecton(CircleGeometry a, CircleGeometry b)
        {
            return new Point[0];
        }

        public static Point[] PolygonPolygonIntersection(PolygonGeometry a, PolygonGeometry b)
        {
            LineGeometry[] edges = a.Edges;

            Point[] intersections = null;
            foreach (LineGeometry aEdge in edges)
            {
                intersections = intersections == null 
                    ? GeometryPolygonIntersection(aEdge, b) 
                    : intersections.Union(GeometryPolygonIntersection(aEdge, b)).ToArray();
            }

            return intersections;
        }

        public static Point[] LineCircleIntersection(LineGeometry line, CircleGeometry circle)
        {
            Vector d = line.Direction();

            // compute the distance between the points A and E, where
            // E is the point of AB closest the circle center (Cx, Cy)
            double t = d.X * (circle.Center.X - line.StartPoint.X) + d.Y * (circle.Center.Y - line.StartPoint.Y);

            Point e = t * d + line.StartPoint;

            double lec = e.DistanceTo(circle.Center);


            // test if the line intersects the circle
            if (lec >= circle.Radius)
                return lec.AlmostEquals(circle.Radius) ? new[] {e} : new Point[0];


            // Compute distance from t to circle intersection point
            double dt = Math.Sqrt(Math.Pow(circle.Radius, 2) - Math.Pow(lec, 2));

            // Compute intersection points
            return new[] {(t - dt) * d + line.StartPoint, (t + dt) * d + line.StartPoint};
        }

        public static Point[] LinePolygonIntersection(LineGeometry line, PolygonGeometry polygon)
        {
            return GeometryPolygonIntersection(line, polygon);
        }

        public static Point[] CirclePolygonIntersection(CircleGeometry circle, PolygonGeometry polygon)
        {
            return GeometryPolygonIntersection(circle, polygon);
        }

        private static Point[] GeometryPolygonIntersection(Geometry geometry, PolygonGeometry polygon)
        {
            LineGeometry[] edges = polygon.Edges;

            Point[] intersections = null;
            foreach (LineGeometry edge in edges)
            {
                intersections = intersections == null
                    ? geometry.Intersect(edge)
                    : intersections.Union(geometry.Intersect(edge)).ToArray();
            }

            return intersections;
        }
    }
}