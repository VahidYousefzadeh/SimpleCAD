using System;
using System.Windows;

namespace Viewer
{
    public sealed class Rectangle : Polygon
    {
        /// <summary>
        /// Initializes an instance of <see cref="Rectangle"/> class
        /// exactly large enough to contain the two given points.
        /// </summary>
        public Rectangle(Point firstCorner, Point secondCorner)
            : base(Corners(firstCorner, secondCorner))
        {
        }

        private static Point[] Corners(Point firstCorner, Point secondCorner)
        {
            var rectangle = new Rect(firstCorner, secondCorner);
            return new[] {rectangle.TopLeft, rectangle.TopRight, rectangle.BottomRight, rectangle.BottomLeft};
        }

        public override T Write<T>(IWriter<T> writer)
        {
            throw new NotImplementedException();
        }
    }
}