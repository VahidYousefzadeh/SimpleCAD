using System;
using System.Windows;
using Viewer.Geometry;

namespace Viewer.Graphics
{
    public sealed class Rectangle : Polygon
    {
        /// <summary>
        /// Initializes an instance of <see cref="Rectangle"/> class
        /// exactly large enough to contain the two given points.
        /// </summary>
        public Rectangle(Point firstCorner, Point secondCorner)
            : base(Corners(firstCorner, secondCorner))
        {
        }

        private static Point[] Corners(Point firstCorner, Point secondCorner)
        {
            var rectangle = new Rect(firstCorner, secondCorner);
            return new[] {rectangle.TopLeft, rectangle.TopRight, rectangle.BottomRight, rectangle.BottomLeft};
        }

        public override T Write<T>(IWriter<T> writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            var geometry = (PolygonGeometry)Geometry;
            Point[] points =
            {
                geometry.Edges[0].StartPoint,
                geometry.Edges[1].EndPoint
            };

            return writer.WriteRectangle(points[0], points[1], Color, LineStyle, Filled);
        }
    }
}