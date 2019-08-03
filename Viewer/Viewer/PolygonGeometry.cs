using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Viewer
{
    public sealed class PolygonGeometry : Geometry
    {
        public LineGeometry[] Edges { get; }
        public override Rect Bounds { get; }

        public PolygonGeometry(params Point[] points)
        {
            Edges = GetEdges(points);
            Bounds = GetBounds();
        }

        public override Point[] Intersect(Geometry other)
        {
            switch (other)
            {
                case LineGeometry line:
                    return IntersectionHelper.LinePolygonIntersection(line, this);
                case CircleGeometry circle:
                    return IntersectionHelper.CirclePolygonIntersection(circle, this);
                case PolygonGeometry polygon:
                    return IntersectionHelper.PolygonPolygonIntersection(polygon, this);
                default:
                    return new Point[0];
            }
        }

        public override string ToString()
        {
            IList<Point> points = Edges.Select(o => o.StartPoint).ToList();

            string str = "";
            for (int i = 0; i < Edges.Length; i++)
            {
                str = $"X{i + 1}: \t\t {(double) Math.Round((decimal) points[i].X, 3)} \n" +
                      $"Y{i + 1}: \t\t {(double) Math.Round((decimal) points[i].Y, 3)} \n" + str;
            }

            return str;
        }

        private static LineGeometry[] GetEdges(IReadOnlyList<Point> points)
        {
            LineGeometry[] edges = new LineGeometry[points.Count];

            int count = 0;
            while (count < points.Count - 1)
            {
                edges[count] = new LineGeometry(points[count], points[count + 1]);
                count++;
            }

            edges[count] = new LineGeometry(points[count], points[0]);

            return edges;
        }

        private Rect GetBounds()
        {
            Rect bounds = Rect.Empty;
            foreach (LineGeometry edge in Edges)
            {
                bounds.Union(edge.Bounds);
            }

            return bounds;
        }
    }
}