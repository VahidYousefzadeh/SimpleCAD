using System;
using System.Windows;

namespace Viewer
{
    public sealed class Rectangle : Polygon
    {
        /// <summary>
        /// Initializes an instance of <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle(Point origin, double width, double height, double rotation, bool filled)
            : base(filled, Corners(origin, width, height, rotation))
        {
        }

        private static Point[] Corners(Point origin, double width, double height, double rotation)
        {
            return null;
        }

        public override T Write<T>(IWriter<T> writer)
        {
            throw new NotImplementedException();
        }
    }
}