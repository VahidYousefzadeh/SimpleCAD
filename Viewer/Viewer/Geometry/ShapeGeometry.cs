using System.Windows;

namespace Viewer.Geometry
{
    public abstract class ShapeGeometry
    {
        public abstract Point[] Intersect(ShapeGeometry other);

        public abstract Rect Bounds { get; }
    }
}