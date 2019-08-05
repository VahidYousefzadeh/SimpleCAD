using System.Linq;
using System.Windows;
using Viewer.Geometry;

namespace Viewer.Graphics
{
    public sealed class Triangle : Polygon
    {
        /// <summary>
        /// Initializes an instance of <see cref="Triangle"/> class.
        /// </summary>
        public Triangle(Point a, Point b, Point c)
            : base(a, b, c)
        {
        }

        public override T Write<T>(IWriter<T> writer)
        {
            var geometry = (PolygonGeometry) Geometry;
            Point[] points = geometry.Edges.Select(o => o.StartPoint).ToArray();
            return writer.WriteTriangle(points[0], points[1], points[2], Color, LineStyle, Filled);
        }
    }
}