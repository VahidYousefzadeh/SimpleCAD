using System.Windows;

namespace Viewer.Geometry
{
    public abstract class Geometry
    {
        public abstract Point[] Intersect(Geometry other);

        public abstract Rect Bounds { get; }
    }
}