using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public abstract class Polygon : Shape
    {
        protected readonly bool m_filled;

        protected Polygon(bool filled, params Point[] corners)
        {
            Geometry = new PolygonGeometry(corners);

            m_filled = filled;

            InvalidateVisual();
        }

        protected override void Render(DrawingContext drawingContext)
        {
            PolygonGeometry polygonGeometry = (PolygonGeometry) Geometry;

            //var streamGeometry = new StreamGeometry();
            //using (StreamGeometryContext geometryContext = streamGeometry.Open())
            //{
            //    geometryContext.BeginFigure(polygonGeometry..First(), true, true);
            //    geometryContext.PolyLineTo(
            //        new PointCollection(corners.Skip(1)),
            //        true,
            //        true);

            //    drawingContext.DrawGeometry(null, GetPen(), streamGeometry);

            //}
        }
    }
}