using System;
using System.Linq;
using System.Windows;

namespace Viewer.Geometry
{
    public static class IntersectionHelper
    {
        /// <summary>
        /// Intersection of two line segments
        /// </summary>
        public static Point[] LineLineIntersection(LineGeometry la, LineGeometry lb)
        {
            if (!la.Bounds.IntersectsWith(lb.Bounds))
                return new Point[0];

            double dx1 = la.EndPoint.X - la.StartPoint.X;
            double dy1 = la.EndPoint.Y - la.StartPoint.Y;
            double dx2 = lb.EndPoint.X - lb.StartPoint.X;
            double dy2 = lb.EndPoint.Y - lb.StartPoint.Y;
            double d = dy2 * dx1 - dx2 * dy1;
            // Return if lines are parallel
            if (Math.Abs(Math.Round(d)) < MathHelper.Epsilon)
            {
                return new Point[0];
            }

            if (Math.Abs(la.StartPoint.X - la.EndPoint.X) < MathHelper.Epsilon &&
                Math.Abs(lb.StartPoint.X - lb.EndPoint.X) < MathHelper.Epsilon &&
                Math.Abs(la.StartPoint.X - lb.StartPoint.X) < MathHelper.Epsilon)
            {
                return new Point[0];
            }


            // We are using Cos of the angle between two lines to account for nearly parallel lines
            double cosAngle =
                Math.Abs((dx1 * dx2 + dy1 * dy2) / Math.Sqrt((dx1 * dx1 + dy1 * dy1) * (dx2 * dx2 + dy2 * dy2)));
            if (cosAngle > 1 - 0.001)
            {
                return new Point[0];
            }

            double na = (lb.EndPoint.X - lb.StartPoint.X) * (la.StartPoint.Y - lb.StartPoint.Y) - (lb.EndPoint.Y - lb.StartPoint.Y) * (la.StartPoint.X - lb.StartPoint.X);
            double nb = (la.EndPoint.X - la.StartPoint.X) * (la.StartPoint.Y - lb.StartPoint.Y) - (la.EndPoint.Y - la.StartPoint.Y) * (la.StartPoint.X - lb.StartPoint.X);

            double ua = na / d;
            double ub = nb / d;

            var pt = new Point[1] {new Point(la.StartPoint.X + ua * (la.EndPoint.X - la.StartPoint.X), la.StartPoint.Y + ua * (la.EndPoint.Y - la.StartPoint.Y)) };

            // The lines do not intersect (but they will if they are extended)
            if (ua + MathHelper.Epsilon < 0 ||
                ua - MathHelper.Epsilon > 1 ||
                ub + MathHelper.Epsilon < 0 ||
                ub - MathHelper.Epsilon > 1)
            {
                return new Point[0];
            }


            return pt;
        }

        public static Point[] CircleCircleIntersecton(CircleGeometry a, CircleGeometry b)
        {
            return new Point[0];
        }

        public static Point[] PolygonPolygonIntersection(PolygonGeometry a, PolygonGeometry b)
        {
            if (!a.Bounds.IntersectsWith(b.Bounds))
                return new Point[0];

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
            if (!line.Bounds.IntersectsWith(circle.Bounds))
                return new Point[0];

            Vector d = line.Direction();

            // compute the distance between the points A and E, where
            // E is the point of AB closest the circle center (Cx, Cy)
            double t = d.X * (circle.Center.X - line.StartPoint.X) + d.Y * (circle.Center.Y - line.StartPoint.Y);

            Point e = t * d + line.StartPoint;

            double lec = e.DistanceTo(circle.Center);


            // test if the line intersects the circle
            if (lec >= circle.Radius)
                return lec.AlmostEquals(circle.Radius) ? new[] { e } : new Point[0];


            // Compute distance from t to circle intersection point
            double dt = Math.Sqrt(Math.Pow(circle.Radius, 2) - Math.Pow(lec, 2));

            // Compute intersection points
            return new[] { (t - dt) * d + line.StartPoint, (t + dt) * d + line.StartPoint };
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
            if (!geometry.Bounds.IntersectsWith(polygon.Bounds))
                return new Point[0];

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