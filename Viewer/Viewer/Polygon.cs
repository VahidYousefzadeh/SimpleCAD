using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public abstract class Polygon : Shape
    {
        private bool m_filled;

        public bool Filled
        {
            get => m_filled;
            set
            {
                m_filled = value;
                m_isDirty = true;
            }
        }

        protected Polygon(params Point[] corners)
        {
            Geometry = new PolygonGeometry(corners);

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