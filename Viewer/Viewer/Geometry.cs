using System.Windows;

namespace Viewer
{
    public abstract class Geometry
    {
        public abstract Point[] Intersect(Geometry other);

        public abstract Rect Bounds { get; }
    }
}