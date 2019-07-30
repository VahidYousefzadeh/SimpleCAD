using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    internal sealed class Circle : Shape
    {
        /// <summary>
        /// Initializes an instance of <see cref="Circle"/> class.
        /// </summary>
        public Circle(Brush fill, Pen pen, Point center, double radius) : base(pen)
        {
            Geometry = new CircleGeometry(center, radius);

            using (DrawingContext drawingContext = RenderOpen())
            {
                drawingContext.DrawEllipse(fill, pen, center, radius, radius);
            }
        }
    }
}