using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public abstract class Polygon : Shape
    {
        protected Polygon(Brush fill, Pen pen, params Point[] corners) : base(pen)
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