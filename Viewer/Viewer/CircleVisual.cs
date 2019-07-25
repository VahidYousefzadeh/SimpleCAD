using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    internal sealed class CircleVisual : DrawingVisual
    {
        public CircleVisual(Brush fill, Pen pen, Point center, double radius)
        {
            using (DrawingContext drawingContext = RenderOpen())
            {
                drawingContext.DrawEllipse(fill, pen, center, radius, radius);
            }
        }
    }
}