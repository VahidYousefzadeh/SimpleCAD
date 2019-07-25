using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    internal sealed class TriangleVisual : DrawingVisual
    {
        public TriangleVisual(Brush fill, Pen pen, Point a, Point b, Point c)
        {
            var triangleGeometry = new StreamGeometry();
            using (StreamGeometryContext geometryContext = triangleGeometry.Open())
            {
                geometryContext.BeginFigure(a, true, true);
                geometryContext.PolyLineTo(new PointCollection { b, c }, true, true);

                using (DrawingContext drawingContext = RenderOpen())
                {
                    drawingContext.DrawGeometry(fill, pen, triangleGeometry);
                }
            }
        }

        public sealed class Line2D
        {
            
        }
    }
}