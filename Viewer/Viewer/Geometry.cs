using System.Windows;

namespace Viewer
{
    public abstract class Geometry
    {
        internal abstract Point[] Intersect(Geometry other);

        internal abstract Rect Bounds { get; }
    }
}