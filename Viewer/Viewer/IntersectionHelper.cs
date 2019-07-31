using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Viewer
{
    public static class IntersectionHelper
    {
        public static Point[] Intersections(Geometry[] geometries)
        {
            IList<Rect> bounds = geometries.Select(o => o.Bounds).ToList();

            IList<double> xs = bounds
                .Select(o => o.Left)
                .Union(bounds.Select(o => o.Right))
                .Distinct(new DoubleComparer())
                .OrderBy(o => o)
                .ToList();


            for (int i = 0; i < xs.Count - 1; i++)
            {
                var interval = new DoubleInterval(xs[i], xs[i + 1]);

                IList<Geometry> candidates = new List<Geometry>();
                foreach (Geometry geometry in geometries)
                {
                    var gi = new DoubleInterval(geometry.Bounds.Left, geometry.Bounds.Right);

                    if (gi.Intersects(interval) || gi.Contains(interval))
                        candidates.Add(geometry);
                }
            }

            return null;
        }

        /// <summary>
        /// Intersection of two line segments
        /// </summary>
        public static Point[] LineSegementsIntersect(LineGeometry a, LineGeometry b)
        {
            if (!a.Bounds.IntersectsWith(b.Bounds))
                return new Point[0];

            var origin = new Point(0d, 0d);
            Vector a1 = a.StartPoint - origin;
            Vector a2 = a.EndPoint - origin;
            Vector b1 = b.StartPoint - origin;
            Vector b2 = b.EndPoint - origin;

            Vector r = a2 - a1;
            Vector s = b2 - b1;
            double rxs = Vector.CrossProduct(r, s);

            // The lines are either collinear or parallel
            if (rxs.AlmostEquals(0d))
                return new Point[0];

            double t = Vector.CrossProduct(b1 - a1, s) / rxs;
            double u = Vector.CrossProduct(b1 - a1, r) / rxs;

            return !rxs.AlmostEquals(0d) && (0 <= t && t <= 1) && (0 <= u && u <= 1)
                ? new[] {new Point((a1 + t * r).X, (a1 + t * r).Y)}
                : new Point[0];
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