using System.Windows;

namespace Viewer
{
    public sealed class PolygonGeometry : Geometry
    {
        public LineGeometry[] Edges { get; } 

        public PolygonGeometry(params Point[] points)
        {
            Edges = new LineGeometry[points.Length];

            int count = 0;
            while (count < points.Length - 1)
            {
                Edges[count] = new LineGeometry(points[count], points[count + 1]);
                count++;
            }

            Edges[count] = new LineGeometry(points[count], points[0]);
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

        internal override Rect Bounds()
        {
            Rect bounds = Rect.Empty;
            foreach (LineGeometry edge in Edges)
            {
                bounds.Union(edge.Bounds());
            }

            return bounds;
        }
    }
}