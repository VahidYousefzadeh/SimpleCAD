using System.Collections.Generic;
using System.Windows;

namespace Viewer
{
    public sealed class PolygonGeometry : Geometry
    {
        public LineGeometry[] Edges { get; }
        internal override Rect Bounds { get; }

        public PolygonGeometry(params Point[] points)
        {
            Edges = GetEdges(points);
            Bounds = GetBounds();
        }

        internal override Point[] Intersect(Geometry other)
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