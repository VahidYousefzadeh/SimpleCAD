using System;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace Viewer
{
    public sealed class Rectangle : Polygon
    {
        /// <summary>
        /// Initializes an instance of <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle(Brush fill, Point origin, double width, double height, double rotation)
            : base(fill, Corners(origin, width, height, rotation))
        {
        }

        private static Point[] Corners(Point origin, double width, double height, double rotation)
        {
            return null;
        }

        protected override string ToJsonInternal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        protected override XElement[] ToXmlInternal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}