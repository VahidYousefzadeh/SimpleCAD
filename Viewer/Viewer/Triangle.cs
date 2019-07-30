using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public sealed class Triangle : Polygon
    {
        /// <summary>
        /// Initializes an instance of <see cref="Triangle"/> class.
        /// </summary>
        public Triangle(Brush fill, Pen pen, Point a, Point b, Point c)
            : base(fill, pen, a, b, c)
        {
        }
    }
}