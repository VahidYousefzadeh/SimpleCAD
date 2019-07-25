using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    internal sealed class LineVisual : DrawingVisual
    {
        public LineVisual(Pen pen, Point startPoint, Point endPoint)
        {
            using (DrawingContext drawingContext = RenderOpen())
            {
                drawingContext.DrawLine(pen, startPoint, endPoint);
            }
        }
    }
}