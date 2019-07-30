using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public sealed class Rectangle : Polygon
    {
        /// <summary>
        /// Initializes an instance of <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle(Brush fill, Pen pen, Point origin, double width, double height, double rotation)
            : base(fill, pen, Corners(origin, width, height, rotation))
        {
        }

        private static Point[] Corners(Point origin, double width, double height, double rotation)
        {
            return null;
        }
    }
}