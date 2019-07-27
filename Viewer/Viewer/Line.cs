using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    internal sealed class Line : Shape
    {
        /// <summary>
        /// Initializes an instance of <see cref="Line"/> class.
        /// </summary>
        public Line(Pen pen, Point startPoint, Point endPoint)
        {
            Geometry = new LineGeometry(startPoint, endPoint);

            using (DrawingContext drawingContext = RenderOpen())
            {
                drawingContext.DrawLine(pen, startPoint, endPoint);
            }
        }
    }
}