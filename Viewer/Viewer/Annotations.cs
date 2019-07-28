using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public sealed class IntersectionHelper
    {
        public IntersectionHelper(params Geometry[] geometries)
        {

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

    public sealed class Annotations : FrameworkElement
    {
        private readonly VisualCollection m_children;

        private static readonly Pen s_pen = new Pen(Brushes.Blue, 2d);

        public Annotations(View view)
        {
            m_children = new VisualCollection(this);

            Point[] intersections = FindIntersections(view.Shapes);


            Stopwatch s = new Stopwatch();
            s.Start();


            if (intersections != null)
            {
                m_children.Add(new CrossSymbols(s_pen, intersections));
            }

            s.Stop();
            MessageBox.Show(s.ElapsedMilliseconds.ToString());
        }

        protected override int VisualChildrenCount => m_children.Count;

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= m_children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return m_children[index];
        }

        private static Point[] FindIntersections(IList<Shape> shapes)
        {
            Point[] intersections = null;
            for (int i = 0; i < shapes.Count - 1; i++)
            {
                Geometry a = shapes[i].Geometry;
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    Geometry b = shapes[j].Geometry;
                    intersections = intersections == null
                        ? a.Intersect(b)
                        : intersections.Union(a.Intersect(b)).ToArray();
                }
            }

            return intersections;
        }
    }
}