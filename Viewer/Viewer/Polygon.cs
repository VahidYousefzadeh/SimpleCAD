using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    internal sealed class Polygon : Shape
    {
        /// <summary>
        /// Initializes an instance of <see cref="Triangle"/> class.
        /// </summary>
        public static Polygon Triangle(Brush fill, Pen pen, Point a, Point b, Point c)
        {
            return new Polygon(fill, pen, a, b, c);
        }

        /// <summary>
        /// Initializes an instance of <see cref="Rectangle"/> class.
        /// </summary>
        public static Polygon Rectangle(Brush fill, Pen pen, Point a, Point b, int rotation)
        {
            return null;
        }

        private Polygon(Brush fill, Pen pen, params Point[] corners)
        {
            Geometry = new PolygonGeometry(corners);

            var streamGeometry = new StreamGeometry();
            using (StreamGeometryContext geometryContext = streamGeometry.Open())
            {
                geometryContext.BeginFigure(corners.First(), true, true);
                geometryContext.PolyLineTo(
                    new PointCollection(corners.Skip(1)),
                    true, 
                    true);

                using (DrawingContext drawingContext = RenderOpen())
                {
                    drawingContext.DrawGeometry(fill, pen, streamGeometry);
                }
            }
        }
    }
}